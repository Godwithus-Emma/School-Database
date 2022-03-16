using System;
using System.Collections.Generic;
using System.Text;

namespace School_Database.Models
{
    public class School:BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Management> Managements { get; set; }
        public List<Administrative> Administratives { get; set; }
        public List<Teacher> Teachers { get; set; } = new List<Teacher>();
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Parent> Parents { get; set; } = new List<Parent>();
        
        public void Admit(Student newStudent)
        {
            if (newStudent == null)
            {
                throw new ArgumentNullException(nameof(newStudent));
            }

            if (string.IsNullOrEmpty(newStudent.Name))
                throw new InvalidOperationException("Empty name not allowed");
            if (string.IsNullOrWhiteSpace(newStudent.Name))
                throw new InvalidOperationException("Empty name not allowed");
            Students.Add(newStudent);
        }
        public void Expell(Student expelledStudent)
        {
            if (expelledStudent == null)
                throw new ArgumentNullException(nameof(expelledStudent));
            if (string.IsNullOrEmpty(expelledStudent.Name))
                throw new InvalidOperationException("Empty name not allowed");
            if (string.IsNullOrWhiteSpace(expelledStudent.Name))
                throw new InvalidOperationException("Empty name not allowed");
            Students.Remove(expelledStudent);
        }

        public void EmployTeacher(Teacher newTeacher)
        {
            if (newTeacher == null)
                throw new ArgumentNullException(nameof(newTeacher));
            if (string.IsNullOrEmpty(newTeacher.Name))
                throw new InvalidOperationException("Empty name not allowed");
            if (string.IsNullOrWhiteSpace(newTeacher.Name))
                throw new InvalidOperationException("Empty name not allowed");
            Teachers.Add(newTeacher);
        }
        public void EmployAdmin(Administrative newAdmin)
        {
            if (newAdmin == null)
                throw new ArgumentNullException(nameof(newAdmin));
            if (string.IsNullOrEmpty(newAdmin.Name))
                throw new InvalidOperationException("Empty name not allowed");
            if (string.IsNullOrWhiteSpace(newAdmin.Name))
                throw new InvalidOperationException("Empty name not allowed");
            Administratives.Add(newAdmin);
        }
        public void EmployManagemnt(Management newMgt)
        {
            if (newMgt == null)
                throw new ArgumentNullException(nameof(newMgt));
            if (string.IsNullOrEmpty(newMgt.Name))
                throw new InvalidOperationException("Empty name not allowed");
            if (string.IsNullOrWhiteSpace(newMgt.Name))
                throw new InvalidOperationException("Empty name not allowed");
            Managements.Add(newMgt);
        }



        public void SackTeacher(Teacher sackedTeacher)
        {
            if (sackedTeacher == null)
                throw new ArgumentNullException(nameof(sackedTeacher));
            if (string.IsNullOrEmpty(sackedTeacher.Name))
                throw new InvalidOperationException("Empty name not allowed");
            if (string.IsNullOrWhiteSpace(sackedTeacher.Name))
                throw new InvalidOperationException("Empty name not allowed");
            Teachers.Remove(sackedTeacher);
        }

        public void SackAdmin(Administrative sackedAdmin)
        {
            if (sackedAdmin == null)
                throw new ArgumentNullException(nameof(sackedAdmin));
            if (string.IsNullOrEmpty(sackedAdmin.Name))
                throw new InvalidOperationException("Empty name not allowed");
            if (string.IsNullOrWhiteSpace(sackedAdmin.Name))
                throw new InvalidOperationException("Empty name not allowed");
            Administratives.Remove(sackedAdmin);
        }
        public void SackManagement(Management sackedMgt)
        {
            if (sackedMgt == null)
                throw new ArgumentNullException(nameof(sackedMgt));
            if (string.IsNullOrEmpty(sackedMgt.Name))
                throw new InvalidOperationException("Empty name not allowed");
            if (string.IsNullOrWhiteSpace(sackedMgt.Name))
                throw new InvalidOperationException("Empty name not allowed");
            Managements.Remove(sackedMgt);
        }
        public void PromoteManagemnt(string mgtName, MgtRoles mgtRole)
        {
            if (mgtName == null)
                throw new ArgumentNullException(nameof(mgtName));
            foreach(var mgt in Managements)
            {
                if (mgt.Name == mgtName)
                {
                    mgt.Role = mgtRole;
                    break;
                }
                else
                    throw new InvalidOperationException("Name does not exist!");
            }
        }

        public void PromoteAdmin(string adminName, AdminRoles adminRole)
        {
            if (adminName == null)
                throw new ArgumentNullException(nameof(adminName));
            foreach (var admin in Administratives)
            {
                if (admin.Name == adminName)
                {
                    admin.Role = adminRole;
                    break;
                }
                else
                    throw new InvalidOperationException("Name does not exist!");
            }
        }
    }
}
