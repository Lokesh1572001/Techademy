using System.ComponentModel.DataAnnotations;
using TechAdemyAPI.DAL;

namespace TechAdemyAPI.Models
{
    public class Registration
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string password { get; set; }    
    }



}
