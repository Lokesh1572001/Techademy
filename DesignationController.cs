using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechAdemyAPI.DAL;
using TechAdemyAPI.Models;


namespace TechAdemyAPI.Controllers
{
    [ApiController]
    [Route("api/controller2")]
    public class DesignationController : Controller
    {
        private readonly TechAdemyDbContext dbContext;
        private readonly ILogger<DesignationController> logger;

        public DesignationController(TechAdemyDbContext dbContext, ILogger<DesignationController> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        [HttpGet]

        public async Task<IActionResult> AllEmployees()
        {
            //add logger before requesting db details
            logger.LogInformation("Getting All Employees");
            return Ok(await dbContext.employees.ToListAsync());
            //successfully send the response to the requested
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployeeDesignation(EmployeeDesignation employeeDesignation)
        {
            logger.LogDebug("Adding the Employees Designation");
            var employees = new EmployeeDesignation()
            {
                DesignationName=employeeDesignation.DesignationName,
                RoleName=employeeDesignation.RoleName,
                DepartmentName=employeeDesignation.DepartmentName


            };
            await dbContext.employees.AddRangeAsync(employees);
            await dbContext.SaveChangesAsync();
            return Ok(employees);
        }


        [HttpGet]
        [Route("{id:int}")]

        public async Task<IActionResult> GetSingleEmployee([FromRoute] int id)
        {
            logger.LogDebug("get by id method executing...");
            var employees = await dbContext.employees.FindAsync(id);
            if (employees == null)
            {
                
                logger.LogWarning($"Employee ID {id} not found");
                throw new Exception("Some error occured");
                return NotFound();
            }
            return Ok(employees);
        }

        [HttpPut]
        [Route("{id:int}")]

        public async Task<IActionResult> UpdateEmployee([FromRoute] int id, EmployeeDesignation employeeDesignation)
        {
            var employees = await dbContext.employees.FindAsync(id);
            if (employees != null)
            {
                employees.DesignationName = employeeDesignation.DesignationName;
                employees.RoleName= employeeDesignation.RoleName;
                employees.DesignationName= employeeDesignation.DesignationName;

                await dbContext.SaveChangesAsync();

                return Ok(employees);
            }
           // logger.LogWarning($"Employee ID {id} not found");
            return NotFound("The Employee Designation is not found");
        }

        [HttpDelete]
        [Route("{id:int}")]

        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            var employees = await dbContext.employees.FindAsync(id);
            if (employees != null)
            {
                dbContext.Remove(employees);
                dbContext.SaveChanges();

                return Ok(employees);
            }
           // logger.LogWarning($"Employee ID {id} not found");
            return NotFound("The Employee Designation is not found");
        }
    }
}
