using SmartSchool.BL.DTO;
using SmartSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.Interface
{
    public interface ISessionRepo
    {
        public Session Create(SessionDTO obj);

        public IEnumerable<SessionDTO> GetAll();

        public SessionDTO GetById(int id);

        public void Delete(int id);

        public Session Edit(SessionDTO obj);
        public IEnumerable<SessionDTO> GetByClassIdDate(int classid, DateTime date);

        //public bool CheckSchedule(SessionDTO obj);

    }
}
