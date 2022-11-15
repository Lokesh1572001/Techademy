using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechAdemyAPI.DAL;
using TechAdemyAPI.Models;

namespace TechAdemyAPI.Controllers
{
    [ApiController]
    [Route("api/requestleave")]
    public class RequestLeaveController : Controller
    {
        private readonly TechAdemyDbContext dbContext;
        private readonly ILogger<RequestLeaveController> logger;

        public RequestLeaveController(TechAdemyDbContext dbContext, ILogger<RequestLeaveController> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLeaves()
        {
            logger.LogInformation("Getting All Employees");
            return Ok(await dbContext.requestLeaves.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddLeave(RequestLeave request)
        {
            logger.LogDebug("Adding the Leave");
            var employee = new RequestLeave()
            {
                LeaveType = request.LeaveType,
                Time = request.Time,
                Reason = request.Reason
            };
            await dbContext.requestLeaves.AddAsync(employee);
            await dbContext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpGet]
        [Route("{id:int}")]

        public async Task<IActionResult> GetSingleEmployee([FromRoute] int id)
        {
            logger.LogDebug("get by id method executing...");

            
            var employees = await dbContext.requestLeaves.FindAsync(id);
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

        public async Task<IActionResult> UpdateEmployee([FromRoute] int id, RequestLeave requestLeave)
        {
            var employees = await dbContext.requestLeaves.FindAsync(id);
            if (employees != null)
            {
                employees.LeaveType=requestLeave.LeaveType;
                employees.Time=requestLeave.Time;
                employees.Reason = requestLeave.Reason;

                await dbContext.SaveChangesAsync();

                return Ok(employees);
            }
            return NotFound("The Designation is not found");
        }

        [HttpDelete]
        [Route("{id:int}")]

        public async Task<IActionResult> DeleteEmployeeDesignation([FromRoute] int id)
        {
            var employees = await dbContext.requestLeaves.FindAsync(id);
            if (employees != null)
            {
                dbContext.Remove(employees);
                dbContext.SaveChanges();

                return Ok("The Employee Desingnation got deleted......!");
            }
            return NotFound("The Designation is not found");
        }
    }
}
