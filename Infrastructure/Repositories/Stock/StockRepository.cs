using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Data;
using AppStock.Models;

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppStock.Infrastructure.Repositories.Stock
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(ApplicationDbContext));
        }
        
        public async Task<IEnumerable<StockEntity>> GetAllAsync()
        {
            return await _context.StockEntities
                                    .Include(a => a.Article)
                                        .ThenInclude(a => a.ArticleFamille)
                                    .ToListAsync()
                                    ;
        }
        
        public async Task<StockEntity> GetOneByIdAsync(int id)
        {
            return await _context.StockEntities
                                    .Include(a => a.Article)
                                        .ThenInclude(a => a.ArticleFamille)
                                    .FirstOrDefaultAsync(m => m.ArticleID == id)
                                    ;
        }
        
        public async Task<StockEntity> AddAsync(StockEntity item)
        {
            _context.StockEntities.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<StockEntity> UpdateAsync(StockEntity item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
        
        public async Task<StockEntity> DeleteAsync(StockEntity item)
        {
            item.IsDeleted = true;
            _context.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
        

        public bool ExistForArticleId(int id)
        {
            return _context.StockEntities.Any(i => i.ArticleID == id);
        }
    }
}