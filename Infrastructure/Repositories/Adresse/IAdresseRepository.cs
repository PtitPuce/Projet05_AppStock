using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Repositories.Adresse
{
    public interface IAdresseRepository
    {
        #pragma warning disable 1591
        
        Task<IEnumerable<AdresseEntity>> GetAllAsync();
        Task<AdresseEntity> GetOneByIdAsync(int id);
        Task<AdresseEntity> AddAsync(AdresseEntity item);
        Task<AdresseEntity> UpdateAsync(AdresseEntity item);
        Task<AdresseEntity> DeleteAsync(AdresseEntity item);
        bool Exist(int id);

        #pragma warning restore 1591              
    }
}