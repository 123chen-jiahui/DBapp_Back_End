using Autofac.Extensions.DependencyInjection;
using Hospital;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace WebApiTests
{
    public static class TestHostBuild
    {
        public static IHostBuilder GetTestHost()
        {
            //代码和网站Program中CreateHostBuilder代码很类似，去除了AddNlogService以免跑测试生成很多日志
            //如果网站并没有使用autofac替换原生DI容器，UseServiceProviderFactory这句话可以去除
            //关键是webBuilder中的UseTestServer，建立TestServer用于集成测试
            return new HostBuilder()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                .UseTestServer()//关键时多了这一行建立TestServer
                .UseStartup<Startup>();
            });
        }
        /// <summary>
        /// 生成带token的httpclient
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static HttpClient GetTestClientWithToken(this IHost host, string id, string role)
        {
            var client = host.GetTestClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {CreateJwtToken(id, role)}");//把token加到Header中
            return client;
        }

        public static string CreateJwtToken(string userId, string roleName)
        {
            // header：定义了token编码的算法，我们使用最通用的HmacSha256编码
            var signingAlgorithm = SecurityAlgorithms.HmacSha256;

            // payload：属于自定义的内容，根据项目需求进行填写，可能会用到用户的Id，用户名，email等
            // 需要使用Claim进行处理
            var claims = new List<Claim> // 声明一个claim数组
            {
                // 在jwt中，Id有一个专有名词叫sub
                new Claim(JwtRegisteredClaimNames.Sub, userId), // 第二个参数是用户Id的准确信息
                new Claim(ClaimTypes.Role, roleName) // 添加身份信息
            };

            // signiture：数字签名。签名需要私钥，可以把私钥放入配置文件appsetting.json
            // 然后通过IOC容器将配置文件服务的依赖通过构建函数注入进来，就像TouristRouteRepository.cs中的_context一样
            var secretByte = Encoding.UTF8.GetBytes("HelloWorldHelloWorld"); // 将私钥（字符串）通过UTF8编码转换为字节
            var signingKey = new SymmetricSecurityKey(secretByte); // 使用非对称算法对私钥进行加密
            var signingCredentials = new SigningCredentials(signingKey, signingAlgorithm); // 使用HmacSha256验证加密后的私钥
            // 使用这些数据创建jwttoken
            var token = new JwtSecurityToken(
                // issuer: "fakexiecheng.com", // 谁发布了token
                // audience: "fakexiecheng.com", // 该token将会发布给谁，就是项目前端，使用统一域名
                // 将以上两条放到appsetting中
                issuer: "TongjiHospital.com",
                audience: "TongjiHospital.com",
                claims,
                notBefore: DateTime.UtcNow, // 发布时间
                expires: DateTime.UtcNow.AddDays(31), // 有效期，一天
                signingCredentials // 数字签名
            );
            var s = new JwtSecurityTokenHandler().WriteToken(token);
            Console.WriteLine(s);
            return s;
            // return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
