using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Data;
using AppStock.Models;

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppStock.Infrastructure.Repositories.Contact
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;
        public ContactRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(ApplicationDbContext));
        }
        
        public async Task<IEnumerable<ContactEntity>> GetAllAsync()
        {
            return await _context.ContactEntities
                                    .Include(o => o.Adresse)
                                    .Include(o => o.User)
                                    .ToListAsync();
        }
        
        public async Task<ContactEntity> GetOneByIdAsync(int id)
        {
            return await _context.ContactEntities
                                    .Include(o => o.Adresse)
                                    .Include(o => o.User)
                                    .FirstOrDefaultAsync(m => m.Id == id);
        }
        
        public async Task<ContactEntity> GetOneByUserIdAsync(string id)
        {
            return await _context.ContactEntities
                                    .Include(o => o.Adresse)
                                    .Include(o => o.User)
                                    .FirstOrDefaultAsync(m => m.UserId == id);
        }
        

        public async Task<ContactEntity> AddAsync(ContactEntity item)
        {
            _context.ContactEntities.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<ContactEntity> UpdateAsync(ContactEntity item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
        
        public async Task<ContactEntity> DeleteAsync(ContactEntity item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }
        

        public bool Exist(int id)
        {
            return _context.ContactEntities.Any(i => i.Id == id);
        }

    }
}