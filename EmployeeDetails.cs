using System.ComponentModel.DataAnnotations;

namespace TechAdemyAPI.Models
{
    public class EmployeeDetails
    {
        [Key]
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int Phone { get; set; }
        public string MailId { get; set; }
        public string Address { get; set; }
    }
}
