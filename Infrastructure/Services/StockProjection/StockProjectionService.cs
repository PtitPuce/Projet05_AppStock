using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Runtime.Serialization;
using AppStock.Models;
using AppStock.Utils;
using AppStock.Infrastructure.Exceptions;

using AppStock.Infrastructure.Services.Commande;
using AppStock.Infrastructure.Services.CommandeFournisseur;
using AppStock.Infrastructure.Services.Stock;

namespace AppStock.Infrastructure.Services.StockProjection
{
    public class StockProjectionService : IStockProjectionService
    {
        private readonly IStockService _service_stock;
        private readonly ICommandeService _service_comm_client;
        private readonly ICommandeFournisseurService _service_comm_fournisseur;
        public StockProjectionService(IStockService service_stock,
                             ICommandeService service_comm_client,
                             ICommandeFournisseurService service_comm_fournisseur)
        {
            _service_stock = service_stock;
            _service_comm_client = service_comm_client;
            _service_comm_fournisseur = service_comm_fournisseur;
        }

        
        public async Task<int> Projection(int id_article)
        {
            int _projection = 0;

            StockEntity stock = await _service_stock.GetOneById(id_article);
            int q_stock = stock.Quantite;

            int q_comm_fournisseur = _service_comm_fournisseur.getTotalPendingArticles(id_article);
            int q_comm_clients = _service_comm_client.getTotalPendingArticles(id_article);

            _projection = q_stock + q_comm_fournisseur - q_comm_clients;
            return _projection;
        }
    }
}