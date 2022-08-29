using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Dtos
{
    // 用于返回展示
    public class StaffDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int age { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }

        // 添加的返回显示
        public string Position { get; set; }
        public string Skilled { get; set; }
        public string Introduction { get; set; }
        public string Photo { get; set; }
    }
}
