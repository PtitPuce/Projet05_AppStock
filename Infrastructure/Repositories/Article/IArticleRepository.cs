using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Repositories.Article
{
    public interface IArticleRepository
    {
        #pragma warning disable 1591
        
        Task<IEnumerable<ArticleEntity>> GetAllAsync();
        Task<IEnumerable<ArticleEntity>> getAllByFournisseurIdAsync(int id);
        Task<ArticleEntity> GetOneByIdAsync(int id);
        Task<ArticleEntity> AddAsync(ArticleEntity item);
        Task<ArticleEntity> UpdateAsync(ArticleEntity item);
        Task<ArticleEntity> DeleteAsync(ArticleEntity item);
        
        bool Exist(int id);
        IQueryable<ArticleEntity> QueryForStock();

        #pragma warning restore 1591
    }
}