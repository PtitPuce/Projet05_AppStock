using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Runtime.Serialization;
using AppStock.Models;
using AppStock.Utils;
using AppStock.Infrastructure.Exceptions;
using AppStock.Infrastructure.Repositories.InventaireLigne;
using AppStock.Infrastructure.Services.Article;

namespace AppStock.Infrastructure.Services.InventaireLigne
{
    public class InventaireLigneService : IInventaireLigneService
    {
        private readonly IInventaireLigneRepository _repository;
        private readonly IArticleService _service_article;
        public InventaireLigneService(IInventaireLigneRepository repository, IArticleService service_article)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(IInventaireLigneRepository));
            _service_article = service_article ?? throw new ArgumentNullException(nameof(IArticleService));
        }

        public async Task<InventaireLigneEntity> AddArticle(InventaireEntity Inventaire, int id_article)
        {
            InventaireLigneEntity ligne = new InventaireLigneEntity();
            ligne.Inventaire = Inventaire;
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

        public async Task<IEnumerable<InventaireLigneEntity>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<InventaireLigneEntity> GetOneById(int id)
        {
            return await _repository.GetOneByIdAsync(id);
        }

        public async Task<InventaireLigneEntity> Update(InventaireLigneEntity item)
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