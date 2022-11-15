using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechAdemyAPI.DAL;
using TechAdemyAPI.Models;

namespace TechAdemyAPI.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class RegistrationController : Controller
    {
        private readonly TechAdemyDbContext dbContext;
        private readonly ILogger<RegistrationController> logger;

        public RegistrationController(TechAdemyDbContext dbContext, ILogger<RegistrationController> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            logger.LogInformation("Getting All Employees");
            return Ok(await dbContext.registrations.ToListAsync());
        }

        [HttpPost]

        public async Task<IActionResult> AddUsers(Registration registration)
        {
            logger.LogDebug("Adding the Employees Registration");
            var users = new Registration()
            {
                Username = registration.Username,
                password = registration.password

            };
            await dbContext.registrations.AddAsync(users);
            await dbContext.SaveChangesAsync();
            return Ok(users);
        }
        [HttpGet]
        [Route("{id:int}")]

        public async Task<IActionResult> GetSingleUser([FromRoute] int id)
        {
            logger.LogDebug("get by id method executing...");

           

            var users = await dbContext.registrations.FindAsync(id);

            if (users == null)
            {
                logger.LogWarning($"Employee ID {id} not found");
                throw new Exception("Some error occured");
                return NotFound();
            }
            return Ok(users);
        }
        [HttpPut]
        [Route("{id:int}")]

        public async Task<IActionResult> UpdateUser([FromRoute] int id,Registration registration)
        {
            var users=await dbContext.registrations.FindAsync(id);
            if(users != null)
            {
                users.Username=registration.Username;
                users.password=registration.password;

                await dbContext.SaveChangesAsync();

                return Ok(users);
            }
            return NotFound("The User is not found");
        }

        [HttpDelete]
        [Route("{id:int}")]

        public async Task<IActionResult> DeleteUsers([FromRoute] int id)
        {
            var users=await dbContext.registrations.FindAsync(id);
            if (users != null)
            {
                dbContext.Remove(users);
                dbContext.SaveChanges();

                return Ok("The User got deleted......!");
            }
            return NotFound("The Users is not found");
        }

    }
}
