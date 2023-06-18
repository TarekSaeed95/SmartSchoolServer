using Microsoft.EntityFrameworkCore;
using SmartSchool.BL.Interface;
using SmartSchool.BL.DTO;
using SmartSchool.DAL.Context;
using SmartSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SmartSchool.BL.Repository
{
    public class ScheduleRepo : IScheduleRepo
    {
        static object locker = new object();
        public SmartSchoolContext Db { get; }
        public ScheduleRepo(SmartSchoolContext db)
        {
            Db = db;
        }
        #region AdminRole
        public IEnumerable<ScheduleDTO> GetAll()
        {
            var allSchedules = Db.Schedules.Include(a => a.Class).Select(obj => new ScheduleDTO()
            {
                Id = obj.Id,
                Day = obj.Day,
                ClassId = obj.ClassId,
                ClassRoomName = obj.Class.Name,
            });
            return allSchedules;
        }



        public ScheduleDTO GetById(int id)
        {
            var mySchedule = Db.Schedules.Where(r => r.Id == id).Select(obj => new ScheduleDTO()
            {
                Id = obj.Id,
                Day = obj.Day,
                ClassId = obj.ClassId,
                ClassRoomName = obj.Class.Name
            }).FirstOrDefault();



            return mySchedule;
        }



        public Schedule Create(ScheduleDTO obj)
        {
            lock (locker)
            {
                var schedule = Db.Schedules.FirstOrDefault(p => p.Day == obj.Day && p.ClassId == obj.ClassId);
                if (schedule == null)
                {
                    schedule = new Schedule()
                    {
                        Day = obj.Day,
                        ClassId = obj.ClassId,
                    };



                    Db.Schedules.Add(schedule);
                    Db.SaveChanges();
                }
                return schedule;
            }
        }
        //int x = Db.Schedules.Count(s => s.Id == schedule.Id);
        // var y = Db.Schedules.FirstOrDefault(s => s.Id == schedule.Id);
        // if (x <= 1)
        // {



        //     Db.Schedules.Remove(y);
        // }
        // }
        //var x = Db.Schedules.Where(s => s.ClassId == schedule.ClassId);
        //var y = x.FirstOrDefault();
        //var allschedules = x.ToList();
        //Db.Schedules.RemoveRange(allschedules);
        //Db.Schedules.Add(y);





        public void Delete(int id)
        {
            var mySchedule = Db.Schedules.Find(id);
            Db.Schedules.Remove(mySchedule);
            Db.SaveChanges();
        }



        public Schedule Edit(ScheduleDTO obj)
        {
            Schedule S = Db.Schedules.Find(obj.Id);
            S.Id = obj.Id;
            S.Day = obj.Day;
            S.ClassId = obj.ClassId;
            Db.SaveChanges();
            return S;
        }




        #endregion



        #region ParentRole & StudentRole
        public IEnumerable<SessionDTO> GetStudentSchedule(int classid, DateTime start, DateTime end)
        {



            var x = from s in Db.Schedules
                    join c in Db.ClassRooms
                    on s.ClassId equals c.Id
                    join e in Db.Sessions
                    on s.Id equals e.ScheduleID
                    join t in Db.Teachers
                    on e.TeacherID equals t.Id
                    join b in Db.Subjects
                    on t.SubjectId equals b.Id
                    where c.Id == classid && s.Day >= start && s.Day <= end
                    select new SessionDTO()
                    {
                        Id = e.Id,
                        TeacherID = t.Id,
                        SubjectName = b.Name,
                        TeacherName = t.FullName,
                        SessionNo = e.SessionNo,
                        ScheduleDay = s.Day.ToString(),
                        ClassRoomName = c.Name
                    };
            return x;



            #region another way



            //    var schedule = Db.Schedules.Where(s => s.ClassId == classid && s.Day >= start && s.Day <= end).Include(s=>s.Class).Include(s=>s.Sessions).ToList();



            //    List<SessionVM> sessions = new List<SessionVM>();



            //    foreach (var item in schedule)
            //    {
            //        foreach (var s in item.Sessions)
            //        {
            //            var x = Db.Sessions.Where(c=>c.Id==s.Id).Include(x=>x.Teacher).ThenInclude(a=>a.Subject).FirstOrDefault();



            //            SessionVM VMsession = new SessionVM()
            //            {
            //                Id = x.Id,
            //                TeacherID = x.TeacherID,
            //                SubjectName = x.Teacher.Subject.Name,
            //                TeacherName = x.Teacher.FullName,
            //                SessionNo = x.SessionNo,
            //                ScheduleDay = item.Day.ToString(),
            //                ClassName = item.Class.Name
            //            };



            //            sessions.Add(VMsession);
            //        }
            //    }  



            //    return sessions;



            #endregion



        }
        #endregion

        #region TeacherRole
        public IEnumerable<SessionDTO> GetTeacherSchedule(string identity, DateTime start, DateTime end)
        {
            var teacher = Db.Teachers.Where(x => x.IdentityUserId == identity).FirstOrDefault();
            var teacherSessions = Db.Sessions.Where(s => s.TeacherID == teacher.Id && s.Schedule.Day >= start && s.Schedule.Day <= end).Select(x => new SessionDTO()
            {
                Id = x.Id,
                TeacherID = x.TeacherID,
                SubjectName = x.Teacher.Subject.Name,
                TeacherName = x.Teacher.FullName,
                SessionNo = x.SessionNo,
                ScheduleDay = x.Schedule.Day.ToString(),
                ClassRoomName = x.Schedule.Class.Name
            }
            );
            return teacherSessions;
        }
        #endregion



        //public bool CheckSchedule (ScheduleDTO obj)
        //{
        //    //var myschedule = Db.Schedules.FirstOrDefault(s => s.Id == obj.ScheduleID);
        //    var myClassRoom = Db.ClassRooms.Where(c => c.Id == obj.ClassId).FirstOrDefault();
        //    var isThereStudents = Db.Students.Where(s => s.ClassRoomID == myClassRoom.Id).FirstOrDefault();
        //    return isThereStudents == null;
        //}
    }
}