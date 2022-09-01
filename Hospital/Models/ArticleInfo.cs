using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Models
{
    public class ArticleInfo
    {
        [Key]
        [Required]
        [Column("ID")]
        public Guid Id { get; set; }

        [Required]
        [Column("TITLE")]
        [MaxLength(40)]
        public string Title { get; set; }

        [Required]
        [Column("TYPE")]
        [MaxLength(10)]
        public string Type { get; set; }

        [Required]
        [Column("TIME")]
        public long Time { get; set; }
        [Column("IMGURL")]

        [Required]
        [MaxLength(20)]
        public string Author { get; set; }
        // public string ImgURL { get; set; }
        public IEnumerable<ArticleImg> articleImgs { get; set; }
        public ArticleContent Content { get; set; }
    }
}
