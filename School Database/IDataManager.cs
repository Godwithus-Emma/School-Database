using School_Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace School_Database
{
    public interface IDataManager
    {
        IEnumerable<BaseEntity> GetAllItems();
    }
}
