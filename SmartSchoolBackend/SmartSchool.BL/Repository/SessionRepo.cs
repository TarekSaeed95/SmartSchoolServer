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
    public class SessionRepo : ISessionRepo
    {
        public SmartSchoolContext Db { get; }
        public SessionRepo(SmartSchoolContext db)
        {
            Db = db;
        }
        public IEnumerable<SessionDTO> GetAll()
        {
            var allSessions = Db.Sessions.Include(a=>a.Schedule).Include(a=>a.Teacher).ThenInclude(a=>a.Subject).Select(obj => new SessionDTO()
            {
                Id = obj.Id,
                SessionNo = obj.SessionNo,
                TeacherID = obj.TeacherID,
                ScheduleID = obj.ScheduleID,
                TeacherName = obj.Teacher.FullName,
                SubjectName = obj.Teacher.Subject.Name,
                ClassRoomName = obj.Schedule.Class.Name,
                ScheduleDay = obj.Schedule.Day.ToString("dddd")       
            });
            return allSessions;
        }

        public SessionDTO GetById(int id)
        {
            var mySession = Db.Sessions.Where(r => r.Id == id).Include(a => a.Teacher).ThenInclude(a => a.Subject).Select(obj => new SessionDTO()
            {
                Id = obj.Id,
                SessionNo = obj.SessionNo,
                TeacherID = obj.TeacherID,
                ScheduleID = obj.ScheduleID,
                TeacherName = obj.Teacher.FullName,
                SubjectName = obj.Teacher.Subject.Name,
                ClassRoomName = obj.Schedule.Class.Name,
                ScheduleDay = obj.Schedule.Day.ToString("dddd")       

            }).FirstOrDefault();
            
            return mySession;
        }

        public Session Create(SessionDTO obj)
        {

            //(s.SessionNo == obj.SessionNo && s.SessionNo == obj.ScheduleID && s.Schedule.Class.Name == obj.ClassRoomName)
            // ||

           var existSession = Db.Sessions.Include(s=>s.Schedule).Where(s =>
               (s.SessionNo == obj.SessionNo && s.TeacherID == obj.TeacherID && s.Schedule.Day.ToString() == obj.ScheduleDay)
              || (s.SessionNo == obj.SessionNo && s.ScheduleID==obj.ScheduleID)
            ).FirstOrDefault();

            //var myschedule = Db.Schedules.FirstOrDefault(s => s.Id == obj.ScheduleID);
            //var myClassRoom = Db.ClassRooms.Where(c => c.Id == myschedule.ClassId).FirstOrDefault();
            //var isThereStudents = Db.Students.Any(s => s.ClassRoomID == myClassRoom.Id);
                //if (existSession == null && isThereStudents)
                if (existSession == null)
                {
                Session Session = new Session()
                {
                    Id = obj.Id,
                    SessionNo = obj.SessionNo,
                    TeacherID = obj.TeacherID,
                    ScheduleID = obj.ScheduleID
                };

                Db.Sessions.Add(Session);
                Db.SaveChanges();
                return Session;
            }
            return default;           
        }

        public void Delete(int id)
        {
            var mySession = Db.Sessions.Find(id);
            Db.Sessions.Remove(mySession);
            Db.SaveChanges();
        }

        public Session Edit(SessionDTO obj)
        {
            Session S = Db.Sessions.Find(obj.Id);
            S.Id = obj.Id;
            S.SessionNo = obj.SessionNo;
            S.TeacherID = obj.TeacherID;
            S.ScheduleID = obj.ScheduleID;
            Db.SaveChanges();
            return S;
        }

        public IEnumerable<SessionDTO> GetByClassIdDate(int classid, DateTime date)
        {
            var mySchedule = Db.Sessions.Include(p => p.Schedule).Include(p => p.Teacher).ThenInclude(p => p.Subject).Where(r => r.Schedule.ClassId == classid && r.Schedule.Day ==date).Select(obj => new SessionDTO()
            {
                Id = obj.Id,
                TeacherID = obj.Teacher.Id,
                ScheduleDay = obj.Schedule.Day.ToString(),
                ScheduleID = obj.Schedule.Id,
                SessionNo = obj.SessionNo,
                SubjectName = obj.Teacher.Subject.Name,
                TeacherName = obj.Teacher.FullName
            }).ToList();
            return mySchedule;
        }

        //public bool CheckSchedule(SessionDTO obj)
        //{
        //    var myschedule = Db.Schedules.FirstOrDefault(s => s.Id == obj.ScheduleID);
        //    var myClassRoom = Db.ClassRooms.Where(c => c.Id == myschedule.ClassId).FirstOrDefault();
        //    var isThereStudents = Db.Students.Where(s => s.ClassRoomID == myClassRoom.Id).FirstOrDefault();
        //    return isThereStudents == null;
        //}

    }
}
