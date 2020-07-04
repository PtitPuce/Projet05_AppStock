using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Repositories.Inventaire
{
    public interface IInventaireRepository
    {
        #pragma warning disable 1591
        
        Task<IEnumerable<InventaireEntity>> GetAllAsync();
        Task<InventaireEntity> GetOneByIdAsync(int id);
        Task<InventaireEntity> GetOneByIdArticleFamilleAsync(int id);
        Task<InventaireEntity> AddAsync(InventaireEntity item);
        Task<InventaireEntity> UpdateAsync(InventaireEntity item);
        Task<InventaireEntity> DeleteAsync(InventaireEntity item);
        Task<InventaireEntity> ValidateAsync(InventaireEntity item);
        bool Exist(int id);



        #pragma warning restore 1591    
    }
}