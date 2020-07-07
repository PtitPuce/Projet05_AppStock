using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Runtime.Serialization;
using AppStock.Models;
using AppStock.Utils;
using AppStock.Infrastructure.Exceptions;
using AppStock.Infrastructure.Repositories.CommandeFournisseur;
using AppStock.Infrastructure.Services.CommandeFournisseurLigne;
using AppStock.Infrastructure.Services.Stock;
using AppStock.Infrastructure.Services.StockProjection;

namespace AppStock.Infrastructure.Services.CommandeFournisseur
{
    public class CommandeFournisseurService : ICommandeFournisseurService
    {
        private readonly ICommandeFournisseurRepository _repository;
        private readonly ICommandeFournisseurLigneService _service_ligne;
        private readonly IStockService _service_stock;
        private readonly Lazy<IStockProjectionService> _service_stock_projection;
        public CommandeFournisseurService(ICommandeFournisseurRepository repository,
                                          ICommandeFournisseurLigneService service_ligne,
                                          IStockService service_stock,
                                          Lazy<IStockProjectionService> service_stock_projection
                                          )
        {
             _service_ligne = service_ligne;
             _service_stock = service_stock;
             _service_stock_projection = service_stock_projection;
            _repository = repository ?? throw new ArgumentNullException(nameof(ICommandeFournisseurRepository));
        }

        public async Task<CommandeFournisseurEntity> Add(CommandeFournisseurEntity item)
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

        public async Task<IEnumerable<CommandeFournisseurEntity>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<CommandeFournisseurEntity> GetOneById(int id)
        {
            return await _repository.GetOneByIdAsync(id);
        }

        public async Task<CommandeFournisseurEntity> Update(CommandeFournisseurEntity item)
        {
            if(!_repository.Exist(item.Id)){
                throw new NotFoundException(ExceptionMessageUtil.NOT_FOUND);
            }
            return await _repository.UpdateAsync(item);
        }

        public bool Exist(int id){
            return _repository.Exist(id);
        }


        // ARTICLES
        public async Task<CommandeFournisseurLigneEntity> AddArticle(CommandeFournisseurEntity commande, int id_article)
        {
            /*
                delegation service CommandeFournisseurLigne
            */
            return await _service_ligne.AddArticle(commande, id_article);

        }

        // Quantite totale d'article en tension (representent une charge pour le stock)
        public int getTotalPendingArticles(int id_article)
        {   
            int total = _repository.getTotalPendingArticles(id_article);
            return total;
        }

        public int calculateArticleAdvisedQuantity(ArticleEntity article, int projection_calculated)
        {
            var i = 0;
            var proj = 0;
            var quantite = 0;
            do
            {
                i++;
                quantite = article.Threshold * i;
                var pp = _service_stock_projection.Value.Projection(article.Id);
                proj =  pp.Result + quantite; // await _service_stock_projection.Projection(  article.Id )
            }
            while( proj < article.Threshold ); 

            return quantite;
        }

    }
}