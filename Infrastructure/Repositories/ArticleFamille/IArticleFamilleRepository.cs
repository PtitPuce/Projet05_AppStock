using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Repositories.ArticleFamille
{
    public interface IArticleFamilleRepository
    {
        #pragma warning disable 1591
        
        Task<IEnumerable<ArticleFamilleEntity>> GetAllAsync();
        Task<ArticleFamilleEntity> GetOneByIdAsync(int id);
        Task<ArticleFamilleEntity> AddAsync(ArticleFamilleEntity item);
        Task<ArticleFamilleEntity> UpdateAsync(ArticleFamilleEntity item);
        Task<ArticleFamilleEntity> DeleteByIdAsync(ArticleFamilleEntity item);
        bool Exist(int id);

        #pragma warning restore 1591         
    }
}