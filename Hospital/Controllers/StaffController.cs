using AutoMapper;
using Hospital.Dtos;
using Hospital.Helper;
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
        // [Authorize(Roles = "Admin")]
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

        [HttpPut("uploadPhoto/{staffId}")]
        public async Task<IActionResult> UploadPhoto([FromRoute] int staffId)
        {
            // 找到该医生
            var staff = await _userRepository.GetStaffByStaffIdAsync(staffId);
            if (staff == null)
            {
                return BadRequest("医生不存在");
            }

            // 获取图片
            string photo = Request.Form["photo"];
            // Console.WriteLine("photo is {0}", photo);
            string newPhoto = PhotoUpload.UploadPhoto(photo, "staffPhoto/" + staffId.ToString());
            if (newPhoto != null)
            {
                staff.Photo = newPhoto;
                await _userRepository.SaveAsync();
                return NoContent();
            }
            return BadRequest("上传图片出错");
        }

        [HttpPost("test")]
        public IActionResult Test()
        {
            string photo = Request.Form["photo"];
            Console.WriteLine("photo is {0}", photo);
            Console.WriteLine("aaaaahello");
                Console.WriteLine("hello");
                string newPhoto = PhotoUpload.UploadPhoto(photo, "staffPhoto/" + "5");
                if (newPhoto != null)
                {
                    return NoContent();
                    // Console.WriteLine("successfully make newPhoto");
                }
            return BadRequest();
        }


        // 完善医生信息
        [HttpPut("refine/{staffId}")]
        public async Task<IActionResult> Refine(
            [FromRoute] int staffId,
            [FromBody] StaffForRefine staffForRefine
        )
        {
            // 找到该医生
            var staff = await _userRepository.GetStaffByStaffIdAsync(staffId);
            if (staff == null)
            {
                return NotFound("医生不存在");
            }

            // 完善信息
            staff.Introduction = staffForRefine.Introduction;
            staff.Position = staffForRefine.Position;
            staff.Skilled = staffForRefine.Skilled;

            // 保存数据库
            await _userRepository.SaveAsync();
            return NoContent();
        }
    }
}
