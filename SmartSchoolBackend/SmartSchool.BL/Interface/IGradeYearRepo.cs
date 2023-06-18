using SmartSchool.BL.DTO;
using SmartSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.Interface
{
    public interface IGradeYearRepo
    {
        public GradeYear Create(GradeYearDTO obj);

        public IEnumerable<GradeYearDTO> GetAll();

        public GradeYearDTO GetById(int id);
        public IQueryable<GradeYearDTO> GetByClassId(int id);

        public void Delete(int id);

    }
}
