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
using AppStock.Infrastructure.Services.Stock;
using System.Runtime.Serialization;

namespace AppStock.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _service;
        private readonly IStockService _service_stock;

        private readonly ApplicationDbContext _context;

        public ArticleController(IArticleService service, IStockService service_stock, ApplicationDbContext context)
        {
            _context = context;
            _service = service;
            _service_stock = service_stock;
        }

        // GET: Article
        public async Task<IActionResult> Index()
        {
            /*
                A FAIRE
                Pour filtrer les entites disponibles a l'affichage, il faudra determiner la nature de l'utilisateur
                    CLIENT  ->  tel quel (càd. avec les QueryFilters d'activés)
                    ADMIN   -> .IgnoreQueryFilters()
            */
            ViewData["Service"] = _service;
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
            ViewData["FournisseurID"] = new SelectList(_context.FournisseurEntities, "Id", "Raison");
            ViewData["ArticleFamilleID"] = new SelectList(_context.ArticleFamilleEntities, "Id", "Code");
            ViewData["NomTypeTVAID"] = new SelectList(_context.NomTypeTVAEntities, "Id", "Code");
            return View();
        }

        // POST: Article/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Libelle,PrixUnitaire,FournisseurId,ArticleFamilleId,NomTypeTVAId,Threshold")] ArticleEntity article)
        {
            if (ModelState.IsValid)
            {
                var item = await _service.Add(article);
                // init STOCK par defaut(quantite==threshold)
                await _service_stock.InitStockForArticle(article);
                return RedirectToAction(nameof(Index));
            }

            /**
                A FAIRE :: preparer et utiliser les services manquants
            **/
            ViewData["FournisseurID"] = new SelectList(_context.FournisseurEntities, "Id", "Raison", article.FournisseurId);
            ViewData["ArticleFamilleID"] = new SelectList(_context.ArticleFamilleEntities, "Id", "Code", article.ArticleFamilleId);
            ViewData["NomTypeTVAID"] = new SelectList(_context.NomTypeTVAEntities, "Id", "Code", article.NomTypeTVAId);
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
            ViewData["FournisseurID"] = new SelectList(_context.FournisseurEntities, "Id", "Raison", article.FournisseurId);
            ViewData["ArticleFamilleID"] = new SelectList(_context.ArticleFamilleEntities, "Id", "Code", article.ArticleFamilleId);
            ViewData["NomTypeTVAID"] = new SelectList(_context.NomTypeTVAEntities, "Id", "Code", article.NomTypeTVAId);
            return View(article);
        }

        // POST: Article/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Libelle,PrixUnitaire,FournisseurId,ArticleFamilleId,NomTypeTVAId,Threshold")] ArticleEntity article)
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
            ViewData["FournisseurID"] = new SelectList(_context.FournisseurEntities, "Id", "Raison", article.FournisseurId);
            ViewData["ArticleFamilleID"] = new SelectList(_context.ArticleFamilleEntities, "Id", "Code", article.ArticleFamilleId);
            ViewData["NomTypeTVAID"] = new SelectList(_context.NomTypeTVAEntities, "Id", "Code", article.NomTypeTVAId);
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
