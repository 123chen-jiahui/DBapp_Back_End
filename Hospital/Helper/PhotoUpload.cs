using Aliyun.OSS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hospital.Helper
{
    public class PhotoUpload
    {
        public static string UploadPhoto(string ImgBase64, string objectPath)
        {
            // yourEndpoint填写Bucket所在地域对应的Endpoint。以华东1（杭州）为例，Endpoint填写为https://oss-cn-hangzhou.aliyuncs.com。
            var endpoint = "https://oss-cn-shanghai.aliyuncs.com";
            var accessKeyId = "LTAI5t8A5fZEVnXHDvmagwZt";
            var accessKeySecret = "b23SvypcKCkVgUoQXAfI5A9kgVH98I";
            var bucketName = "tongjihospital-data";

            try
            {
                //string ImgBase64 = Request.Form["base"];
                string pattern = @"^data:image/(?<type>\w+);";
                string type = Regex.Matches(ImgBase64, pattern)[0].Groups["type"].Value;
                string objectName = objectPath + "." + type;

                byte[] arr = Convert.FromBase64String(ImgBase64.Split(',')[1]);//.Split(',')[1]
                MemoryStream ms = new MemoryStream(arr);

                // 创建OssClient实例。
                var client = new OssClient(endpoint, accessKeyId, accessKeySecret);


                var res = client.PutObject(bucketName, objectName, ms, new ObjectMetadata() { ContentType = "image/" + type });
                string imgurl = "https://tongjihospital-data.oss-cn-shanghai.aliyuncs.com/" + objectName;

                Console.WriteLine("Put object succeeded");
                return imgurl; // 返回url
            }
            catch (Exception ex)
            {
                Console.WriteLine("Put object failed, {0}", ex.Message);
                return null;
            }
        }
    }
}
