using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleCodingChallenge.Common.DTO;
using SimpleCodingChallenge.DataAccess.Database;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace SimpleCodingChallenge.Business.Actions.Employees
{
    public class GetAllEmployeesCommand : IRequest<GetAllEmployeesCommandResponse>
    {

    }

    public class GetAllEmployeesCommandResponse
    {
        public List<EmployeeDto> EmployeeList { get; set; }
    }

    public class GetAllEmployeesCommandHandler : IRequestHandler<GetAllEmployeesCommand, GetAllEmployeesCommandResponse>
    {
        private readonly SimpleCodingChallengeDbContext dbContext;
        private readonly IMapper mapper;

        public GetAllEmployeesCommandHandler(SimpleCodingChallengeDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<GetAllEmployeesCommandResponse> Handle(GetAllEmployeesCommand request, CancellationToken cancellationToken)
        {
            var employees = await dbContext.Employees.ToListAsync();
            var dataList = mapper.Map<List<EmployeeDto>>(employees);
            return new GetAllEmployeesCommandResponse
            {
                EmployeeList = dataList
            };
        }
    }
}
