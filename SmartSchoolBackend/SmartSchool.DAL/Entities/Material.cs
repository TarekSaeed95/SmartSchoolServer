using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.DAL.Entities
{
    public class Material
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public int? SubjectId { get; set; }
        public virtual Subject? Subject { get; set; }
    }
}
