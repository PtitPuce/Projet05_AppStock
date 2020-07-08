using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Data;
using AppStock.Models;

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppStock.Infrastructure.Repositories.Fournisseur
{
    public class FournisseurRepository : IFournisseurRepository
    {
        private readonly ApplicationDbContext _context;
        public FournisseurRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(ApplicationDbContext));
        }
        
        public async Task<FournisseurEntity> GetOneByIdAsync(int id)
        {
            return await _context.FournisseurEntities
                                .Include(o => o.Adresse)                                
                                .FirstOrDefaultAsync(o => o.Id == id)
                                ;
        }
    }
}