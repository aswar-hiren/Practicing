using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Interface_patient
{
    public interface IPatientDashForm
    {
        public void InsertPatientDashForm(Patient_dash_info model,int? temp);
    }
}
