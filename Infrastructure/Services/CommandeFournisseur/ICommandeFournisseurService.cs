using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Services.CommandeFournisseur
{
    public interface ICommandeFournisseurService
    {
        #pragma warning disable 1591
        Task<IEnumerable<CommandeFournisseurEntity>> GetAll();
        Task<CommandeFournisseurEntity> GetOneById(int id);
        Task<CommandeFournisseurEntity> Add(CommandeFournisseurEntity item);
        Task<CommandeFournisseurEntity> Update(CommandeFournisseurEntity item);
        Task DeleteById(int id);
        bool Exist(int id);

        // ARTICLES
        Task<CommandeFournisseurLigneEntity> AddArticle(CommandeFournisseurEntity commandeFournisseur, int id_article);
        int getTotalPendingArticles(int id_article);
        int calculateArticleAdvisedQuantity(ArticleEntity article, int projection_calculated);

        #pragma warning restore 1591         
    }
}