using AutoMapper;
using Hospital.Dtos;
using Hospital.Models;
using Hospital.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    [Route("api/medicalRecord")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly IResourceRepository _resourceRepository;
        private readonly IMapper _mapper;
        public MedicalRecordController(
            IUserRepository userRepository,
            IResourceRepository resourceRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _resourceRepository = resourceRepository;
            _mapper = mapper;
        }

        // GET: api/<MedicalRecordController>
        [HttpGet("{PatientId}",Name = "GetMedicalRecord")]
        [Authorize]
        public async Task<IActionResult> GetMedicalRecord([FromRoute] int patientId)
        {
            // 获得医疗记录
            var medicalRecord = await _userRepository.GetMedicalRecordByMedicalRecordIdAsync(patientId);

            // 输出MedicalRecord的具体信息，所以需要MedicalRecordDto
            return Ok(_mapper.Map<IEnumerable<MedicalRecordDto>> (medicalRecord));
        }

        // POST api/<MedicalRecordController>
        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public IActionResult CreatMedicalRecord([FromBody] MedicalRecordForCreationDto medicalRecordForCreationDto)
        {
            var medicalRecordModel = _mapper.Map<MedicalRecord>(medicalRecordForCreationDto);
            _userRepository.AddMedicalRecord(medicalRecordModel);
            var medicalRecordToReturn = _mapper.Map<MedicalRecordDto>(medicalRecordModel);
            return CreatedAtRoute(
                "GetMedicalRecord",
                new { patientId = medicalRecordToReturn.PatientId },
                medicalRecordToReturn
                );
        }
    }
}
