﻿using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Dtos
{
    public class PatientDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public ICollection<MedicalRecordDto> MedicalRecords { get; set; }
        public ShoppingCartDto ShoppingCart { get; set; }
    }
}
