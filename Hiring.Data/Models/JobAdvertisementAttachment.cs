using System.ComponentModel.DataAnnotations;

namespace Hiring.Data.Models
{
    public class JobAdvertisementAttachment
    {
        public int Id { get; set; }
        
        [Required]
        public string Url { get; set; }

        public int JobAdvertisementId { get; set; }
        public JobAdvertisement JobAdvertisement { get; set; }
    }
}