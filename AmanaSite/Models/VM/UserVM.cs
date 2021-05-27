using System.ComponentModel.DataAnnotations;

namespace AmanaSite.Models.VM
{
    public class UserVM
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
    }
}