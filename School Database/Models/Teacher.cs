using System;
using System.Collections.Generic;
using System.Text;

namespace School_Database.Models
{
    public class Teacher : Person
    {
        public Class Class { get; set; }
        //public Subject Subjects { get; set; }
        public string PhoneNumber { get; set; }

    }
}
