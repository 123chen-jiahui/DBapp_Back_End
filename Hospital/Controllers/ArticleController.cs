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
    public class ArticleController : ControllerBase
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

        [HttpGet("{articleInfoId}")]
        public async Task<IActionResult> GetArticleDetailById([FromRoute] Guid articleInfoId)
        {
            var articInfoFromRepo = await _articleRepository.GetArticleInfoByIdAsync(articleInfoId);
            if(articInfoFromRepo==null)
            {
                return NotFound("未找到相关文稿信息");
            }
            var articleContentFromRepo = await _articleRepository.GetArticleContentByIdAsync(articleInfoId);
            if(articleContentFromRepo == null)
            {
                return NotFound("未找到文稿内容");
            }
            var articleImgsFromRepo = await _articleRepository.GetArticleImgByIdAsync(articleInfoId);
            //IEnumerable<string> articleImgsAdress = new IEnumerable<string>();
            List<string> articleImgsAdressList = new List<string>();

            if (articleImgsFromRepo != null)
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
                Title = articInfoFromRepo.Title,
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
            Console.WriteLine(articleForCreationDto);
            var articleInfoToRepo = new ArticleInfo
            {
                Id = Guid.NewGuid(),
                Title = articleForCreationDto.Title,
                Type = articleForCreationDto.Type,
                Author = articleForCreationDto.Author,
                Time = articleForCreationDto.Time,
                
            };

            _articleRepository.AddArticleInfo(articleInfoToRepo);
            var articleContentToRepo = new ArticleContent
            {
                Id = articleInfoToRepo.Id,
                Content = articleForCreationDto.Content,
            };

            _articleRepository.AddArticleContent(articleContentToRepo);
            if (articleForCreationDto.ImgsURL != null && articleForCreationDto.ImgsURL.Count() > 0)
            {
                foreach (var item in articleForCreationDto.ImgsURL)
                {
                    var articleImgsToRepo = new ArticleImg
                    {
                        Id = articleInfoToRepo.Id,
                        ImgAddress = item,
                    };
                    _articleRepository.AddArticleImg(articleImgsToRepo);
                }
            }
            await _articleRepository.SaveAsync();
            return Ok(articleInfoToRepo.Id);
        }

        [HttpDelete("{articleInfoId:Guid}")]
        public async Task<IActionResult> DeleteArticle(Guid articleInfoId)
        {
            var articleInfoFromRepo = await _articleRepository.GetArticleInfoByIdAsync(articleInfoId);
            if(articleInfoFromRepo==null)
            {
                return NotFound("未找到该编号的文稿信息");
            }
            _articleRepository.DeleteArticle(articleInfoId);
            await _articleRepository.SaveAsync();
            return Ok();
        }
    }
}
