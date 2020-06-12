using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Repositories.CommandeLigne
{
    public interface ICommandeLigneRepository
    {
        #pragma warning disable 1591
        
        Task<IEnumerable<CommandeLigneEntity>> GetAllAsync();
        Task<CommandeLigneEntity> GetOneByIdAsync(int id);
        Task<CommandeLigneEntity> AddAsync(CommandeLigneEntity item);
        Task<CommandeLigneEntity> UpdateAsync(CommandeLigneEntity item);
        Task<CommandeLigneEntity> DeleteAsync(CommandeLigneEntity item);
        bool Exist(int id);

        #pragma warning restore 1591    
    }
}