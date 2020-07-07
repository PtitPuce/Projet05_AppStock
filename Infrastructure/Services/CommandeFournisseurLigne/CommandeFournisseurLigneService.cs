using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Runtime.Serialization;
using AppStock.Models;
using AppStock.Utils;
using AppStock.Infrastructure.Exceptions;
using AppStock.Infrastructure.Repositories.CommandeFournisseurLigne;
using AppStock.Infrastructure.Services.Article;

namespace AppStock.Infrastructure.Services.CommandeFournisseurLigne
{
    public class CommandeFournisseurLigneService : ICommandeFournisseurLigneService
    {
        private readonly ICommandeFournisseurLigneRepository _repository;
        private readonly IArticleService _service_article;
        public CommandeFournisseurLigneService(ICommandeFournisseurLigneRepository repository, IArticleService service_article)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(ICommandeFournisseurLigneRepository));
            _service_article = service_article ?? throw new ArgumentNullException(nameof(IArticleService));
        }

        public async Task<CommandeFournisseurLigneEntity> AddArticle(CommandeFournisseurEntity commande_fournisseur, ArticleEntity article, int article_quantite)
        {
            foreach (CommandeFournisseurLigneEntity l in commande_fournisseur.CommandeFournisseurLignes)
            {
                if (l.ArticleId == article.Id)
                {
                    l.Quantite = l.Quantite + article_quantite;
                    return await Update(l);
                }
            }
            
            CommandeFournisseurLigneEntity ligne = new CommandeFournisseurLigneEntity();
            ligne.CommandeFournisseurId = commande_fournisseur.Id;
            ligne.ArticleId = article.Id;
            ligne.Quantite = article_quantite;

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

        public async Task<IEnumerable<CommandeFournisseurLigneEntity>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<CommandeFournisseurLigneEntity> GetOneById(int id)
        {
            return await _repository.GetOneByIdAsync(id);
        }

        public async Task<CommandeFournisseurLigneEntity> Update(CommandeFournisseurLigneEntity item)
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