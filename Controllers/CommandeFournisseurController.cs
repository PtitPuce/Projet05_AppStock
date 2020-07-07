using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppStock.Data;
using AppStock.Models;
using AppStock.Models.DTO;
using AppStock.Infrastructure.Services.Article;
using AppStock.Infrastructure.Services.CommandeFournisseur;
using AppStock.Infrastructure.Services.CommandeFournisseurLigne;
using AppStock.Infrastructure.Services.Contact;
using AppStock.Infrastructure.Services.Stock;
using Microsoft.AspNetCore.Identity;

namespace AppStock.Controllers
{
    public class CommandeFournisseurController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _user_manager; 
        private readonly IArticleService _service_article;
        private readonly ICommandeFournisseurService _service_commande_fournisseur;
        private readonly ICommandeFournisseurLigneService _service_commande_fournisseur_ligne;
        private readonly IContactService _service_contact;
        private readonly IStockService _service_stock;

        public CommandeFournisseurController(
                            ApplicationDbContext context
                            , UserManager<IdentityUser> user_manager 
                            , IArticleService service_article 
                            , ICommandeFournisseurService service_commande_fournisseur 
                            , ICommandeFournisseurLigneService service_commande_fournisseur_ligne
                            , IContactService service_contact 
                            , IStockService service_stock)
        {
            _context = context;
            _user_manager = user_manager;
            _service_article = service_article ;
            _service_commande_fournisseur = service_commande_fournisseur ;
            _service_commande_fournisseur_ligne = service_commande_fournisseur_ligne;
            _service_contact = service_contact ;
            _service_stock = service_stock ;
        }
        // GET: Commande MONITOR
        public async Task<IActionResult> Dashboard(string filtre="T")
        {
            var applicationDbContext = _context.CommandeFournisseurEntities
                                    .Include(o => o.Fournisseur)
                                    .Include(o => o.Contact)
                                        .ThenInclude(o => o.Adresse)
                                    .Include(o => o.NomCommandeFournisseurStatut)
                                    .Include(o => o.CommandeFournisseurLignes)
                                        .ThenInclude(o => o.Article)
                                            .ThenInclude(o => o.NomTypeTVA)
                                    .Where(o => o.NomCommandeFournisseurStatut.Code == filtre );

            ViewData["filtre"] = filtre;
            return View(await applicationDbContext.ToListAsync());
        }

        // RECEPTION
        public async Task<IActionResult> StatutConfirmReception(int id)
        {
            CommandeFournisseurEntity commande_fournisseur = await _service_commande_fournisseur.GetOneById(id);

            if(commande_fournisseur != null)
            {
                commande_fournisseur.NomCommandeFournisseurStatut = await _context.NomCommandeFournisseurStatutEntities.Where(o => o.Code=="R").FirstOrDefaultAsync();
                await _service_commande_fournisseur.Update(commande_fournisseur);

                // impact stock
                await _service_commande_fournisseur.UploadStock(commande_fournisseur);
            }

            return RedirectToAction(nameof(Dashboard), new { filtre="V"});
        }

        // ANNULATION
        public async Task<IActionResult> StatutAnnulation(int id)
        {
            CommandeFournisseurEntity commande_fournisseur = await _service_commande_fournisseur.GetOneById(id);

            if(commande_fournisseur != null)
            {
                commande_fournisseur.NomCommandeFournisseurStatut = await _context.NomCommandeFournisseurStatutEntities.Where(o => o.Code=="A").FirstOrDefaultAsync();
                await _service_commande_fournisseur.Update(commande_fournisseur);
            }

            return RedirectToAction(nameof(Dashboard), new { filtre="A"});
        }

        // GET: CommandeFournisseur
        public async Task<IActionResult> Index()
        {
            return View(await _service_commande_fournisseur.GetAll());
        }

