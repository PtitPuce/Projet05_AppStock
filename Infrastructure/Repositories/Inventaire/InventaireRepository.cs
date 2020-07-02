using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Data;
using AppStock.Models;

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppStock.Infrastructure.Repositories.Inventaire
{
    public class InventaireRepository : IInventaireRepository
    {
        private readonly ApplicationDbContext _context;
        public InventaireRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(ApplicationDbContext));
        }
        
        public async Task<IEnumerable<InventaireEntity>> GetAllAsync()
        {
            return await _context.InventaireEntities
                                    .Include(o => o.NomInventaireStatut)
                                    .Include(o => o.User)
                                    .Include(o => o.ArticleFamille)
                                        .ThenInclude (o => o.Articles)
                                            .ThenInclude(o => o.Stock)
                                    .Include(o => o.InventaireLignes)
                                    .ToListAsync();
        }
        
        public async Task<InventaireEntity> GetOneByIdAsync(int id)
        {
            return await _context.InventaireEntities
                                    .Include(o => o.NomInventaireStatut)
                                    .Include(o => o.User)
                                    .Include(o => o.ArticleFamille)
                                        .ThenInclude (o => o.Articles)
                                            .ThenInclude(o => o.Stock)
                                    .Include(o => o.InventaireLignes)
                                    .FirstOrDefaultAsync(m => m.Id == id);
        }
        
        public async Task<InventaireEntity> AddAsync(InventaireEntity item)
        {
            item.NomInventaireStatutId = 1; // hardCode "En cours"
            _context.InventaireEntities.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<InventaireEntity> UpdateAsync(InventaireEntity item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
        
        public async Task<InventaireEntity> DeleteAsync(InventaireEntity item)
        {
            item.IsDeleted = true;
            _context.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<InventaireEntity> ValidateAsync(InventaireEntity item)
        {
            item.NomInventaireStatutId = 2; // hardCode "VALIDEE"
            _context.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
        

        public bool Exist(int id)
        {
            return _context.InventaireEntities.Any(i => i.Id == id);
        }
    }
}