using SmartSchool.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.Interface
{
    public interface IExamResultRepo
    {
        public void generateStudentsGrades();
        public IEnumerable<ExamResultDTO> getResultsByClassRoom(int classid, int subjectid);
        public List<ExamResultDTO> Edit(List<ExamResultDTO> Result);

        public void upgradeStudent();

        public List<ExamResultDTO> getFullGrade(string studentId, int gradeYearId);
        #region Parent and Student Role
        public List<ExamResultDTO> getStudentExamResults(string studentId, int gradeYearId);
        public List<ExamResultDTO> getFirstTermGrade(string studentId, int gradeYearId);
        #endregion
    }
}
