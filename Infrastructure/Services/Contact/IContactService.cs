using System.Collections.Generic;
using System.Threading.Tasks;
using AppStock.Models;

namespace AppStock.Infrastructure.Services.Contact
{
    public interface IContactService
    {
        #pragma warning disable 1591
        Task<IEnumerable<ContactEntity>> GetAll();
        Task<ContactEntity> GetOneById(int id);
        Task<ContactEntity> Add(ContactEntity item);
        Task<ContactEntity> Update(ContactEntity item);
        Task DeleteById(int id);
        bool Exist(int id);
        

        #pragma warning restore 1591         
    }
}