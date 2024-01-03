using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PC_backend.Models;

public partial class Candidate
{

    public int CandidateId { get; set; }
	public string? FirstName { get; set; }
    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? Gender { get; set; }

    public string? NativeLanguage { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? Email { get; set; }

    public string? LandlineNumber { get; set; }

    public string? MobileNumber { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<CandidateAddress> CandidateAddresses { get; set; } = new List<CandidateAddress>();

    public virtual ICollection<CandidatePhotoId> CandidatePhotoIds { get; set; } = new List<CandidatePhotoId>();

    public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();

    public virtual ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();

    public virtual ICollection<ExamVoucher> ExamVouchers { get; set; } = new List<ExamVoucher>();

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual Usertbl? User { get; set; }
}
