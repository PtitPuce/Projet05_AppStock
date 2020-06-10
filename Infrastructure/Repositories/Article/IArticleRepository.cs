using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Repositories.Article
{
    public interface IArticleRepository
    {
        #pragma warning disable 1591
        
        Task<IEnumerable<ArticleEntity>> GetAllAsync();
        Task<ArticleEntity> GetOneByIdAsync(int id);
        Task<ArticleEntity> AddAsync(ArticleEntity item);
        Task<ArticleEntity> UpdateAsync(ArticleEntity item);
        Task<ArticleEntity> DeleteByIdAsync(ArticleEntity item);
        bool Exist(int id);

        #pragma warning restore 1591
    }
}