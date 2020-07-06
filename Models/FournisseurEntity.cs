using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AppStock.Models
{
    [Table("app_fournisseur")]
    public class FournisseurEntity
    {
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        [Column("fournisseur_uid")]
        public int Id { get; set; }
        [Column("fournisseur_raison")]
        public string Raison { get;set; }
        
        [Column("fournisseur_telephone")]
        public string Telephone { get;set; }
        [Column("fournisseur_email")]
        public string Email { get;set; }

        [Column("fournisseur_adresse_uid")]
        public int? AdresseId { get; set; }

        public AdresseEntity Adresse { get;set; }
    }
}