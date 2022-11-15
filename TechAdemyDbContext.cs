using Microsoft.EntityFrameworkCore;
using TechAdemyAPI.Models;

namespace TechAdemyAPI.DAL
{
    public class TechAdemyDbContext : DbContext
    {
        public TechAdemyDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<EmployeeDesignation> employees { get; set; }
        public DbSet<EmployeeDetails> employeesDetails { get; set; }
        public DbSet<Registration> registrations { get; set; }
        public DbSet<RequestLeave> requestLeaves { get; set; }
        public DbSet<PaymentPagecs> paymentPages { get; set; }

    }
}
