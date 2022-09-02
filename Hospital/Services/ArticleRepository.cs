using Hospital.Database;
using Hospital.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AppDbContext _context;
        public ArticleRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddArticleContent(ArticleContent articleContent)
        {
            _context.ArticleContents.AddAsync(articleContent);
        }

        public void AddArticleImg(ArticleImg articleImg)
        {
            _context.ArticleImgs.AddAsync(articleImg);
        }

        public void AddArticleInfo(ArticleInfo articleInfo)
        {
            _context.ArticleInfos.AddAsync(articleInfo);
        }

        public void DeleteArticle(Guid Id)
        {
            ArticleInfo articleInfo = _context.ArticleInfos.FirstOrDefault(x => x.Id == Id);
            _context.ArticleInfos.Remove(articleInfo);
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<ArticleInfo>> GetAllArticleInfosAsync()
        {
            return await _context.ArticleInfos.ToListAsync();
        }

        public async Task<ArticleContent> GetArticleContentByIdAsync(Guid Id)
        {
            return await _context.ArticleContents.Where(ac=>ac.Id==Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ArticleImg>> GetArticleImgByIdAsync(Guid Id)
        {
            return await _context.ArticleImgs.Where(ai => ai.Id == Id).ToListAsync();
        }

        public async Task<ArticleInfo> GetArticleInfoByIdAsync(Guid Id)
        {
            return await _context.ArticleInfos.Where(ai => ai.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
