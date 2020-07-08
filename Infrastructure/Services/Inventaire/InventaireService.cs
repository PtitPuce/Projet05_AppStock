using System.Runtime.CompilerServices;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Runtime.Serialization;
using AppStock.Models;
using AppStock.Utils;
using AppStock.Infrastructure.Exceptions;
using AppStock.Infrastructure.Repositories.Inventaire;
using AppStock.Infrastructure.Services.InventaireLigne;
using AppStock.Infrastructure.Services.Stock;

namespace AppStock.Infrastructure.Services.Inventaire
{
    public class InventaireService : IInventaireService
    {
        private readonly IInventaireRepository _repository;
        private readonly IInventaireLigneService _service_ligne;
        private readonly IStockService _service_stock;
        public InventaireService(IInventaireRepository repository,  IInventaireLigneService service_ligne, IStockService service_stock)
        {
             _service_ligne = service_ligne;
             _service_stock = service_stock;
            _repository = repository ?? throw new ArgumentNullException(nameof(IInventaireRepository));
        }

        public async Task<InventaireEntity> Add(InventaireEntity item)
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

        public async Task<IEnumerable<InventaireEntity>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<InventaireEntity> GetOneByIdWithLignes(int id)
        {
            InventaireEntity inventaire = await _repository.GetOneByIdAsync(id);
            // 1. On vérifie le flag de l'inventaire
            // 2. Si le flag est "En cours" et sans ligne alors on ajoute les articles de la famille d'article et leur stock en base dans la ligne de l'inventaire
            if ((inventaire.NomInventaireStatut.Code == "E") && (inventaire.InventaireLignes.Count == 0)) // hardCode "En cours"
            {
                foreach (ArticleEntity a in inventaire.ArticleFamille.Articles)
                {
                    await _service_ligne.AddArticle(inventaire, a);
                }
                // Récupération de l'inventaire avec les lignes
                inventaire = await _repository.GetOneByIdAsync(id);
            }   
            return inventaire;
        }

        public async Task<InventaireEntity> GetOneById(int id)
        {
            return await _repository.GetOneByIdAsync(id);
        }

        public async Task<InventaireEntity> GetOneByIdByArticleFamilleId(int id)
        {
            return await _repository.GetOneByIdByArticleFamilleIdAsync(id);
        }

        public async Task<InventaireEntity> Update(InventaireEntity item)
        {
            if(!_repository.Exist(item.Id)){
                throw new NotFoundException(ExceptionMessageUtil.NOT_FOUND);
            }
            return await _repository.UpdateAsync(item);
        }

        public async Task<InventaireEntity> Validate(InventaireEntity item)
        {
            if(!_repository.Exist(item.Id)){
                throw new NotFoundException(ExceptionMessageUtil.NOT_FOUND);
            }
            
            // Mise à jour de la quantité en stock pour chaque article
            foreach (InventaireLigneEntity l in item.InventaireLignes)
            {
                StockEntity s = await _service_stock.GetOneById(l.ArticleId);
                s.Quantite = l.QuantiteComptee;
                await _service_stock.Update(s);
            }

            return await _repository.ValidateAsync(item);
        }

        public bool isEditable(InventaireEntity item)
        {
            if (item.NomInventaireStatut.Code == "E")
            {
                return true; 
            }
            else
            {
                return false;
            }
            
        }

        public bool Exist(int id){
            return _repository.Exist(id);
        }
    }
}