        // GET: CommandeFournisseur/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commandeFournisseurEntity = await _context.CommandeFournisseurEntities
                .Include(c => c.Contact)
                .Include(c => c.Fournisseur)
                .Include(c => c.NomCommandeFournisseurStatut)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commandeFournisseurEntity == null)
            {
                return NotFound();
            }

            return View(commandeFournisseurEntity);
        }

        // GET: CommandeFournisseur/Create
        public IActionResult Create()
        {
            ViewData["FournisseurId"] = new SelectList(_context.FournisseurEntities, "Id", "Raison");
            return View();
        }

        // POST: CommandeFournisseur/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Numero,Commentaire,FournisseurId")] CommandeFournisseurEntity commandeFournisseurEntity)
        {
            if (ModelState.IsValid)
            {   // recuperer l'utilisateur courant
                var _user = await _user_manager.GetUserAsync(HttpContext.User);
                var _contact = await _service_contact.GetOneByUserId(_user.Id);
                commandeFournisseurEntity.Contact = _contact;

                await _service_commande_fournisseur.Add(commandeFournisseurEntity);
                return RedirectToAction("Edit", new { Id = commandeFournisseurEntity.Id });
            }
            ViewData["ContactId"] = new SelectList(_context.ContactEntities, "Id", "Id", commandeFournisseurEntity.ContactId);
            ViewData["FournisseurId"] = new SelectList(_context.FournisseurEntities, "Id", "Id", commandeFournisseurEntity.FournisseurId);
            ViewData["NomCommandeFournisseurStatutId"] = new SelectList(_context.NomCommandeFournisseurStatutEntities, "Id", "Code", commandeFournisseurEntity.NomCommandeFournisseurStatutId);
            return View(commandeFournisseurEntity);
        }

        // GET: CommandeFournisseur/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var commandeFournisseurEntity = await _service_commande_fournisseur.GetOneById(id);
            if (commandeFournisseurEntity == null)
            {
                return NotFound();
            }
            if (commandeFournisseurEntity.NomCommandeFournisseurStatut.Code != "C")
            {
                throw new NotImplementedException();
            }
            ViewData["FournisseurId"] = new SelectList(_context.FournisseurEntities, "Id", "Raison", commandeFournisseurEntity.FournisseurId);
            ViewData["ArticleId"] = new SelectList(await _service_article.getAllByFournisseurId(commandeFournisseurEntity.FournisseurId), "Id", "Libelle");
            return View(commandeFournisseurEntity);
        }

        // POST: CommandeFournisseur/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Numero,Commentaire,ContactId,FournisseurId,NomCommandeFournisseurStatutId")] CommandeFournisseurEntity commandeFournisseurEntity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service_commande_fournisseur.Update(commandeFournisseurEntity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommandeFournisseurEntityExists(commandeFournisseurEntity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewData["FournisseurId"] = new SelectList(_context.FournisseurEntities, "Id", "Raison", commandeFournisseurEntity.FournisseurId);
                ViewData["ArticleId"] = new SelectList(await _service_article.getAllByFournisseurId(commandeFournisseurEntity.FournisseurId), "Id", "Libelle");
                return View(commandeFournisseurEntity);
            }
            ViewData["ContactId"] = new SelectList(_context.ContactEntities, "Id", "Id", commandeFournisseurEntity.ContactId);
            ViewData["FournisseurId"] = new SelectList(_context.FournisseurEntities, "Id", "Id", commandeFournisseurEntity.FournisseurId);
            ViewData["NomCommandeFournisseurStatutId"] = new SelectList(_context.NomCommandeFournisseurStatutEntities, "Id", "Code", commandeFournisseurEntity.NomCommandeFournisseurStatutId);
            return View(commandeFournisseurEntity);
        }

        [HttpPost]
        public async Task<IActionResult> FournisseurChange(int CommandeFournisseurId, int FournisseurId)
        {
            // Prévoir un avertissement

            var commande_fournisseur = await _service_commande_fournisseur.GetOneById(CommandeFournisseurId);
            if ( commande_fournisseur.FournisseurId != FournisseurId)
            {
                await _service_commande_fournisseur.FournisseurChange(commande_fournisseur, FournisseurId);
            }
            return RedirectToAction("Edit", new { Id = CommandeFournisseurId });
        }

        [HttpPost]
        public async Task<IActionResult> ArticleAdd(int CommandeFournisseurId, int ArticleId, int QuantiteArticle)
        {
            var commande_fournisseur = await _service_commande_fournisseur.GetOneById(CommandeFournisseurId);
            var article = await _service_article.GetOneById(ArticleId);
            await _service_commande_fournisseur.AddArticle(commande_fournisseur, article, QuantiteArticle);
            return RedirectToAction("Edit", new { Id = CommandeFournisseurId });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLigneQuantite(int CommandeFournisseurId, int LigneQuantite, int LigneId)
        {
            CommandeFournisseurLigneEntity ligne = await _service_commande_fournisseur_ligne.GetOneById(LigneId);
            ligne.Quantite = LigneQuantite;

            await _service_commande_fournisseur_ligne.Update(ligne);

            return RedirectToAction("Edit", new { Id = CommandeFournisseurId });
        }

        public async Task<IActionResult> DeleteLigne(int id)
        {
            var commande_fournisseur_ligne = await _service_commande_fournisseur_ligne.GetOneById(id);
            if (commande_fournisseur_ligne == null)
            {
                return NotFound();
            }
            int commande_fournisseur_id = commande_fournisseur_ligne.CommandeFournisseurId;
            await _service_commande_fournisseur_ligne.DeleteById(commande_fournisseur_ligne.Id);

            return RedirectToAction("Edit", new { Id = commande_fournisseur_id });
        }

        public async Task<IActionResult> ValiderCommande(int id)
        {
            var commande_fournisseur = await _service_commande_fournisseur.GetOneById(id);
            await _service_commande_fournisseur.Validate(commande_fournisseur);
            return RedirectToAction(nameof(Index));
        }

        // GET: CommandeFournisseur/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commandeFournisseurEntity = await _context.CommandeFournisseurEntities
                .Include(c => c.Contact)
                .Include(c => c.Fournisseur)
                .Include(c => c.NomCommandeFournisseurStatut)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commandeFournisseurEntity == null)
            {
                return NotFound();
            }

            return View(commandeFournisseurEntity);
        }

        // POST: CommandeFournisseur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commandeFournisseurEntity = await _context.CommandeFournisseurEntities.FindAsync(id);
            _context.CommandeFournisseurEntities.Remove(commandeFournisseurEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommandeFournisseurEntityExists(int id)
        {
            return _context.CommandeFournisseurEntities.Any(e => e.Id == id);
        }
    }
}
