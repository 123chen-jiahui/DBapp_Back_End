using System;

namespace Hospital.Dtos
{
    public class ArticleInfoDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public long Time { get; set; }
    }
}
