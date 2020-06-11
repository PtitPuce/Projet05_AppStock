using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AppStock.Models
{
    [Table("app_contact")]
    public class ContactEntity
    {
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        [Column("contact_uid")]
        public int Id { get; set; }
        
        [Column("contact_nom")]
        public string Nom { get;set; }
        [Column("contact_prenom")]
        public string Prenom { get;set; }
        [Column("contact_telephone")]
        public string Telephone { get;set; }

        [Column("contact_adresse_uid")]
        public int? AdresseId { get; set; }
        [Column("contact_user_identity_uid")]
        public string UserId { get; set; }

        public IdentityUser User {get;set;}
        public AdresseEntity Adresse { get;set; }
    }
}