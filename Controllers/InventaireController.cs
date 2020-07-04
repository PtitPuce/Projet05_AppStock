using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppStock.Data;
using AppStock.Models;
using Microsoft.AspNetCore.Identity;
using AppStock.Infrastructure.Services.Inventaire;
using AppStock.Infrastructure.Services.InventaireLigne;
using AppStock.Infrastructure.Services.Stock;
using AppStock.Infrastructure.Services.ArticleFamille;

namespace AppStock.Controllers
{
    public class InventaireController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _user_manager; 
        private readonly IInventaireService _service_inventaire;
        private readonly IInventaireLigneService _service_inventaire_ligne;
        private readonly IStockService _service_stock;
        private readonly IArticleFamilleService _service_article_famille;
        public SelectList ArticleFamillesWithStockSL { get; set; } // Liste des familles d'articles qui dont tous les articles ont du stock
        public InventaireController( ApplicationDbContext context
                            , UserManager<IdentityUser> user_manager 
                            , IInventaireService service_inventaire 
                            , IInventaireLigneService service_inventaire_ligne
                            , IStockService service_stock
                            , IArticleFamilleService service_article_famille)
        {
            _context = context;
            _user_manager = user_manager;
            _service_inventaire = service_inventaire ;
            _service_inventaire_ligne = service_inventaire_ligne;
            _service_stock = service_stock;
            _service_article_famille = service_article_famille;
        }

        // GET: Inventaire
        public async Task<IActionResult> Index()
        {
            return View(await _service_inventaire.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLigneQuantiteComptee(int ligne_quantite_comptee, int ligne_Id)
        {
            InventaireLigneEntity ligne = await _service_inventaire_ligne.GetOneById(ligne_Id);
            ligne.QuantiteComptee = ligne_quantite_comptee;

            await _service_inventaire_ligne.Update(ligne);

            return RedirectToAction("Edit", new { id = ligne.InventaireId });
        }

        public async Task<IActionResult> ValiderInventaire(int id)
        {
            var inventaire = await _service_inventaire.GetOneByIdWithLignes(id);
            await _service_inventaire.Validate(inventaire);
            return RedirectToAction(nameof(Index));
        }


        // GET: Inventaire/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var item = await _service_inventaire.GetOneByIdWithLignes(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Inventaire/Create
        public IActionResult Create()
        {
            PopulateArticleFamillesWithStockDropDownList();
            //ViewData["ArticleFamilleId"] = new SelectList(_context.ArticleFamilleEntities, "Id", "Libelle");
            ViewData["ArticleFamilleId"] = ArticleFamillesWithStockSL;
            return View();
        }

        // POST: Inventaire/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, ArticleFamilleId")] InventaireEntity item)
        {
            // recuperer l'utilisateur courant
            var _user = await _user_manager.GetUserAsync(HttpContext.User);
            item.UserId = _user.Id;
            
            if (ModelState.IsValid)
            {
                await _service_inventaire.Add(item);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticleFamilleId"] = new SelectList(_context.ArticleFamilleEntities, "Id", "Code", item.ArticleFamilleId);
            return View(item);
        }

        // GET: Inventaire/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var inventaireEntity = await _service_inventaire.GetOneByIdWithLignes(id);
            if (inventaireEntity == null)
            {
                return NotFound();
            }
            if (!_service_inventaire.isEditable(inventaireEntity))
            {
                throw new NotImplementedException();
            }
            return View(inventaireEntity);
        }

        // GET: Inventaire/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _service_inventaire.GetOneById(id);
            if (item == null)
            {
                return NotFound();
            }
            if (!_service_inventaire.isEditable(item))
            {
                throw new NotImplementedException();
            }

            return View(item);
        }

        // POST: Inventaire/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service_inventaire.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        private bool InventaireEntityExists(int id)
        {
            return _context.InventaireEntities.Any(e => e.Id == id);
        }

        // Renvoie la liste des familles qui ont des articles en stock
        public void PopulateArticleFamillesWithStockDropDownList(object selectedArticleFamille = null)
        {
            var articleFamilleQuery = _context.ArticleFamilleEntities
                                .Include(o => o.Articles)
                                    .ThenInclude(o => o.Stock)
                                .Where(o=> o.Articles.Any(i => i.Stock != null))
                                .OrderBy(z=> z.Libelle);

            ArticleFamillesWithStockSL = new SelectList(articleFamilleQuery.AsNoTracking(),
                        "Id", "Libelle", selectedArticleFamille);
        }
    }
}
