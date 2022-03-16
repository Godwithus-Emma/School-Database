using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace School_Database.Models
{
    public abstract class Person:BaseEntity
    {
        [Required]
        [Prompt(Info = "Surname MiddleName FirstName")]
        public string Name { get; set; }
        [Prompt(Info = "Male/Female")]
        public Gender Gender { get; set; }
        [Required]
        public string HomeAddress { get; set; }
        [Prompt(Info = "MM/DD/YYYY")]
        public DateTime DateOfBirth { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
