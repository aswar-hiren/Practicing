using DataLayer.Models;
using DataLayer.ViewModel;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Interface_patient
{
    public  interface IPatientRequest
    {
        public uservm getUserDat(string search);
        public void Adduser(uservm model);
        public List<City> getcity();
        public void Userdelete(int id);
        public uservm getUser(int id);
        public void Updateuser(uservm model);
    }
}
