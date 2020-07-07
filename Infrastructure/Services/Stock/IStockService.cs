using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Services.Stock
{
    public interface IStockService
    {
        #pragma warning disable 1591
        Task<IEnumerable<StockEntity>> GetAll();
        Task<StockEntity> GetOneById(int id);
        Task<StockEntity> Add(StockEntity item);
        Task<StockEntity> Update(StockEntity item);
        Task DeleteById(int id);
        Task InitStockForArticle(ArticleEntity article);
        bool Exist(int id);
        bool IsStable(StockEntity item);
        Task<bool> IsSupposedlyAvailable(int id_article, int quantite);

        #pragma warning restore 1591
    }
}