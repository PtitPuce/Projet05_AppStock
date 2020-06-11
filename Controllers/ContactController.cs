using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppStock.Data;
using AppStock.Models;
using AppStock.Models.DTO;
using AppStock.Infrastructure.Services.Contact;

using AutoMapper;


namespace AppStock.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IContactService _service_contact;
        private readonly IMapper _mapper;


        public ContactController(ApplicationDbContext context, IMapper mapper, IContactService service_contact)
        {
            _context = context;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _service_contact = service_contact ?? throw new ArgumentNullException(nameof(service_contact));
        }

        // GET: Contact
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ContactEntities.Include(c => c.Adresse).Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Contact/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactEntity = await _context.ContactEntities
                .Include(c => c.Adresse)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactEntity == null)
            {
                return NotFound();
            }

            return View(contactEntity);
        }

        // GET: Contact/Create
        public IActionResult Create()
        {
            ContactEntity contact_blank = new ContactEntity();
            AdresseEntity adresse_blank = new AdresseEntity();

            contact_blank.Prenom = "Cle";
            contact_blank.Nom = "Cha";
            adresse_blank.Champ1 = "txt txt 1";
            adresse_blank.Champ2 = "txt txt 2";

            var _dto        = _mapper.Map<ContactWithAdresseDTO>(contact_blank);
            _dto.Adresse    = _mapper.Map<AdresseDTO>(adresse_blank);
            
            return View(_dto);
        }

        // POST: Contact/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ContactWithAdresseDTO _dto )
        {
            if (ModelState.IsValid)
            {
                AdresseEntity adresse_db = _mapper.Map<AdresseEntity>(_dto.Adresse);
                ContactEntity contact_db = _mapper.Map<ContactEntity>(_dto);
                contact_db.Adresse = adresse_db;
                
                _context.Add(contact_db);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(_dto);
        }

        // GET: Contact/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactEntity = await _context.ContactEntities.FindAsync(id);
            if (contactEntity == null)
            {
                return NotFound();
            }
            ViewData["AdresseId"] = new SelectList(_context.AdresseEntities, "Id", "Id", contactEntity.AdresseId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", contactEntity.UserId);
            return View(contactEntity);
        }

        // POST: Contact/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prenom,Telephone,AdresseId,UserId")] ContactEntity contactEntity)
        {
            if (id != contactEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactEntityExists(contactEntity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdresseId"] = new SelectList(_context.AdresseEntities, "Id", "Id", contactEntity.AdresseId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", contactEntity.UserId);
            return View(contactEntity);
        }

        // GET: Contact/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactEntity = await _context.ContactEntities
                .Include(c => c.Adresse)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactEntity == null)
            {
                return NotFound();
            }

            return View(contactEntity);
        }

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactEntity = await _context.ContactEntities.FindAsync(id);
            _context.ContactEntities.Remove(contactEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactEntityExists(int id)
        {
            return _context.ContactEntities.Any(e => e.Id == id);
        }
    }
}
