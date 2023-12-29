using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace dbmanip.Models{
public class CandidateAddress
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AddressID { get; set; }
    public int CandidateID { get; set; }
    public string Address { get; set; }
    public string AddressLine2 { get; set; }
    public string CountryOfResidence { get; set; }
    public string StateTerritoryProvince { get; set; }
    public string TownCity { get; set; }
    public string PostalCode { get; set; }

    [ForeignKey("CandidateID")]
    public Candidates Candidate { get; set; }
}
}