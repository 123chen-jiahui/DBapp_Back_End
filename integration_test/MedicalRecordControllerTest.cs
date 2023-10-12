using Hospital.Dtos;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiTests;

namespace integration_test
{
    class MedicalRecordControllerTest
    {
        const string _testUrl = "/api/medicalRecord/";
        const string _mediaType = "application/json";
        readonly Encoding _encoding = Encoding.UTF8;

        // 输入参数为空
        [Test]
        public async Task TestGetMedicalRecord()
        {
            // arrange
            string url = $"{_testUrl}";
            using var host = await TestHostBuild.GetTestHost().StartAsync();//启动TestServer

            // act
            var response = await host.GetTestClientWithToken("1000000", "Patient").GetAsync(url);
            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.MethodNotAllowed);
            // response.EnsureSuccessStatusCode();
        }

        // 输入参数不合法
        [Test]
        public async Task TestGetMedicalRecord2()
        {
            // arrange
            int patientId = 0;
            string url = $"{_testUrl}{patientId}";
            using var host = await TestHostBuild.GetTestHost().StartAsync();//启动TestServer

            // act
            var response = await host.GetTestClientWithToken("1000000", "Patient").GetAsync(url);
            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            // response.EnsureSuccessStatusCode();
        }

        // 正常情况
        [Test]
        public async Task TestGetMedicalRecord3()
        {
            // arrange
            int patientId = 1000000;
            string url = $"{_testUrl}{patientId}";
            using var host = await TestHostBuild.GetTestHost().StartAsync();//启动TestServer

            // act
            var response = await host.GetTestClientWithToken("1000000", "Patient").GetAsync(url);
            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            // response.EnsureSuccessStatusCode();
        }

        // 权限不正确
        [Test]
        public async Task TestCreatMedicalRecord()
        {
            // arrange
            MedicalRecordForCreationDto medicalRecordForCreationDto = new MedicalRecordForCreationDto
            {
                PatientId = 1000000,
                StaffId = 2000031,
                DiagnosticResult = "多穿衣服"
            };
            var json = JsonConvert.SerializeObject(medicalRecordForCreationDto);
            var content = new StringContent(json, _encoding, _mediaType);
            string url = $"{_testUrl}";
            using var host = await TestHostBuild.GetTestHost().StartAsync();//启动TestServer

            // act
            var response = await host.GetTestClientWithToken("1000000", "Patient").PostAsync(url, content);
            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Forbidden);
            // response.EnsureSuccessStatusCode();
        }

        // 输出参数为空
        [Test]
        public async Task TestCreatMedicalRecord2()
        {
            // arrange
            var content = new StringContent("", _encoding, _mediaType);
            string url = $"{_testUrl}";
            using var host = await TestHostBuild.GetTestHost().StartAsync();//启动TestServer

            // act
            var response = await host.GetTestClientWithToken("2000031", "Doctor").PostAsync(url, content);
            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
            // response.EnsureSuccessStatusCode();
        }

        // 一切正常
        [Test]
        public async Task TestCreatMedicalRecord3()
        {
            // arrange
            MedicalRecordForCreationDto medicalRecordForCreationDto = new MedicalRecordForCreationDto
            {
                PatientId = 1000000,
                StaffId = 2000031,
                DiagnosticResult = "多穿衣服"
            };
            var json = JsonConvert.SerializeObject(medicalRecordForCreationDto);
            var content = new StringContent(json, _encoding, _mediaType);
            string url = $"{_testUrl}";
            using var host = await TestHostBuild.GetTestHost().StartAsync();//启动TestServer

            // act
            var response = await host.GetTestClientWithToken("2000031", "Doctor").PostAsync(url, content);
            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }
    }
}
