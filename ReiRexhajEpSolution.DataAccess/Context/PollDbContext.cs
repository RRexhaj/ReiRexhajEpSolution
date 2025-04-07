using Microsoft.EntityFrameworkCore;
using ReiRexhajEpSolution.Domain.Models;
using System.Collections.Generic;

namespace ReiRexhajEpSolution.DataAccess.Context
{
    public class PollDbContext : DbContext
    {
        public PollDbContext(DbContextOptions<PollDbContext> options) : base(options)
        {
        }

        public DbSet<Poll> Polls { get; set; }
    }
}
