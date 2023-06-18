using SmartSchool.BL.DTO;
using SmartSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.Interface
{
    public interface IParentRepo
    {


        #region Admin Role
        public IEnumerable<ParentDTO> GetAll();
        public ParentDTO GetbyId(string id);
        public Parent Edit(ParentDTO pnt);
        public void Delete(string id);
        #endregion


        #region Parent Role

        public ParentDTO GetByIdentity(string identity);

        #endregion
    }
}
