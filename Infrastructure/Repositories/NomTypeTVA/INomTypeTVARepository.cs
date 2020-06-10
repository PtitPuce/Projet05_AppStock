using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Repositories.NomTypeTVA
{
    public interface INomTypeTVARepository
    {
        #pragma warning disable 1591
        
        Task<IEnumerable<NomTypeTVAEntity>> GetAllAsync();
        Task<NomTypeTVAEntity> GetOneByIdAsync(int id);
        Task<NomTypeTVAEntity> AddAsync(NomTypeTVAEntity item);
        Task<NomTypeTVAEntity> UpdateAsync(NomTypeTVAEntity item);
        Task<NomTypeTVAEntity> DeleteAsync(NomTypeTVAEntity item);
        bool Exist(int id);

        #pragma warning restore 1591              
    }
}