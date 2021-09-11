using Dapper;
using Microsoft.EntityFrameworkCore;
using SimpleCodingChallenge.DataAccess.Entity;
using System.Threading.Tasks;

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

        public async Task<string> GetNextEmployeeID()
        {
            var newID = await Database.GetDbConnection().QuerySingleAsync<int>("select max(CAST(right(e.EmployeeID, len(e.EmployeeID) - 2) AS INT)) + 1 from Employees as e");
            return $"UC{newID}";
        }
    }
}
