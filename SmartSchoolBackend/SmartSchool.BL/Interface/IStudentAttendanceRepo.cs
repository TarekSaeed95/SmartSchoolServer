using SmartSchool.BL.DTO;
using SmartSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.Interface
{
    public interface IStudentAttendanceRepo
    {
        public void generateAttendance();
        public List<StudentAttendanceDTO> getAllAttendance(int id);

        //public IEnumerable<StudentAttendanceDTO> getTodayAttendance();

        public void addStudentAttendance(List<StudentAttendanceDTO> studentsAtt);

    }
}
