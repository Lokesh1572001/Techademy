using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechAdemyAPI.DAL;
using TechAdemyAPI.Models;

namespace TechAdemyAPI.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeDetailsController : Controller
    {
        private readonly TechAdemyDbContext dbContext;
        private readonly ILogger<EmployeeDetailsController> logger;

        public EmployeeDetailsController(TechAdemyDbContext dbContext, ILogger<EmployeeDetailsController> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            logger.LogInformation("Getting All Employees");
            return Ok(await dbContext.employeesDetails.ToListAsync());
           
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeDetails employee)
        {
            logger.LogDebug("Adding the Employees Designation");
            var employees = new EmployeeDetails()
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                Phone = employee.Phone,
                MailId = employee.MailId,
                Address = employee.Address


            };
            await dbContext.employeesDetails.AddAsync(employees);
            await dbContext.SaveChangesAsync();
            return Ok(employees);
        }

        [HttpGet]
        [Route("{id:int}")]

        public async Task<IActionResult> GetSingleEmployee([FromRoute] int id)
        {
            logger.LogDebug("get by id method executing...");

           
            var employees=await dbContext.employeesDetails.FindAsync(id);
            if (employees == null)
            {
                logger.LogWarning($"Employee ID {id} not found");
                throw new Exception("Id Not Found");
                return NotFound();
            }
            return Ok(employees);
        }

        [HttpPut]
        [Route("{id:int}")]

        public async Task<IActionResult> UpdateEmployee([FromRoute] int id, EmployeeDetails employeeDetails)
        {
            var employees = await dbContext.employeesDetails.FindAsync(id);
            if (employees != null)
            {
                employees.EmployeeId = employeeDetails.EmployeeId;
                employees.EmployeeName= employeeDetails.EmployeeName;
                employees.Phone= employeeDetails.Phone;
                employees.MailId= employeeDetails.MailId;
                employees.Address = employeeDetails.Address;

                await dbContext.SaveChangesAsync();

                return Ok(employees);
            }
            return NotFound("The Employee is not found");
        }

        [HttpDelete]
        [Route("{id:int}")]
            
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            var employees = await dbContext.employeesDetails.FindAsync(id);
            if (employees != null)
            {
                dbContext.Remove(employees);
                dbContext.SaveChanges();

                return Ok(employees);
            }
            
            return NotFound("The Employee is not found");
        }
    }
}
