using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Runtime.Serialization;
using AppStock.Models;
using AppStock.Utils;
using AppStock.Infrastructure.Exceptions;
using AppStock.Infrastructure.Repositories.Contact;

namespace AppStock.Infrastructure.Services.Contact
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;
        public ContactService(IContactRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(IContactRepository));
        }

        public async Task<ContactEntity> Add(ContactEntity item)
        {
            return await _repository.AddAsync(item);
        }

        public async Task<IEnumerable<ContactEntity>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ContactEntity> GetOneById(int id)
        {
            return await _repository.GetOneByIdAsync(id);
        }

        public async Task<ContactEntity> GetOneByUserId(string id)
        {
            return await _repository.GetOneByUserIdAsync(id);
        }

        public async Task<ContactEntity> Update(ContactEntity item)
        {
            if(!_repository.Exist(item.Id)){
                throw new NotFoundException(ExceptionMessageUtil.NOT_FOUND);
            }
            return await _repository.UpdateAsync(item);
        }

        public async Task DeleteById(int id)
        {
            var item = await _repository.GetOneByIdAsync(id);

            if(item is null){
                throw new NotFoundException(ExceptionMessageUtil.NOT_FOUND);
            }
            
            await _repository.DeleteAsync(item);
        }

        public bool Exist(int id){
            return _repository.Exist(id);
        }
    }
}