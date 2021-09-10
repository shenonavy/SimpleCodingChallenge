using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleCodingChallenge.Common.DTO;
using SimpleCodingChallenge.DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace SimpleCodingChallenge.Business.Actions.Employees
{
    public class GetEmployeeByFilterCommand : IRequest<GetEmployeeByFilterCommandResponse>
    {
        public string _Filter { get; set; }
        public string _Value { get; set; }
        public GetEmployeeByFilterCommand(string Filter, string Value)
        {
            _Filter = Filter;
            _Value = Value;
        }
    }

    public class GetEmployeeByFilterCommandResponse
    {
        public EmployeeDto EmployeeObj { get; set; }
    }

    public class GetEmployeeByFilterCommandHandler : IRequestHandler<GetEmployeeByFilterCommand, GetEmployeeByFilterCommandResponse>
    {
        private readonly SimpleCodingChallengeDbContext dbContext;
        private readonly IMapper mapper;

        public GetEmployeeByFilterCommandHandler(SimpleCodingChallengeDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<GetEmployeeByFilterCommandResponse> Handle(GetEmployeeByFilterCommand request, CancellationToken cancellationToken)
        {
            var employee = request._Filter switch
            {
                "ID" => await dbContext.Employees.Where(x=> x.ID == new Guid(request._Value)).SingleOrDefaultAsync(),
                "EmployeeId" => await dbContext.Employees.Where(x=> x.EmployeeID.ToLower() == request._Value.ToLower()).SingleOrDefaultAsync(),
                "Email" => await dbContext.Employees.Where(x=> x.Email.ToLower() == request._Value.ToLower()).SingleOrDefaultAsync(),
                _ => null
            };

            var EmpData = mapper.Map<EmployeeDto>(employee);
            return new GetEmployeeByFilterCommandResponse
            {
                EmployeeObj = EmpData
            };
        }
    }
}