using Employment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment.Application.DTOs
{
    public class VacanciesApplicationDTO
    {
       
        public int Id { get; set; }

        public int? FkVacancyId { get; set; }

        public string FkApplicantId { get; set; }

        public DateTime? ApplicationDate { get; set; }

    }
}
