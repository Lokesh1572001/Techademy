using System.ComponentModel.DataAnnotations;

namespace TechAdemyAPI.Models
{
    public class PaymentPagecs
    {
        [Key]
        public int Id { get; set; }
        public int AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public int CVV { get; set; }
        public string DateCreated { get; set; }
        public string DateOfExpiry { get; set; }
    }
}
