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
using AppStock.Infrastructure.Services.Adresse;

using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AppStock.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _user_manager; 
        private readonly IMapper _mapper;
        private readonly IContactService _service_contact;
        private readonly IAdresseService _service_adresse;


        public ContactController(ApplicationDbContext context,
                                    IMapper mapper,
                                    IContactService service_contact,
                                    IAdresseService service_adresse,
                                    UserManager<IdentityUser> user_manager)
        {
            _context            = context;
            _mapper             = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _service_contact    = service_contact ?? throw new ArgumentNullException(nameof(service_contact));
            _service_adresse    = service_adresse  ?? throw new ArgumentNullException(nameof(service_adresse));
            _user_manager       = user_manager  ?? throw new ArgumentNullException(nameof(user_manager));
        }

        // GET: Contact
        public async Task<IActionResult> Index()
        {
            return View(await _service_contact.GetAll());
        }

        // GET: Contact/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var contactEntity = await _service_contact.GetOneById(id);
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
            
            return View(GetDTO(contact_blank, adresse_blank));
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
                var _user =   await _user_manager.GetUserAsync(HttpContext.User);

                AdresseEntity adresse_db = _mapper.Map<AdresseEntity>(_dto.Adresse);
                ContactEntity contact_db = _mapper.Map<ContactEntity>(_dto);
                contact_db.Adresse = adresse_db;                
                contact_db.UserId = _user.Id;
                
                await _service_contact.Add(contact_db);

                return RedirectToAction(nameof(Index));
            }

            return View(_dto);
        }

        // GET: Contact/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var contactEntity = await _service_contact.GetOneById(id);
            if (contactEntity == null)
            {
                return NotFound();
            }

            var _dto = GetDTOWithId(contactEntity, contactEntity.Adresse);
            return View( _dto );
        }

        // POST: Contact/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ContactWithAdresseDTOAndId _dtoWithId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AdresseEntity adresse_db = _mapper.Map<AdresseEntity>(_dtoWithId.Adresse);
                    ContactEntity contact_db = _mapper.Map<ContactEntity>(_dtoWithId);
                    contact_db.Adresse = adresse_db;

                    await _service_contact.Update(contact_db);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactEntityExists(_dtoWithId.Id))
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
            
            return View(_dtoWithId);
        }

        // GET: Contact/Delete/5
        public Task<IActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> DeleteConfirmed(int id)
        {
            throw new NotImplementedException();
        }

        private bool ContactEntityExists(int id)
        {
            return _service_contact.Exist(id);
        }

        private ContactWithAdresseDTO GetDTO(ContactEntity contact, AdresseEntity adresse)
        {
            var _dto        = _mapper.Map<ContactWithAdresseDTO>(contact);
            _dto.Adresse    = _mapper.Map<AdresseDTO>(adresse);
            return _dto;
        }
        private ContactWithAdresseDTOAndId GetDTOWithId(ContactEntity contact, AdresseEntity adresse)
        {
            var _dto        = _mapper.Map<ContactWithAdresseDTOAndId>(contact);
            _dto.Adresse    = _mapper.Map<AdresseDTOWithId>(adresse);
            return _dto;
        }
    }
}
