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

        public async Task<InventaireEntity> GetOneById(int id)
        {
            return await _repository.GetOneByIdAsync(id);
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
                //s.Quantite = s.Quantite - l.Quantite;
                await _service_stock.Update(s);
            }

            return await _repository.ValidateAsync(item);
        }

        public bool Exist(int id){
            return _repository.Exist(id);
        }
    }
}