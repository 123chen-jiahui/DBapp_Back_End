using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public interface IArticleRepository
    {
        Task<ArticleInfo> GetArticleInfoByIdAsync(Guid Id);
        Task<ArticleContent> GetArticleContentByIdAsync(Guid Id);

        Task <IEnumerable<ArticleImg>> GetArticleImgByIdAsync(Guid Id);
        Task<IEnumerable<ArticleInfo>> GetAllArticleInfosAsync();

        void AddArticleInfo(ArticleInfo articleInfo);
        void AddArticleContent(ArticleContent articleContent);
        void AddArticleImg(ArticleImg articleImg);

        Task<bool> SaveAsync(); 
    }
}
