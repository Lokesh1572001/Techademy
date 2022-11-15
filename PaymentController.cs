using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechAdemyAPI.DAL;
using TechAdemyAPI.Models;

namespace TechAdemyAPI.Controllers
{
    [ApiController]
    [Route("api/controller5")]
    public class PaymentController : Controller
    {
        private readonly TechAdemyDbContext dbContext;
        private readonly ILogger<PaymentController> logger;

        public PaymentController(TechAdemyDbContext dbContext, ILogger<PaymentController> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            logger.LogInformation("Getting All Employees");
            return Ok(await dbContext.paymentPages.ToListAsync());
        }
        [HttpPost]

        public async Task<IActionResult> AddPayment(PaymentPagecs paymentPagecs)
        {
            logger.LogDebug("Adding the Employees Payment");
            var pay = new PaymentPagecs()
            {
                AccountNumber = paymentPagecs.AccountNumber,
                AccountHolderName=paymentPagecs.AccountHolderName,
                CVV=paymentPagecs.CVV,
                DateCreated=paymentPagecs.DateCreated,
                DateOfExpiry=paymentPagecs.DateOfExpiry

            };
            await dbContext.paymentPages.AddRangeAsync(pay);
            await dbContext.SaveChangesAsync();
            return Ok(pay);
        }

        [HttpGet]
        [Route("{id:int}")]

        public async Task<IActionResult> GetSinglePayment([FromRoute] int id)
        {
            logger.LogDebug("get by id method executing...");

           
            var pay = await dbContext.paymentPages.FindAsync(id);
            if (pay == null)
            {
                logger.LogWarning($"Employee ID {id} not found");
                throw new Exception("Id error occured");
                return NotFound();
            }
            return Ok(pay);
        }

        [HttpPut]
        [Route("{id:int}")]

        public async Task<IActionResult> UpdatePaymentDetails([FromRoute] int id, PaymentPagecs paymentPagecs)
        {
            var pay = await dbContext.paymentPages.FindAsync(id);
            if (pay != null)
            {
                pay.AccountNumber=paymentPagecs.AccountNumber;
                pay.AccountHolderName=paymentPagecs.AccountHolderName;
                pay.CVV=paymentPagecs.CVV;
                pay.DateCreated=paymentPagecs.DateCreated;
                pay.DateOfExpiry= paymentPagecs.DateOfExpiry;

                await dbContext.SaveChangesAsync();

                return Ok(pay);
            }
            return NotFound("Payment Details got updated");
        }

        [HttpDelete]
        [Route("{id:int}")]

        public async Task<IActionResult> DeletePayment([FromRoute] int id)
        {
            var pay = await dbContext.paymentPages.FindAsync(id);
            if (pay != null)
            {
                dbContext.Remove(pay);
                dbContext.SaveChanges();

                return Ok(pay);
            }
            return NotFound("Payment is not found");
        }

    }
}
