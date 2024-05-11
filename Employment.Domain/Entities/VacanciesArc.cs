using System;
using System.Collections.Generic;

namespace Employment.Domain.Entities;

public partial class VacanciesArc
{
    public int Id { get; set; }

    public string VacancyName { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public int? NumberOfApplications { get; set; }

    public bool? IsExpired { get; set; }

    public bool? IsPosted { get; set; }

    public bool? IsActive { get; set; }
}
