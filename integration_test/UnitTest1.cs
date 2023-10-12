using Hospital.Dtos;
using Hospital.Models;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiTests;

namespace integration_test
{
    public class Tests
    {
        const string _testUrl = "/patients/";
        const string _breakUrl = "/break/";
        const string _mediaType = "application/json";
        readonly Encoding _encoding = Encoding.UTF8;




        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        [TestCase(1000000)]
        public async Task GetPatientByPatientId(int Id)
        {
            // arrange
            string url = $"{_testUrl}{Id}";
            using var host = await TestHostBuild.GetTestHost().StartAsync();//����TestServer

            // act
            var response = await host.GetTestClientWithToken("1000000", "Patient").GetAsync(url);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(url);
            // var apiResult = JsonConvert.DeserializeObject<ApiResult<>>
            var result = (await response.Content.ReadAsStringAsync());
            PatientDto p = JsonConvert.DeserializeObject<PatientDto>(result);
            // Console.WriteLine(result);
            Console.WriteLine(p.Id);
            Console.WriteLine(p.Name);
            Console.WriteLine(p.Gender);
            Console.WriteLine("hello world!");
        }

        // ����BreakController
        // ��ʼʱ�����ڽ���ʱ��
        [Test]
        public async Task TestAskForBreak()
        {
            // arrange
            DateTime t1 = DateTime.Now;
            DateTime t2 = DateTime.Now.AddDays(10);
            BreakForCreationDto breakForCreationDto = new BreakForCreationDto
            {
                FromTime = t2,
                ToTime = t1
            };
            string url = $"{_breakUrl}askForBreak";
            var json = JsonConvert.SerializeObject(breakForCreationDto);
            var content = new StringContent(json, _encoding, _mediaType);
            using var host = await TestHostBuild.GetTestHost().StartAsync();//����TestServer

            // act
            var response = await host.GetTestClientWithToken("2000031", "Staff").PostAsync(url, content);
            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            // response.EnsureSuccessStatusCode();
        }

        // ��ʼʱ��ͽ���ʱ�����
        [Test]
        public async Task TestAskForBreak2()
        {
            // arrange
            DateTime t1 = DateTime.Now;
            DateTime t2 = t1;
            BreakForCreationDto breakForCreationDto = new BreakForCreationDto
            {
                FromTime = t2,
                ToTime = t1
            };
            string url = $"{_breakUrl}askForBreak";
            var json = JsonConvert.SerializeObject(breakForCreationDto);
            var content = new StringContent(json, _encoding, _mediaType);
            using var host = await TestHostBuild.GetTestHost().StartAsync();//����TestServer

            // act
            var response = await host.GetTestClientWithToken("2000031", "Staff").PostAsync(url, content);
            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }

        // ȱ���������
        [Test]
        public async Task TestAskForBreak3()
        {
            // arrange
            DateTime t1 = DateTime.Now;
            DateTime t2 = DateTime.Now.AddDays(10);
            BreakForCreationDto breakForCreationDto = new BreakForCreationDto
            {
                FromTime = t1,
                ToTime = t2,
            };
            string url = $"{_breakUrl}askForBreak";
            var json = JsonConvert.SerializeObject(breakForCreationDto);
            var content = new StringContent(json, _encoding, _mediaType);
            using var host = await TestHostBuild.GetTestHost().StartAsync();//����TestServer

            // act
            var response = await host.GetTestClientWithToken("2000031", "Staff").PostAsync(url, content);
            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }

        // һ������
        [Test]
        public async Task TestAskForBreak4()
        {
            // arrange
            DateTime t1 = DateTime.Now;
            DateTime t2 = DateTime.Now.AddDays(10);
            BreakForCreationDto breakForCreationDto = new BreakForCreationDto
            {
                FromTime = t1,
                ToTime = t2,
                Reason = "���岻��"
            };
            string url = $"{_breakUrl}askForBreak";
            var json = JsonConvert.SerializeObject(breakForCreationDto);
            var content = new StringContent(json, _encoding, _mediaType);
            using var host = await TestHostBuild.GetTestHost().StartAsync();//����TestServer

            // act
            var response = await host.GetTestClientWithToken("2000031", "Staff").PostAsync(url, content);
            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        // ����Ϊ��
        [Test]
        public async Task TaskTestSetState()
        {
            // arrange
            string url = $"{_breakUrl}approve";
            var content = new StringContent("", _encoding, _mediaType);
            using var host = await TestHostBuild.GetTestHost().StartAsync();//����TestServer
            // act
            var response = await host.GetTestClientWithToken("2000031", "Staff").PostAsync(url, content);
            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }

        // �������Ϸ���Id�����ڣ�
        [Test]
        public async Task TaskTestSetState2()
        {
            // arrange
            string url = $"{_breakUrl}approve";
            BreakApproveDto breakApproveDto = new BreakApproveDto
            {
                Id = "100000",
                State = BreakState.agreed
            };
            var json = JsonConvert.SerializeObject(breakApproveDto);
            var content = new StringContent(json, _encoding, _mediaType);
            using var host = await TestHostBuild.GetTestHost().StartAsync();//����TestServer
            // act
            var response = await host.GetTestClientWithToken("2000031", "Staff").PostAsync(url, content);
            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
        }

        // һ������
        [Test]
        public async Task TaskTestSetState3()
        {
            // arrange
            string url = $"{_breakUrl}approve";
            BreakApproveDto breakApproveDto = new BreakApproveDto
            {
                Id = "a8b0ae9b-e33a-4a7c-b472-e8251c4d40f0",
                State = BreakState.agreed
            };
            var json = JsonConvert.SerializeObject(breakApproveDto);
            var content = new StringContent(json, _encoding, _mediaType);
            using var host = await TestHostBuild.GetTestHost().StartAsync();//����TestServer
            // act
            var response = await host.GetTestClientWithToken("2000031", "Staff").PostAsync(url, content);
            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }
    }
}