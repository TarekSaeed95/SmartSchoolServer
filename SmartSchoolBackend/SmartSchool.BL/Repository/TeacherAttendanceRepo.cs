using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartSchool.BL.Interface;
using SmartSchool.BL.DTO;
using SmartSchool.DAL.Context;
using SmartSchool.DAL.Entities;

namespace SmartSchool.BL.Repository
{
    public class TeacherAttendanceRepo : ITeacherAttendanceRepo
    {
        public SmartSchoolContext Db { get; }
        public TeacherAttendanceRepo(SmartSchoolContext _db)
        {
            Db = _db;
        }

        public List<TeacherAttendanceDTO> GetAllAttendance()
        {
            var allAttendance = Db.TeacherAttendances.Include(t => t.Teacher).Select(att => new TeacherAttendanceDTO()
            {
                Id = att.Id,
                Date = att.Date,
                State = att.State,
                TeacherId = att.TeacherId,
                TeacherName = att.Teacher.FullName
            }).ToList();
            return allAttendance;
        }

        public void GenerateAttendance()
        {
            var Teachers = Db.Teachers.ToList();
            var teachersAttendances = Db.TeacherAttendances.ToList();
            if (teachersAttendances.Count == 0){
                for (int i = 0; i < Teachers.Count; i++){
                TeacherAttendance att = new()
                {
                    Date = DateTime.Now.Date,
                    TeacherId = Teachers[i].Id,
                    State = false
                };
                Db.TeacherAttendances.Add(att);
            }
            Db.SaveChanges();
            }
        }

        public void AddTeacherAttendance(List<TeacherAttendanceDTO> _teachersAtt)
        {
            foreach (var Teach in _teachersAtt){
                var t = Db.Teachers.Find(Teach.TeacherId);
                var t2= Db.TeacherAttendances.Where(p=>p.TeacherId==Teach.TeacherId).FirstOrDefault();
                if (t != null)
                {
                    if (Teach.State == false)
                    {
                        t2.State = false;
                    }
                    else
                    {
                        t2.State=true;
                    }
                }
            }

            Db.SaveChanges();
            //if (DateTime.Now.TimeOfDay.Hours <= 16)
            //if (DateTime.Now.TimeOfDay.Hours >= 23)
            if (DateTime.Now.Hour >= 23)
            {
                var x = Db.TeacherAttendances.Where(p => p.State == true).ToList();
                Db.TeacherAttendances.RemoveRange(x);
            }
            else
            {
                foreach (var Teach in _teachersAtt)
                {
                    var t = Db.Teachers.Find(Teach.TeacherId);
                    if (t != null)
                    {
                        if (Teach.State == false)
                        {
                            t.AbsenceDays += 1;
                        }
                        var teacherAtt = Db.TeacherAttendances.Find(Teach.Id);
                        Db.TeacherAttendances.Remove(teacherAtt);
                    }
                }
            }
            Db.SaveChanges();
        }


    }
}
