﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment.Application.DTOs
{
    public class VacancyDTO
    {
        public int Id { get; set; }

        public string VacancyName { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public int? MaxNumberOfApplications { get; set; }

        public bool? IsExpired { get; set; }

        public bool? IsPosted { get; set; }

        public bool? IsActive { get; set; }
        public int CurrentNumberOfApplication { get; set; } 

    }
}
