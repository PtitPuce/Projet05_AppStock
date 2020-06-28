using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Services.Inventaire
{
    public interface IInventaireService
    {
        #pragma warning disable 1591
        Task<IEnumerable<InventaireEntity>> GetAll();
        Task<InventaireEntity> GetOneById(int id);
        Task<InventaireEntity> Add(InventaireEntity item);
        Task<InventaireEntity> Update(InventaireEntity item);
        Task<InventaireEntity> Validate(InventaireEntity item);
        Task DeleteById(int id);
        bool Exist(int id);
        
        #pragma warning restore 1591         
    }
}