using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace School_Database
{
    public static class Extensions
    {
        public static bool IsDatabaseType(this PropertyInfo property)
        {
            return property.PropertyType.IsPrimitive || property.PropertyType == typeof(string) || property.PropertyType == typeof(DateTime) || property.PropertyType.IsEnum;
        }
    }
}
