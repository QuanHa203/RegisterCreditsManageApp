using System;
using System.Collections.Generic;

namespace RegisterCreditsManageApp.Models;

public partial class Subject
{
    public int IdSubject { get; set; }

    public int IdSemester { get; set; }

    public int? IdRegisterCredits { get; set; }

    public string SubjectName { get; set; } = null!;

    public int NumberOfCredits { get; set; }

    public bool? IsPass { get; set; }

    public virtual RegisterCredit? IdRegisterCreditsNavigation { get; set; }

    public virtual Semester IdSemesterNavigation { get; set; } = null!;
}
