using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Runtime.Serialization;
using AppStock.Models;
using AppStock.Utils;
using AppStock.Infrastructure.Exceptions;
using AppStock.Infrastructure.Repositories.Stock;

using AppStock.Infrastructure.Services.Commande;
using AppStock.Infrastructure.Services.CommandeFournisseur;


namespace AppStock.Infrastructure.Services.Stock
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _repository;
        private readonly ICommandeService _service_comm_client;
        private readonly ICommandeFournisseurService _service_comm_fournisseur;
        public StockService(IStockRepository repository,
                             ICommandeService service_comm_client,
                             ICommandeFournisseurService service_comm_fournisseur)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(IStockRepository));
            _service_comm_client = service_comm_client;
            _service_comm_fournisseur = service_comm_fournisseur;
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

        public async Task<int> Projection(int id_article)
        {
            int _projection = 0;

            StockEntity stock = await GetOneById(id_article);
            int q_stock = stock.Quantite;

            int q_comm_fournisseur = _service_comm_fournisseur.getTotalPendingArticles(id_article);
            int q_comm_clients = _service_comm_client.getTotalPendingArticles(id_article);

            _projection = q_stock + q_comm_fournisseur - q_comm_clients;
            return _projection;
        }
    }
}