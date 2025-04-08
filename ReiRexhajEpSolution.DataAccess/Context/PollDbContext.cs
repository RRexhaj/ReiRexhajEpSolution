using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReiRexhajEpSolution.Domain.Models;

namespace ReiRexhajEpSolution.DataAccess.Context
{
    public class PollDbContext : IdentityDbContext
    {
        // Constructor to pass DbContext options
        public PollDbContext(DbContextOptions<PollDbContext> options) : base(options) { }

        // DbSet for Poll entity
        public DbSet<Poll> Polls { get; set; }

        // Configure entity relationships and constraints
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Poll entity
            modelBuilder.Entity<Poll>(entity =>
            {
                entity.HasKey(p => p.Id); // Primary key

                entity.Property(p => p.Title)
                      .IsRequired()
                      .HasMaxLength(200); // Title is required with a max length of 200

                entity.Property(p => p.Option1Text)
                      .HasMaxLength(100); // Option1Text max length

                entity.Property(p => p.Option2Text)
                      .HasMaxLength(100); // Option2Text max length

                entity.Property(p => p.Option3Text)
                      .HasMaxLength(100); // Option3Text max length

                entity.Property(p => p.DateCreated)
                      .HasDefaultValueSql("GETDATE()"); // Default value for DateCreated
            });
        }

        // Configure the database connection with retry logic
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=DESKTOP-K9QLOED\\SQLEXPRESS,1433;Database=PollDb;Trusted_Connection=True;MultipleActiveResultSets=true",
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(10),
                            errorNumbersToAdd: null
                        );
                        sqlOptions.CommandTimeout(60); // Increase timeout to 60 seconds
                    }
                );
            }
        }


    }
}
