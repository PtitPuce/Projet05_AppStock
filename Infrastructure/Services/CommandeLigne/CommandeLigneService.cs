using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Runtime.Serialization;
using AppStock.Models;
using AppStock.Utils;
using AppStock.Infrastructure.Exceptions;
using AppStock.Infrastructure.Repositories.CommandeLigne;
using AppStock.Infrastructure.Services.Article;

namespace AppStock.Infrastructure.Services.CommandeLigne
{
    public class CommandeLigneService : ICommandeLigneService
    {
        private readonly ICommandeLigneRepository _repository;
        private readonly IArticleService _service_article;
        public CommandeLigneService(ICommandeLigneRepository repository, IArticleService service_article)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(ICommandeLigneRepository));
            _service_article = service_article ?? throw new ArgumentNullException(nameof(IArticleService));
        }

        public async Task<CommandeLigneEntity> AddArticle(CommandeEntity commande, int id_article)
        {
            CommandeLigneEntity ligne = new CommandeLigneEntity();
            ligne.Commande = commande;
            ligne.Article = await _service_article.GetOneById(id_article);

            return await _repository.AddAsync(ligne);
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


        public object getPriceTotals(CommandeLigneEntity item)
        {
            var _ht = item.Quantite * item.Article.PrixUnitaire;
            var _tva = _ht * (item.Article.NomTypeTVA.Taux /100.0M);
            var _ttc = _ht + _tva;

            var data = new
            {
                HT = _ht,
                TVA = _tva,
                TTC = _ttc
            };

            return data;
        }
    }
}