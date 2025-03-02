using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppStock.Models
{
    [Table("app_commande_fournisseur")]
    public class CommandeFournisseurEntity : ITimedEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("commande_fournisseur_uid")]
        public int Id { get; set; }
        [Column("commande_fournisseur_numero")]
        public string Numero { get; set; } = "Defaut";
        [Column("commande_fournisseur_commentaire")]
        public string Commentaire { get; set; } = "Pas de commentaire";
        [Column("commande_fournisseur_fournisseur_uid")]
        [DisplayName("Fournisseur")]
        public int FournisseurId { get; set; }
        [Column("commande_fournisseur_statut_uid")]
        public int NomCommandeFournisseurStatutId { get; set; }
        [Column("commande_fournisseur_is_auto")]
        public bool isAuto { get; set; } = false;
        
        // soft delete
        [Column("is_deleted")]
        public bool IsDeleted { get;set; } = false;  

        // interface DATE
        [Column("created_at")]
        public DateTime CreatedAt {get; set;}
        [Column("updated_at")]
        public DateTime UpdatedAt {get; set;}

        public FournisseurEntity Fournisseur { get; set; }
        public NomCommandeFournisseurStatutEntity NomCommandeFournisseurStatut { get;set; }
        public ICollection<CommandeFournisseurLigneEntity> CommandeFournisseurLignes { get; set; }
    }
}