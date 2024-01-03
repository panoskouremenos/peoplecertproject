using System;
using System.Collections.Generic;

namespace PC_backend.Models;

public partial class Usertbl
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? PasswordHash { get; set; }

    public int? RoleId { get; set; }

    public int? IsActive { get; set; }

    public virtual ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();

    public virtual UserRole? Role { get; set; }
}
