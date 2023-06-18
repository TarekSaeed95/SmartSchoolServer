using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SmartSchool.BL.Helpers;
using SmartSchool.BL.Interface;
using SmartSchool.BL.DTO;
using SmartSchool.DAL.Context;
using SmartSchool.DAL.Entities;
using SmartSchool.DAL.OurEnums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.Repository
{
    public class TeacherRepo : ITeacherRepo
    {
        public SmartSchoolContext Db { get; }
        public UserManager<IdentityUser> User { get; }
        public TeacherRepo(SmartSchoolContext db,UserManager<IdentityUser> user)
        {
            Db = db;
            User = user;
        }
        #region AdminRole
        public string SaveInDb(TeacherDTO obj, string TeacherIdentityId)
        {
            Teacher T = new Teacher()
            {
                Id = Guid.NewGuid().ToString(),
                FullName = obj.FullName,
                Gender = (Gender)Enum.Parse(typeof(Gender), obj.Gender.ToLower()),
                Phone = obj.Phone,
                Salary = obj.Salary,
                SubjectId = obj.SubjectId,
                Address = obj.Address,
                PhotoUrl = obj.PhotoUrl,
                HireDate = obj.HireDate,
                AbsenceDays = obj.AbsenceDays,
                MaxDayOff = obj.MaxDayOff,
                IdentityUserId = TeacherIdentityId,
            };

            Db.Teachers.Add(T);
            Db.SaveChanges();

            return T.Id;
        }
        public IEnumerable<TeacherDTO> GetAll()
        {
            var allTeachers = Db.Teachers.Include(t => t.Subject).Include(t => t.IdentityUser).Select(obj => new TeacherDTO()
            {
                Id = obj.Id,
                FullName = obj.FullName,
                Gender = obj.Gender.ToString(),
                Phone = obj.Phone,
                Salary = obj.Salary,
                SubjectId = obj.SubjectId,
                Address = obj.Address,
                PhotoUrl = obj.PhotoUrl,
                HireDate = obj.HireDate,
                SubjectName = obj.Subject.Name,
                AbsenceDays = obj.AbsenceDays,
                MaxDayOff = obj.MaxDayOff,
                Email = obj.IdentityUser.Email
            });

            return allTeachers;
        }
        public TeacherDTO GetById(string id)
        {
            var myTeacher = Db.Teachers.Include(t => t.Subject).Include(t => t.IdentityUser).Where(r => r.Id == id).Select(obj => new TeacherDTO()
            {
                Id = obj.Id,
                FullName = obj.FullName,
                Gender = obj.Gender.ToString(),
                Phone = obj.Phone,
                Salary = obj.Salary,
                SubjectId = obj.SubjectId,
                Address = obj.Address,
                PhotoUrl = obj.PhotoUrl,
                HireDate = obj.HireDate,
                SubjectName = obj.Subject.Name,
                AbsenceDays = obj.AbsenceDays,
                MaxDayOff = obj.MaxDayOff,
                Email=obj.IdentityUser.Email
            }).FirstOrDefault();

            return myTeacher;
        }
        public void Delete(string id)
        {
            /////
            
            var teacherSessions = Db.Sessions.Where(s => s.TeacherID == id).ToList();

            Db.RemoveRange(teacherSessions);
            /////

            var myTeacher = Db.Teachers.Find(id);
            Db.Teachers.Remove(myTeacher);
            Db.SaveChanges();
        }
        public Teacher Edit(TeacherEditDTO obj)
        {
            Teacher t = Db.Teachers.Find(obj.Id);

            t.Id = obj.Id;
            t.FullName = obj.FullName;
            t.Gender = (Gender)Enum.Parse(typeof(Gender), obj.Gender.ToLower());
            t.Phone = obj.Phone;
            t.Salary = obj.Salary;
            t.Address = obj.Address;
            t.HireDate = obj.HireDate;
            t.MaxDayOff = obj.MaxDayOff;
            t.AbsenceDays = obj.AbsenceDays;

            if (obj.Photo != null)
            {
                t.PhotoUrl = UploadFile.Photo(obj.Photo, "TeacherImages");
            }
            Db.SaveChanges();
            return t;
        }
        #endregion
        
        #region TeacherRole
        public TeacherDTO GetByIdentity(string identity)
        {

            var myTeacher = Db.Teachers.Include(t => t.Subject).Include(t => t.IdentityUser)
                .Where(r => r.IdentityUserId == identity).Select(obj => new TeacherDTO()
            {
                Id = obj.Id,
                FullName = obj.FullName,
                Gender = obj.Gender.ToString(),
                Phone = obj.Phone,
                Salary = obj.Salary,
                SubjectId = obj.SubjectId,
                Address = obj.Address,
                PhotoUrl = obj.PhotoUrl,
                HireDate = obj.HireDate,
                SubjectName = obj.Subject.Name,
                MaxDayOff=obj.MaxDayOff,
                AbsenceDays = obj.AbsenceDays,
                Email=obj.IdentityUser.Email
            }).FirstOrDefault();
            return myTeacher;
        } 
        #endregion
        
    }
}
