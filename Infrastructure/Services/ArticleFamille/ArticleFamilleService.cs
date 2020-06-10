using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Runtime.Serialization;
using AppStock.Models;
using AppStock.Utils;
using AppStock.Infrastructure.Exceptions;
using AppStock.Infrastructure.Repositories.ArticleFamille;

namespace AppStock.Infrastructure.Services.ArticleFamille
{
    public class ArticleFamilleService : IArticleFamilleService
    {
        private readonly IArticleFamilleRepository _repository;
        public ArticleFamilleService(IArticleFamilleRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(IArticleFamilleRepository));
        }

        public async Task<ArticleFamilleEntity> Add(ArticleFamilleEntity item)
        {
            return await _repository.AddAsync(item);
        }

        public async Task DeleteById(int id)
        {
            var item = await _repository.GetOneByIdAsync(id);

            if(item is null){
                throw new NotFoundException(ExceptionMessageUtil.NOT_FOUND);
            }
            
            await _repository.DeleteByIdAsync(item);
        }

        public async Task<IEnumerable<ArticleFamilleEntity>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ArticleFamilleEntity> GetOneById(int id)
        {
            return await _repository.GetOneByIdAsync(id);
        }

        public async Task<ArticleFamilleEntity> Update(ArticleFamilleEntity item)
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