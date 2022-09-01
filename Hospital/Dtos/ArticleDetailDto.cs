using System;
using System.Collections.Generic;

namespace Hospital.Dtos
{
    public class ArticleDetailDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public long Time { get; set; }

        public string Content { get; set; }
        public IEnumerable<string> ImgAdress { get; set; }
        
    }
}
