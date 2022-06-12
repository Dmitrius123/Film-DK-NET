using Data.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Film:BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Budget { get; set; }

        public int ActorId { get; set; }    
        public Actor Actor { get; set; }
        public int DirectorId { get; set; }
        public Director Director { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }

        
    }
}
