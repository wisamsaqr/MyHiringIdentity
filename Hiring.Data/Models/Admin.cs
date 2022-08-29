namespace Hiring.Data.Models
{
    public class Admin : BaseEntity
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        public string? ImageUrl { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}