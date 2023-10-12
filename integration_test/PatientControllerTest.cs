using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApiTests;

namespace integration_test
{
    class PatientControllerTest
    {
        const string _testUrl = "/patients/";
        const string _mediaType = "application/json";
        readonly Encoding _encoding = Encoding.UTF8;
        
        [Test]
        [TestCase(1000000)]
        [TestCase(1000001)]
        [TestCase(1000002)]
        [TestCase(2000000)]
        [TestCase(2000000)]
        public async Task TestGetPatientByPatientId(int patientId)
        {
            // arrange
            string url = $"{_testUrl}{patientId}";
            using var host = await TestHostBuild.GetTestHost().StartAsync();//启动TestServer

            // act
            var response = await host.GetTestClientWithToken(patientId.ToString(), "Patient").GetAsync(url);

            // assert
            if (patientId == 1000000)
                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            else
                Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        [TestCase("陈", true)]
        [TestCase("家", true)]
        [TestCase("辉", true)]
        [TestCase("躞", false)]
        [TestCase("1", false)]
        public async Task TestGetPatientsByName(string keyWord, bool flag)
        {
            // arrange
            string url = $"{_testUrl}";
            using var host = await TestHostBuild.GetTestHost().StartAsync();//启动TestServer

            // act
            var response = await host.GetTestClientWithToken(keyWord, "Patient").GetAsync(url);

            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        [TestCase(1000000)]
        [TestCase(1000001)]
        [TestCase(1000002)]
        [TestCase(2000000)]
        [TestCase(2000000)]
        public async Task TestGetPatientDetail(int patientId)
        {
            // arrange
            string url = $"{_testUrl}detail/{patientId}";
            using var host = await TestHostBuild.GetTestHost().StartAsync();//启动TestServer

            // act
            var response = await host.GetTestClientWithToken(patientId.ToString(), "Patient").GetAsync(url);

            // assert
            if (patientId == 1000000)
                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            else
                Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }
    }
}
