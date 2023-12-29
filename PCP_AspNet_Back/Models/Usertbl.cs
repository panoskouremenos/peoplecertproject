using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace dbmanip.Models{
public class Usertbl
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserID { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public int UserRoleID { get; set; }
    public int IsActive { get; set; }

    [ForeignKey("UserRoleID")]
    public UserRole Role { get; set; }
}
}