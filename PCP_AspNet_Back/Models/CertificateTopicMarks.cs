using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace dbmanip.Models{
public class CertificateTopicMarks
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CertificateTopicMarksID { get; set; }
    public int CertificateID { get; set; }
    public string TopicDesc { get; set; }
    public int NumberOfAwardedMarks { get; set; }
    public int NumberOfPossibleMarks { get; set; }

    [ForeignKey("CertificateID")]
    public Certificate Certificate { get; set; }
}
}