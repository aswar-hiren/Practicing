using DataLayer.DataContext;
using DataLayer.Models;
using DataLayer.ViewModels;
using LogicLayer.Interface_patient;
using Microsoft.AspNetCore.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace LogicLayer.Repositary_patient
{
    public class PatientDashForm : IPatientDashForm
    {
        private readonly HellodocPrjContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public PatientDashForm(HellodocPrjContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public void InsertPatientDashForm(Patient_dash_info model,int? temp)
        {
            var userone = _context.Users.FirstOrDefault(u => u.Userid == temp);

          
            Request request = new Request
            {
                Userid = temp,
                Firstname = model.FirstName,
                Lastname = model.LastName,
                Phonenumber = model.PhoneNumber,
                Email = model.Email,
                Createddate = DateTime.Now,
                Status = 1,
                User = userone,
                Requesttypeid=1
            };

            _context.Requests.Add(request);
            Requeststatuslog requeststatuslog = new Requeststatuslog
            {
                Createddate = DateTime.Now,
                Requestid = request.Requestid,
                Request = request
            };
            _context.Requeststatuslogs.Add(requeststatuslog);
            Requestclient requestclient = new Requestclient
            {
                Firstname = model.FirstName,
                Lastname = model.LastName,
                Email = model.Email,
                Zipcode = model.Zipcode,
                State = model.State,
                City = model.City,
                Street = model.Street,
                Request = request,
                BirthDate=model.BirthDate,
             
                Notes=model.details

            };
            _context.Requestclients.Add(requestclient);
            _context.SaveChanges();
            Region region = _context.Regions.Where(rg => rg.Name.ToLower() == model.City.ToLower()).FirstOrDefault();
            if (region == null)
            {
                Region regionone = new Region()
                {
                    Name = model.City
                };
                _context.Regions.Add(regionone);
                requestclient.Region = regionone;
                requestclient.Regionid = regionone.Regionid;
                _context.SaveChanges();
            }
            else
            {
                requestclient.Regionid = region.Regionid;
                requestclient.Region = region;
            }
            string uniquefilename = null;
            if (model.Photo != null)
            {
                string uploadfolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                uniquefilename = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filepath = Path.Combine(uploadfolder, uniquefilename);
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    model.Photo.CopyTo(stream);
                }
            }
            Requestwisefile requestwisefile = new Requestwisefile
            {
                Requestid = request.Requestid,
                Filename = uniquefilename,
                Request = request,
                Createddate = DateTime.Now,

            };

            _context.Requestwisefiles.Add(requestwisefile);

            _context.SaveChanges();
        }
    }
}
