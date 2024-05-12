﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Employment.Domain.Entities;

public partial class AspNetUser : IdentityUser
{
    public string Id { get; set; }

    public string? Name { get; set; }

    public string Email { get; set; }

    public DateTime? BirthDate { get; set; }

    public string UserName { get; set; }

    public string NormalizedUserName { get; set; }

    public string NormalizedEmail { get; set; }

    public bool? EmailConfirmed { get; set; }

    public string PasswordHash { get; set; }

    public string SecurityStamp { get; set; }

    public string ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool? PhoneNumberConfirmed { get; set; }

    public bool? TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool? LockoutEnabled { get; set; }

    public int? AccessFailedCount { get; set; }

    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

    public virtual ICollection<Vacancy> Vacancies { get; set; } = new List<Vacancy>();

    public virtual ICollection<VacanciesApplication> VacanciesApplications { get; set; } = new List<VacanciesApplication>();

    public virtual ICollection<VacanciesApplicationsArc> VacanciesApplicationsArcs { get; set; } = new List<VacanciesApplicationsArc>();

    public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
}
