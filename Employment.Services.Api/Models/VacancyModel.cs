using System.ComponentModel.DataAnnotations;

namespace Employment.Services.Api.Models
{
    public class VacancyModel
    {

        public int Id { get; set; }

        [Required]
        public string VacancyName { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? StartDate { get; set; }
      
        public DateTime? ExpirationDate { get; set; }

        [Required]
        [Range (1,Double.PositiveInfinity, ErrorMessage = "Number Of Applications Is Required")]
        public int MaxNumberOfApplications { get; set; }

        public bool? IsExpired { get; set; }

        public bool? IsPosted { get; set; }

        public bool? IsActive { get; set; }
    }
}
