using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace School_Database
{

    public class DataCollector<T> where T : new()
    {
        public T Prompt()
        {

            T newData = new T();
            var properties = typeof(T).GetProperties().Where(property => property.GetCustomAttribute<ExcludeFromPromptAttribute>() == null && property.IsDatabaseType());
            
            foreach (var property in properties)
            {
                do
                {
                    Console.WriteLine($"Enter {typeof(T).Name} {property.Name}");
                    var navigation = property.GetCustomAttribute<NavigationKeyAttribute>();
                    if (navigation != null)
                    {
                        var table = navigation.Table;
                        var dataManagerType = typeof(DataManager<>).MakeGenericType(table);
                        var dataManager = (IDataManager)Activator.CreateInstance(dataManagerType); //new dataManagerType
                        var items = dataManager.GetAllItems();
                        Console.WriteLine($"Choose a {table.Name}");
                        Console.WriteLine(string.Join("\r\n", items.Select(it => $"{it.Id}: {it}")));
                        int id = 0;
                        do
                        {
                            var entry = Console.ReadLine();
                            id = int.Parse(entry);
                            var exist = items.Any(it => it.Id == id);
                            if (!exist)
                            {
                                Console.WriteLine($"{id} is invalid");
                                continue;
                            }
                            break;
                        } while (true);
                        property.SetValue(newData, id);
                    }
                    else
                    {
                        var options = property.GetCustomAttribute<PromptAttribute>();
                        if (options != null)
                        {
                            Console.WriteLine(options.Info);
                        }
                        var data = Console.ReadLine();
                        data = data.Trim();
                        try
                        {
                            var value = TypeDescriptor.GetConverter(property.PropertyType).ConvertFromInvariantString(data);
                            property.SetValue(newData, value);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            continue;
                        }
                    }
                    break;
                } while (true);
            }
            return newData;
        }






        PropertyInfo SelectPropertyToEdit()
        {
            var properties = typeof(T).GetProperties().Where(property => property.GetCustomAttribute<ExcludeFromPromptAttribute>() == null && property.IsDatabaseType());
            Console.WriteLine("What do you want to edit?");
            int i = 1;
            foreach (var property in properties)
            {
                Console.WriteLine($"{i}. {typeof(T).Name} {property.Name}");
                i++;
            }
            var res = Console.ReadLine();
            int number = int.Parse(res);
            return properties.ElementAt(number-1);
        }

        public void PromptEdit(T target)
        {
            var property = SelectPropertyToEdit();
            Console.WriteLine(@$"Enter new value: Initial value({target})");
            var value = Console.ReadLine();
            var tvalue = TypeDescriptor.GetConverter(property.PropertyType).ConvertFromInvariantString(value);
            property.SetValue(target, tvalue);
        }


    }
}


