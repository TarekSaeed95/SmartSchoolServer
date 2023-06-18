using Microsoft.AspNetCore.Http;
using SmartSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.DTO
{
    public class MaterialDTO
    {
        public string Type { get; set; }
        public int? SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string Path { get; set; }
    }
}
