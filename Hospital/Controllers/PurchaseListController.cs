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

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
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
        [HttpHead]
        public IActionResult GetPurchaseLists([FromQuery] int staffid)
        {
            var purchaseListsFromRepo = _purchaseListRepository.GetPurchaseLists(staffid);
            if(purchaseListsFromRepo == null || purchaseListsFromRepo.Count()<=0)
            {
                return NotFound("未找到采购清单");
            }
            return Ok(_mapper.Map<IEnumerable<PurchaseListDto>>(purchaseListsFromRepo));
        }

        // 根据采购清单编号获取清单数据
        // api/purchaselist/{purchaseListId}
        [HttpGet("{purchaseListId:Guid}",Name = "GetPurchaseListById")]
        [HttpHead]
        public IActionResult GetPurchaseListById(Guid purchaseListId)
        {
            var purchaselistFromRepo = _purchaseListRepository.GetPurchaseListById(purchaseListId);
            if (purchaselistFromRepo == null)
            {
                return NotFound($"采购清单{purchaselistFromRepo}找不到");
            }
            return Ok(_mapper.Map<PurchaseListDto>(purchaselistFromRepo));
        }

        // 获取某一采购清单的采购详细信息，具体购买的物品信息
        // api/purchaselist/{purchaseListId}/items
        [HttpGet("{purchaseListId:Guid}/items",Name = "GetPurchaseListItems")]
        public IActionResult GetPurchaseListItems(Guid purchaseListId)
        {
            if (!_purchaseListRepository.PurchaseListExists(purchaseListId))
                return NotFound("不存在相应采购清单");
            var itemsFromRepo = _purchaseListRepository.GetPurchaseListItemsById(purchaseListId);
            if (itemsFromRepo == null || itemsFromRepo.Count() <= 0)
                return NotFound($"不存在{purchaseListId}的采购项");
            return Ok(_mapper.Map<IEnumerable<PurchaseListItemDto>>(itemsFromRepo));   
        }

        [HttpPost]
        public IActionResult CreatePurchaseList([FromBody] PurchaseListForCreationDto purchaseListForCreationDto)
        {
            var PurchaseListModel = _mapper.Map<PurchaseList>(purchaseListForCreationDto);
            _purchaseListRepository.AddPurchaseList(PurchaseListModel);
            _purchaseListRepository.Save();
            var purchaseListToReturn = _mapper.Map<PurchaseListDto>(PurchaseListModel);

            return CreatedAtRoute(
                "GetPurchaseListById",
                new { purchaseListId = purchaseListToReturn.Id },
                purchaseListToReturn
                );
        }

        [HttpPost]
        public IActionResult CreatePurchaseListItem(
            [FromRoute] Guid purchaseListId,
            [FromBody] PurchaseListItemForCreationDto purchaseListItemForCreationDto
            )
        {
            if (!_purchaseListRepository.PurchaseListExists(purchaseListId))
                return NotFound("不存在相应采购清单");

            var purchaseListItemModel=_mapper.Map<PurchaseListItem>(purchaseListItemForCreationDto);
            _purchaseListRepository.AddPurchaseListItem(purchaseListId, purchaseListItemModel);
            _purchaseListRepository.Save();

            var ItemToReturn = _mapper.Map<PurchaseListItemDto>(purchaseListItemModel);
            return CreatedAtRoute(
                "GetPurchaseListItems",
                new { purchaseListId = purchaseListItemModel.PurchaseListId },
                ItemToReturn
                );
        }
    }
}
