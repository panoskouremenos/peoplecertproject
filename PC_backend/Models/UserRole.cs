using System;
using System.Collections.Generic;

namespace PC_backend.Models;

public partial class UserRole
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public string? RoleDescription { get; set; }

    public virtual ICollection<Usertbl> Usertbls { get; set; } = new List<Usertbl>();
}
