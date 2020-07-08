using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppStock.Data;
using AppStock.Models;
using AppStock.Models.DTO;
using AppStock.Infrastructure.Services.Article;
using AppStock.Infrastructure.Services.Commande;
using AppStock.Infrastructure.Services.CommandeFournisseur;
using AppStock.Infrastructure.Services.CommandeLigne;
using AppStock.Infrastructure.Services.Contact;
using AppStock.Infrastructure.Services.Stock;
using AppStock.Infrastructure.Services.Adresse;

using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AppStock.Controllers
{
    public class CommandeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _user_manager; 
        private readonly IMapper _mapper;
        private readonly IArticleService _service_article;
        private readonly ICommandeService _service_commande;
        private readonly ICommandeFournisseurService _service_commande_fournisseur;
        private readonly ICommandeLigneService _service_commande_ligne;
        private readonly IContactService _service_contact;
        private readonly IStockService _service_stock;
        private readonly IAdresseService _service_adresse;
         
        public CommandeController(
                            ApplicationDbContext context
                            , UserManager<IdentityUser> user_manager 
                            , IMapper mapper 
                            , IArticleService service_article 
                            , ICommandeService service_commande
                            , ICommandeFournisseurService service_commande_fournisseur 
                            , ICommandeLigneService service_commande_ligne
                            , IContactService service_contact 
                            , IStockService service_stock 
                            , IAdresseService service_adresse)
        {
            _context = context;
            _user_manager = user_manager;
            _mapper = mapper ;
            _service_article = service_article ;
            _service_commande = service_commande ;
            _service_commande_fournisseur = service_commande_fournisseur ;
            _service_commande_ligne = service_commande_ligne;
            _service_contact = service_contact ;
            _service_stock = service_stock ;
            _service_adresse = service_adresse;
        }

        // GET: Commande
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CommandeEntities.Include(c => c.Contact).Include(c => c.NomCommandeStatut);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Commande MONITOR
        public async Task<IActionResult> Dashboard(string filtre="A")
        {
            var applicationDbContext = _context.CommandeEntities
                                            .Include(o => o.Contact)
                                                .ThenInclude(o => o.Adresse)
                                            .Include(o => o.NomCommandeStatut)
                                            .Include(o => o.CommandeLignes)
                                                .ThenInclude(o => o.Article)
                                                    .ThenInclude(o => o.Stock)
                                            .Where(o => o.NomCommandeStatut.Code == filtre );

            ViewData["filtre"] = filtre;
            return View(await applicationDbContext.ToListAsync());
        }

        // LIVRAISON :: Depart
        public async Task<IActionResult> StatutStartLivraison(int id)
        {
            CommandeEntity commande = await _service_commande.GetOneById(id);

            if(commande != null)
            {
                commande.NomCommandeStatut = await _context.NomCommandeStatutEntities.Where(o => o.Code=="L").FirstOrDefaultAsync();
                await _service_commande.Update(commande);

                // impact stock
                await _service_commande.DownloadStock(commande);
            }

            return RedirectToAction(nameof(Dashboard), new { filtre="L"});
        }

        // LIVRAISON :: OK
        public async Task<IActionResult> StatutConfirmLivraison(int id)
        {
            CommandeEntity commande = await _service_commande.GetOneById(id);

            if(commande != null)
            {
                commande.NomCommandeStatut = await _context.NomCommandeStatutEntities.Where(o => o.Code=="V").FirstOrDefaultAsync();
                await _service_commande.Update(commande);
            }

            return RedirectToAction(nameof(Dashboard), new { filtre="V"});
        }

        // ANNULATION
        public async Task<IActionResult> StatutAnnulation(int id)
        {
            CommandeEntity commande = await _service_commande.GetOneById(id);
            bool wasLivraison = false;

            if(commande != null)
            {
                wasLivraison = commande.NomCommandeStatut.Code == "L";
                commande.NomCommandeStatut = await _context.NomCommandeStatutEntities.Where(o => o.Code=="X").FirstOrDefaultAsync();
                await _service_commande.Update(commande);
                
                // si c'etait en cours de livraison, alors on renfloue le stock
                if(wasLivraison)
                {
                    await _service_commande.UploadStock(commande);
                }
            }

            return RedirectToAction(nameof(Dashboard), new { filtre="X"});
        }

        // Panier :: affichage du Panier
        [HttpGet]
        public async Task<IActionResult> Panier()
        {
            // recuperer l'utilisateur courant
            var _user = await _user_manager.GetUserAsync(HttpContext.User);
            var _contact = await _service_contact.GetOneByUserId(_user.Id);
            var _panier = await _service_commande.GetPanierForContactId(_contact.Id);

            var _dto = _mapper.Map<CommandeDTO>(_panier);
            return View(_dto); // renvoie DTO
        }

        
        [HttpGet]
        public async Task<IActionResult> AddArticleToPanier( int id )
        {
            // recuperer l'utilisateur courant
            var _user = await _user_manager.GetUserAsync(HttpContext.User);
            var _contact = await _service_contact.GetOneByUserId(_user.Id);
            var _panier = await _service_commande.GetPanierForContactId(_contact.Id);

            // Vérification de la présence de l'article dans la commande
            // Si présent, on ajoute +1 à sa quantité
            foreach (CommandeLigneEntity l in _panier.CommandeLignes)
            {
                if (l.ArticleId == id)
                {
                    l.Quantite ++;
                    await _service_commande_ligne.Update(l);
                    return RedirectToAction(nameof(Panier));
                }
            }

            // Si l'article est absent, ajout à la commande
            // ENSUITE Ajout de Article au Panier [quantite = 1]
            await _service_commande.AddArticle(_panier, id);
            return RedirectToAction(nameof(Panier));

        }

        [HttpPost]
        public async Task<IActionResult> UpdateLigneQuantite(int ligne_Quantite, int ligne_Id)
        {
            CommandeLigneEntity ligne = await _service_commande_ligne.GetOneById(ligne_Id);
            ligne.Quantite = ligne_Quantite;

            await _service_commande_ligne.Update(ligne);

            return RedirectToAction(nameof(Panier));
        }

        public async Task<IActionResult> DeleteLigne(int id)
        {
            await _service_commande_ligne.DeleteById(id);

            return RedirectToAction(nameof(Panier));
        }

        public async Task<IActionResult> AdresseLivraison(int id)
        {
            var _commande = await _service_commande.GetOneById(id);
            var _dto = _mapper.Map<CommandeDTO>(_commande);
            return View(_dto); // renvoie DTO
        }

        public async Task<IActionResult> ValiderCommande(CommandeDTO _dto)
        {
            AdresseEntity adresse_db = _mapper.Map<AdresseEntity>(_dto.Contact.Adresse);
            var _commande = await _service_commande.GetOneById(_dto.Id);
            _commande.Adresse = _dto.Contact.Adresse;
            await _service_commande.Validate(_commande);

            // verification pour CommandeFournisseur automatique
            var liste_commande_fournisseur_auto = await _service_commande_fournisseur.getCommandesFournisseurAuto(_commande);
            if(liste_commande_fournisseur_auto.Count > 0)
            {
                foreach (var item in liste_commande_fournisseur_auto)
                {
                    item.NomCommandeFournisseurStatut = await _context.NomCommandeFournisseurStatutEntities.Where(o => o.Code=="T").FirstOrDefaultAsync();
                    await _service_commande_fournisseur.Add(item);
                }
            }

            return RedirectToAction(nameof(Panier));
        }

        // GET: Commande/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commandeEntity = await _context.CommandeEntities
                .Include(c => c.Contact)
                .Include(c => c.NomCommandeStatut)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commandeEntity == null)
            {
                return NotFound();
            }

            return View(commandeEntity);
        }

        // GET: Commande/Create
        public IActionResult Create()
        {
            ViewData["ContactId"] = new SelectList(_context.ContactEntities, "Id", "Id");
            ViewData["NomCommandeStatutId"] = new SelectList(_context.NomCommandeStatutEntities, "Id", "Code");
            return View();
        }

        // POST: Commande/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Numero,Commentaire,ContactId,NomCommandeStatutId,IsDeleted")] CommandeEntity commandeEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commandeEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContactId"] = new SelectList(_context.ContactEntities, "Id", "Id", commandeEntity.ContactId);
            ViewData["NomCommandeStatutId"] = new SelectList(_context.NomCommandeStatutEntities, "Id", "Code", commandeEntity.NomCommandeStatutId);
            return View(commandeEntity);
        }

        // GET: Commande/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commandeEntity = await _context.CommandeEntities.FindAsync(id);
            if (commandeEntity == null)
            {
                return NotFound();
            }
            ViewData["ContactId"] = new SelectList(_context.ContactEntities, "Id", "Id", commandeEntity.ContactId);
            ViewData["NomCommandeStatutId"] = new SelectList(_context.NomCommandeStatutEntities, "Id", "Code", commandeEntity.NomCommandeStatutId);
            return View(commandeEntity);
        }

        // POST: Commande/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Numero,Commentaire,ContactId,NomCommandeStatutId,IsDeleted,CreatedAt")] CommandeEntity commandeEntity)
        {
            if (id != commandeEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commandeEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommandeEntityExists(commandeEntity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContactId"] = new SelectList(_context.ContactEntities, "Id", "Id", commandeEntity.ContactId);
            ViewData["NomCommandeStatutId"] = new SelectList(_context.NomCommandeStatutEntities, "Id", "Code", commandeEntity.NomCommandeStatutId);
            return View(commandeEntity);
        }

        // GET: Commande/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commandeEntity = await _context.CommandeEntities
                .Include(c => c.Contact)
                .Include(c => c.NomCommandeStatut)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commandeEntity == null)
            {
                return NotFound();
            }

            return View(commandeEntity);
        }

        // POST: Commande/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commandeEntity = await _context.CommandeEntities.FindAsync(id);
            _context.CommandeEntities.Remove(commandeEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommandeEntityExists(int id)
        {
            return _context.CommandeEntities.Any(e => e.Id == id);
        }
    }
}
