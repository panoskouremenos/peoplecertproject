using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace dbmanip.Models{
public class UserRole
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RoleID { get; set; }
    public string RoleName { get; set; }
    public string RoleDescription { get; set; }
}
}