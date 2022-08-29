using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Dtos
{
    public class StaffForRefine
    {
        public Position Position { get; set; }
        public string Skilled { get; set; }
        public string Introduction { get; set; }
    }
}
