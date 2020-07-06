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
    public class FournisseurController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FournisseurController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fournisseur
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FournisseurEntities.Include(f => f.Adresse);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Fournisseur/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fournisseurEntity = await _context.FournisseurEntities
                .Include(f => f.Adresse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fournisseurEntity == null)
            {
                return NotFound();
            }

            return View(fournisseurEntity);
        }

        // GET: Fournisseur/Create
        public IActionResult Create()
        {
            ViewData["AdresseId"] = new SelectList(_context.AdresseEntities, "Id", "Id");
            return View();
        }

        // POST: Fournisseur/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Raison,Telephone,Email,AdresseId")] FournisseurEntity fournisseurEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fournisseurEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdresseId"] = new SelectList(_context.AdresseEntities, "Id", "Id", fournisseurEntity.AdresseId);
            return View(fournisseurEntity);
        }

        // GET: Fournisseur/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fournisseurEntity = await _context.FournisseurEntities.FindAsync(id);
            if (fournisseurEntity == null)
            {
                return NotFound();
            }
            ViewData["AdresseId"] = new SelectList(_context.AdresseEntities, "Id", "Id", fournisseurEntity.AdresseId);
            return View(fournisseurEntity);
        }

        // POST: Fournisseur/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Raison,Telephone,Email,AdresseId")] FournisseurEntity fournisseurEntity)
        {
            if (id != fournisseurEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fournisseurEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FournisseurEntityExists(fournisseurEntity.Id))
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
            ViewData["AdresseId"] = new SelectList(_context.AdresseEntities, "Id", "Id", fournisseurEntity.AdresseId);
            return View(fournisseurEntity);
        }

        // GET: Fournisseur/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fournisseurEntity = await _context.FournisseurEntities
                .Include(f => f.Adresse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fournisseurEntity == null)
            {
                return NotFound();
            }

            return View(fournisseurEntity);
        }

        // POST: Fournisseur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fournisseurEntity = await _context.FournisseurEntities.FindAsync(id);
            _context.FournisseurEntities.Remove(fournisseurEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FournisseurEntityExists(int id)
        {
            return _context.FournisseurEntities.Any(e => e.Id == id);
        }
    }
}
