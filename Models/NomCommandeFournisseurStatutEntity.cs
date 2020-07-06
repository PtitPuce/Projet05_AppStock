using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppStock.Models
{
    [Table("app_nom_commande_fournisseur_statut")]
    public class NomCommandeFournisseurStatutEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("commande_fournisseur_statut_uid")]
        public int Id { get; set; }
        [Column("commande_fournisseur_statut_code")]
        [Required]
        public string Code { get; set; }
        [Column("commande_fournisseur_statut_libelle")]
        [Required]
        public string Libelle { get; set; }
    }
}