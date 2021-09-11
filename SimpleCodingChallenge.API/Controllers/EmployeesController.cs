using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleCodingChallenge.API.Models;
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
        public async Task<ActionResult<EmployeeDto>> Create([FromBody] NewEmployeeDetails employeeDetails)
        {
            try
            {
                var result = await mediator.Send(new CreateNewEmployeeCommand
                {
                    FirstName = employeeDetails.FirstName,
                    LastName = employeeDetails.LastName,
                    Email = employeeDetails.Email,
                    Address = employeeDetails.Address,
                    Salary = employeeDetails.Salary,
                    BirthDate = employeeDetails.BirthDate,
                    Country = employeeDetails.Country,
                    Department = employeeDetails.Department,
                    JobTitle = employeeDetails.JobTitle
                });

                return Ok(result.Employee);
            }
            catch (Exception ex)
            {
                return BadRequest($"Some value provided were incorrect. Details: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<EmployeeDto>> GetByID(string employeeID)
        {
            if (string.IsNullOrEmpty(employeeID))
                return BadRequest("Employee ID cannot be null or empty");

            var result = await mediator.Send(new GetEmployeeByIdCommand { EmployeeID = employeeID });

            // Usually telling people that the ID is not found in the database is a bad thing,
            // because people can brute-force to find a proper ID
            return result.Employee;
        }
    }
}
