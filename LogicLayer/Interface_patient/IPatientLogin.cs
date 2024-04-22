using DataLayer.Models;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Interface_patient
{
    public interface IPatientLogin
    {
        Aspnetuser ValidateLogin(PatientLoginView model);
        Physician Getphy(string ID);
    }
}
