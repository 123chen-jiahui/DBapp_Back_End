using AutoMapper;
using Hospital.Dtos;
using Hospital.Models;
using Hospital.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    [Route("purchaselist")]
    //[Authorize(Roles="Admin")]
    [ApiController]
    public class PurchaseListController : ControllerBase
    {
        private readonly IPurchaseListRepository _purchaseListRepository;
        private IMapper _mapper;
        public PurchaseListController(
            IPurchaseListRepository purchaseListRepository,
            IMapper mapper)
        {
            _purchaseListRepository = purchaseListRepository;
            _mapper = mapper;
        }

        // 获取所有的采购清单
        // api/purchaselist
        // 查询某个职工负责的采购清单 //api/purchaselist?staffid={}
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPurchaseLists([FromQuery] int staffid)
        {
            
            var purchaseListsFromRepo = await _purchaseListRepository.GetPurchaseListsAsync(staffid);
            if(purchaseListsFromRepo == null || purchaseListsFromRepo.Count()<=0)
            {
                return NotFound("未找到采购清单");
            }
            return Ok(_mapper.Map<IEnumerable<PurchaseListDto>>(purchaseListsFromRepo));
        }

        // 根据采购清单编号获取清单数据
        // api/purchaselist/{purchaseListId}
        [HttpGet("{purchaseListId:Guid}",Name = "GetPurchaseListById")]
        [Authorize]
        public async Task<IActionResult> GetPurchaseListByIdAsync(Guid purchaseListId)
        {
            var purchaselistFromRepo = await _purchaseListRepository.GetPurchaseListByIdAsync(purchaseListId);
            if (purchaselistFromRepo == null)
            {
                return NotFound($"采购清单{purchaselistFromRepo}找不到");
            }
            return Ok(_mapper.Map<PurchaseListDto>(purchaselistFromRepo));
        }

        // 获取某一采购清单的采购详细信息，具体购买的物品信息
        // api/purchaselist/{purchaseListId}/items
        [HttpGet("{purchaseListId:Guid}/items",Name = "GetPurchaseListItems")]
        [Authorize]
        public async Task<IActionResult> GetPurchaseListItemsAsync(Guid purchaseListId)
        {
            if (!(await _purchaseListRepository.PurchaseListExistsAsync(purchaseListId)))
                return NotFound("不存在相应采购清单");
            var itemsFromRepo =await _purchaseListRepository.GetPurchaseListItemsByIdAsync(purchaseListId);
            if (itemsFromRepo == null || itemsFromRepo.Count() <= 0)
                return NotFound($"不存在{purchaseListId}的采购项");
            return Ok(_mapper.Map<IEnumerable<PurchaseListItemDto>>(itemsFromRepo));   
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePurchaseList([FromBody] PurchaseListForCreationDto purchaseListForCreationDto)
        {
            var PurchaseListModel = _mapper.Map<PurchaseList>(purchaseListForCreationDto);
            foreach(PurchaseListItem item in PurchaseListModel.PurchaseListItems)
            {
                item.PurchaseListId = PurchaseListModel.Id;
                _purchaseListRepository.AddPurchaseListItem(PurchaseListModel.Id, item);
            }
            _purchaseListRepository.AddPurchaseList(PurchaseListModel);
            await _purchaseListRepository.SaveAsync();
            var purchaseListToReturn = _mapper.Map<PurchaseListDto>(PurchaseListModel);

            return CreatedAtRoute(
                "GetPurchaseListById",
                new { purchaseListId = purchaseListToReturn.Id },
                purchaseListToReturn
                );
        }
    }
}
