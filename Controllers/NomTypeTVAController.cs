using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppStock.Data;
using AppStock.Models;
using AppStock.Infrastructure.Services.NomTypeTVA;

namespace AppStock.Controllers
{
    public class NomTypeTVAController : Controller
    {
        private readonly INomTypeTVAService _service;
        private readonly ApplicationDbContext _context;

        public NomTypeTVAController(INomTypeTVAService service, ApplicationDbContext context)
        {
            _context = context;
            _service = service;
        }

        // GET: NomTypeTVA
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());
        }

        // GET: NomTypeTVA/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var nomTypeTVA = await _service.GetOneById(id);
            if (nomTypeTVA == null)
            {
                return NotFound();
            }

            return View(nomTypeTVA);
        }

        // GET: NomTypeTVA/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NomTypeTVA/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Libelle,Taux")] NomTypeTVAEntity nomTypeTVA)
        {
            if (ModelState.IsValid)
            {
                var item = await _service.Add(nomTypeTVA);
                return RedirectToAction(nameof(Index));
            }
            return View(nomTypeTVA);
        }

        // GET: NomTypeTVA/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var nomTypeTVA = await _service.GetOneById(id);
            if (nomTypeTVA == null)
            {
                return NotFound();
            }
            return View(nomTypeTVA);
        }

        // POST: NomTypeTVA/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Libelle,Taux")] NomTypeTVAEntity nomTypeTVA)
        {
            if (id != nomTypeTVA.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.Update(nomTypeTVA);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NomTypeTVAExists(nomTypeTVA.Id))
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
            return View(nomTypeTVA);
        }

        // GET: NomTypeTVA/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var nomTypeTVA = await _service.GetOneById(id);
            if (nomTypeTVA == null)
            {
                return NotFound();
            }

            return View(nomTypeTVA);
        }

        // POST: NomTypeTVA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        private bool NomTypeTVAExists(int id)
        {
            return _service.Exist(id);
        }
    }
}
