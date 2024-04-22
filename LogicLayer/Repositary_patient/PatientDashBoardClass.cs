using DataLayer.DataContext;
using DataLayer.ViewModels;
using LogicLayer.Interface_patient;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace LogicLayer.Repositary_patient
{
    public class PatientDashBoardClass : IPatientDashBoard
    {
        private readonly HellodocPrjContext _context;
       public PatientDashBoardClass(HellodocPrjContext context)
        {
            _context = context;
        }
        public PatientDashboard ShowRequest(int? userone)
        {
            PatientDashboard patientDashboard = new PatientDashboard();
            var username = _context.Users.FirstOrDefault(u => u.Userid == userone).Firstname;
            patientDashboard.requests = _context.Requests.Where(u => u.Userid == userone).OrderByDescending(u => u.Createddate).ToList();
            patientDashboard.userName= username;

            patientDashboard.wiseFiles = _context.Requestwisefiles.ToList();
            patientDashboard.User = _context.Users.FirstOrDefault(u => u.Userid == userone);
            return patientDashboard;
        }
    }
}
