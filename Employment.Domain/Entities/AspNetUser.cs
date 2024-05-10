using System;
using Employment.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using NetDevPack.Domain;

namespace Employment.Infra.Data
{
    public class AspNetUser : IdentityUser , IAggregateRoot
    {
        public AspNetUser(string id, string name, string email, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }

        // Empty constructor for EF
        protected AspNetUser() { }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

        public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

        public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();

    }
}