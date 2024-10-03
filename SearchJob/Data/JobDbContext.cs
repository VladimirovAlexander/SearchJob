using Microsoft.EntityFrameworkCore;
using SearchJob.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace SearchJob.Data
{
    public class JobDbContext : IdentityDbContext<AppUser>
    {

        public DbSet<Job> Jobs { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        
        public JobDbContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Favorite>(x => x.HasKey(p => new{ p.JobId, p.AppUserId }));
            modelBuilder.Entity<Favorite>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Favorites)
                .HasForeignKey(u => u.AppUserId);

            modelBuilder.Entity<Favorite>()
                .HasOne(u => u.Job)
                .WithMany(u => u.Favorites)
                .HasForeignKey(u => u.JobId);

            List<IdentityRole> identityRoles = new List<IdentityRole>()
            {
                new IdentityRole
                {

                    Name = "Admin",
                    NormalizedName = "ADMIN"

                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }

            };
            modelBuilder.Entity<IdentityRole>().HasData(identityRoles);

            

        }
    }
}
