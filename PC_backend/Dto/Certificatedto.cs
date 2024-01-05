using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PC_backend.Dto
{
    public class Certificatedto
    {

        public string? Title { get; set; }

        public string? AssessmentTestCode { get; set; }

        public int? MinimumScore { get; set; }

        public int? MaximumScore { get; set; }

        public virtual ICollection<CertificateTopicMarkdto> CertificateTopicMarks { get; set; } = new List<CertificateTopicMarkdto>();

        //public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    }
}
