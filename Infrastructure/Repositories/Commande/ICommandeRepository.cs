using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Repositories.Commande
{
    public interface ICommandeRepository
    {
        #pragma warning disable 1591
        
        Task<IEnumerable<CommandeEntity>> GetAllAsync();
        Task<CommandeEntity> GetOneByIdAsync(int id);
        Task<CommandeEntity> GetPanierByContactId(int id);

        Task<CommandeEntity> AddAsync(CommandeEntity item);
        Task<CommandeEntity> UpdateAsync(CommandeEntity item);
        Task<CommandeEntity> DeleteAsync(CommandeEntity item);
        Task<CommandeEntity> ValidateAsync(CommandeEntity item);
        bool Exist(int id);
        int getTotalPendingArticles(int id_article);


        #pragma warning restore 1591    
    }
}