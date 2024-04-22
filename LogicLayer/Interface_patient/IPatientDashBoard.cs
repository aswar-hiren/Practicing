using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.ViewModels;

namespace LogicLayer.Interface_patient
{
    public interface IPatientDashBoard
    {   
        public PatientDashboard ShowRequest(int? userone);

    }
}
