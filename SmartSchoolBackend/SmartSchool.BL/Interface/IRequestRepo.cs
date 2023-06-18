using SmartSchool.BL.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.Interface
{
    public interface IRequestRepo
    {
        public int Create (RequestDTO obj);

        public IEnumerable<RequestDTO> GetAll();

        public RequestDTO GetById (int id);

        public  string SaveInDb (int id,string ParentId, string StudentId);

        public void Delete (int id);
        public bool RequestExist(RequestDTO Request);

    }
}
