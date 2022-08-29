using System.ComponentModel.DataAnnotations;

namespace Hiring.Data.Models
{
    public class Foundation : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string WorkNature { get; set; }
        public string Address { get; set; }
        public List<User> Users { get; set; }
        public List<JobAdvertisement> JobAdvertisements { get; set; }
    }
}