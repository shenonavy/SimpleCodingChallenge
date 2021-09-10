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
    public class EmployeeAddCommand : IRequest<EmployeeAddCommandResponse>
    {
        public EmployeeDto employeeDto { get; set; }
        public EmployeeAddCommand(EmployeeDto employeeDto)
        {
            this.employeeDto = employeeDto;
        }
    }
    public class EmployeeAddCommandResponse
    {
        public EmployeeDto EmployeeObj { get; set; }
    }

    public class EmployeeAddCommandHandler : IRequestHandler<EmployeeAddCommand, EmployeeAddCommandResponse>
    {
        private readonly SimpleCodingChallengeDbContext dbContext;
        private readonly IMapper mapper;

        public EmployeeAddCommandHandler(SimpleCodingChallengeDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<EmployeeAddCommandResponse> Handle(EmployeeAddCommand request, CancellationToken cancellationToken)
        {
            var _guid = Guid.NewGuid();
            var employee = new Employee();
            mapper.Map(request.employeeDto, employee);

            employee.ID = _guid;

            dbContext.Add(employee);
            await dbContext.SaveChangesAsync();

            var EmpData = mapper.Map<EmployeeDto>(employee);

            return new EmployeeAddCommandResponse
            {
                EmployeeObj = EmpData
            };
        }
    }
}