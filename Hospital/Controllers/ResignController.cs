using AutoMapper;
using Hospital.Dtos;
using Hospital.Models;
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
{
    //填写、查看审批状态、获取审批列表、进行审批
    [Route("resign")]
    [ApiController]
    public class ResignController : ControllerBase
    {
        private readonly IAffairsRepository _affairsRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ResignController(
            IAffairsRepository affairsRepository,
            IUserRepository userRepository,
            IMapper mapper,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _userRepository = userRepository;
            _affairsRepository = affairsRepository;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        /* 填写 */
        [HttpPost("askForResign")]
        [Authorize]
        public async Task<IActionResult> AskForResign([FromBody] ResignForCreationDto resignForCreationDto)
        {
            var staffId = _httpContextAccessor
                .HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            ResignState state = ResignState.waitForApproval;
            DateTime time = DateTime.Now;
            string reason = resignForCreationDto.Reason;
            var admin = await _userRepository.GetAdminByAsync();

            var resign = new Resign()            //break有歧义所以用breakk
            {
                Id = Guid.NewGuid().ToString(),
                StaffId = Convert.ToInt32(staffId),
                Time = time,
                State = state,
                Reason = reason,
                AdminId = admin.Id
            };

            // 保存数据
            await _affairsRepository.AddResignAsync(resign);
            await _affairsRepository.SaveAsync();
            return Ok(_mapper.Map<ResignDto>(resign));
        }

        /* 获取我的历史辞职列表，借鉴了获取staffs列表 */
        [HttpGet("history")]
        [Authorize]
        public async Task<IActionResult> GetResignsByStaffId()
        {
            var staffIdS = _httpContextAccessor
                .HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int staffId = Convert.ToInt32(staffIdS);
            var resigns = await _affairsRepository.GetResignsByStaffIdAsync(staffId);
            var resignDto = _mapper.Map<IEnumerable<ResignDto>>(resigns);
            return Ok(resignDto);
        }

        /* 获取待审批列表 */
        [HttpGet("approveList")]
        [Authorize]
        public async Task<IActionResult> GetUnapprovedResigns()
        {
            var resigns = await _affairsRepository.GetUnapprovedResignsAsync();
            var resignDto = _mapper.Map<IEnumerable<ResignDto>>(resigns);
            return Ok(resignDto);
        }

        /* 修改状态 */
        [HttpPost("approve")]
        [Authorize]
        public async Task<IActionResult> SetState([FromBody] ResignApproveDto resignApproveDto)
        {
            // 保存数据
            var resign = await _affairsRepository.GetResignByIdAsync(resignApproveDto.Id);
            resign.State = resignApproveDto.State;
            //从员工表中删除,因为删除员工表后这条辞职信息不存在那么员工无法查看辞职申请结果，故暂不删除
       /*     if (resignApproveDto.State == ResignState.agreed) { 
                var staff = await _userRepository.GetStaffByStaffIdAsync(resign.StaffId);
      
                _userRepository.DeleteStaff(staff);
                await _userRepository.SaveAsync(); 
            }*/
            await _affairsRepository.SaveAsync();
            return Ok();
        }
    }
}
