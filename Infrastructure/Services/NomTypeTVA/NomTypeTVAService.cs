using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Runtime.Serialization;
using AppStock.Models;
using AppStock.Utils;
using AppStock.Infrastructure.Exceptions;
using AppStock.Infrastructure.Repositories.NomTypeTVA;

namespace AppStock.Infrastructure.Services.NomTypeTVA
{
    public class NomTypeTVAService : INomTypeTVAService
    {
        private readonly INomTypeTVARepository _repository;
        public NomTypeTVAService(INomTypeTVARepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(INomTypeTVARepository));
        }

        public async Task<NomTypeTVAEntity> Add(NomTypeTVAEntity item)
        {
            return await _repository.AddAsync(item);
        }

        public async Task DeleteById(int id)
        {
            var item = await _repository.GetOneByIdAsync(id);

            if(item is null){
                throw new NotFoundException(ExceptionMessageUtil.NOT_FOUND);
            }
            
            await _repository.DeleteAsync(item);
        }

        public async Task<IEnumerable<NomTypeTVAEntity>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<NomTypeTVAEntity> GetOneById(int id)
        {
            return await _repository.GetOneByIdAsync(id);
        }

        public async Task<NomTypeTVAEntity> Update(NomTypeTVAEntity item)
        {
            if(!_repository.Exist(item.Id)){
                throw new NotFoundException(ExceptionMessageUtil.NOT_FOUND);
            }
            return await _repository.UpdateAsync(item);
        }

        public bool Exist(int id){
            return _repository.Exist(id);
        }
    }
}