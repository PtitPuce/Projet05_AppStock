using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Data;
using AppStock.Models;

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppStock.Infrastructure.Repositories.InventaireLigne
{
    public class InventaireLigneRepository : IInventaireLigneRepository
    {
        private readonly ApplicationDbContext _context;
        public InventaireLigneRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(ApplicationDbContext));
        }
        
        public async Task<IEnumerable<InventaireLigneEntity>> GetAllAsync()
        {
            return await _context.InventaireLigneEntities
                                    .Include(o => o.Article)
                                    .Include(o => o.Article.ArticleFamille)
                                    .Include(o => o.Inventaire)
                                    .ToListAsync();
        }
        
        public async Task<InventaireLigneEntity> GetOneByIdAsync(int id)
        {
            return await _context.InventaireLigneEntities
                                    .Include(o => o.Article)
                                    .Include(o => o.Article.ArticleFamille)
                                    .Include(o => o.Inventaire)
                                    .FirstOrDefaultAsync(m => m.Id == id);
        }
        
        public async Task<InventaireLigneEntity> AddAsync(InventaireLigneEntity item)
        {
            _context.InventaireLigneEntities.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<InventaireLigneEntity> UpdateAsync(InventaireLigneEntity item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
        
        public async Task<InventaireLigneEntity> DeleteAsync(InventaireLigneEntity item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }
        

        public bool Exist(int id)
        {
            return _context.InventaireLigneEntities.Any(i => i.Id == id);
        }

    }
}