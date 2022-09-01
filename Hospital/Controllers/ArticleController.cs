using AutoMapper;
using Hospital.Dtos;
using Hospital.Models;
using Hospital.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    [Route("article")]
    [ApiController]
    public class ArticleController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        private IMapper _mapper;
        public ArticleController(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        // 获取所有的文稿
        // /api/article
        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> GetArticleInfosAsync()
        {
            var articleInfoFromRepo = await _articleRepository.GetAllArticleInfosAsync();
            if (articleInfoFromRepo == null || articleInfoFromRepo.Count() <= 0)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<ArticleInfoDto>>(articleInfoFromRepo));
        }

        [HttpGet("{articleInfoId:Guid}")]
        [HttpHead]
        public async Task<IActionResult> GetArticleDetailById(Guid Id)
        {
            var articInfoFromRepo = await _articleRepository.GetArticleInfoByIdAsync(Id);
            if(articInfoFromRepo==null)
            {
                return NotFound("未找到相关文稿信息");
            }
            var articleContentFromRepo = await _articleRepository.GetArticleContentByIdAsync(Id);
            if(articleContentFromRepo == null)
            {
                return NotFound("未找到文稿内容");
            }
            var articleImgsFromRepo = await _articleRepository.GetArticleImgByIdAsync(Id);
            //IEnumerable<string> articleImgsAdress = new IEnumerable<string>();
            List<string> articleImgsAdressList = new List<string>();

            if (articleImgsFromRepo == null)
            {
                foreach (var item in articleImgsFromRepo)
                {
                    articleImgsAdressList.Add(item.ImgAddress.ToString());
                }
            }
            IEnumerable<string> articleImgsAdress=articleImgsAdressList;
            var articleDetail = new ArticleDetailDto
            {
                Id = articInfoFromRepo.Id,
                Type = articInfoFromRepo.Type,
                Author = articInfoFromRepo.Author,
                Time = articInfoFromRepo.Time,
                Content = articleContentFromRepo.Content,
                ImgAdress = articleImgsAdress,
            };
            return Ok(articleDetail);
        }

        [HttpPost]
        public async Task<IActionResult> PostArticle([FromBody] ArticleForCreationDto articleForCreationDto)
        {
            var articleInfoToRepo = _mapper.Map<ArticleInfo>(articleForCreationDto);
            _articleRepository.AddArticleInfo(articleInfoToRepo);
            articleInfoToRepo.Content.Id=articleInfoToRepo.Id;
            _articleRepository.AddArticleContent(articleInfoToRepo.Content);
            foreach (var item in articleInfoToRepo.articleImgs)
            {
                item.Id=articleInfoToRepo.Id;
                _articleRepository.AddArticleImg(item);
            }
            await _articleRepository.SaveAsync();
            return Ok(articleInfoToRepo.Id);
        }
    }
}
