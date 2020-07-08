using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Runtime.Serialization;
using AppStock.Models;
using AppStock.Utils;
using AppStock.Infrastructure.Exceptions;
using AppStock.Infrastructure.Repositories.Stock;

using AppStock.Infrastructure.Services.StockProjection;
using AppStock.Models.DTO;

namespace AppStock.Infrastructure.Services.Stock
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _repository;
        private readonly Lazy<IStockProjectionService> _service_stock_projection;
        public StockService(
                                IStockRepository repository,
                                Lazy<IStockProjectionService> service_stock_projection
                                )
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(IStockRepository));
            _service_stock_projection = service_stock_projection;
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

        public async Task<bool> IsSupposedlyAvailable(int id_article, int quantite)
        {
            bool value = false;
            int proj = await _service_stock_projection.Value.Projection(id_article);
            if(proj >= quantite)
            {
                value = true;
            }
            return value;
        }

        public async Task<bool> IsSupposedlyUnavailableForCommande(CommandeDTO commande)
        {
            foreach (CommandeLigneEntity ligne in commande.CommandeLignes)
            {
                int proj = await _service_stock_projection.Value.Projection(ligne.ArticleId);
                if(ligne.Quantite > proj)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> IsReadyForShipment(CommandeEntity commande)
        {
            foreach (CommandeLigneEntity ligne in commande.CommandeLignes)
            {
                if(ligne.Quantite > ligne.Article.Stock.Quantite)
                {
                    return await Task.FromResult(false);
                }
            }
            return await Task.FromResult(true);
        }

    }
}