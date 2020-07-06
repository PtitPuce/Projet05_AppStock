using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Repositories.CommandeFournisseurLigne
{
    public interface ICommandeFournisseurLigneRepository
    {
        #pragma warning disable 1591
        
        Task<IEnumerable<CommandeFournisseurLigneEntity>> GetAllAsync();
        Task<CommandeFournisseurLigneEntity> GetOneByIdAsync(int id);
        Task<CommandeFournisseurLigneEntity> AddAsync(CommandeFournisseurLigneEntity item);
        Task<CommandeFournisseurLigneEntity> UpdateAsync(CommandeFournisseurLigneEntity item);
        Task<CommandeFournisseurLigneEntity> DeleteAsync(CommandeFournisseurLigneEntity item);
        bool Exist(int id);

        #pragma warning restore 1591    
    }
}