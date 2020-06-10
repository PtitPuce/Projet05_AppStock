using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Services.Article
{
    public interface IArticleService
    {
        #pragma warning disable 1591
        Task<IEnumerable<ArticleEntity>> GetAll();
        Task<ArticleEntity> GetOneById(int id);
        Task<ArticleEntity> Add(ArticleEntity item);
        Task<ArticleEntity> Update(ArticleEntity item);
        Task DeleteById(int id);
        
        bool Exist(int id);
        bool IsStable(ArticleEntity item);
        IQueryable<ArticleEntity> QueryForStock();

        #pragma warning restore 1591
    }
}