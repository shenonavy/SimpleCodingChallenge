using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleCodingChallenge.Common.DTO;
using SimpleCodingChallenge.DataAccess.Database;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using SimpleCodingChallenge.DataAccess.Entity;
using System;

namespace SimpleCodingChallenge.Business.Actions.Employees
{
    public class CreateNewEmployeeCommand : IRequest<CreateNewEmployeeCommandResult>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public string JobTitle { get; set; }
        public double Salary { get; set; }
        public string Department { get; set; }
        public string Country { get; set; }
    }
    public class CreateNewEmployeeCommandResult
    {
        public EmployeeDto Employee { get; set; }
    }

    public class CreateNewEmployeeCommandHandler : IRequestHandler<CreateNewEmployeeCommand, CreateNewEmployeeCommandResult>
    {
        private readonly SimpleCodingChallengeDbContext dbContext;
        private readonly IMapper mapper;

        public CreateNewEmployeeCommandHandler(SimpleCodingChallengeDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<CreateNewEmployeeCommandResult> Handle(CreateNewEmployeeCommand request, CancellationToken cancellationToken)
        {
            var newEmployeeID = await dbContext.GetNextEmployeeID();
            var employee = new Employee
            {
                ID = Guid.NewGuid(),
                EmployeeID = newEmployeeID,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                BirthDate = request.BirthDate,
                Country = string.IsNullOrEmpty(request.Country) ? "N/A" : request.Country,
                Department = request.Department,
                Email = request.Email,
                JobTitle = request.JobTitle,
                Salary = request.Salary
            };

            dbContext.Add(employee);
            await dbContext.SaveChangesAsync();

            var EmpData = mapper.Map<EmployeeDto>(employee);

            return new CreateNewEmployeeCommandResult
            {
                Employee = EmpData
            };
        }
    }
}