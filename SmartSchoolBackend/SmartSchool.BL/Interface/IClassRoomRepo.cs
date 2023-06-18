using SmartSchool.BL.Repository;
using SmartSchool.BL.DTO;
using SmartSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.Interface
{
    public interface IClassRoomRepo
    {
        public ClassRoom Create(ClassRoomDTO obj);
        public IEnumerable<ClassRoomDTO> GetAll();
        public ClassRoomDTO GetById(int id);
        public IEnumerable<ClassRoomDTO> GetBySubjectId(int id);
        public void Delete(int id);
    }
}
