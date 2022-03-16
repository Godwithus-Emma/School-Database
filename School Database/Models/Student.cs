using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace School_Database.Models
{
    public class Student:Person
    {
        [NavigationKey(Table = typeof(Class))]
        public int ClassId { get; set; }
        public Class Class { get; set; }
       public List<Subject> Subject { get; set; } = new List<Subject>();
        public Parent Parent { get; set; }
        //public override string ToString()
        //{
        //    var s = string.Join(",", Subject.Select(sub=>sub.ToString()));
        //    var subjects = "";
        //    foreach(var sub in Subject)
        //    {
        //        subjects += sub.ToString();
        //        subjects += ",";
        //    }
        //    //return $"{Id}...{HomeAddress}...{Class}...{subjects}";
        //}
    }
}
