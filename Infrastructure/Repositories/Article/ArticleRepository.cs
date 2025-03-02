using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Data;
using AppStock.Models;

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppStock.Infrastructure.Repositories.Article
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _context;
        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(ApplicationDbContext));
        }
        
        public async Task<IEnumerable<ArticleEntity>> GetAllAsync()
        {
            return await _context.ArticleEntities
                                    .Include(a => a.Fournisseur)
                                    .Include(a => a.ArticleFamille)
                                    .Include(a => a.NomTypeTVA)
                                    .ToListAsync()
                                    ;
        }

        public async Task<IEnumerable<ArticleEntity>> getAllByFournisseurIdAsync(int id)
        {
            return await _context.ArticleEntities
                                    .Include(a => a.Fournisseur)
                                    .Include(a => a.ArticleFamille)
                                    .Include(a => a.NomTypeTVA)
                                    .Include(a => a.Stock)
                                    .Where(a=> a.FournisseurId == id)
                                    .ToListAsync()
                                    ;
        }

        public IQueryable<ArticleEntity> QueryForStock(){
            var query =  _context.ArticleEntities
                                .Where(o=> o.Stock == null)
                                .OrderBy(z=> z.Libelle);
            return query;
        }
        
        public async Task<ArticleEntity> GetOneByIdAsync(int id)
        {
            return await _context.ArticleEntities
                                    .Include(a => a.Fournisseur)
                                    .Include(a => a.ArticleFamille)
                                    .Include(a => a.NomTypeTVA)
                                    .FirstOrDefaultAsync(m => m.Id == id)
                                    ;
        }
        
        public async Task<ArticleEntity> AddAsync(ArticleEntity item)
        {
            _context.ArticleEntities.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<ArticleEntity> UpdateAsync(ArticleEntity item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
        
        public async Task<ArticleEntity> DeleteAsync(ArticleEntity item)
        {
            item.IsDeleted = true;
            _context.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
        

        public bool Exist(int id)
        {
            return _context.ArticleEntities.Any(i => i.Id == id);
        }
    }
}