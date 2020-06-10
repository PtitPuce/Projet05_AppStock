using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppStock.Data;
using AppStock.Models;

namespace AppStock.Controllers
{
    public class ArticleFamilleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArticleFamilleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ArticleFamille
        public async Task<IActionResult> Index()
        {
            return View(await _context.ArticleFamilles.ToListAsync());
        }

        // GET: ArticleFamille/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleFamille = await _context.ArticleFamilles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articleFamille == null)
            {
                return NotFound();
            }

            return View(articleFamille);
        }

        // GET: ArticleFamille/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ArticleFamille/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Libelle")] ArticleFamille articleFamille)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articleFamille);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(articleFamille);
        }

        // GET: ArticleFamille/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleFamille = await _context.ArticleFamilles.FindAsync(id);
            if (articleFamille == null)
            {
                return NotFound();
            }
            return View(articleFamille);
        }

        // POST: ArticleFamille/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Libelle")] ArticleFamille articleFamille)
        {
            if (id != articleFamille.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articleFamille);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleFamilleExists(articleFamille.Id))
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
            return View(articleFamille);
        }

        // GET: ArticleFamille/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articleFamille = await _context.ArticleFamilles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articleFamille == null)
            {
                return NotFound();
            }

            return View(articleFamille);
        }

        // POST: ArticleFamille/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var articleFamille = await _context.ArticleFamilles.FindAsync(id);
            _context.ArticleFamilles.Remove(articleFamille);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleFamilleExists(int id)
        {
            return _context.ArticleFamilles.Any(e => e.Id == id);
        }
    }
}
