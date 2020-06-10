using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppStock.Models
{
    [Table("app_article_famille")]
    public class ArticleFamille
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("article_famille_uid")]
        public int Id { get; set; }
        [Column("article_famille_code")]
        [Required]
        public string Code { get; set; }
        [Column("article_famille_libelle")]
        [Required]
        public string Libelle { get; set; }    
        
        public ICollection<Article> Articles { get; set; }    
    }
}