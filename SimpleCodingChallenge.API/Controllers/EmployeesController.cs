using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleCodingChallenge.Business.Actions.Employees;
using SimpleCodingChallenge.Common.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleCodingChallenge.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class EmployeesController : Controller
    {
        private readonly IMediator mediator;

        public EmployeesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<EmployeeDto>>> Index()
        {
            var result = await mediator.Send(new GetAllEmployeesCommand());
            return result.EmployeeList;
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult<EmployeeDto>> Create(EmployeeDto employeeDto)
        {
            var output = await mediator.Send(new GetEmployeeByFilterCommand(Filter: "EmployeeId", Value: employeeDto.EmployeeID));

            if(output.EmployeeObj != null) return BadRequest("Employee Id already exists!");

            output = null;

            output = await mediator.Send(new GetEmployeeByFilterCommand(Filter: "Email", Value: employeeDto.Email));

            if(output.EmployeeObj != null) return BadRequest("Email already exists!");

            var result = await mediator.Send(new EmployeeAddCommand(employeeDto: employeeDto));
            
            return Ok(result.EmployeeObj);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<ActionResult<EmployeeDto>> GetByID(string Id)
        {
            if(!string.IsNullOrEmpty(Id))
            {
                var result = await mediator.Send(new GetEmployeeByIdCommand(EmployeeId: Id));
                if(result.EmployeeObj != null) return result.EmployeeObj;
                else return BadRequest("Id not found!");
            }
            else return BadRequest("Id cannot be empty!");
        }
    }
}
