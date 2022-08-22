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

// 有关员工的操作
namespace Hospital.Controllers
{
    [Route("staff")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public StaffController(
            IUserRepository userRepository,
            IMapper mapper
        )
        {
            _userRepository = userRepository;
            _mapper = mapper;
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

        /* 获取员工基本信息 */
        [HttpGet("{staffId}")]
        [Authorize]
        public async Task<IActionResult> GetStaffById([FromRoute] int staffId)
        {
            var staff = await _userRepository.GetStaffByStaffIdAsync(staffId);

            return Ok(_mapper.Map<StaffDto>(staff));
        }
    }
}
