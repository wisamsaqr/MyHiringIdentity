using Hiring.Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace Hiring.Data.Models
{
    public class User : IdentityUser
    {
        public UserType UserType { get; set; }
        public Admin Admin { get; set; }
        public JobSeeker JobSeeker { get; set; }

        public int? FoundationId { get; set; }
        public Foundation Foundation { get; set; }

        public bool IsDelete { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}