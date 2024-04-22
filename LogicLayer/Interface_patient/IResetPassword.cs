using DataLayer.Models;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Interface_patient
{
    public interface IResetPassword
    {
        public  void ResetThepass(ResetPassViewModel model, string aspid);
        public string getaspuser(string email);

    }
}
