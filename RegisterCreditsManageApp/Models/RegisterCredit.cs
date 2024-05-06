using System;
using System.Collections.Generic;

namespace RegisterCreditsManageApp.Models;

public partial class RegisterCredit
{
    public int IdRegisterCredits { get; set; }

    public int IdStudent { get; set; }

    public int IdClassRoom { get; set; }

    public int IdSubject { get; set; }

    public int IdTeacher { get; set; }

    public bool? IsPass { get; set; }

    public virtual ClassRoom IdClassRoomNavigation { get; set; } = null!;

    public virtual Subject IdSubjectNavigation { get; set; } = null!;
}
