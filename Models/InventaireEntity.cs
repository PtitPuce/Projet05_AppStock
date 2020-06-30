using System;
using System.Collections.Generic;
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
        public string UserId { get; set; }
        [Column("inventaire_statut_uid")]
        public int NomInventaireStatutId { get; set; }
        [Column("article_famille_uid")]
        public int ArticleFamilleId { get; set; }
        [Column("inventaire_date_cloture")]
        public DateTime DateCloture { get; set; }
    
        // soft delete
        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;  

        // interface DATE
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public IdentityUser User { get; set; }
        public NomInventaireStatutEntity NomInventaireStatut { get;set; }
        public ArticleFamilleEntity ArticleFamille { get; set; }
        public ICollection<InventaireLigneEntity> InventaireLignes { get; set; }


    }
}