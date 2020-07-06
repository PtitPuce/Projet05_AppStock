using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Repositories.CommandeFournisseur
{
    public interface ICommandeFournisseurRepository
    {
        #pragma warning disable 1591
        
        Task<IEnumerable<CommandeFournisseurEntity>> GetAllAsync();
        Task<CommandeFournisseurEntity> GetOneByIdAsync(int id);
        Task<CommandeFournisseurEntity> AddAsync(CommandeFournisseurEntity item);
        Task<CommandeFournisseurEntity> UpdateAsync(CommandeFournisseurEntity item);
        Task<CommandeFournisseurEntity> DeleteAsync(CommandeFournisseurEntity item);
        bool Exist(int id);
        int getTotalPendingArticles(int id_article);



        #pragma warning restore 1591    
    }
}