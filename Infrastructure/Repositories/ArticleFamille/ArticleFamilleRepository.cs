using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Data;
using AppStock.Models;

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppStock.Infrastructure.Repositories.ArticleFamille
{
    public class ArticleFamilleRepository : IArticleFamilleRepository
    {
        private readonly ApplicationDbContext _context;
        public ArticleFamilleRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(ApplicationDbContext));
        }
        
        public async Task<IEnumerable<ArticleFamilleEntity>> GetAllAsync()
        {
            return await _context.ArticleFamilleEntities.ToListAsync();
        }
        
        public async Task<ArticleFamilleEntity> GetOneByIdAsync(int id)
        {
            return await _context.ArticleFamilleEntities.FirstOrDefaultAsync(m => m.Id == id);
        }
        
        public async Task<ArticleFamilleEntity> AddAsync(ArticleFamilleEntity item)
        {
            _context.ArticleFamilleEntities.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<ArticleFamilleEntity> UpdateAsync(ArticleFamilleEntity item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
        
        public async Task<ArticleFamilleEntity> DeleteAsync(ArticleFamilleEntity item)
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