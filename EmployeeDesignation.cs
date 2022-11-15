using System.ComponentModel.DataAnnotations;

namespace TechAdemyAPI.Models
{
    public class EmployeeDesignation
    {
        [Key]
        public int Id { get; set; }
        public string DesignationName { get; set; }
        public string RoleName { get; set; }
        public string DepartmentName { get; set; }
        
    }
}
