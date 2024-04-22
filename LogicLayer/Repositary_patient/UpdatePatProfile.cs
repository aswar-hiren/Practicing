using DataLayer.DataContext;
using DataLayer.Models;
using DataLayer.ViewModels;
using LogicLayer.Interface_patient;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace LogicLayer.Repositary_patient
{
    public class UpdatePatProfile : IUpdatePatientProfile
    {
        private readonly HellodocPrjContext _context;
        public UpdatePatProfile(HellodocPrjContext context)
        {
            _context = context;
        }
        public void Updateprofile(PatientDashboard model, User userone, Aspnetuser aspuserone)
        {
            if (userone != null)
            { 
                userone.Firstname = model.PatientProfile.FirstName;
                userone.Lastname = model.PatientProfile.LastName;
                userone.Email = model.PatientProfile.Email;
                userone.Street = model.PatientProfile.Street;
                userone.City = model.PatientProfile.City;
                userone.State = model.PatientProfile.State;
                userone.Zipcode = model.PatientProfile.Zipcode;
                userone.Mobile = model.PatientProfile.PhoneNumber;

            }
            if (aspuserone != null)
            {
                aspuserone.Username = model.PatientProfile.FirstName + " " + model.PatientProfile.LastName;
            }

            _context.SaveChanges();
        }
        public void CancelCasePatient(int reqid,BlockView model)
        {
            Request request = _context.Requests.Where(r => r.Requestid == reqid).FirstOrDefault();
            Requeststatuslog requeststatuslog = new Requeststatuslog
            {
                Requestid = reqid,
                Request = request,
                Status = 7,
                Notes = model.TransNotes,
                Createddate = DateTime.Now,
            };
            _context.Requeststatuslogs.Add(requeststatuslog);
            _context.SaveChanges();
            request.Status = 7;

            _context.SaveChanges();
        }
        public JwtData DecodeToken(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadToken(jwtToken) as JwtSecurityToken;
            PatientLoginView view = new PatientLoginView();
            if (token != null)
            {
                Console.WriteLine(token.Claims);
                // Access user information
                //string userId = token.Claims.First(claim =>  == model.email).Value;
                string status = token.Claims.FirstOrDefault(claim=>claim.Type==ClaimTypes.SerialNumber).Value;
                string reqid = token.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;
                // Access user role if available
                //var roleClaim = token.Claims.FirstOrDefault(claim => claim.Type == "role");
                //string role = roleClaim != null ? roleClaim.Value : "No role specified";

                // Output user information
                //Console.WriteLine($"User ID: {userId}");

                JwtData jwtData = new JwtData();

                //view.userid = userId;
                jwtData.reqid = Convert.ToInt32(reqid);
                jwtData.status = Convert.ToInt32(status);
                return jwtData;
            }
            else
            {
                Console.WriteLine("Invalid token");
                JwtData jwtData = new JwtData();
                return jwtData;
            }
        }
        public int Getstatus(int reqid)
        {
            return _context.Requests.Where(r => r.Requestid == reqid).FirstOrDefault().Status;
        }
        public void AgreePatient(int reqid)
        {
            Request request = _context.Requests.Where(r => r.Requestid == reqid).FirstOrDefault();
            Requeststatuslog requeststatuslog = new Requeststatuslog
            {
                Requestid = reqid,
                Request = request,
                Status = 4,
             
                Createddate = DateTime.Now,
            };
            _context.Requeststatuslogs.Add(requeststatuslog);
            _context.SaveChanges();
            request.Status = 4;

            _context.SaveChanges();
        }
    }
}
