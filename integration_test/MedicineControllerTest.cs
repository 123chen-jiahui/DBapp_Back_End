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
    class MedicineControllerTest
    {
        const string _testUrl = "/medicine/";
        const string _mediaType = "application/json";
        readonly Encoding _encoding = Encoding.UTF8;

        // 权限错误
        [Test]
        public async Task TestGetMedicinesCount()
        {
            // arrange
            string url = $"{_testUrl}count?keyWord=";
            using var host = await TestHostBuild.GetTestHost().StartAsync();//启动TestServer

            // act
            var response = await host.GetTestClientWithToken("1000000", "Patient").GetAsync(url);
            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Forbidden);
        }

        // 输入参数为空
        [Test]
        public async Task TestGetMedicinesCount2()
        {
            // arrange
            string url = $"{_testUrl}count?keyWord=";
            using var host = await TestHostBuild.GetTestHost().StartAsync();//启动TestServer

            // act
            var response = await host.GetTestClientWithToken("2000031", "Doctor").GetAsync(url);
            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
        }

        // 输入参数不合法
        [Test]
        public async Task TestGetMedicinesCount3()
        {
            // arrange
            string url = $"{_testUrl}count?keyWord=jiji";
            using var host = await TestHostBuild.GetTestHost().StartAsync();//启动TestServer

            // act
            var response = await host.GetTestClientWithToken("2000031", "Doctor").GetAsync(url);
            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
        }

        // 输入参数不合法
        [Test]
        public async Task TestGetMedicinesCount4()
        {
            // arrange
            string url = $"{_testUrl}count?keyWord=连花清瘟胶囊";
            using var host = await TestHostBuild.GetTestHost().StartAsync();//启动TestServer

            // act
            var response = await host.GetTestClientWithToken("2000031", "Doctor").GetAsync(url);
            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }


    }
}
