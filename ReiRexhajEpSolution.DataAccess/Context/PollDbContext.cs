using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReiRexhajEpSolution.Domain.Models;

namespace ReiRexhajEpSolution.DataAccess.Context
{
    public class PollDbContext : IdentityDbContext
    {
        public PollDbContext(DbContextOptions<PollDbContext> options)
            : base(options)
        {
        }

        // DbSet for Poll model
        public DbSet<Poll> Polls { get; set; }

        // Optional: further configuration
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Custom configurations if needed.
        }
    }
}
