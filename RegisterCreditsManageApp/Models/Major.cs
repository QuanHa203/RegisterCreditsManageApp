using System;
using System.Collections.Generic;

namespace RegisterCreditsManageApp.Models;

public partial class Major
{
    public int IdMajors { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<MainClass> MainClasses { get; set; } = new List<MainClass>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
