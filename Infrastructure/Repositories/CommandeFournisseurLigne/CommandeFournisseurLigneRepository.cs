using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Data;
using AppStock.Models;

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppStock.Infrastructure.Repositories.CommandeFournisseurLigne
{
    public class CommandeFournisseurLigneRepository : ICommandeFournisseurLigneRepository
    {
        private readonly ApplicationDbContext _context;
        public CommandeFournisseurLigneRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(ApplicationDbContext));
        }
        
        public async Task<IEnumerable<CommandeFournisseurLigneEntity>> GetAllAsync()
        {
            return await _context.CommandeFournisseurLigneEntities
                                    .Include(o => o.Article)
                                    .Include(o => o.Article.ArticleFamille)
                                    .Include(o => o.Article.NomTypeTVA)
                                    .Include(o => o.CommandeFournisseur)
                                    .ToListAsync();
        }
        
        public async Task<CommandeFournisseurLigneEntity> GetOneByIdAsync(int id)
        {
            return await _context.CommandeFournisseurLigneEntities
                                    .Include(o => o.Article)
                                    .Include(o => o.Article.ArticleFamille)
                                    .Include(o => o.Article.NomTypeTVA)
                                    .Include(o => o.CommandeFournisseur)
                                    .FirstOrDefaultAsync(m => m.Id == id);
        }
        
        public async Task<CommandeFournisseurLigneEntity> AddAsync(CommandeFournisseurLigneEntity item)
        {
            _context.CommandeFournisseurLigneEntities.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<CommandeFournisseurLigneEntity> UpdateAsync(CommandeFournisseurLigneEntity item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
        
        public async Task<CommandeFournisseurLigneEntity> DeleteAsync(CommandeFournisseurLigneEntity item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public bool Exist(int id)
        {
            return _context.CommandeFournisseurLigneEntities.Any(i => i.Id == id);
        }

    }
}