using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Dtos
{
    public class DepartmentDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Building { get; set; }
        public string Phone { get; set; }
        public ICollection<StaffDto> Staff { get; set; }
    }
}
