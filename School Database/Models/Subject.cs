using System;
using System.Collections.Generic;
using System.Text;

namespace School_Database.Models
{
    public class Subject
    {
        [NavigationKey(Table = typeof(Teacher))]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
