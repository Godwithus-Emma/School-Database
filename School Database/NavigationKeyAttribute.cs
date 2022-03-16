using System;
using System.Collections.Generic;
using System.Text;

namespace School_Database
{
    public class NavigationKeyAttribute : Attribute
    {
        public Type Table { get; set; }
    }
}
