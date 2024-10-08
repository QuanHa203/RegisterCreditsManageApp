﻿using System;
using System.Collections.Generic;

namespace RegisterCreditsManageApp.Models;

public partial class ClassRoom
{
    public string IdClassRoom { get; set; } = null!;

    public int IdMainClass { get; set; }

    public int IdSubject { get; set; }

    public int IdSemester { get; set; }

    public string Name { get; set; } = null!;

    public string Schedule { get; set; } = null!;

    public bool Status { get; set; }

    public int Capacity { get; set; }

    public int CurrentStudent { get; set; }

    public DateOnly StartRegisterDate { get; set; }

    public DateOnly EndRegisterDate { get; set; }

    public virtual Subject IdSubjectNavigation { get; set; } = null!;

    public virtual ICollection<RegisterCredit> RegisterCredits { get; set; } = new List<RegisterCredit>();
}
