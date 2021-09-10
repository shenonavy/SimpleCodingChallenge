using Microsoft.EntityFrameworkCore;
using SimpleCodingChallenge.DataAccess.Entity;

namespace SimpleCodingChallenge.DataAccess.Database
{
    public class SimpleCodingChallengeDbContext : DbContext
    {
        public SimpleCodingChallengeDbContext(DbContextOptions<SimpleCodingChallengeDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SimpleCodingChallengeDbContext).Assembly);
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
