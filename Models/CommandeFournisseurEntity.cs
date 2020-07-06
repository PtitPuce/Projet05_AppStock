using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppStock.Models
{
    public class CommandeFournisseurEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("commande_fournisseur_uid")]
        public int Id { get; set; }
        [Column("commande_fournisseur_numero")]
        public string Numero { get; set; } = "Defaut";
        [Column("commande_fournisseur_commentaire")]
        public string Commentaire { get; set; } = "Pas de commentaire";
        [Column("commande_fournisseur_contact_uid")]
        public int ContactId { get; set; }
        [Column("commande_fournisseur_fournisseur_uid")]
        public int FournisseurId { get; set; }
        [Column("commande_fournisseur_statut_uid")]
        public int NomCommandeFournisseurStatutId { get; set; }
        
        // soft delete
        [Column("is_deleted")]
        public bool IsDeleted { get;set; } = false;  

        // interface DATE
        [Column("created_at")]
        public DateTime CreatedAt {get; set;}
        [Column("updated_at")]
        public DateTime UpdatedAt {get; set;}

        public ContactEntity Contact { get;set; }
        public FournisseurEntity Fournisseur { get; set; }
        public NomCommandeFournisseurStatutEntity NomCommandeFournisseurStatut { get;set; }
        public ICollection<CommandeFournisseurLigneEntity> CommandeFournisseurLignes { get; set; }
    }
}