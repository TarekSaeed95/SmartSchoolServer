using Microsoft.AspNetCore.Http;
using SmartSchool.DAL.Context;
using SmartSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.Helpers
{
    public class UploadFile
    {
        public static string Photo(string photoString, string folderName)
        {
            //Should we add separate folders or not 
            Random rnd = new Random();
            Byte[] bytes = Convert.FromBase64String(photoString);
            string filePath1 = (folderName+"/" + Path.GetFileName(photoString) + rnd.Next() + ".jpg");
            System.IO.File.WriteAllBytes("wwwroot/" + filePath1, bytes);
            photoString = filePath1;
            return photoString; 
        }
        //public static string Material(IFormCollection formCollection,SmartSchoolContext DB)
        //{
            
        //    var file = formCollection.Files.First();
        //    var Subject = formCollection.FirstOrDefault(x => x.Key == "SubjectId").Value[0];
        //    var Type = formCollection.FirstOrDefault(x => x.Key == "Type").Value[0];
        //    var folderName = "wwwroot/Material/" + Type.ToLower();
        //    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

        //        var fileName = Guid.NewGuid().ToString() + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //        var fullPath = Path.Combine(pathToSave, fileName);
        //        string dbPath = (folderName + "/" + fileName);
        //        using (var stream = new FileStream(fullPath, FileMode.Create))
        //        {
        //            file.CopyTo(stream);
        //        }
        //    DB.Materials.Add(new Material() { SubjectId = int.Parse(Subject), Type = Type, Path = dbPath });
        //    DB.SaveChanges();
        //    return dbPath;
            

        //}
    }
}
