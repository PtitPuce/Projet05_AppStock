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
                                    .Include(o => o.NomCommandeType)
                                    .Include(o => o.CommandeLignes)
                                        .ThenInclude(o => o.Article)
                                            .ThenInclude(o => o.NomTypeTVA)
                                    .ToListAsync();
        }
        
        public async Task<CommandeEntity> GetOneByIdAsync(int id)
        {
            return await _context.CommandeEntities
                                    .Include(o => o.Contact)
                                    .Include(o => o.NomCommandeStatut)
                                    .Include(o => o.NomCommandeType)
                                    .Include(o => o.CommandeLignes)
                                        .ThenInclude(o => o.Article)
                                            .ThenInclude(o => o.NomTypeTVA)
                                    .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<CommandeEntity> GetPanierByContactId(int id)
        {
            var _panier = await _context.CommandeEntities
                                    .Where(o => o.NomCommandeStatut.Code == "P" 
                                                && o.ContactId == id)
                                    .FirstOrDefaultAsync();
            
            if(_panier is null || _panier.Id == 0)
            {
                CommandeEntity _commande = new CommandeEntity();
                _commande.ContactId = id;
                _commande.NomCommandeStatutId = 1; // hardCode "PANIER"
                _commande.NomCommandeTypeId = 2; // hardCode "CLIENT"
                
                await AddAsync(_commande);
                return await GetOneByIdAsync(_commande.Id);
            }

            return await GetOneByIdAsync(_panier.Id);
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