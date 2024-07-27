using System.ComponentModel.DataAnnotations;

namespace Employment.Services.Api.Models
{
    public class UserModel
    {
        public string Name { get; set; }
        public int ICNumber { get; set; }
        public string MobileNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

           
    }
}
