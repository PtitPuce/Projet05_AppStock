using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppStock.Data;
using AppStock.Models;
using AppStock.Infrastructure.Services.Article;
using System.Runtime.Serialization;

namespace AppStock.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _service;

        private readonly ApplicationDbContext _context;

        public ArticleController(IArticleService service, ApplicationDbContext context)
        {
            _context = context;
            _service = service;
        }

        // GET: Article
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());
        }

        // GET: Article/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var article = await _service.GetOneById(id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Article/Create
        public IActionResult Create()
        {
            /**
                A FAIRE :: preparer et utiliser les services manquants
            **/
            ViewData["ArticleFamilleID"] = new SelectList(_context.ArticleFamilleEntities, "Id", "Code");
            ViewData["NomTypeTVAID"] = new SelectList(_context.NomTypeTVAEntities, "Id", "Code");
            return View();
        }

        // POST: Article/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Libelle,PrixUnitaire,ArticleFamilleID,NomTypeTVAID")] ArticleEntity article)
        {
            if (ModelState.IsValid)
            {
                var item = await _service.Add(article);
                return RedirectToAction(nameof(Index));
            }

            /**
                A FAIRE :: preparer et utiliser les services manquants
            **/
            ViewData["ArticleFamilleID"] = new SelectList(_context.ArticleFamilleEntities, "Id", "Code", article.ArticleFamilleID);
            ViewData["NomTypeTVAID"] = new SelectList(_context.NomTypeTVAEntities, "Id", "Code", article.NomTypeTVAID);
            return View(article);
        }

        // GET: Article/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var article = await _service.GetOneById(id);
            if (article == null)
            {
                return NotFound();
            }

            /**
                A FAIRE :: preparer et utiliser les services manquants
            **/
            ViewData["ArticleFamilleID"] = new SelectList(_context.ArticleFamilleEntities, "Id", "Code", article.ArticleFamilleID);
            ViewData["NomTypeTVAID"] = new SelectList(_context.NomTypeTVAEntities, "Id", "Code", article.NomTypeTVAID);
            return View(article);
        }

        // POST: Article/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Libelle,PrixUnitaire,ArticleFamilleID,NomTypeTVAID")] ArticleEntity article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.Update(article);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id))
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
            
            /**
                A FAIRE :: preparer et utiliser les services manquants
            **/
            ViewData["ArticleFamilleID"] = new SelectList(_context.ArticleFamilleEntities, "Id", "Code", article.ArticleFamilleID);
            ViewData["NomTypeTVAID"] = new SelectList(_context.NomTypeTVAEntities, "Id", "Code", article.NomTypeTVAID);
            return View(article);
        }

        // GET: Article/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var article = await _service.GetOneById(id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Article/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _service.Exist(id);
        }
    }
}
