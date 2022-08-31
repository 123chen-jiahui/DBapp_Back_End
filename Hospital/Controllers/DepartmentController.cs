using AutoMapper;
using Hospital.Dtos;
using Hospital.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    [Route("department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IResourceRepository _resourRepository;
        private readonly IMapper _mapper;

        public DepartmentController(
            IResourceRepository resourceRepository,
            IMapper mapper
        )
        {
            _resourRepository = resourceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        // [Authorize]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _resourRepository.GetDepartments();
            return Ok(_mapper.Map<IEnumerable<DepartmentDto>>(departments));
        }

        [HttpGet("detail")]
        public async Task<IActionResult> GetDepartmentsDetail()
        {
            var departmentsDetail = await _resourRepository.GetDepartmentsDetail();
            return Ok(_mapper.Map<IEnumerable<DepartmentDetailDto>>(departmentsDetail));
        }
    }
}
