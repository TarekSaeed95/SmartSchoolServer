using Microsoft.AspNetCore.Identity;
using SmartSchool.BL.Helpers;
using SmartSchool.BL.Interface;
using SmartSchool.BL.Models;
using SmartSchool.BL.Services;
using SmartSchool.BL.DTO;
using SmartSchool.DAL.Context;
using SmartSchool.DAL.Entities;
using SmartSchool.DAL.OurEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;


namespace SmartSchool.BL.Repository
{
    public class RequestRepo : IRequestRepo
    {
        public SmartSchoolContext Db { get; }
        public IAuthService AuthService { get; }
        private readonly UserManager<IdentityUser> UserManager;


        public RequestRepo(SmartSchoolContext db, IAuthService authService, UserManager<IdentityUser> userManager)
        {
            Db = db;
            AuthService = authService;
            this.UserManager = userManager;
        }


        public int Create(RequestDTO obj)
        {
       

            Request r1 = new Request()
             {
                id = obj.id,
                StudentEmail = obj.StudentEmail,
                StudentFirstName = obj.StudentFirstName,
                StudentGender = (Gender)Enum.Parse(typeof(Gender), obj.StudentGender.ToLower()),
                StudentPhone = obj.StudentPhone,
                StudentBirthDate = obj.StudentBirthDate,
                Address= obj.Address,
                ParentFullName = obj.ParentFullName,
                ParentEmail = obj.ParentEmail,
                ParentPhone = obj.ParentPhone,
                StudentPhotoUrl = obj.StudentPhotoUrl,
                StudentBirthCertPhotoUrl = obj.StudentBirthCertPhotoUrl,
                IdentityParentPhotoUrl = obj.IdentityParentPhotoUrl,
                Password = EncodePassword.EncodePasswordToBase64(obj.Password),
                
            };
            
            Db.Requests.Add(r1);
            Db.SaveChanges();

            return r1.id;
        }

        public IEnumerable<RequestDTO> GetAll()
        {
            var allRequests = Db.Requests.Select(obj => new RequestDTO()
            {
                id = obj.id,
                StudentEmail = obj.StudentEmail,
                StudentFirstName = obj.StudentFirstName,

                StudentGender = obj.StudentGender.ToString(),
                StudentPhone = obj.StudentPhone,
                StudentBirthDate = obj.StudentBirthDate,
                Address = obj.Address,
                ParentFullName = obj.ParentFullName,
                ParentEmail = obj.ParentEmail,
                ParentPhone = obj.ParentPhone,
                StudentPhotoUrl = obj.StudentPhotoUrl,
                StudentBirthCertPhotoUrl = obj.StudentBirthCertPhotoUrl,
                IdentityParentPhotoUrl = obj.IdentityParentPhotoUrl,
                Password = obj.Password
                
            });

            return allRequests;
        }

        public RequestDTO GetById(int id)
        {
            var myRequest = Db.Requests.Where(r => r.id == id).Select(obj => new RequestDTO()
            {
                id = obj.id,
                StudentEmail = obj.StudentEmail,
                StudentFirstName = obj.StudentFirstName,
                StudentGender = obj.StudentGender.ToString(),
                StudentPhone = obj.StudentPhone,
                StudentBirthDate = obj.StudentBirthDate,
                Address = obj.Address,
                ParentFullName = obj.ParentFullName,
                ParentEmail = obj.ParentEmail,
                ParentPhone = obj.ParentPhone,
                StudentPhotoUrl = obj.StudentPhotoUrl,
                StudentBirthCertPhotoUrl = obj.StudentBirthCertPhotoUrl,
                IdentityParentPhotoUrl = obj.IdentityParentPhotoUrl,
                Password = obj.Password
            }).FirstOrDefault();

            return myRequest;
        }

        public string SaveInDb(int id,string ParentIdentityId, string StudentIdentityId)
        {
            string parentId;

            var myRequest = GetById(id);

            if (Db.Parents.FirstOrDefault(p=>p.IdentityUserId== ParentIdentityId) == null)
            {
                Parent P = new Parent()
                {
                    Id = Guid.NewGuid().ToString(),
                    ParentFullName = myRequest.ParentFullName,
                    IdentityParentPhotoUrl = myRequest.IdentityParentPhotoUrl,
                    ParentPhone = myRequest.ParentPhone,
                    IdentityUserId = ParentIdentityId,
                };
                Db.Parents.Add(P);
                Db.SaveChanges();

                parentId = P.Id;

            }
            else
            {
                parentId = Db.Parents.Where(p => p.IdentityUserId == ParentIdentityId).FirstOrDefault().Id;

            }



            Student S = new Student()
            {   Id = Guid.NewGuid().ToString(),
                StudentFirstName = myRequest.StudentFirstName +" "+ myRequest.ParentFullName,
                ParentID = parentId,
                Address = myRequest.Address,
                Gender = (Gender)Enum.Parse(typeof(Gender), myRequest.StudentGender.ToLower()),
                StudentPhone = myRequest.StudentPhone,
                StudentBirthDate = myRequest.StudentBirthDate,
                StudentPhotoUrl = myRequest.StudentPhotoUrl,
                StudentBirthCertPhotoUrl = myRequest.StudentBirthCertPhotoUrl,
                IdentityUserId = StudentIdentityId,
            };
            Db.Students.Add(S);
            Db.SaveChanges();
            return S.Id;
        }

        public void Delete(int id)
        {
            var RemoveRequest = Db.Requests.Find(id);
            Db.Requests.Remove(RemoveRequest);
            Db.SaveChanges();
        }

        public bool RequestExist(RequestDTO Request)
        {
            var request = Db.Requests.FirstOrDefault(r => ( r.StudentEmail == Request.StudentEmail|| r.ParentEmail==Request.StudentEmail));
            
            return request!=null;
        }
    }



}

