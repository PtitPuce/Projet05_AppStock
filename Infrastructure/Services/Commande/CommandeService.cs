using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Runtime.Serialization;
using AppStock.Models;
using AppStock.Utils;
using AppStock.Infrastructure.Exceptions;
using AppStock.Infrastructure.Repositories.Commande;
using AppStock.Infrastructure.Services.CommandeLigne;
using AppStock.Infrastructure.Services.Stock;

namespace AppStock.Infrastructure.Services.Commande
{
    public class CommandeService : ICommandeService
    {
        private readonly ICommandeRepository _repository;
        private readonly ICommandeLigneService _service_ligne;
        private readonly IStockService _service_stock;
        public CommandeService(ICommandeRepository repository,  ICommandeLigneService service_ligne, IStockService service_stock)
        {
             _service_ligne = service_ligne;
             _service_stock = service_stock;
            _repository = repository ?? throw new ArgumentNullException(nameof(ICommandeRepository));
        }

        public async Task<CommandeEntity> Add(CommandeEntity item)
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

        public async Task<IEnumerable<CommandeEntity>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<CommandeEntity> GetOneById(int id)
        {
            return await _repository.GetOneByIdAsync(id);
        }

        public async Task<CommandeEntity> GetPanierForContactId(int id)
        {
            // on regarde si un enregistrement existe 
            var _panier = await _repository.GetPanierByContactId(id);
            return _panier;
        }


        public async Task<CommandeEntity> Update(CommandeEntity item)
        {
            if(!_repository.Exist(item.Id)){
                throw new NotFoundException(ExceptionMessageUtil.NOT_FOUND);
            }
            return await _repository.UpdateAsync(item);
        }

        public async Task<CommandeEntity> Validate(CommandeEntity item)
        {
            if(!_repository.Exist(item.Id)){
                throw new NotFoundException(ExceptionMessageUtil.NOT_FOUND);
            }
            
            // Mise à jour de la quantité en stock pour chaque article
            foreach (CommandeLigneEntity l in item.CommandeLignes)
            {
                StockEntity s = await _service_stock.GetOneById(l.ArticleId);
                s.Quantite = s.Quantite - l.Quantite;
                await _service_stock.Update(s);
            }

            return await _repository.ValidateAsync(item);
        }

        public bool Exist(int id){
            return _repository.Exist(id);
        }


        // ARTICLES
        public async Task<CommandeLigneEntity> AddArticle(CommandeEntity commande, int id_article)
        {
            /*
                delegation service CommandeLigne
            */
            return await _service_ligne.AddArticle(commande, id_article);

        }

        // Quantite totale d'article en tension (representent une charge pour le stock)
        public int getTotalPendingArticles(int id_article)
        {   
            int total = _repository.getTotalPendingArticles(id_article);
            return total;
        }
    }
}