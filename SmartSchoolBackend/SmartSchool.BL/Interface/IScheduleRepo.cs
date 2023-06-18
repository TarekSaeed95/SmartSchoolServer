using SmartSchool.BL.DTO;
using SmartSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.Interface
{
    public interface IScheduleRepo
    {
        #region AdminRole
        public  Schedule Create(ScheduleDTO obj);

        public IEnumerable<ScheduleDTO> GetAll();

        public ScheduleDTO GetById(int id);

        public void Delete(int id);

        public Schedule Edit(ScheduleDTO obj);

        //public bool CheckSchedule(ScheduleDTO obj);
        #endregion

        #region ParentRole & StudentRole
        public IEnumerable<SessionDTO> GetStudentSchedule(int classid, DateTime start, DateTime end);
        #endregion

        #region TeacherRole
        public IEnumerable<SessionDTO> GetTeacherSchedule(string identity, DateTime start, DateTime end); 
        #endregion


    }
}
