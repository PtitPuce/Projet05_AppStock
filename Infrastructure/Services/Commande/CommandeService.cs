using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Runtime.Serialization;
using AppStock.Models;
using AppStock.Utils;
using AppStock.Infrastructure.Exceptions;
using AppStock.Infrastructure.Repositories.Commande;
using AppStock.Infrastructure.Services.CommandeLigne;

namespace AppStock.Infrastructure.Services.Commande
{
    public class CommandeService : ICommandeService
    {
        private readonly ICommandeRepository _repository;
        private readonly ICommandeLigneService _service_ligne;
        public CommandeService(ICommandeRepository repository,  ICommandeLigneService service_ligne)
        {
             _service_ligne = service_ligne;
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
    }
}