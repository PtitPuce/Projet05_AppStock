using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Services.StockProjection
{
    public interface IStockProjectionService
    {
        #pragma warning disable 1591
        Task<int> Projection(int id_article);
        #pragma warning restore 1591
    }
}