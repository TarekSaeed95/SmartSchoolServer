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
    public class ClassRoomRepo :IClassRoomRepo
    {
       
        public SmartSchoolContext Db { get; }
        public ClassRoomRepo(SmartSchoolContext db)
        {
            Db = db;
        }

        public IEnumerable<ClassRoomDTO> GetAll()
        {
            var allClassRooms = Db.ClassRooms.Include("gradeYear").Select(obj => new ClassRoomDTO()
            {
                Id = obj.Id,
                Name = obj.Name,
                gradeYearId = obj.gradeYearId,
                GradeYearName  = obj.gradeYear.Name,

            });

            return allClassRooms;
        }

        public ClassRoomDTO GetById(int id)
        {
            var myClassRoom = Db.ClassRooms.Include("gradeYear").Where(r => r.Id == id).Select(obj => new ClassRoomDTO()
            {
                Id = obj.Id,
                Name = obj.Name,
                gradeYearId = obj.gradeYearId,
                GradeYearName = obj.gradeYear.Name,

            }).FirstOrDefault();

            return myClassRoom;
        }

        public IEnumerable<ClassRoomDTO> GetBySubjectId(int id)
        {

            var x = from c in Db.ClassRooms
                    join g in Db.GradeYears
                    on c.gradeYearId equals g.Id
                    join s in Db.Subjects
                    on g.Id equals s.GradeYearId
                    where s.Id == id
                    select new ClassRoomDTO()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        gradeYearId = c.gradeYearId,
                        GradeYearName = c.gradeYear.Name

                    };
            return x;
        }


        public ClassRoom Create(ClassRoomDTO obj)
        {

            var existedClassRoom = Db.ClassRooms.Where(s => s.Name.ToLower() == obj.Name.ToLower()).FirstOrDefault();

            if(existedClassRoom == null)
            {
                ClassRoom classRoom = new ClassRoom()
                {
                    Id = obj.Id,
                    Name = obj.Name,
                    gradeYearId = obj.gradeYearId,

                };

                Db.ClassRooms.Add(classRoom);
                Db.SaveChanges();
                return classRoom;
            }

            return default;
        }


        public void Delete(int id)
        {
            var myClassRoom = Db.ClassRooms.Find(id);
            Db.ClassRooms.Remove(myClassRoom);
            Db.SaveChanges();
        }

       
    }
}
