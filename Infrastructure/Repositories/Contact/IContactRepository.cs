using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Repositories.Contact
{
    public interface IContactRepository
    {
        #pragma warning disable 1591
        
        Task<IEnumerable<ContactEntity>> GetAllAsync();
        Task<ContactEntity> GetOneByIdAsync(int id);
        Task<ContactEntity> GetOneByUserIdAsync(string id);
        Task<ContactEntity> AddAsync(ContactEntity item);
        Task<ContactEntity> UpdateAsync(ContactEntity item);
        Task<ContactEntity> DeleteAsync(ContactEntity item);
        bool Exist(int id);

        #pragma warning restore 1591         
    }
}