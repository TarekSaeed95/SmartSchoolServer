using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartSchool.BL.DTO;

namespace SmartSchool.BL.Interface
{
    public interface ITeacherAttendanceRepo
    {
        public void GenerateAttendance();
        public List<TeacherAttendanceDTO> GetAllAttendance();
        public void AddTeacherAttendance(List<TeacherAttendanceDTO> _teachersAtt);
    }
}
