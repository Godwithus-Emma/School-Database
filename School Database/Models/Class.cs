using System;
using System.Collections.Generic;
using System.Text;

namespace School_Database.Models
{
    public class Class:BaseEntity 
    {
        public string Name { get; set; }
        public string Teacher { get; set; }
        //public Teacher Teacher { get; set; }
        [ExcludeFromPrompt]
        public List<Student> Students { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
