using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Dtos
{   // 注意枚举类型Role和Gender，在http请求Body中的json应该是以数字而不是字符串发送
    // 换句话说，不要带双引号！！！
    public class BreakApproveDto
    {
        public Guid Id { get; set; }   
        public BreakState State { get; set; }  //前端提供选择,2通过，3拒绝
    }
}
