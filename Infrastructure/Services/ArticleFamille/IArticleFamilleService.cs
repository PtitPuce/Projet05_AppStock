using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Services.ArticleFamille
{
    public interface IArticleFamilleService
    {
        #pragma warning disable 1591
        Task<IEnumerable<ArticleFamilleEntity>> GetAll();
        Task<ArticleFamilleEntity> GetOneById(int id);
        Task<ArticleFamilleEntity> Add(ArticleFamilleEntity item);
        Task<ArticleFamilleEntity> Update(ArticleFamilleEntity item);
        Task DeleteById(int id);
        bool Exist(int id);
        

        #pragma warning restore 1591         
    }
}