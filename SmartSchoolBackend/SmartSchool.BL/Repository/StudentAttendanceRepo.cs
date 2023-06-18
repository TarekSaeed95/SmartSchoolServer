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
    public class StudentAttendanceRepo : IStudentAttendanceRepo
    {
        public SmartSchoolContext Db { get; }
        public StudentAttendanceRepo(SmartSchoolContext db)
        {
            Db = db;

        }

        //no need now

        //public IEnumerable<StudentAttendanceDTO> getTodayAttendance()
        //{
        //    var todayAttendance = Db.StudAttendances.Include(s => s.student).Where(att=>att.date.ToString("dddd") ==DateTime.Now.ToString("dddd")).Select(att => new StudentAttendanceDTO()
        //    {

        //        id = att.id,
        //        date = att.date,
        //        state = att.state,
        //        studentId = att.studentId,
        //        studentName = att.student.StudentFirstName


        //    });


        //    return todayAttendance;
        //}
        public List<StudentAttendanceDTO> getAllAttendance(int id)
        {
            var allAttendance = Db.StudAttendances.Include(s => s.student).Where(s=>s.student.ClassRoomID==id).Select(att => new StudentAttendanceDTO()
            {

                id = att.id,
                date = att.date,
                state = att.state,
                studentId = att.studentId,
                studentName = att.student.StudentFirstName


            }).ToList();


            return allAttendance;
        }

        public void generateAttendance()
        {
            var students = Db.Students.ToList();
            var studentsAttendances = Db.StudAttendances.ToList();

            if (studentsAttendances.Count ==0)
            {
                for (int i = 0; i < students.Count; i++)
                {
                    StudentAttendance att = new StudentAttendance()
                    {
                        date = DateTime.Now.Date,
                        studentId = students[i].Id,
                        state = false
                    };
                    Db.StudAttendances.Add(att);
                }

                Db.SaveChanges();
            }

        }




        public void addStudentAttendance(List<StudentAttendanceDTO> studentsAtt)
        {
            foreach (var stud in studentsAtt)
            {
                var t = Db.Students.Find(stud.studentId);
                var t2 = Db.StudAttendances.Where(p => p.studentId == stud.studentId).FirstOrDefault();
                if (t != null)
                {
                    if (stud.state != false)
                    {
                        t2.state = true;
                    }
                }
            }

            Db.SaveChanges();

            //if (DateTime.Now.TimeOfDay.Hours <=11)
            if (DateTime.Now.Hour >= 23)
            {
                var x = Db.StudAttendances.Where(p => p.state == true).ToList();
                Db.StudAttendances.RemoveRange(x);
            }
            else
            {
                foreach (var stud in studentsAtt)
                {
                    var t = Db.Students.Find(stud.studentId);
                    if (t != null)
                    {
                        if (stud.state == false)
                        {
                            t.AbsenceDays += 1;
                        }
                        var studentAtt = Db.StudAttendances.Find(stud.id);
                        Db.StudAttendances.Remove(studentAtt);
                    }
                }
            }
            Db.SaveChanges();
        }


        //public void generateAttendance()
        //{
        //    var students = Db.Students.ToList();
        //    var studentsAttendances= Db.StudAttendances.ToList();

        //    if (studentsAttendances.Count == 0)
        //    {
        //        for (int i = 0; i < students.Count; i++)
        //        {
        //            StudentAttendance att = new StudentAttendance()
        //            {
        //                date = DateTime.Now.Date,
        //                studentId = students[i].Id,
        //                state = false
        //            };
        //            Db.StudAttendances.Add(att);
        //        }

        //        Db.SaveChanges();
        //    }

        //}

        //// delete attendance record after take student attendance

        //public void addStudentAttendance(IEnumerable<StudentAttendanceDTO> studentsAtt)
        //{


        //    foreach (var stud in studentsAtt)
        //    {
        //        var s = Db.Students.Find(stud.studentId);

        //        if (s != null)
        //        {
        //            if (stud.state == false)
        //            {
        //                s.AbsenceDays += 1;


        //            }

        //            var studentAtt = Db.StudAttendances.Find(stud.id);
        //            Db.StudAttendances.Remove(studentAtt);


        //        }
        //    }

        //    Db.SaveChanges();

        //}
    }
}
