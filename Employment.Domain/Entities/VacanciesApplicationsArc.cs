using System;
using System.Collections.Generic;

namespace Employment.Domain.Entities;

public partial class VacanciesApplicationsArc
{
    public int Id { get; set; }

    public int? FkVacancyId { get; set; }

    public string FkApplicantId { get; set; }

    public DateTime? ApplicationDate { get; set; }

    public virtual AspNetUser FkApplicant { get; set; }
}
