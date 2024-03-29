using System;
using System.Collections.Generic;

namespace RegisterCreditsManageApp.Models;

public partial class Student
{
    public int Id { get; set; }

    public int IdCourseYear { get; set; }

    public int IdMajors { get; set; }

    public int IdClass { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public bool Gender { get; set; }

    public string EmailAddress { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual Class IdClassNavigation { get; set; } = null!;

    public virtual CourseYear IdCourseYearNavigation { get; set; } = null!;

    public virtual Major IdMajorsNavigation { get; set; } = null!;
}
