using System;
using System.Collections.Generic;

namespace RegisterCreditsManageApp.Models;

public partial class MainClass
{
    public int IdMainClass { get; set; }

    public int IdMajors { get; set; }

    public int IdCurrentRegisterSemester { get; set; }

    public string Name { get; set; } = null!;

    public int CourseYear { get; set; }

    public virtual Semester IdCurrentRegisterSemesterNavigation { get; set; } = null!;

    public virtual Major IdMajorsNavigation { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
