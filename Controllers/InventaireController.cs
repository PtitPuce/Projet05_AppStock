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

        // GET: Inventaire/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventaireEntity = await _context.InventaireEntities
                .Include(i => i.ArticleFamille)
                .Include(i => i.NomInventaireStatut)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventaireEntity == null)
            {
                return NotFound();
            }

            return View(inventaireEntity);
        }

        // GET: Inventaire/Create
        public IActionResult Create()
        {
            ViewData["ArticleFamilleId"] = new SelectList(_context.ArticleFamilleEntities, "Id", "Code");
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
            var inventaireEntity = await _service_inventaire.GetOneById(id);
            if (inventaireEntity == null)
            {
                return NotFound();
            }
            return View(inventaireEntity);
        }

        // POST: Inventaire/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,NomInventaireStatutId,ArticleFamilleId,DateCloture,IsDeleted,CreatedAt,UpdatedAt")] InventaireEntity inventaireEntity)
        {
            if (id != inventaireEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventaireEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventaireEntityExists(inventaireEntity.Id))
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
            ViewData["ArticleFamilleId"] = new SelectList(_context.ArticleFamilleEntities, "Id", "Code", inventaireEntity.ArticleFamilleId);
            ViewData["NomInventaireStatutId"] = new SelectList(_context.NomInventaireStatutEntities, "Id", "Code", inventaireEntity.NomInventaireStatutId);
            return View(inventaireEntity);
        }

        // GET: Inventaire/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventaireEntity = await _context.InventaireEntities
                .Include(i => i.ArticleFamille)
                .Include(i => i.NomInventaireStatut)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventaireEntity == null)
            {
                return NotFound();
            }

            return View(inventaireEntity);
        }

        // POST: Inventaire/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventaireEntity = await _context.InventaireEntities.FindAsync(id);
            _context.InventaireEntities.Remove(inventaireEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventaireEntityExists(int id)
        {
            return _context.InventaireEntities.Any(e => e.Id == id);
        }
    }
}
