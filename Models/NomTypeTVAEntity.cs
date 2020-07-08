using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppStock.Models
{
    [Table("app_nom_type_tva")]
    public class NomTypeTVAEntity
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
        //[DisplayFormat(DataFormatString = "{0:#.##}", ApplyFormatInEditMode = true)]
        public decimal Taux { get; set; }

        // soft delete
        [Column("is_deleted")]
        public bool IsDeleted { get;set; } = false;

        public string DisplayName
        {
            get
            {
                return this.Taux.ToString("0.##") + " %" + " (" +this.Libelle + ")";
            }
        }
    }
}