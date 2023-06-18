using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SmartSchool.BL.Interface;
using SmartSchool.BL.DTO;
using SmartSchool.DAL.Context;
using SmartSchool.DAL.Entities;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.Repository
{
    public class ExamResultRepo:IExamResultRepo
    {
        public SmartSchoolContext Db { get; }
        public ExamResultRepo(SmartSchoolContext db)
        {
            Db = db;

        }
        public IEnumerable<ExamResultDTO> getResultsByClassRoom(int classid,int subjectid)
        {
            var x = from e in Db.ExamResults.Include(s => s.Subject)
                    join s in Db.Students
                    on e.StudentId equals s.Id
                    join c in Db.ClassRooms
                    on s.ClassRoomID equals c.Id
                    //join g in Db.GradeYears
                    //on c.gradeYearId equals g.Id
                    where c.Id == classid && e.SubjectId==subjectid
                    select new ExamResultDTO()
                    {
                        Id=e.Id,
                        StudentId = s.Id,
                        SubjectId = e.SubjectId,
                        FirstTermGrade = e.FirstTermGrade,
                        SecondTermGrade = e.SecondTermGrade,
                        Total = e.Total,
                        Year= e.Year,
                        TotalSubjectMark = e.Subject.TotalMark,
                        SubjectName = e.Subject.Name,
                        StudentName = s.StudentFirstName,
                        ClassRoomName = c.Name,
                        Passed=e.Passed
                    };

            return x;


        }

        public void generateStudentsGrades()
        {

          if(Db.ExamResults.ToList().Count== 0)
            {

                var x = from s in Db.Students
                        join c in Db.ClassRooms
                        on s.ClassRoomID equals c.Id
                        join g in Db.GradeYears
                        on c.gradeYearId equals g.Id
                        join b in Db.Subjects
                        on g.Id equals b.GradeYearId
                        select new ExamResult()
                        {
                            StudentId = s.Id,
                            SubjectId = b.Id,
                            FirstTermGrade = 0,
                            SecondTermGrade = 0,
                            Total = 0,
                            Year=DateTime.Now.Year,
                            Passed=false,
         
                        };

                 
                Db.ExamResults.AddRange(x);
                Db.SaveChanges();

            }


            //var students = Db.Students.ToList();
            ////var studentsAttendances = Db.StudAttendances.ToList();
            //var examResult = Db.ExamResults.ToList();

            ////check condition for date
            //if (examResult.Count == 0)
            //{
            //    foreach (var stud in students)
            //    {

            //        var gradeYearSubjects = Db.Students.Where(s=>s.ClassRoomID==stud.ClassRoomID).Select(s => s.ClassRoom.gradeYear.Subjects).ToList();


            //        foreach (var sub in gradeYearSubjects)
            //        {


            //            ExamResult exam = new ExamResult()
            //            {
            //               StudentId=stud.Id,
            //               SubjectId=sub.id
            //            };
            //            Db.ExamResults.Add(exam);
            //        }

            //    }

            //    Db.SaveChanges();
            //}

        }
        public List<ExamResultDTO> Edit(List<ExamResultDTO> Result)
        {

            //var x= Db.ExamResults
            List<ExamResultDTO> falseresult =new List<ExamResultDTO>();
            foreach (var e in Result)
            {
                var result = Db.ExamResults.Where(x=>x.Id==e.Id).Include(e => e.Subject).FirstOrDefault();
                if (e.FirstTermGrade <= (result.Subject.TotalMark / 2) && e.SecondTermGrade <= (result.Subject.TotalMark / 2))
                {
                    result.StudentId = e.StudentId;
                    result.SubjectId = e.SubjectId;
                    result.FirstTermGrade = e.FirstTermGrade;
                    result.SecondTermGrade = e.SecondTermGrade;
                    result.Total = e.FirstTermGrade + e.SecondTermGrade;
                    result.Year = e.Year;
                    if (result.Total >= (result.Subject.TotalMark) / 2)
                    {
                        result.Passed = true;
                    }
                    Db.Update(result);
                }
                else
                {
                    falseresult.Add(e);
                }
            }

            Db.SaveChanges();
            return falseresult;

        }

        public void upgradeStudent()
        {

            var students = Db.Students.ToList();
            bool allPassed = true;
     

            foreach (var student in students)
            {
                var studentResults = Db.ExamResults.Include(s => s.Subject).Where(r => r.StudentId == student.Id);

                foreach (var r in studentResults)
                {

                    if (!r.Passed)
                    {
                        allPassed = false;
                        
                    }

                }

                if (allPassed && student.Fees)
                {
                    student.Fees = false;

                    var studentClass = Db.ClassRooms.Where(c => c.Id == student.ClassRoomID).FirstOrDefault().Name.Split('/');

                    int gradeYear = int.Parse(studentClass[0]) + 1;

                    if (gradeYear <= 6)
                    {

                        var studentClassUpgrade = gradeYear.ToString() + "/" + studentClass[1];

                        var studentNewClass = Db.ClassRooms.Where(c => c.Name == studentClassUpgrade).FirstOrDefault();

                        if (studentNewClass == null)
                        {
                            var studClass = int.Parse(studentClass[1]);
                            while (studentNewClass == null && studClass > 1)
                            {
                                studentClassUpgrade = gradeYear.ToString() + "/" + (--studClass);

                                studentNewClass = Db.ClassRooms.Where(c => c.Name == studentClassUpgrade).FirstOrDefault();
                            }
                            student.ClassRoomID = studentNewClass.Id;
                        }
                        else
                        {
                            student.ClassRoomID = studentNewClass.Id;

                        }
                    }


                }

                Db.SaveChanges();

                allPassed = true;

            }


            //var sourceData = Db.ExamResults.ToList();

            //Db.Database.ExecuteSql($"insert into PreviousExamResults select FirstTermGrade,SecondTermGrade,Total,Passed,StudentId,SubjectId,Year from ExamResults");
            //Db.ExamResults.RemoveRange(sourceData);

            //Db.SaveChanges();



            // future update for deleting schedule after upgrade student
            //var Schedule = Db.Schedules.ToList();
            //Db.Schedules.RemoveRange(Schedule);

            var sourceData = Db.ExamResults.ToList();
        
            foreach (var s in sourceData)
            {
                var previousExamResult = new PreviousExamResult()
                {
                    StudentId = s.StudentId,
                    FirstTermGrade = s.FirstTermGrade,
                    Passed = s.Passed,
                    SecondTermGrade = s.SecondTermGrade,
                    SubjectId = s.SubjectId,
                    Total = s.Total,
                    Year=s.Year

                };
                Db.PreviousExamResults.Add(previousExamResult);
            }
            Db.ExamResults.RemoveRange(sourceData);
            Db.SaveChanges();



        }

        #region Parent and Student Role
        public List<ExamResultDTO> getStudentExamResults(string studentId, int gradeYearId)
        {
            var grades = Db.PreviousExamResults.Where(e => e.StudentId == studentId && e.Subject.GradeYearId == gradeYearId ).Include(s => s.Subject).Select(x => new ExamResultDTO()
            {
                FirstTermGrade = x.FirstTermGrade,
                SecondTermGrade = x.SecondTermGrade,
                SubjectName = x.Subject.Name,
                TotalSubjectMark = x.Subject.TotalMark,
                Passed = x.Passed,
                SubjectId = x.SubjectId,
                Year= x.Year,
                StudentId = x.StudentId,
                Total = x.Total,

            }).ToList();
            return grades;
        }

        public List<ExamResultDTO> getFirstTermGrade(string studentId, int gradeYearId)
        {
            var grades = Db.ExamResults.Where(e => e.StudentId == studentId && e.Subject.GradeYearId == gradeYearId&& e.Student.Fees==true).Include(s => s.Subject).Select(x => new ExamResultDTO()
            {
                FirstTermGrade = x.FirstTermGrade,
                Passed = x.Passed,
                SubjectName=x.Subject.Name,
                SubjectId = x.SubjectId,
                TotalSubjectMark=x.Subject.TotalMark/2,
                Year = x.Year,
                StudentId = x.StudentId,
                Total = x.Total,

            }).ToList();
            return grades;
        }
        public List<ExamResultDTO> getFullGrade(string studentId, int gradeYearId)
        {
            var grades = Db.ExamResults.Where(e => e.StudentId == studentId && e.Subject.GradeYearId == gradeYearId).Include(s => s.Subject).Select(x => new ExamResultDTO()
            {
                FirstTermGrade = x.FirstTermGrade,
                SecondTermGrade= x.SecondTermGrade,
                Passed = x.Passed,
                SubjectName = x.Subject.Name,
                SubjectId = x.SubjectId,
                TotalSubjectMark = x.Subject.TotalMark,
                Year = x.Year,
                StudentId = x.StudentId,
                Total = x.Total,
            }).ToList();
            return grades;
        }
        #endregion


    }
}
