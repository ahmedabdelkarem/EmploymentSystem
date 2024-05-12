using System;
using System.Collections.Generic;

namespace Employment.Domain.Entities;

public partial class Vacancy
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

    public int? CurrentNumberOfApplication { get; set; }

    public virtual AspNetUser CreatedByNavigation { get; set; }

    public virtual ICollection<VacanciesApplication> VacanciesApplications { get; set; } = new List<VacanciesApplication>();
}
