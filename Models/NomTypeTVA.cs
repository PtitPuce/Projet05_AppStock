using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppStock.Models
{
    [Table("app_nom_type_tva")]
    public class NomTypeTVA
    {
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        [Column("tva_uid")]
        public int Id { get; set; }
        [Column("tva_code")]
        [Required]
        public string Code { get; set; }
        [Column("tva_libelle")]
        [Required]
        public string Libelle { get; set; }
        [Column("tva_taux")]
        public decimal Taux { get; set; }
    }
}