using System;
using System.Collections.Generic;

namespace RegisterCreditsManageApp.Models;

public partial class User
{
    public int IdUser { get; set; }

    public int IdRole { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual Role IdRoleNavigation { get; set; } = null!;

    public virtual Student? Student { get; set; }
}
