using System.Collections.Generic;

namespace AmanaSite.Models.VM
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Phonenumber { get; set; }
        public bool Locked { get; set; }
        public List<string> Roles { get; set; }
    }
}