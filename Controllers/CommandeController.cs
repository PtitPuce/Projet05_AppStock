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
    public class CommandeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommandeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Commande
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CommandeEntities.Include(c => c.Contact).Include(c => c.NomCommandeStatut).Include(c => c.NomCommandeType);
            return View(await applicationDbContext.ToListAsync());
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
                .Include(c => c.NomCommandeType)
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
            ViewData["NomCommandeTypeId"] = new SelectList(_context.NomCommandeTypeEntities, "Id", "Code");
            return View();
        }

        // POST: Commande/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Numero,Commentaire,ContactId,NomCommandeStatutId,NomCommandeTypeId,IsDeleted")] CommandeEntity commandeEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commandeEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContactId"] = new SelectList(_context.ContactEntities, "Id", "Id", commandeEntity.ContactId);
            ViewData["NomCommandeStatutId"] = new SelectList(_context.NomCommandeStatutEntities, "Id", "Code", commandeEntity.NomCommandeStatutId);
            ViewData["NomCommandeTypeId"] = new SelectList(_context.NomCommandeTypeEntities, "Id", "Code", commandeEntity.NomCommandeTypeId);
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
            ViewData["NomCommandeTypeId"] = new SelectList(_context.NomCommandeTypeEntities, "Id", "Code", commandeEntity.NomCommandeTypeId);
            return View(commandeEntity);
        }

        // POST: Commande/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Numero,Commentaire,ContactId,NomCommandeStatutId,NomCommandeTypeId,IsDeleted,CreatedAt")] CommandeEntity commandeEntity)
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
            ViewData["NomCommandeTypeId"] = new SelectList(_context.NomCommandeTypeEntities, "Id", "Code", commandeEntity.NomCommandeTypeId);
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
                .Include(c => c.NomCommandeType)
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
