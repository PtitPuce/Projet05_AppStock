using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Data;
using AppStock.Models;

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppStock.Infrastructure.Repositories.CommandeLigne
{
    public class CommandeLigneRepository : ICommandeLigneRepository
    {
        private readonly ApplicationDbContext _context;
        public CommandeLigneRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(ApplicationDbContext));
        }
        
        public async Task<IEnumerable<CommandeLigneEntity>> GetAllAsync()
        {
            return await _context.CommandeClientLigneEntities
                                    .Include(o => o.Article)
                                    .ToListAsync();
        }
        
        public async Task<CommandeLigneEntity> GetOneByIdAsync(int id)
        {
            return await _context.CommandeClientLigneEntities
                                    .Include(o => o.Article)
                                    .FirstOrDefaultAsync(m => m.Id == id);
        }
        
        public async Task<CommandeLigneEntity> AddAsync(CommandeLigneEntity item)
        {
            _context.CommandeClientLigneEntities.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<CommandeLigneEntity> UpdateAsync(CommandeLigneEntity item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
        
        public async Task<CommandeLigneEntity> DeleteAsync(CommandeLigneEntity item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }
        

        public bool Exist(int id)
        {
            return _context.CommandeClientLigneEntities.Any(i => i.Id == id);
        }

    }
}