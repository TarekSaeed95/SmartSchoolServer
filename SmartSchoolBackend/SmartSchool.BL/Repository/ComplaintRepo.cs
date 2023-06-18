using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using SmartSchool.BL.Interface;
using SmartSchool.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using SmartSchool.DAL.Context;
using SmartSchool.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace SmartSchool.BL.Repository
{
    public class ComplaintRepo:IComplaintRepo
    {

        public  IConfiguration _config;
        public SmartSchoolContext Db;

        public ComplaintRepo(IConfiguration config, SmartSchoolContext db)
        {
            _config = config;
            Db = db;
        }

        public void SendEmail(ComplaintDTO request)
        {
            string studentsNames = "";
            var parent = Db.Parents.Where(p => p.IdentityUserId == request.ParentId).Include(p=>p.IdentityUser).Include(p=>p.Students).FirstOrDefault();
            //var parentStudents = Db.Students.Where(s => s.ParentID == parent.Id).ToList();
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("Email").Value));
            email.To.Add(MailboxAddress.Parse("smartschoolcomplaints@gmail.com"));
            email.Subject = request.Subject;

            foreach( var student in parent.Students)
            {
                studentsNames += $"{student.StudentFirstName},";
            }

            email.Body = new TextPart(TextFormat.Plain) {
                Text =  $"Parent ID: {parent.IdentityUserId}\nParent Full Name: {parent.ParentFullName}\nParent Email: {parent.IdentityUser.Email}\nParent Phone: {parent.ParentPhone}\nParent Childs: {studentsNames}\nComplaint: {request.Body}"
            };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("Host").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("Email").Value, _config.GetSection("Password").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
