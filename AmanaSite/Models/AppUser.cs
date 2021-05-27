using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace AmanaSite.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
       public ICollection<AppUserRole> UserRoles { get; set; }  
    }
}