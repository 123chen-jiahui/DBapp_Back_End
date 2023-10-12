using AutoMapper;
using Hospital.Dtos;
using Hospital.ResourceParameter;
using Hospital.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 有关药品的信息
namespace Hospital.Controllers
{
    [Route("medicine")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IMapper _mapper;

        public MedicineController(
            IResourceRepository resourceRepository,
            IMapper mapper
        )
        {
            _resourceRepository = resourceRepository;
            _mapper = mapper;
        }

        // 取得满足关键词的药品数量
        [HttpGet("count")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetMedicinesCount(
            [FromQuery] string keyWord    
        )
        {
            if (keyWord == "" || keyWord == null)
            {
                return NotFound();
            }
            var count = await _resourceRepository.CountMedinesAsync(keyWord);
            if (count == 0)
            {
                return NotFound("未找到药品");
            }
            return Ok(count);
        }

        // 医生根据关键词查找药品
        // 需要设置分页
        [HttpGet]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetMedicines(
            [FromQuery] string keyWord,
            [FromQuery] PageResourceParameter parameter
        )
        {
            var medicine = await _resourceRepository.GetMedicinesAsync(keyWord, parameter.PageNumber, parameter.PageSize);

            return Ok(_mapper.Map<IEnumerable<MedicineDto>>(medicine));
        }

        [HttpPost("output")]
        [Authorize] // 这里的权限验证是为了方便起见
        public async Task<IActionResult> OutPut([FromBody] MedicineForDeletionDto medicineForDeletionDto)
        {
            int idLen = medicineForDeletionDto.Id.Length;
            int numberLen = medicineForDeletionDto.Number.Length;

            if (idLen != numberLen)
            {
                return BadRequest("药品和数量无法一一对应");
            }

            for (int i = 0; i < idLen; i++)
            {
                var medicine = await _resourceRepository.GetMedicineAsync(medicineForDeletionDto.Id[i]);
                medicine.Inventory -= medicineForDeletionDto.Number[i];
            }
            await _resourceRepository.SaveAsync();
            return NoContent();
        }
    }
}
