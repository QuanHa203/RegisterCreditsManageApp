using System;
using System.Collections.Generic;

namespace RegisterCreditsManageApp.Models;

public partial class RegisterSubject
{
    public int IdRegisterSubject { get; set; }

    public int IdSemester { get; set; }

    public string Name { get; set; } = null!;

    public int NumberOfCredits { get; set; }

    public bool IsRegister { get; set; }

    public virtual ICollection<RegisterCredit> RegisterCredits { get; set; } = new List<RegisterCredit>();
}
