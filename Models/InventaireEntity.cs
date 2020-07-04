using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AppStock.Models
{
    [Table("app_inventaire")]
    public class InventaireEntity : ITimedEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("inventaire_uid")]
        public int Id { get; set; }
        [Column("inventaire_user_uid")]
        [DisplayName("ID de l'utilisateur")]
        public string UserId { get; set; }
        [Column("inventaire_statut_uid")]
        public int NomInventaireStatutId { get; set; }
        [Column("article_famille_uid")]
        public int ArticleFamilleId { get; set; }
        [Column("inventaire_date_cloture")]
        [DisplayName("Date de clôture")]
        public DateTime? DateCloture { get; set; }
    
        // soft delete
        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;  

        // interface DATE
        [Column("created_at")]
        [DisplayName("Date de création")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        [DisplayName("Date de la dernière modification")]
        public DateTime UpdatedAt { get; set; }

        public IdentityUser User { get; set; }
        [DisplayName("Statut")]
        public NomInventaireStatutEntity NomInventaireStatut { get;set; }
        [DisplayName("Famille d'article")]
        public ArticleFamilleEntity ArticleFamille { get; set; }
        public ICollection<InventaireLigneEntity> InventaireLignes { get; set; }


    }
}