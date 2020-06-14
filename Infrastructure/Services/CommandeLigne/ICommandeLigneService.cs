using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Services.CommandeLigne
{
    public interface ICommandeLigneService
    {
        #pragma warning disable 1591
        Task<IEnumerable<CommandeLigneEntity>> GetAll();
        Task<CommandeLigneEntity> GetOneById(int id);
        Task<CommandeLigneEntity> AddArticle(CommandeEntity commande, int id_article);
        Task<CommandeLigneEntity> Update(CommandeLigneEntity item);
        Task DeleteById(int id);
        bool Exist(int id);

        object getPriceTotals(CommandeLigneEntity item);

        #pragma warning restore 1591         
    }
}