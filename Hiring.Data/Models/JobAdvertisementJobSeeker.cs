namespace Hiring.Data.Models
{
    public class JobAdvertisementJobSeeker
    {
        public int JobAdvertisementId { get; set; }
        public JobAdvertisement JobAdvertisement { get; set; }

        public int JobSeekerId { get; set; }
        public JobSeeker JobSeeker { get; set; }
    }
}