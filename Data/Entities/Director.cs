using Data.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Director:BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
