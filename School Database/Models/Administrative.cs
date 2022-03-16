using System;
using System.Collections.Generic;
using System.Text;

namespace School_Database.Models
{
    public class Administrative:Person
    {
        public AdminRoles Role { get; set; }
    }
}
