using System.Collections.Generic;
using AppStock.Models;
using AppStock.Models.DTO;

namespace AppStock.Models.DTO
{
    public class CommandeDTO
    {
        public int Id { get; set; }
        public string Numero { get; set; }   
        public string Commentaire { get; set; } 

        public int ContactId { get; set; }
        public int NomCommandeStatutId { get; set; }
        public int NomCommandeTypeId { get; set; }

        public ContactEntity Contact {get;set;}
        public NomCommandeStatutEntity NomCommandeStatut {get;set;}
        public NomCommandeTypeEntity NomCommandeType {get;set;}

        public ICollection<CommandeLigneDTOWithId> CommandeLignes {get;set;}

        
        // virtuels
        public double TotalTVA {get;set;}
        public double TotalHT {get;set;}
        public double TotalTTC {get;set;}
    }
}