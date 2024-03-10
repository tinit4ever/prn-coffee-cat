using Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ApplicationUser : IdentityUser
    {
        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }
    }

}
