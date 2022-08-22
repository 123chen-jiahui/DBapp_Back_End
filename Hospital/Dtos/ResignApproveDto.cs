using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Dtos
{
    public class ResignApproveDto
    {
        public Guid Id { get; set; }
        public ResignState State { get; set; }  //前端提供选择,2通过，3拒绝
    }
}
