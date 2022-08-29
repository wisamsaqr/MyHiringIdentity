using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Hiring.Data.Models;

namespace Hiring.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Foundation> Foundations { get; set; }
        public DbSet<JobSeeker> JobSeekers { get; set; }
        public DbSet<JobAdvertisement> JobAdvertisements { get; set; }
        public DbSet<JobAdvertisementAttachment> JobAdvertisementAttachments { get; set; }
        public DbSet<JobAdvertisementJobSeeker> JobAdvertisementJobSeekers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<JobSeeker>()
            //    .HasOne<User>(j => j.User)
            //    .WithOne(u => u.JobSeeker)
            //    .HasForeignKey<JobSeeker>(j => j.UserId);

            builder.Entity<Admin>()
                .HasOne<User>(x => x.User)
                .WithOne(x => x.Admin)
                .HasForeignKey<Admin>(x => x.UserId);

            builder.Entity<JobAdvertisementJobSeeker>().HasKey(x => new { x.JobAdvertisementId, x.JobSeekerId});
        }
    }
}