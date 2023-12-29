using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace dbmanip.Models{
public class Candidates
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CandidateID { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public string NativeLanguage { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public string LandlineNumber { get; set; }
    public string MobileNumber { get; set; }
    public int UserTblID { get; set; }

    [ForeignKey("UserTblID")]
    public Usertbl User { get; set; }
}
}