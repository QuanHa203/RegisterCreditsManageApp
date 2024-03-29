using System;
using System.Collections.Generic;

namespace RegisterCreditsManageApp.Models;

public partial class Teacher
{
    public int IdTeacher { get; set; }

    public int IdMajors { get; set; }

    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<RegisterCredit> RegisterCredits { get; set; } = new List<RegisterCredit>();
}
