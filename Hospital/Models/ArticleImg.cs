using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Models
{
    public class ArticleImg
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string ImgAddress { get; set; }

        [ForeignKey("ArticleInfoId")]
        public ArticleInfo ArticleInfo { get; set; }
    }
}
