using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Data;
using AppStock.Models;

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppStock.Infrastructure.Repositories.NomTypeTVA
{
    public class NomTypeTVARepository : INomTypeTVARepository
    {
        private readonly ApplicationDbContext _context;
        public NomTypeTVARepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(ApplicationDbContext));
        }
        
        public async Task<IEnumerable<NomTypeTVAEntity>> GetAllAsync()
        {
            return await _context.NomTypeTVAEntities.ToListAsync();
        }
        
        public async Task<NomTypeTVAEntity> GetOneByIdAsync(int id)
        {
            return await _context.NomTypeTVAEntities.FirstOrDefaultAsync(m => m.Id == id);
        }
        
        public async Task<NomTypeTVAEntity> AddAsync(NomTypeTVAEntity item)
        {
            _context.NomTypeTVAEntities.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<NomTypeTVAEntity> UpdateAsync(NomTypeTVAEntity item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
        
        public async Task<NomTypeTVAEntity> DeleteAsync(NomTypeTVAEntity item)
        {
            item.IsDeleted = true;
            _context.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
        

        public bool Exist(int id)
        {
            return _context.NomTypeTVAEntities.Any(i => i.Id == id);
        }
    }
}