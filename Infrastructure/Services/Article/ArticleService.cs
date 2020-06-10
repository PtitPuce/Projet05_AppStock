using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Runtime.Serialization;
using AppStock.Models;
using AppStock.Utils;
using AppStock.Infrastructure.Exceptions;
using AppStock.Infrastructure.Repositories.Article;


namespace AppStock.Infrastructure.Services.Article
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _repository;
        public ArticleService(IArticleRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(IArticleRepository));
        }

        public async Task<ArticleEntity> Add(ArticleEntity item)
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

        public async Task<IEnumerable<ArticleEntity>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ArticleEntity> GetOneById(int id)
        {
            return await _repository.GetOneByIdAsync(id);
        }

        public async Task<ArticleEntity> Update(ArticleEntity item)
        {
            if(!_repository.Exist(item.Id)){
                throw new NotFoundException(ExceptionMessageUtil.NOT_FOUND);
            }
            return await _repository.UpdateAsync(item);
        }

        public bool Exist(int id){
            return _repository.Exist(id);
        }

        public bool IsStable(ArticleEntity item)
        {
            bool val = false;

            if(item.NomTypeTVA is null 
                || item.ArticleFamille is null
            ){
            }
            else if(  
                !item.NomTypeTVA.IsDeleted
                && !item.ArticleFamille.IsDeleted
              )
            {
                val = true;
            }
            
            return val;
        }
    }
}