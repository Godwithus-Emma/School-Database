using School_Database.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace School_Database
{
    class Program
    {
        
        static void Line(){
            Console.WriteLine("***********************");
        }
        static string Prompt(string prompt)
        {
            Console.WriteLine(prompt);
            string value = Console.ReadLine();
            value = value.Trim();
            return value;
        }

        //static void ViewDatabase<T>() where T:BaseEntity, new()
        //{
        //    string id = Prompt("Enter your Id");
        //    int idd = int.Parse(id);
       //     DataManager<T> manager = new DataManager<T>();
        //    manager.GetEntity(idd);

        //}

        static void PromptAndSave<T>() where T:BaseEntity,new()
        {
            DataCollector<T> collector = new DataCollector<T>();
            var entity = collector.Prompt();
            DataManager<T> manager = new DataManager<T>();
            manager.AddEntity(entity);
        }
        //static void ShowNameAndId<T>() where T : BaseEntity, new()
        //{
        //    var path = typeof(T).Name + "csv";
        //    DataManager<T> manager = new DataManager<T>();
        //    Class @class = new Class();
        //    manager.GetEntity(@class);

        //}


        static void PromptAndUpdate<T>(int id) where T : BaseEntity, new()
        {
            DataManager<T> manager = new DataManager<T>();
            var entity = manager.GetEntity(id);
            DataCollector<T> collector = new DataCollector<T>();
            collector.PromptEdit(entity);
            manager.UpdateEntity(entity);
        }

        static void ViewDataBase<T>() where T:BaseEntity, new()
        {
            var path = typeof(T).Name + ".csv";
            if (!File.Exists(path))
            {
                Console.WriteLine("File does not exist");
            }
            else
            {
                var read = File.ReadAllText(path);
                Console.WriteLine(read);
            }
        }

        static int GetId()
        {
            int idd = int.Parse(Prompt("Enter your Id"));
            return idd;
        }



        static void Main(string[] args)
        
        {
            
            var num = new[] { 2, 3, 5 };
            var sqrt = from number in num let sqt = number * number where sqt < 10 select new { number, sqt };
            foreach(var nn in sqrt)
                Console.WriteLine(nn);


            //return; 
            //entity framework//delegate//reflection//primitive//generic//extension//

            var studentManager = new DataManager<Student>();
            var teacherManager = new DataManager<Teacher>();
            var classManager = new DataManager<Class>();
            var parentManager = new DataManager<Parent>();
            var managementManager = new DataManager<Management>();

            //string entry = Prompt("Enter the number of entry you would like to make");
            //int numberOfEntry = int.Parse(entry);
            //while (numberOfEntry-->0)
            bool load = true;
            while(load)
            {
                string key = Prompt(@"What would you like to do?
            1. Add details
            2. View database
            3. Edit database");

                string task = Prompt(@"Choose a category
                        1. Student
                        2. Parent
                        3. Teacher
                        4. Class
                        5. Management");
                if (key == "1")
                {
                    

                    
                    switch (task)
                    {
                        case "1":
                                PromptAndSave<Student>();
                           // var entity = classManager.ShowNameAndId(classManager);
                            break;

                        case "2":
                            PromptAndSave<Parent>();
                            break;

                        case "3":
                            PromptAndSave<Teacher>();
                            break;

                        case "4":
                            PromptAndSave<Class>();
                            break;

                        case "5":
                            PromptAndSave<Management>();
                            break;
                    }
                }

                if (key == "2")

                {
                    
                    var view = Prompt((@" How do want to view?
                                       1. By Id
                                       2. Entire database"));
                    
                    switch (task)
                    {

                        case "1":
                            if (view == "1")
                            {
                                Student student = studentManager.GetEntity(GetId());
                                Line();
                                studentManager.GetString(student);
                                Line();
                            }
                            else
                            {
                                Line();
                                ViewDataBase<Student>();
                                Line();
                            }
                            break;

                        case "2":
                            if (view == "1")
                            {
                                Parent parent = parentManager.GetEntity(GetId());
                                Line();
                                parentManager.GetString(parent);
                                Line();
                            }
                            else
                            {
                                Line();
                                ViewDataBase<Parent>();
                                Line();
                            }
                            break;

                        case "3":
                            if (view == "1")
                            {
                                Teacher teacher = teacherManager.GetEntity(GetId());
                                Line();
                                teacherManager.GetString(teacher);
                                Line();
                            }
                            else
                            {
                                Line();
                                ViewDataBase<Teacher>();
                                Line();
                            }
                            break;

                        case "4":
                            if (view == "1")
                            {
                                Class @class = classManager.GetEntity(GetId());
                                Line();
                                classManager.GetString(@class);
                                Line();
                            }
                            else
                            {
                                Line();
                                ViewDataBase<Class>();
                                Line();
                            }
                            break;
                        case "5":
                            if (view == "1")
                            {
                                Management management = managementManager.GetEntity(GetId());
                                Line();
                                managementManager.GetString(management);
                                Line();
                            }
                            else
                            {
                                Line();
                                ViewDataBase<Management>();
                                Line();
                            }
                            break;
                    }
                        
                }

                if(key == "3")
                {

                    switch (task)
                    {
                        case "1":
                           PromptAndUpdate<Student>(GetId());
                          break;
                        case "2":
                            PromptAndUpdate<Parent>(GetId());
                            break;
                        case "3":
                            PromptAndUpdate<Teacher>(GetId());
                            break;
                        case "4":
                            PromptAndUpdate<Class>(GetId());
                            break;
                        case "5":
                            PromptAndUpdate<Management>(GetId());
                            break;
                    }

                }

            }


        }
    }
}
