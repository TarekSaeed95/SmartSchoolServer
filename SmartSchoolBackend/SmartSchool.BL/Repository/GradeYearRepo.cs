using Microsoft.EntityFrameworkCore;
using SmartSchool.BL.Interface;
using SmartSchool.BL.DTO;
using SmartSchool.DAL.Context;
using SmartSchool.DAL.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.Repository
{
    public class GradeYearRepo :IGradeYearRepo
    {
        public SmartSchoolContext Db { get; }
        public GradeYearRepo(SmartSchoolContext db)
        {
            Db = db; 
        }
        public IEnumerable<GradeYearDTO> GetAll()
        {
            var allGradYears = Db.GradeYears.Select(obj => new GradeYearDTO()
            {
                Id = obj.Id,
                Name = obj.Name,
                Fees = obj.Fees,
               

            });

            return allGradYears;
        }

        public GradeYearDTO GetById(int id)
        {
            var myGradYear = Db.GradeYears.Where(r => r.Id == id).Select(obj => new GradeYearDTO()
            {
                Id = obj.Id,
                Name = obj.Name,
                Fees = obj.Fees,

            }).FirstOrDefault();

            return myGradYear;
        }
        public IQueryable<GradeYearDTO>  GetByClassId(int id)
        {
            var x = from g in Db.GradeYears
                    join c in Db.ClassRooms
                    on g.Id equals c.gradeYearId
                    where c.Id ==id
                    select new GradeYearDTO()
                    {
                        Id = g.Id,
                        Name = g.Name
                    };
            return x;
        }

        public GradeYear Create(GradeYearDTO obj)
        {
            var gradeYear = Db.GradeYears.Where(s => s.Name.ToLower() == obj.Name.ToLower()).FirstOrDefault();

            if (gradeYear == null)
            {
                GradeYear gradYear = new GradeYear()
                {
                    Id = obj.Id,
                    Name = obj.Name,
                    Fees = obj.Fees,

                };

                Db.GradeYears.Add(gradYear);
                Db.SaveChanges();
                return gradYear;

            }
            return default;


        }

      
        public void Delete(int id)
        {
            var gradeYear = Db.GradeYears.Find(id);
            Db.GradeYears.Remove(gradeYear);
            Db.SaveChanges();
        }

      
    }
}
