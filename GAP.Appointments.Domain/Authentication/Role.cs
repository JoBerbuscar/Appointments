using System;
using System.Collections.Generic;
using System.Linq;

namespace GAP.Appointments.Domain.Authentication
{    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
