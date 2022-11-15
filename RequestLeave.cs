using System.ComponentModel.DataAnnotations;

namespace TechAdemyAPI.Models
{
    public class RequestLeave
    {
        [Key]
        public int Id { get; set; }
        public string LeaveType { get; set; }
        public DateTime Time { get; set; }
        public string Reason { get; set; }
    }
}
