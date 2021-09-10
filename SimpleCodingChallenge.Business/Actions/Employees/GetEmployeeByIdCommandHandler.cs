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
    public class GetEmployeeByIdCommand : IRequest<GetEmployeeByIdCommandResponse>
    {
        public string EmployeeID { get; set; }
    }
    
    public class GetEmployeeByIdCommandResponse
    {
        public EmployeeDto Employee { get; set; }
    }

    public class GetEmployeeByIdCommandHandler : IRequestHandler<GetEmployeeByIdCommand, GetEmployeeByIdCommandResponse>
    {
        private readonly SimpleCodingChallengeDbContext dbContext;
        private readonly IMapper mapper;

        public GetEmployeeByIdCommandHandler(SimpleCodingChallengeDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<GetEmployeeByIdCommandResponse> Handle(GetEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            var employee = await dbContext.Employees.SingleOrDefaultAsync(x=> x.EmployeeID.Equals(request.EmployeeID));
            return new GetEmployeeByIdCommandResponse
            {
                Employee = mapper.Map<EmployeeDto>(employee)
            };
        }
    }
}