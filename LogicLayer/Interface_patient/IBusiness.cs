using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Interface_patient
{
    public interface IBusiness
    {
        public void InsertBusinessData(BusinessViewModel model,string AspId);
        public void InsertBusinessData(BusinessViewModel model);
    }
}
