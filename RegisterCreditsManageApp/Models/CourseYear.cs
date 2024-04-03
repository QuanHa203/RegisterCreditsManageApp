using System;
using System.Collections.Generic;

namespace RegisterCreditsManageApp.Models;

public partial class CourseYear
{
    public int IdCourseYear { get; set; }

    public int CourseYearName { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
