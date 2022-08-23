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
namespace Hospital.Controllers
{   //填写、查看请假列表、获取审批列表、进行审批
    [Route("break")]
    [ApiController]
    public class BreakController: ControllerBase
    {
        private readonly IAffairsRepository _affairsRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BreakController(
            IAffairsRepository affairsRepository,
            IUserRepository userRepository,
            IMapper mapper,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _affairsRepository = affairsRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        /* 填写 */
        [HttpPost("askForBreak")]
        [Authorize]
        public async Task<IActionResult> AskForBreak([FromBody] BreakForCreationDto breakForCreationDto)
        {
            var staffId = _httpContextAccessor
                .HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

          
            BreakState state = BreakState.waitForApproval;
            DateTime fromTime = breakForCreationDto.FromTime;
            DateTime toTime = breakForCreationDto.ToTime;
            string reason = breakForCreationDto.Reason;
            var admin=await _userRepository.GetAdminByAsync();

            var breakk = new Break()            //break有歧义所以用breakk
            {
                Id = Guid.NewGuid().ToString(),
                StaffId= Convert.ToInt32(staffId),
   
                FromTime = fromTime,
                ToTime = toTime,
                State = state,
                Reason = reason,
                AdminId=admin.Id
            };

            // 保存数据
            await _affairsRepository.AddBreakAsync(breakk);
            await _affairsRepository.SaveAsync();
            return Ok();
        }


        /* 获取我的历史请假列表，借鉴了获取staffs列表 */
        [HttpGet("history")]
        [Authorize]
        public async Task<IActionResult> GetBreaksByStaffId()
        {
            var staffIdS = _httpContextAccessor
                .HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int staffId = Convert.ToInt32(staffIdS);
            var breaks = await _affairsRepository.GetBreaksByStaffIdAsync(staffId);
            var breakDto = _mapper.Map<IEnumerable<BreakDto>>(breaks);
            return Ok(breakDto);
        }

        /* 获取待审批列表，借鉴了获取staffs列表 */
        [HttpGet("approveList")]
        [Authorize]
        public async Task<IActionResult> GetUnapprovedBreaks()
        {
            var breaks = await _affairsRepository.GetUnapprovedBreaksAsync();
            var breakDto = _mapper.Map<IEnumerable<BreakDto>>(breaks);
            return Ok(breakDto);
        }

        /* 修改状态 */
        [HttpPost("approve")]
        [Authorize]
        public async Task<IActionResult> SetState([FromBody]BreakApproveDto breakApproveDto)
        {
            // 保存数据
            var breakk=await _affairsRepository.GetBreakByIdAsync(breakApproveDto.Id);
            breakk.State = breakApproveDto.State;
            await _affairsRepository.SaveAsync();
            return Ok();
        }
    }
}
