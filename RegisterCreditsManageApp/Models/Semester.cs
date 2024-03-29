using System;
using System.Collections.Generic;

namespace RegisterCreditsManageApp.Models;

public partial class Semester
{
    public int IdSemester { get; set; }

    public string SemesterName { get; set; } = null!;

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();

    public virtual ICollection<Major> IdMajors { get; set; } = new List<Major>();
}
