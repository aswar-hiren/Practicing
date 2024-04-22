using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Interface_patient
{
    public interface IReqWiseFiles
    {
        public List<Requestwisefile> getReqwisefile(int requestid);
    }
}
