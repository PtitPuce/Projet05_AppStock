using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Runtime.Serialization;
using AppStock.Models;
using AppStock.Utils;
using AppStock.Infrastructure.Exceptions;
using AppStock.Infrastructure.Repositories.CommandeLigne;

namespace AppStock.Infrastructure.Services.CommandeLigne
{
    public class CommandeLigneService : ICommandeLigneService
    {
        private readonly ICommandeLigneRepository _repository;
        public CommandeLigneService(ICommandeLigneRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(ICommandeLigneRepository));
        }

        public async Task<CommandeLigneEntity> Add(CommandeLigneEntity item)
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

        public async Task<IEnumerable<CommandeLigneEntity>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<CommandeLigneEntity> GetOneById(int id)
        {
            return await _repository.GetOneByIdAsync(id);
        }

        public async Task<CommandeLigneEntity> Update(CommandeLigneEntity item)
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