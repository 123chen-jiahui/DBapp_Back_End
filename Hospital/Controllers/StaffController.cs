using AutoMapper;
using Hospital.Dtos;
using Hospital.Models;
using Hospital.ResourceParameter;
using Hospital.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

// 有关员工的操作
namespace Hospital.Controllers
{
    [Route("staff")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StaffController(
            IUserRepository userRepository,
            IMapper mapper,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("{DepartmentId}")]
        public async Task<IActionResult> GetStafffs(
            [FromRoute] int departmentId,
            [FromQuery] PageResourceParameter pageParameters 
        ) // 路由都是string
        {
            // int Id = Convert.ToInt32(departmentId);
            var staffsFromRepo = await _userRepository.GetStaffsAsync(departmentId, pageParameters.PageNumber, pageParameters.PageSize);
            if (staffsFromRepo == null || staffsFromRepo.Count() <= 0)
            {
                return NotFound("找不到任何医生");
            }
            var staffDto = _mapper.Map<IEnumerable<StaffDto>>(staffsFromRepo);
            return Ok(staffDto);
        }

        // 返回订单的数量，用于分页
        [HttpGet("{departmentId}/count")]
        public async Task<IActionResult> CountStaff([FromRoute] int departmentId)
        {
            var count = await _userRepository.CountStaffAsync(departmentId);
            return Ok(count);
        }

        /* 获取员工基本信息 */
        [HttpGet("info")]
        [Authorize]
        public async Task<IActionResult> GetStaffById()
        {
            var staffId = _httpContextAccessor
               .HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var staff = await _userRepository.GetStaffByStaffIdAsync(Convert.ToInt32(staffId));

            return Ok(_mapper.Map<StaffDto>(staff));
        }

        // 删除员工
        [HttpDelete("{staffId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStaff([FromRoute] int staffId)
        {
            var staff = await _userRepository.GetStaffByStaffIdAsync(staffId);
            if (staff == null)
            {
                return NotFound("找不到要删除的医生");
            }

            _userRepository.DeleteStaff(staff);
            return NoContent();
        }
    }
}
