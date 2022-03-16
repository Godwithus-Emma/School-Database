using School_Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace School_Database
{
    public class DataManager<T>: IDataManager where T : BaseEntity, new()
    {
        string DatabaseFileName => typeof(T).Name + ".csv";

        IEnumerable<PropertyInfo> GetDBProperties()
        {
            var properties = typeof(T).GetProperties().Where(property => property.IsDatabaseType()).OrderBy(property =>
            {
                if (property.Name == "Id")
                    return 0;
                return 1;
            });
            return properties;
        }

        void CreateDataBase()
        {
            string fileName = DatabaseFileName;
            var properties = GetDBProperties();
            var header = string.Join(",", properties.Select(property => property.Name));
            File.WriteAllText(fileName, header + "\r");
        }

        void EnsureDatabaseExists()
        {
            string fileName = typeof(T).Name + ".csv";
            if (!File.Exists(fileName))
            {
                CreateDataBase();
            }
        }

        string[] GetDatabaseContent()
        {
            var fileContent = File.ReadAllText(DatabaseFileName);
            var lines = fileContent.Split('\r');
            return lines.Skip(1).ToArray();
        }

        string[] GetFileHeader()
        {
            var fileContent = File.ReadAllText(DatabaseFileName);
            var lines = fileContent.Split('\r');
            var header = lines[0];
            var headerNames = header.Split(',');
            return headerNames;
        }

        public void GetString(T entity)
        {
            var properties = typeof(T).GetProperties().Where(property => property.IsDatabaseType());
            foreach(var property in properties)
            {
                var value = property.GetValue(entity);
                Console.WriteLine(@$"{typeof(T).Name} {property.Name} : {value}");
            }
        }

      

        string GetCsv(T entity)
        {
            var headerNames = GetFileHeader();
            var values = headerNames.Select(header =>
            {
                var headerProperty = typeof(T).GetProperty(header);
                var value = headerProperty.GetValue(entity);
                return value.ToString();
            });
            var vals = string.Join(",", values);
            return vals;
        }


        int GetNextId(bool incrementId)
        {
            int iid;
            string iPath = typeof(T).Name + ".txt";
            if (File.Exists(iPath))
            {
                var id = File.ReadAllText(iPath);
                iid = int.Parse(id);
                if (incrementId)
                {
                    iid++;
                    id = iid.ToString();
                    File.WriteAllText(iPath, id);
                }
            }
            else
            {
                File.WriteAllText(iPath, "1");
                iid = 1;
            }
            return iid;
        }

        public void AddEntity(T newData)
        {
            EnsureDatabaseExists();
            int id = GetNextId(true);
            newData.Id = id;

            var vals = GetCsv(newData);
            File.AppendAllText(DatabaseFileName, vals + "\r\n");
        }

        public T GetEntity(int id)
        {
            EnsureDatabaseExists();
            var headerNames = GetFileHeader();
            var lines = GetDatabaseContent();
            int idPosition = Array.IndexOf(headerNames, "Id");
            int linesInFile = lines.Length;
            for (int i = 0; i < linesInFile; i++)
            {
                var line = lines[i].Trim();
                if (!string.IsNullOrEmpty(line))
                {
                    var contents = line.Split(',');
                    var sid = contents[idPosition];
                    var iid = int.Parse(sid);
                    if (id == iid)
                    {
                        var t = new T();
                        int ix = 0;
                        foreach (var headerName in headerNames)
                        {
                            var propertyName = typeof(T).GetProperty(headerName);
                            var currentValue = contents[ix];
                            var tValue = TypeDescriptor.GetConverter(propertyName.PropertyType).ConvertFromInvariantString(currentValue);
                            propertyName.SetValue(t, tValue);
                            ix++;
                        }
                        return t;
                    }
                }
            }
            return default;
        }

        public IEnumerable<T> GetAll()
        {
            List<T> items = new List<T>();
            int id = 0;
            int maxLines = GetNextId(false);
            for (int i = 0; i < maxLines + 1; i++)
            {
                T entity = GetEntity(id++);
                if (entity != default)
                    items.Add(entity);
            }
            return items;
        }

        public bool UpdateEntity(T update)
        {
            var items = GetAll();
            var target = items.SingleOrDefault(it => it.Id == update.Id);
            if (target == null)
                return false;
            var properties = GetDBProperties();
            foreach(var property in properties)
            {
                var value = property.GetValue(update);
                property.SetValue(target, value);
            }
            var stringOfItems = items.Select(it => GetCsv(it));
            var fileData = string.Join("\r\n", stringOfItems);
            var fileHeader = string.Join(",", GetFileHeader());
            var fileContent = fileHeader + "\r\n" + fileData;
            File.WriteAllText(DatabaseFileName, fileContent);
            return true;
        }

        public IEnumerable<BaseEntity> GetAllItems()
        {
            return GetAll();
        }
    }
}

