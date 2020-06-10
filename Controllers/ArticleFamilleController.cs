using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppStock.Data;
using AppStock.Models;
using AppStock.Infrastructure.Services.ArticleFamille;

namespace AppStock.Controllers
{
    public class ArticleFamilleController : Controller
    {
        private readonly IArticleFamilleService _service;
        private readonly ApplicationDbContext _context;

        public ArticleFamilleController(IArticleFamilleService service, ApplicationDbContext context)
        {
            _context = context;
            _service = service;
        }

        // GET: ArticleFamille
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());
        }

        // GET: ArticleFamille/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var articleFamille = await _service.GetOneById(id);
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
        public async Task<IActionResult> Create([Bind("Id,Code,Libelle")] ArticleFamilleEntity articleFamille)
        {
            if (ModelState.IsValid)
            {
                var item = await _service.Add(articleFamille);
                return RedirectToAction(nameof(Index));
            }
            return View(articleFamille);
        }

        // GET: ArticleFamille/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var articleFamille = await _service.GetOneById(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Libelle")] ArticleFamilleEntity articleFamille)
        {
            if (id != articleFamille.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.Update(articleFamille);
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
        public async Task<IActionResult> Delete(int id)
        {
            var articleFamille = await _service.GetOneById(id);
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
            await _service.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleFamilleExists(int id)
        {
            return _service.Exist(id);
        }
    }
}
