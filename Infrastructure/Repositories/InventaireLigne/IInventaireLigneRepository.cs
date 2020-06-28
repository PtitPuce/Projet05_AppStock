using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Repositories.InventaireLigne
{
    public interface IInventaireLigneRepository
    {
        #pragma warning disable 1591
        
        Task<IEnumerable<InventaireLigneEntity>> GetAllAsync();
        Task<InventaireLigneEntity> GetOneByIdAsync(int id);
        Task<InventaireLigneEntity> AddAsync(InventaireLigneEntity item);
        Task<InventaireLigneEntity> UpdateAsync(InventaireLigneEntity item);
        Task<InventaireLigneEntity> DeleteAsync(InventaireLigneEntity item);
        bool Exist(int id);

        #pragma warning restore 1591    
    }
}