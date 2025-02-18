﻿using System.ComponentModel.DataAnnotations;

namespace Employment.Services.Api.Models
{
    public class UserModel
    {
            
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(100, MinimumLength = 6)]
            public string Password { get; set; }

            [Compare("Password")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string RoleName { get; set; }

           
    }
}
