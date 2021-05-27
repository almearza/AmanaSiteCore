using System.ComponentModel.DataAnnotations;

namespace AmanaSite.Models.VM
{
    public class UserVM
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Token { get; set; }
        public string Phonenumber { get; set; }
    }
}