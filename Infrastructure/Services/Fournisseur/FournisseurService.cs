using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Runtime.Serialization;
using AppStock.Models;
using AppStock.Utils;
using AppStock.Infrastructure.Exceptions;
using AppStock.Infrastructure.Repositories.Fournisseur;

/* repo */

namespace AppStock.Infrastructure.Services.Fournisseur
{
    public class FournisseurService : IFournisseurService
    {
        
        private readonly IFournisseurRepository _repository;
        public FournisseurService(IFournisseurRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(IFournisseurRepository));
        }

        public async Task<FournisseurEntity> GetOneById(int id)
        {
            if(id == 0)
            {
                throw new NullReferenceException(nameof(id));
            }

            return await _repository.GetOneByIdAsync(id);
        }
    }
}