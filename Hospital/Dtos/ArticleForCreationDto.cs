using Hospital.Models;
using System;
using System.Collections.Generic;

namespace Hospital.Dtos
{
    public class ArticleForCreationDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public long Time { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        //public ArticleContent Content { get; set; }=new ArticleContent();
        public IEnumerable<string> ImgsURL { get; set; }    
        //public IEnumerable<ArticleImg> articleImgs { get; set; }=new List<ArticleImg>();
    }
}
