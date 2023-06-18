using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.OpenPgp;
using SmartSchool.BL.Helpers;
using SmartSchool.BL.Interface;
using SmartSchool.BL.DTO;
using SmartSchool.DAL.Context;
using SmartSchool.DAL.Entities;
using SmartSchool.DAL.OurEnums;

namespace SmartSchool.BL.Repository
{
    public class StudentRepo : IStudentRepo
    {
        public SmartSchoolContext Db { get; }
        public StudentRepo(SmartSchoolContext _db)
        {
            Db = _db;
        }

      

        #region Admin Role
        public IEnumerable<StudentDTO> GetAll()
        {
            var allStuds = Db.Students.Include(s => s.ClassRoom).Select(s => new StudentDTO()
            {
                Id = s.Id,
                AbsenceDays = s.AbsenceDays,
                Address = s.Address,
                ClassRoomID = s.ClassRoomID,
                ClassRoomName = s.ClassRoom.Name,
                Fees = s.Fees,
                Gender = s.Gender.ToString(),
                MaxDayOff = s.MaxDayOff,
                ParentID = s.ParentID,
                StudentBirthDate = s.StudentBirthDate,
                StudentFirstName = s.StudentFirstName,
                StudentPhone = s.StudentPhone,
                StudentPhotoUrl = s.StudentPhotoUrl,
                StudentBirthCertPhotoUrl = s.StudentBirthCertPhotoUrl,

            });

            return allStuds;
        }
        public StudentDTO GetbyId(string id)
        {
            var myStud = Db.Students.Where(s => s.Id == id).Include(s => s.ClassRoom).Select(s => new StudentDTO()
            {
                Id = s.Id,
                AbsenceDays = s.AbsenceDays,
                Address = s.Address,
                ClassRoomID = s.ClassRoomID,
                ClassRoomName = s.ClassRoom.Name,
                Fees = s.Fees,
                Gender = s.Gender.ToString(),
                //Gender = s.Gender,
                MaxDayOff = s.MaxDayOff,
                ParentID = s.ParentID,
                StudentBirthDate = s.StudentBirthDate,
                StudentFirstName = s.StudentFirstName,
                StudentPhone = s.StudentPhone,
                StudentPhotoUrl = s.StudentPhotoUrl,
                StudentBirthCertPhotoUrl = s.StudentBirthCertPhotoUrl,
            }).FirstOrDefault();

            return myStud;
        }
        public Student Edit(StudentDTO std)
        {
            Student student = Db.Students.Find(std.Id);
            student.Id = std.Id;
            student.AbsenceDays = std.AbsenceDays;
            student.Address = std.Address;
            student.ClassRoomID = std.ClassRoomID;
            student.Fees = std.Fees;
            student.Gender = (Gender?)Enum.Parse(typeof(Gender), std.Gender.ToLower());
            student.MaxDayOff = std.MaxDayOff;
            student.ParentID = std.ParentID;
            student.StudentBirthDate = std.StudentBirthDate;
            student.StudentFirstName = std.StudentFirstName;
            student.StudentPhone = std.StudentPhone;
            if (std.StudentPhoto != null)
            {
                student.StudentPhotoUrl = UploadFile.Photo(std.StudentPhoto, "StudentImages");
            }
            else
            {
                student.StudentPhotoUrl = std.StudentPhotoUrl;
            }
            Db.SaveChanges();
            return student;
        }
        public void Delete(string id)
        {
            Db.Students.Remove(Db.Students.Find(id));
            Db.SaveChanges();
        }
        public IEnumerable<StudentDTO> GetStudentByClass(int classRoomId)

