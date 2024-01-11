using PC_backend.Models;

namespace PC_backend.Dto
{
    public class CertificateTopicMarkdto
    {
       
       // public int? CertificateId { get; set; }

        public string? TopicDesc { get; set; }

        //public virtual Certificate? Certificate { get; set; }

        public virtual ICollection<Questiondto> Questions { get; set; } = new List<Questiondto>();
    }
}
