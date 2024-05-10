using System;
using System.Collections.Generic;

namespace RegisterCreditsManageApp.Models;

public partial class RegisterCredit
{
    public int IdRegisterCredits { get; set; }

    public string IdStudent { get; set; } = null!;

    public string IdClassRoom { get; set; } = null!;

    public int IdSubject { get; set; }

    public int? IdTeacher { get; set; }

    public bool? IsPass { get; set; }

    public bool IsRegister { get; set; }

    public virtual ClassRoom IdClassRoomNavigation { get; set; } = null!;

    public virtual Subject IdSubjectNavigation { get; set; } = null!;
}
