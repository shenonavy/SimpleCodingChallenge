using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleCodingChallenge.DataAccess.Entity
{
    public class Employee
    {
        public Guid ID { get; set; }
        public string EmployeeID { get; set; }

        [StringLength(maximumLength: 100)]
        public string FirstName { get; set; }

        [StringLength(maximumLength: 100)]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public string JobTitle { get; set; }
        public double Salary { get; set; }
        public string Department { get; set; }
        public string Country { get; set; }

        public int Age
        {
            get
            {
                var age = DateTimeOffset.UtcNow.Year - BirthDate.Year;
                if (BirthDate > DateTimeOffset.UtcNow.AddYears(-age)) age--;
                return age;
            }
        }
    }

    public sealed class EmployeeEntityDescriptor : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.ID);

            builder.Property(e => e.EmployeeID)
                .IsRequired();
            builder.HasIndex(e => e.EmployeeID)
                .IsUnique();

            builder.Property(e => e.Email)
                .IsRequired();
            builder.HasIndex(e => e.Email)
                .IsUnique();

            builder.Ignore(e => e.Age);
        }
    }
}
