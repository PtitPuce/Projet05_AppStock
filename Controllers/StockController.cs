using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppStock.Data;
using AppStock.Models;
using AppStock.Infrastructure.Services.Stock;

namespace AppStock.Controllers
{
    public class StockController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockService _service;

        public StockController(ApplicationDbContext context, IStockService service)
        {
            _context = context;
            _service = service;
        }

        // GET: Stock
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());
        }

        // GET: Stock/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var stock = await _service.GetOneById(id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        // GET: Stock/Create
        public IActionResult Create()
        {
            ViewData["ArticleID"] = new SelectList(_context.ArticleEntities, "Id", "Code");
            return View();
        }

        // POST: Stock/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticleID,Quantite")] StockEntity item)
        {
            if (ModelState.IsValid)
            {
                await _service.Add(item);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticleID"] = new SelectList(_context.ArticleEntities, "Id", "Code", item.ArticleID);
            return View(item);
        }

        // GET: Stock/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var stock = await _service.GetOneById(id);
            if (stock == null)
            {
                return NotFound();
            }
            ViewData["ArticleID"] = new SelectList(_context.ArticleEntities, "Id", "Code", stock.ArticleID);
            return View(stock);
        }

        // POST: Stock/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleID,Quantite")] StockEntity item)
        {
            if (id != item.ArticleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.Update(item);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Exists(item.ArticleID))
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
            ViewData["ArticleID"] = new SelectList(_context.ArticleEntities, "Id", "Code", item.ArticleID);
            return View(item);
        }

        // GET: Stock/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var stock = await _service.GetOneById(id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        // POST: Stock/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        private bool Exists(int id)
        {
            return _service.Exist(id);
        }
    }
}
