using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Services.NomTypeTVA
{
    public interface INomTypeTVAService
    {
        #pragma warning disable 1591
        Task<IEnumerable<NomTypeTVAEntity>> GetAll();
        Task<NomTypeTVAEntity> GetOneById(int id);
        Task<NomTypeTVAEntity> Add(NomTypeTVAEntity item);
        Task<NomTypeTVAEntity> Update(NomTypeTVAEntity item);
        Task DeleteById(int id);
        bool Exist(int id);
        

        #pragma warning restore 1591         
    }
}