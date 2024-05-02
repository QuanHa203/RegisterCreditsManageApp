using System;
using System.Collections.Generic;

namespace RegisterCreditsManageApp.Models;

public partial class Semester
{
    public int IdSemester { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<MainClass> MainClasses { get; set; } = new List<MainClass>();

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