        {
            var students = Db.Students.Where(s => s.ClassRoomID == classRoomId).Select(s => new StudentDTO()
            {
                Id = s.Id,
                AbsenceDays = s.AbsenceDays,
                Address = s.Address,
                ClassRoomID = s.ClassRoomID,
                Fees = s.Fees,
                Gender = s.Gender.ToString(),
                MaxDayOff = s.MaxDayOff,
                ParentID = s.ParentID,
                StudentBirthDate = s.StudentBirthDate,
                StudentFirstName = s.StudentFirstName,
                StudentPhone = s.StudentPhone,
                StudentPhotoUrl = s.StudentPhotoUrl,
                StudentBirthCertPhotoUrl = s.StudentBirthCertPhotoUrl,

            }
            );
            return students;
        }
        public IEnumerable<StudentDTO> GetStudentByGradeYear(int gradeYearId)
        {
            var students = Db.Students.Where(s => s.ClassRoom.gradeYearId == gradeYearId).Select(s => new StudentDTO()
            {
                Id = s.Id,
                AbsenceDays = s.AbsenceDays,
                Address = s.Address,
                ClassRoomID = s.ClassRoomID,
                ClassRoomName = s.ClassRoom.Name,
                Fees = s.Fees,
                Gender = s.Gender.ToString(),
                MaxDayOff = s.MaxDayOff,
                ParentID = s.ParentID,
                StudentBirthDate = s.StudentBirthDate,
                StudentFirstName = s.StudentFirstName,
                StudentPhone = s.StudentPhone,
                StudentPhotoUrl = s.StudentPhotoUrl,
                StudentBirthCertPhotoUrl = s.StudentBirthCertPhotoUrl,
            }
            );
            return students;
        }
        public List<StudentDTO> getAbsenceStudents()
        {
            var students = Db.Students.Where(s => s.AbsenceDays >= s.MaxDayOff).Include(s => s.ClassRoom).Select(s => new StudentDTO()
            {
                Id = s.Id,
                AbsenceDays = s.AbsenceDays,
                Address = s.Address,
                ClassRoomID = s.ClassRoomID,
                ClassRoomName = s.ClassRoom.Name,
                Fees = s.Fees,
                Gender = s.Gender.ToString(),
                MaxDayOff = s.MaxDayOff,
                ParentID = s.ParentID,
                StudentBirthDate = s.StudentBirthDate,
                StudentFirstName = s.StudentFirstName,
                StudentPhone = s.StudentPhone,
                StudentPhotoUrl = s.StudentPhotoUrl,
                StudentBirthCertPhotoUrl = s.StudentBirthCertPhotoUrl,
            }).ToList();

            return students;
        }

        #endregion


   

        #region Student Role
        public StudentDTO GetByIdentity(string identity)
        {
            var myStudent = Db.Students.Where(r => r.IdentityUserId == identity).Select(s => new StudentDTO()
            {
                Id = s.Id,
                AbsenceDays = s.AbsenceDays,
                Address = s.Address,
                ClassRoomID = s.ClassRoomID,
                ClassRoomName = s.ClassRoom.Name,
                Fees = s.Fees,
                Gender = s.Gender.ToString(),
                MaxDayOff = s.MaxDayOff,
                ParentID = s.ParentID,
                StudentBirthDate = s.StudentBirthDate,
                StudentFirstName = s.StudentFirstName,
                StudentPhone = s.StudentPhone,
                StudentPhotoUrl = s.StudentPhotoUrl,
                StudentBirthCertPhotoUrl = s.StudentBirthCertPhotoUrl


            }).FirstOrDefault();

            return myStudent;
        }

        public bool CheckStudentsForClassRoom(int classRoomId)

        {
            //law fe students hyeb2a true.
            return !(Db.Students.Where(s => s.ClassRoomID == classRoomId).FirstOrDefault() == null);
        }

        #endregion


        #region Parent Role
        public IEnumerable<StudentDTO> GetByParentId(string id)
        {
            var parentchilds = Db.Students.Where(s => s.ParentID == id).Include(s => s.ClassRoom)
                .ThenInclude(s => s.gradeYear).Select(s => new StudentDTO()
            {
                Id = s.Id,
                AbsenceDays = s.AbsenceDays,
                Address = s.Address,
                ClassRoomID = s.ClassRoomID,
                ClassRoomName = s.ClassRoom.Name,
                Fees = s.Fees,
                GradePrice = (long)s.ClassRoom.gradeYear.Fees,
                Gender = s.Gender.ToString(),
                MaxDayOff = s.MaxDayOff,
                ParentID = s.ParentID,
                StudentBirthDate = s.StudentBirthDate,
                StudentFirstName = s.StudentFirstName,
                StudentPhone = s.StudentPhone,
                StudentPhotoUrl = s.StudentPhotoUrl,
                StudentBirthCertPhotoUrl = s.StudentBirthCertPhotoUrl,
                
            });
            return parentchilds;
        }
        #endregion


    }
}
