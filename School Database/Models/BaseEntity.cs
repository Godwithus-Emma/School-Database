using System;
using System.Collections.Generic;
using System.Text;

namespace School_Database.Models
{
    public abstract class BaseEntity
    {
        [ExcludeFromPrompt]
        public int Id { get; set; }
    }
}
