using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Data;
using AppStock.Models;

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppStock.Infrastructure.Repositories.Commande
{
    public class CommandeRepository : ICommandeRepository
    {
        private readonly ApplicationDbContext _context;
        public CommandeRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(ApplicationDbContext));
        }
        
        public async Task<IEnumerable<CommandeEntity>> GetAllAsync()
        {
            return await _context.CommandeEntities
                                    .Include(o => o.Contact)
                                    .Include(o => o.NomCommandeStatut)
                                    .Include(o => o.CommandeLignes)
                                    .ToListAsync();
        }
        
        public async Task<CommandeEntity> GetOneByIdAsync(int id)
        {
            return await _context.CommandeEntities
                                    .Include(o => o.Contact)
                                    .Include(o => o.NomCommandeStatut)
                                    .Include(o => o.CommandeLignes)
                                    .FirstOrDefaultAsync(m => m.Id == id);
        }
        
        public async Task<CommandeEntity> AddAsync(CommandeEntity item)
        {
            _context.CommandeEntities.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<CommandeEntity> UpdateAsync(CommandeEntity item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
        
        public async Task<CommandeEntity> DeleteAsync(CommandeEntity item)
        {
            item.IsDeleted = true;
            _context.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
        

        public bool Exist(int id)
        {
            return _context.CommandeEntities.Any(i => i.Id == id);
        }

    }
}