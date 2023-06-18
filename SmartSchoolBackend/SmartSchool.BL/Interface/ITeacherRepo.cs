using SmartSchool.BL.DTO;
using SmartSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.Interface
{
    public interface ITeacherRepo
    {
        #region AdminRole
        public string SaveInDb(TeacherDTO obj, string TeacherId);
        public Teacher Edit(TeacherEditDTO obj);
        public IEnumerable<TeacherDTO> GetAll();
        public TeacherDTO GetById(string id);
        public void Delete(string id);
        #endregion

        #region TeacherRole
        public TeacherDTO GetByIdentity(string identity); 
        #endregion

    }
}
