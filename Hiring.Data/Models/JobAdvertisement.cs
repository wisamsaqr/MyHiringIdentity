using Hiring.Data.Enums;

namespace Hiring.Data.Models
{
    public class JobAdvertisement : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime Deadline { get; set; }

        public int FoundationId { get; set; }
        public Foundation Foundation { get; set; }

        public List<JobAdvertisementAttachment> Attachments { get; set; }

        public JobAdvertisementStatus Status { get; set; }

        public List<JobAdvertisementJobSeeker> JobSeekers { get; set; }    // Could be named Applicants
    }
}