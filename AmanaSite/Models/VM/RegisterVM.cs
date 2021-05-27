using System.ComponentModel.DataAnnotations;

namespace AmanaSite.Models.VM
{
    public class RegisterVM
    {
        
        [Required]
        public string Username { get; set; }
        [Required]
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
    }
}