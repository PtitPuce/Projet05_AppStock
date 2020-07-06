using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Data;
using AppStock.Models;

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppStock.Infrastructure.Repositories.CommandeFournisseur
{
    public class CommandeFournisseurRepository : ICommandeFournisseurRepository
    {
        private readonly ApplicationDbContext _context;
        public CommandeFournisseurRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(ApplicationDbContext));
        }
        
        public async Task<IEnumerable<CommandeFournisseurEntity>> GetAllAsync()
        {
            return await _context.CommandeFournisseurEntities
                                    .Include(o => o.Contact)
                                        .ThenInclude(o => o.Adresse)
                                    .Include(o => o.NomCommandeFournisseurStatut)
                                    .Include(o => o.CommandeFournisseurLignes)
                                        .ThenInclude(o => o.Article)
                                            .ThenInclude(o => o.NomTypeTVA)
                                    .ToListAsync();
        }
        
        public async Task<CommandeFournisseurEntity> GetOneByIdAsync(int id)
        {
            return await _context.CommandeFournisseurEntities
                                    .Include(o => o.Contact)
                                        .ThenInclude(o => o.Adresse)
                                    .Include(o => o.NomCommandeFournisseurStatut)
                                    .Include(o => o.CommandeFournisseurLignes)
                                        .ThenInclude(o => o.Article)
                                            .ThenInclude(o => o.NomTypeTVA)
                                    .FirstOrDefaultAsync(m => m.Id == id);
        }
        
        public async Task<CommandeFournisseurEntity> AddAsync(CommandeFournisseurEntity item)
        {
            _context.CommandeFournisseurEntities.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<CommandeFournisseurEntity> UpdateAsync(CommandeFournisseurEntity item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
        
        public async Task<CommandeFournisseurEntity> DeleteAsync(CommandeFournisseurEntity item)
        {
            item.IsDeleted = true;
            _context.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public bool Exist(int id)
        {
            return _context.CommandeFournisseurEntities.Any(i => i.Id == id);
        }

    }
}