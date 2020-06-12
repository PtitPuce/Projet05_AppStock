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
        bool Exist(int id);



        #pragma warning restore 1591    
    }
}