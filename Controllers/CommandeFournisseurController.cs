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

        // GET: CommandeFournisseur
        public async Task<IActionResult> Index()
        {
            //var applicationDbContext = _context.CommandeFournisseurEntities.Include(c => c.Contact).Include(c => c.Fournisseur).Include(c => c.NomCommandeFournisseurStatut);
            //return View(await applicationDbContext.ToListAsync());
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
            //ViewData["ContactId"] = new SelectList(_context.ContactEntities, "Id", "Id");
            ViewData["FournisseurId"] = new SelectList(_context.FournisseurEntities, "Id", "Raison");
            //ViewData["NomCommandeFournisseurStatutId"] = new SelectList(_context.NomCommandeFournisseurStatutEntities, "Id", "Id");
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
                return RedirectToAction(nameof(Edit));
            }
            ViewData["ContactId"] = new SelectList(_context.ContactEntities, "Id", "Id", commandeFournisseurEntity.ContactId);
            ViewData["FournisseurId"] = new SelectList(_context.FournisseurEntities, "Id", "Id", commandeFournisseurEntity.FournisseurId);
            ViewData["NomCommandeFournisseurStatutId"] = new SelectList(_context.NomCommandeFournisseurStatutEntities, "Id", "Code", commandeFournisseurEntity.NomCommandeFournisseurStatutId);
            return View(commandeFournisseurEntity);
        }

        // GET: CommandeFournisseur/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commandeFournisseurEntity = await _context.CommandeFournisseurEntities.FindAsync(id);
            if (commandeFournisseurEntity == null)
            {
                return NotFound();
            }
            ViewData["ContactId"] = new SelectList(_context.ContactEntities, "Id", "Id", commandeFournisseurEntity.ContactId);
            ViewData["FournisseurId"] = new SelectList(_context.FournisseurEntities, "Id", "Id", commandeFournisseurEntity.FournisseurId);
            ViewData["NomCommandeFournisseurStatutId"] = new SelectList(_context.NomCommandeFournisseurStatutEntities, "Id", "Code", commandeFournisseurEntity.NomCommandeFournisseurStatutId);
            return View(commandeFournisseurEntity);
        }

        // POST: CommandeFournisseur/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Numero,Commentaire,ContactId,FournisseurId,NomCommandeFournisseurStatutId,NomCommandeFournisseurTypeId,IsDeleted,CreatedAt,UpdatedAt")] CommandeFournisseurEntity commandeFournisseurEntity)
        {
            if (id != commandeFournisseurEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commandeFournisseurEntity);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContactId"] = new SelectList(_context.ContactEntities, "Id", "Id", commandeFournisseurEntity.ContactId);
            ViewData["FournisseurId"] = new SelectList(_context.FournisseurEntities, "Id", "Id", commandeFournisseurEntity.FournisseurId);
            ViewData["NomCommandeFournisseurStatutId"] = new SelectList(_context.NomCommandeFournisseurStatutEntities, "Id", "Code", commandeFournisseurEntity.NomCommandeFournisseurStatutId);
            return View(commandeFournisseurEntity);
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
