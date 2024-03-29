using System;
using System.Collections.Generic;

namespace RegisterCreditsManageApp.Models;

public partial class Major
{
    public int IdMajors { get; set; }

    public int IdSemester { get; set; }

    public string MajorsName { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Semester> IdSemesters { get; set; } = new List<Semester>();
}
