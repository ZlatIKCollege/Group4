using System;
using System.Collections.Generic;

#nullable disable

namespace AIS_Practice.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public int Role { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public int? PhoneNumber { get; set; }

        public virtual Role RoleNavigation { get; set; }
    }
}
