using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Runtime.Serialization;
using AppStock.Models;
using AppStock.Utils;
using AppStock.Infrastructure.Exceptions;
using AppStock.Infrastructure.Repositories.Adresse;

namespace AppStock.Infrastructure.Services.Adresse
{
    public class AdresseService : IAdresseService
    {
        private readonly IAdresseRepository _repository;
        public AdresseService(IAdresseRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(IAdresseRepository));
        }

        public async Task<AdresseEntity> GetOneById(int id)
        {
            return await _repository.GetOneByIdAsync(id);
        }

        public async Task<AdresseEntity> Add(AdresseEntity item)
        {
            return await _repository.AddAsync(item);
        }

        public async Task<AdresseEntity> Update(AdresseEntity item)
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