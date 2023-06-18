using Microsoft.AspNetCore.Http;
using SmartSchool.BL.Interface;
using SmartSchool.BL.DTO;
using SmartSchool.DAL.Context;
using SmartSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.Repository
{
    public class MaterialRepo:IMaterialRepo
    {
        readonly SmartSchoolContext Db;
        public MaterialRepo(SmartSchoolContext db)
        {
            Db = db;
        }


        public string Material(IFormCollection formCollection)
        {

            var file = formCollection.Files.First();
            var Subject = formCollection.FirstOrDefault(x => x.Key == "SubjectId").Value[0];
            var Type = formCollection.FirstOrDefault(x => x.Key == "Type").Value[0];
            var folderName = "wwwroot/Material/" + Type.ToLower();
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            var fileName = Guid.NewGuid().ToString() + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fullPath = Path.Combine(pathToSave, fileName);
            string dbPath = (folderName + "/" + fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
           Db.Materials.Add(new Material() { SubjectId = int.Parse(Subject), Type = Type, Path = dbPath });
           Db.SaveChanges();
            return dbPath;


        }

        public List<MaterialDTO> getAll()
        {

            var materials = Db.Materials.Select(material => new MaterialDTO()
            {
                SubjectId = material.SubjectId,
                SubjectName = material.Subject.Name,
                Type = material.Type,
                Path = material.Path,

            }
            ).ToList();


            return materials;
        }


        public List<MaterialDTO> getBySubject(int subjectid, string type)
        {
            var Material = Db.Materials.Where(m => m.SubjectId == subjectid && m.Type == type)
                .Select(material => new MaterialDTO()
                {
                    SubjectId = material.SubjectId,
                    SubjectName = material.Subject.Name,
                    Type = material.Type,
                    Path = material.Path,

                }).ToList();


            return Material;
        }

    }
}
