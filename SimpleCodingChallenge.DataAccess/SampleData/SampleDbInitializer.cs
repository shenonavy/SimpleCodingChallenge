using SimpleCodingChallenge.DataAccess.Database;
using SimpleCodingChallenge.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCodingChallenge.DataAccess.SampleData
{
    public static class SampleDbInitializer
    {
        public static async Task Initialize(this SimpleCodingChallengeDbContext dbContext)
        {
            if (dbContext.Employees.Any()) return;

            var employees = new List<Employee>
            {
                new Employee
                {
                    ID = Guid.NewGuid(),
                    EmployeeID = "UC001",
                    FirstName = "Minna",
                    LastName = "Amigon",
                    Address = "2371 Jerrold Ave",
                    Email = "minna_amigon@yahoo.com",
                    BirthDate = DateTimeOffset.Parse("1963-11-19"),
                    Department = "Company",
                    JobTitle = "CEO",
                    Salary = 450000
                },
                new Employee
                {
                    ID = Guid.NewGuid(),
                    EmployeeID = "UC002",
                    FirstName = "Blair",
                    LastName = "Malet",
                    Address = "209 Decker Dr",
                    Email = "bmalet@yahoo.com",
                    BirthDate = DateTimeOffset.Parse("1973-01-12"),
                    Department = "Marketing",
                    JobTitle = "Director",
                    Salary = 250000
                }
            };

            await dbContext.Employees.AddRangeAsync(employees);

            await dbContext.SaveChangesAsync();
        }
    }
}
