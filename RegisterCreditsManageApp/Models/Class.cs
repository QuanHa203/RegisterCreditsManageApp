using System;
using System.Collections.Generic;

namespace RegisterCreditsManageApp.Models;

public partial class Class
{
    public int IdClass { get; set; }

    public string ClassName { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
