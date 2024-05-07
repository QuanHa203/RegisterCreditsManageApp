using System;
using System.Collections.Generic;

namespace RegisterCreditsManageApp.Models;

public partial class Student
{
    public string IdStudent { get; set; } = null!;

    public int IdMajors { get; set; }

    public int IdMainClass { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public bool Gender { get; set; }

    public byte[]? Avatar { get; set; }

    public virtual MainClass IdMainClassNavigation { get; set; } = null!;

    public virtual Major IdMajorsNavigation { get; set; } = null!;

    public virtual User IdStudentNavigation { get; set; } = null!;
}
