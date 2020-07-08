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
        Task<CommandeFournisseurEntity> Validate(CommandeFournisseurEntity item);
        Task DeleteById(int id);
        bool Exist(int id);

        // ARTICLES
        Task<CommandeFournisseurLigneEntity> AddArticle(CommandeFournisseurEntity commande_fournisseur, ArticleEntity article, int quantite_article);
        Task<CommandeFournisseurEntity> FournisseurChange(CommandeFournisseurEntity commande_fournisseur, int id_fournisseur);
        int getTotalPendingArticles(int id_article);
        int calculateArticleAdvisedQuantity(ArticleEntity article, int projection);

        Task<List<CommandeFournisseurEntity>> getCommandesFournisseurAuto(CommandeEntity commande);

        #pragma warning restore 1591         
    }
}