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
    public class CommandeFournisseurController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommandeFournisseurController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CommandeFournisseur
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CommandeFournisseurEntities.Include(c => c.Contact).Include(c => c.Fournisseur).Include(c => c.NomCommandeFournisseurStatut);
            return View(await applicationDbContext.ToListAsync());
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
            ViewData["ContactId"] = new SelectList(_context.ContactEntities, "Id", "Id");
            ViewData["FournisseurId"] = new SelectList(_context.FournisseurEntities, "Id", "Id");
            ViewData["NomCommandeFournisseurStatutId"] = new SelectList(_context.NomCommandeFournisseurStatutEntities, "Id", "Code");
            return View();
        }

        // POST: CommandeFournisseur/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Numero,Commentaire,ContactId,FournisseurId,NomCommandeFournisseurStatutId,NomCommandeFournisseurTypeId,IsDeleted,CreatedAt,UpdatedAt")] CommandeFournisseurEntity commandeFournisseurEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commandeFournisseurEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
