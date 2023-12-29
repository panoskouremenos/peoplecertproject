using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbmanip.Models{
public class CandidatePhotoID
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PhotoID { get; set; }
    public int CandidateID { get; set; }
    public string PhotoIDType { get; set; }
    public string PhotoIDNumber { get; set; }
    public DateTime PhotoIDIssueDate { get; set; }

    [ForeignKey("CandidateID")]
    public Candidates Candidate { get; set; }
}
}