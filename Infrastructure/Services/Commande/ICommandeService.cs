using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Services.Commande
{
    public interface ICommandeService
    {
        #pragma warning disable 1591
        Task<IEnumerable<CommandeEntity>> GetAll();
        Task<CommandeEntity> GetOneById(int id);
        Task<CommandeEntity> Add(CommandeEntity item);
        Task<CommandeEntity> Update(CommandeEntity item);
        Task DeleteById(int id);
        bool Exist(int id);
        

        #pragma warning restore 1591         
    }
}