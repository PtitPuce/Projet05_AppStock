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
                                    .Include(o => o.Fournisseur)
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
                                    .Include(o => o.Fournisseur)
                                        .ThenInclude(o => o.Adresse)
                                    .Include(o => o.NomCommandeFournisseurStatut)
                                    .Include(o => o.CommandeFournisseurLignes)
                                        .ThenInclude(o => o.Article)
                                            .ThenInclude(o => o.NomTypeTVA)
                                    .FirstOrDefaultAsync(m => m.Id == id);
        }
        
        public async Task<CommandeFournisseurEntity> AddAsync(CommandeFournisseurEntity item)
        {
            item.NomCommandeFournisseurStatutId = _context.NomCommandeFournisseurStatutEntities.Where(o => o.Code=="C").FirstOrDefault().Id; // A = création
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

        public async Task<CommandeFournisseurEntity> ValidateAsync(CommandeFournisseurEntity item)
        {
            item.NomCommandeFournisseurStatutId =  _context.NomCommandeFournisseurStatutEntities.Where(o => o.Code=="T").FirstOrDefault().Id; // A = Transmise au transporteur
            _context.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public bool Exist(int id)
        {
            return _context.CommandeFournisseurEntities.Any(i => i.Id == id);
        }


        public int getTotalPendingArticles(int id_article)
        {
            var _total = 0;

            _total = _context.CommandeFournisseurLigneEntities
                        .Include(o => o.CommandeFournisseur)
                            .ThenInclude(o => o.NomCommandeFournisseurStatut)
                        .Where(o => o.CommandeFournisseur.NomCommandeFournisseurStatut.Code == "T") // T == Transmis au Fournisseur (le stock est engagé)
                        .Where(o => o.ArticleId == id_article)
                        .Sum(o => o.Quantite)
                        ;

            return _total;
        }

    }
}