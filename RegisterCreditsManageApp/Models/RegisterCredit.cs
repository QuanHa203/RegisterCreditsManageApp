using System;
using System.Collections.Generic;

namespace RegisterCreditsManageApp.Models;

public partial class RegisterCredit
{
    public int IdRegisterCredits { get; set; }

    public int IdTeacher { get; set; }

    public bool Status { get; set; }

    public int Capacity { get; set; }

    public int CurrentStudent { get; set; }

    public string Schedule { get; set; } = null!;

    public string ClassName { get; set; } = null!;

    public virtual Teacher IdTeacherNavigation { get; set; } = null!;

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
