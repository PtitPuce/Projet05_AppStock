using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Runtime.Serialization;
using AppStock.Models;
using AppStock.Utils;
using AppStock.Infrastructure.Exceptions;
using AppStock.Infrastructure.Repositories.Stock;


namespace AppStock.Infrastructure.Services.Stock
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _repository;
        public StockService(IStockRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(IStockRepository));
        }

        public async Task<StockEntity> Add(StockEntity item)
        {
            return await _repository.AddAsync(item);
        }

        public async Task InitStockForArticle(ArticleEntity article)
        {
            if(!_repository.ExistForArticleId(article.Id))
            {
                var stock = new StockEntity();
                stock.ArticleID = article.Id;
                stock.Quantite = 0;

                await Add(stock);
            }
        }


        public async Task DeleteById(int id)
        {
            var item = await _repository.GetOneByIdAsync(id);

            if(item is null){
                throw new NotFoundException(ExceptionMessageUtil.NOT_FOUND);
            }
            
            await _repository.DeleteAsync(item);
        }

        public async Task<IEnumerable<StockEntity>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<StockEntity> GetOneById(int id)
        {
            return await _repository.GetOneByIdAsync(id);
        }

        public async Task<StockEntity> Update(StockEntity item)
        {
            if(!_repository.ExistForArticleId(item.ArticleID)){
                throw new NotFoundException(ExceptionMessageUtil.NOT_FOUND);
            }
            return await _repository.UpdateAsync(item);
        }

        public bool Exist(int id){
            return _repository.ExistForArticleId(id);
        }

        public bool IsStable(StockEntity item)
        {
            bool val = false;

            if(item.Article is null 
            ){
            }
            else if(  
                !item.Article.IsDeleted
              )
            {
                val = true;
            }
            
            return val;
        }
    }
}