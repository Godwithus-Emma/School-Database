using System;
using System.Collections.Generic;
using System.Text;

namespace School_Database.Models
{
    public class Parent : Person
    {

        public string PhoneNumber { get; set; }
        public List<Student> Students { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
