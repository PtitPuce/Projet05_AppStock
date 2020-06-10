using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Repositories.Stock
{
    public interface IStockRepository
    {
        #pragma warning disable 1591
        
        Task<IEnumerable<StockEntity>> GetAllAsync();
        Task<StockEntity> GetOneByIdAsync(int id);
        Task<StockEntity> AddAsync(StockEntity item);
        Task<StockEntity> UpdateAsync(StockEntity item);
        Task<StockEntity> DeleteAsync(StockEntity item);
        bool Exist(int id);

        #pragma warning restore 1591
    }
}