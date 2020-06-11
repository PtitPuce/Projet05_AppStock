using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Data;
using AppStock.Models;

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppStock.Infrastructure.Repositories.Adresse
{
    public class AdresseRepository : IAdresseRepository
    {
        private readonly ApplicationDbContext _context;
        public AdresseRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(ApplicationDbContext));
        }
        
        public async Task<IEnumerable<AdresseEntity>> GetAllAsync()
        {
            return await _context.AdresseEntities.ToListAsync();
        }
        
        public async Task<AdresseEntity> GetOneByIdAsync(int id)
        {
            return await _context.AdresseEntities.FirstOrDefaultAsync(m => m.Id == id);
        }
        
        public async Task<AdresseEntity> AddAsync(AdresseEntity item)
        {
            _context.AdresseEntities.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<AdresseEntity> UpdateAsync(AdresseEntity item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
        
        public async Task<AdresseEntity> DeleteAsync(AdresseEntity item)
        {
            // Soft delete
            /*
            item.IsDeleted = true;
            _context.Update(item); 
            */
            
            // Hard delete
            _context.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }
        

        public bool Exist(int id)
        {
            return _context.AdresseEntities.Any(i => i.Id == id);
        }
    }
}