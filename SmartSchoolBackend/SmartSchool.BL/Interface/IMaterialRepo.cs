using Microsoft.AspNetCore.Http;
using SmartSchool.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.Interface
{
    public interface IMaterialRepo
    {
        public string Material(IFormCollection formCollection);
        public List<MaterialDTO> getAll();
        public List<MaterialDTO> getBySubject(int subjectid, string type);


    }
}
