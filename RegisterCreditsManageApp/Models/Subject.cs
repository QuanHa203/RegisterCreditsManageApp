using System;
using System.Collections.Generic;

namespace RegisterCreditsManageApp.Models;

public partial class Subject
{
    public int IdSubject { get; set; }

    public int IdSemester { get; set; }

    public int IdMajors { get; set; }

    public string Name { get; set; } = null!;

    public int NumberOfCredits { get; set; }

    public virtual ICollection<ClassRoom> ClassRooms { get; set; } = new List<ClassRoom>();

    public virtual Major IdMajorsNavigation { get; set; } = null!;

    public virtual Semester IdSemesterNavigation { get; set; } = null!;
}
