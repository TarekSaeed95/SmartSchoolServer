using SmartSchool.BL.DTO;
using SmartSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.Interface
{
    public interface ISubjectRepo
    {
        public Subject Create(SubjectDTO obj);

        public IEnumerable<SubjectDTO> GetAll();

        public SubjectDTO GetById(int id);

        public void Delete(int id);
        public IEnumerable<SubjectDTO> GetByClassRoom(int classroomId);

		public List<SubjectDTO> GetByGradeYear(int gradeYearId);
	}
}
