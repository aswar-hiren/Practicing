using DataLayer.Models;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Interface_patient
{
    public interface IUpdatePatientProfile
    {
        public void Updateprofile(PatientDashboard model, User userone, Aspnetuser aspuserone);
        public void CancelCasePatient(int reqid, BlockView model);
        public void AgreePatient(int reqid);
        public JwtData DecodeToken(string jwtToken);
        public int Getstatus(int reqid);
    }
}
