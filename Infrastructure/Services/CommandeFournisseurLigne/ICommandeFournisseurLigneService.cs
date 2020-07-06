using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Services.CommandeFournisseurLigne
{
    public interface ICommandeFournisseurLigneService
    {
        #pragma warning disable 1591
        Task<IEnumerable<CommandeFournisseurLigneEntity>> GetAll();
        Task<CommandeFournisseurLigneEntity> GetOneById(int id);
        Task<CommandeFournisseurLigneEntity> AddArticle(CommandeFournisseurEntity commande, int id_article);
        Task<CommandeFournisseurLigneEntity> Update(CommandeFournisseurLigneEntity item);
        Task DeleteById(int id);
        bool Exist(int id);

        #pragma warning restore 1591         
    }
}