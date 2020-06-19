using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppStock.Models
{
    [Table("app_commande")]
    public class CommandeEntity : ITimedEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("commande_uid")]
        public int Id { get; set; }
        [Column("commande_numero")]
        public string Numero { get; set; } = "Defaut";
        [Column("commande_commentaire")]
        public string Commentaire { get; set; } = "Pas de commentaire";
        [Column("commande_contact_uid")]
        public int ContactId { get; set; }
        [Column("commande_statut_uid")]
        public int NomCommandeStatutId { get; set; }
        [Column("commande_type_uid")]
        public int NomCommandeTypeId { get; set; }
        [Column("commande_adresse_uid")]
        public int? AdresseId { get; set; }
        
        // soft delete
        [Column("is_deleted")]
        public bool IsDeleted { get;set; } = false;  

        // interface DATE
        [Column("created_at")]
        public DateTime CreatedAt {get; set;}
        [Column("updated_at")]
        public DateTime UpdatedAt {get; set;}

        public ContactEntity Contact { get;set; }
        public NomCommandeStatutEntity NomCommandeStatut { get;set; }
        public NomCommandeTypeEntity NomCommandeType { get;set; }
        public AdresseEntity Adresse { get; set; }
        public ICollection<CommandeLigneEntity> CommandeLignes { get; set; }
    }
}