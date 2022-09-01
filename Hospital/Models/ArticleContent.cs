using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Models
{
    public class ArticleContent
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength]
        public string Content { get; set; }

        [ForeignKey("ArticleInfoId")]
        public ArticleInfo ArticleInfo { get; set; }
    }
}
