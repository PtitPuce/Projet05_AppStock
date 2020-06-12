using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppStock.Models
{
    [Table("app_commande_client")]
    public class CommandeEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("commande_client_uid")]
        public int Id { get; set; }
        [Column("commande_client_numero")]
        public int Numero { get; set; }   
        [Column("commande_client_commentaire")]
        public string Commentaire { get; set; }
        [Column("commande_client_date_creation")]
        [Required]
        public DateTime DateCreation { get; set; }
        
        [Column("commande_client_contact_uid")]
        public int ContactId { get; set; }
        [Column("commande_client_statut_uid")]
        public int NomCommandeStatutId { get; set; }
        [Column("commande_client_type_uid")]
        public int NomCommandeTypeId { get; set; }
        
        // soft delete
        [Column("is_deleted")]
        public bool IsDeleted { get;set; } = false;  

        public ContactEntity Contact { get;set; }
        public NomCommandeStatutEntity NomCommandeStatut { get;set; }
        public NomCommandeTypeEntity NomCommandeType { get;set; }
        public ICollection<CommandeLigneEntity> CommandeLignes { get; set; }
    }
}