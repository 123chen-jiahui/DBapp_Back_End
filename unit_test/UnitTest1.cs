using NUnit.Framework;
using Moq;
using Hospital.Services;
using Hospital.Models;
using Hospital.Controllers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Hospital.Dtos;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using MockQueryable.Moq;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Hospital.ResourceParameter;
using System.Security.Claims;
using Hospital.Helper;

namespace unit_test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            mockResourceRepository = new Mock<IResourceRepository>();
            mockUserRepository = new Mock<IUserRepository>();
            mockAffairsRepository = new Mock<IAffairsRepository>();
            mockConfiguration = new Mock<IConfiguration>();
            mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockMapper = new Mock<IMapper>();
            medicineController = new MedicineController(
                mockResourceRepository.Object,
                mockMapper.Object);
            patientsController = new PatientsController(
                mockConfiguration.Object,
                mockUserRepository.Object,
                mockMapper.Object);
            ordersController = new OrdersController(
                mockHttpContextAccessor.Object,
                mockUserRepository.Object,
                mockResourceRepository.Object,
                mockMapper.Object,
                mockHttpClientFactory.Object);
            registrationController = new RegistrationController(
                mockAffairsRepository.Object,
                mockMapper.Object,
                mockHttpClientFactory.Object,
                mockHttpContextAccessor.Object);
            scheduleController = new ScheduleController(
                mockAffairsRepository.Object,
                mockMapper.Object);
            staffController = new StaffController(
                mockUserRepository.Object,
                mockMapper.Object,
                mockHttpClientFactory.Object,
                mockHttpContextAccessor.Object);

            PatientsInit();
            MedicineInit();
            StaffInit();
            ScheduleInit();
            TimeSlotInit();
            WaitLineInit();
        }

        // 测试controller
        private Mock<IResourceRepository> mockResourceRepository;
        private Mock<IUserRepository> mockUserRepository;
        private Mock<IAffairsRepository> mockAffairsRepository;
        private Mock<IConfiguration> mockConfiguration;
        private Mock<IHttpContextAccessor> mockHttpContextAccessor;
        private Mock<IHttpClientFactory> mockHttpClientFactory;
        private Mock<IMapper> mockMapper;
        private MedicineController medicineController;
        private PatientsController patientsController;
        private OrdersController ordersController;
        private RegistrationController registrationController;
        private ScheduleController scheduleController;
        private StaffController staffController;

        // 数据
        private List<Patient> patients;
        private List<Medicine> medicines;
        private List<Staff> staffs;
        private List<TimeSlot> timeSlots;
        private List<Schedule> schedules;
        private List<WaitLine> waitLines;

        private void PatientsInit()
        {
            var patient1 = new Patient
            {
                Id = 1000000,
                GlobalId = "330724200103151816",
                Password = "cjh010315",
                Name = "陈家辉",
                Gender = Gender.male,
                Birthday = new System.DateTime(2001, 3, 15),
                Phone = "15157900530"
            };
            var patient2 = new Patient
            {
                Id = 1000001,
                GlobalId = "330724200103151815",
                Password = "zyn010115",
                Name = "张亚楠",
                Gender = Gender.female,
                Birthday = new System.DateTime(2001, 1, 15),
                Phone = "19821232573"
            };

            patients = new List<Patient>()
            {
                patient1, patient2
            };
        }

        private void MedicineInit()
        {
            var medicine1 = new Medicine
            {
                Id = "H19994016",
                Name = "阿莫西林克拉维酸钾片",
                Price = 20.00M,
                Inventory = 500,
                Indications = "有炎症的患者",
                Manufacturer = "昆明贝克诺顿制药有限公司",
            };
            var medicine2 = new Medicine
            {
                Id = "Z20040063",
                Name = "连花清瘟胶囊",
                Price = 15.00M,
                Inventory = 200,
                Indications = "用于治疗流行性感冒属热毒袭肺症",
                Manufacturer = "石家庄以岭药业股份有限公司"
            };
            medicines = new List<Medicine>()
            {
                medicine1, medicine2
            };
        }

        private void TimeSlotInit()
        {
            var timeSlot1 = new TimeSlot
            {
                Id = 1,
                StartTime = 7,
                EndTime = 9,
            };
            var timeSlot2 = new TimeSlot
            {
                Id = 2,
                StartTime = 13,
                EndTime = 16
            };
            timeSlots = new List<TimeSlot>()
            {
                timeSlot1, timeSlot2
            };
        }

        private void StaffInit()
        {
            var staff1 = new Staff
            {
                Id = 2000021,
                Name = "向紫槐",
                DepartmentId = 1,
            };
            var staff2 = new Staff
            {
                Id = 2000022,
                Name = "罗亚梅",
                DepartmentId = 2
            };
            staffs = new List<Staff>()
            {
                staff1, staff2
            };
        }

        private void ScheduleInit()
        {
            var schedule1 = new Schedule
            {
                StaffId = 2000021,
                Day = 1,
                TimeSlotId = 1,
                RoomId = "201",
                Capacity = 12,
            };
            var schedule2 = new Schedule
            {
                StaffId = 2000021,
                Day = 2,
                TimeSlotId = 1,
                RoomId = "201",
                Capacity = 12,
            };
            var schedule3 = new Schedule
            {
                StaffId = 2000021,
                Day = 3,
                TimeSlotId = 1,
                RoomId = "201",
                Capacity = 12,
            };
            schedules = new List<Schedule>()
            {
                schedule1, schedule2, schedule3
            };
        }

        private void WaitLineInit()
        {
            waitLines = new List<WaitLine>();
        }

        // 测试MedicineController
        // 测试GetMedicineCount方法
        [Test]
        [TestCase("阿莫西林", true, 1)]
        [TestCase("连花清瘟", true, 1)]
        [TestCase("片", true, 1)]
        [TestCase("胶囊", true, 1)]
        [TestCase("急支糖浆", false, 0)]
        [TestCase("阿连", false, 0)]
        public async System.Threading.Tasks.Task Test1Async(string keyword, bool exist, int num)
        {
            // arrange
            mockResourceRepository
                .Setup(repo => repo.CountMedinesAsync(keyword))
                .ReturnsAsync(medicines.Where(m => m.Name.Contains(keyword)).ToList().Count());

            // act
            var result = await medicineController.GetMedicinesCount(keyword);

            // assert
            if (!exist)
            {
                Assert.IsInstanceOf<NotFoundObjectResult>(result);
            }
            else
            {
                Assert.AreEqual(num, ((OkObjectResult)result).Value);
            }
            //Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        [TestCase(new int[] { 1, 2, 3 }, "a", "b")]
        [TestCase(new int[] { 1, 2 }, "a")]
        [TestCase(new int[] { 1, 2, 3 }, "a", "b", "c", "d")]
        [TestCase(new int[] { 1, 2 }, "a", "b", "c")]
        [TestCase(new int[] { })]
        [TestCase(new int[] { 1, 2 }, "a", "b")]
        [TestCase(new int[] { 1, 2, 3 }, "a", "b", "c")]
        public async System.Threading.Tasks.Task TestMedicineControllerOutPut(int[] numbers, params string[] ids)
        {
            // arrange
            mockResourceRepository
                .Setup(repo => repo.GetMedicineAsync(It.IsAny<string>()))
                .ReturnsAsync(new Medicine());
            var medicineForDeletionDto = new MedicineForDeletionDto
            {
                Id = ids,
                Number = numbers
            };
            // act
            var result = await medicineController.OutPut(medicineForDeletionDto);
            // assert
            if (numbers.Length == ids.Length)
            {
                Assert.IsInstanceOf<NoContentResult>(result);
            }
            else
            {
                Assert.IsInstanceOf<BadRequestObjectResult>(result);
            }
        }

        // 测试OrdersController
        [Test]
        [TestCase(1000000, true)]
        [TestCase(1000001, true)]
        [TestCase(1000002, false)]
        public async System.Threading.Tasks.Task TestOrdersControllerGetOrdersForDoctor(int patientId, bool exist)
        {
            // arrange
            var mockPatients = patients.AsQueryable().BuildMock();
            var parameter = new PageResourceParameter
            {
                PageNumber = 1,
                PageSize = 1,
            };
            mockUserRepository
                .Setup(repo => repo.PatientExistsByPatientIdAsync(patientId))
                .ReturnsAsync(mockPatients.Object.Any(p => p.Id == patientId));

            // act
            var result = await ordersController.GetOrdersForDoctor(patientId, parameter);

            // assert
            if (!exist)
            {
                Assert.IsInstanceOf<BadRequestObjectResult>(result);
            }
            else
            {
                Assert.IsInstanceOf<OkObjectResult>(result);
            }
        }

        // 测试RegistrationController
        [Test]
        // [TestCase(0, true)]
        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [TestCase(4, false)]
        [TestCase(5, false)]
        [TestCase(6, false)]
        public async System.Threading.Tasks.Task TestRegistrationControllerCheckout(int day, bool ok)
        {
            // arrange
            var registrationForCreationDto = new RegistrationForCreationDto
            {
                StaffId = 2000021,
                Day = day
            };
            var mockStaffs = staffs.AsQueryable().BuildMock();
            var mockSchedules = schedules.AsQueryable().BuildMock();
            var mockTimeSlots = timeSlots.AsQueryable().BuildMock();
            var mockWaitLines = waitLines.AsQueryable().BuildMock();
            var mockHttpContext = new Mock<HttpContext>();
            var mockUser = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, "1000000") }));
            mockHttpContextAccessor
                .Setup(repo => repo.HttpContext)
                .Returns(mockHttpContext.Object);
            mockHttpContext.SetupGet(x => x.User).Returns(mockUser);

            //var schedule = new Schedule();
            mockAffairsRepository
                .Setup(repo => repo.GetScheduleOfOneDay(
                    registrationForCreationDto.StaffId,
                    registrationForCreationDto.Day))
                .ReturnsAsync(mockSchedules.Object.Where(
                    s => s.StaffId == registrationForCreationDto.StaffId &&
                         s.Day == registrationForCreationDto.Day).FirstOrDefault());
            mockAffairsRepository
                .Setup(repo => repo.GetTimeSlotAsync(It.IsAny<int>()))
                .ReturnsAsync((int x) => mockTimeSlots.Object.Where(ts => ts.Id == x).FirstOrDefault());
            //mockAffairsRepository
            //    .Setup(repo => repo.AddWaitLineAsync(It.IsAny<WaitLine>()))
            //    .Returns((WaitLine waitLine) => waitLines.Add(waitLine))
                
            
            // act
            var result = await registrationController.Checkout(registrationForCreationDto);

            // assert
            if (ok)
            {
                Assert.IsInstanceOf<OkObjectResult>(result);
            }
            else
            {
                Assert.IsInstanceOf<BadRequestObjectResult>(result);
            }
        }

        // 测试ScheduleController
        [Test]
        [TestCase(2000021, true)]
        [TestCase(2000022, false)]
        public async System.Threading.Tasks.Task TestScheduleControllerGetSchedule(int staffId, bool ok)
        {
            // arrange
            var mockSchedules = schedules.AsQueryable().BuildMock();
            mockAffairsRepository
                .Setup(repo => repo.GetScheduleAsync(staffId))
                .ReturnsAsync(mockSchedules.Object.Where(s => s.StaffId == staffId).ToList());

            // act
            var result = await scheduleController.GetSchedule(staffId);

            // assert
            if (ok)
            {
                Assert.IsInstanceOf<OkObjectResult>(result);
            }
            else
            {
                Assert.IsInstanceOf<NotFoundObjectResult>(result);
            }
        }

        // 测试StaffController
        [Test]
        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [TestCase(4, false)]
        public async System.Threading.Tasks.Task TestStaffControllerGetStaff(int departmentId, bool exist)
        {
            // arrange
            var mockStaffs = staffs.AsQueryable().BuildMock();
            mockUserRepository
                .Setup(repo => repo.GetStaffsAsync(departmentId, 1, 1))
                .Returns(PaginationList<Staff>.CreateAsync(1, 1, mockStaffs.Object.Where(s => s.DepartmentId == departmentId)));
            // act
            var result = await staffController.GetStafffs(
                departmentId,
                new PageResourceParameter() { PageNumber = 1, PageSize = 1 });

            if (exist)
            {
                Assert.IsInstanceOf<OkObjectResult>(result);
            }
            else
            {
                Assert.IsInstanceOf<NotFoundObjectResult>(result);
            }
        }
    }
}