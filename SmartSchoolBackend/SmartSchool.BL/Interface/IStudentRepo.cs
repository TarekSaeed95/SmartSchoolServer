using SmartSchool.BL.DTO;
using SmartSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.Interface
{
    public interface IStudentRepo
    {
        #region Admin Role
        public IEnumerable<StudentDTO> GetAll();
        public StudentDTO GetbyId(string id);
        public Student Edit(StudentDTO std);
        public void Delete(string id);
        public IEnumerable<StudentDTO> GetStudentByClass(int classRoomId);
        public IEnumerable<StudentDTO> GetStudentByGradeYear(int gradeYearId);
        public List<StudentDTO> getAbsenceStudents();

        public bool CheckStudentsForClassRoom(int classRoomId);
        #endregion


        #region Student Role
        public StudentDTO GetByIdentity(string identity);

        #endregion


        #region Parent Role
        public IEnumerable<StudentDTO> GetByParentId(string id);

        #endregion

    }
}
