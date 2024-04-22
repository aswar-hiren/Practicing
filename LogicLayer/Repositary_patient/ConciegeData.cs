using DataLayer.DataContext;
using DataLayer.Models;
using DataLayer.ViewModels;
using LogicLayer.Interface_patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace LogicLayer.Repositary_patient
{
   public class ConciegeData : IConcierge
    {
        private readonly HellodocPrjContext _context;
        public ConciegeData(HellodocPrjContext context)
        {
            _context = context;
        }
    
        public void InsertConciegegeData(ConciegeViewModel model,string AspId) {
            User user = _context.Users.Where(u => u.Aspnetuserid == AspId).FirstOrDefault();
        
            
            Request request = new Request
            {
                Firstname = model.c_FirstName,
                Lastname = model.c_LastName,
                Email = model.c_Email,
                Phonenumber=model.c_PhoneNumber,
                Createddate = DateTime.Now,
                Status = 1,
                Requesttypeid = 3,
                User=user,
            };
            Requeststatuslog requeststatuslog = new Requeststatuslog
            {
                Createddate = DateTime.Now,
                Requestid = request.Requestid,
                Request = request
            };
            _context.Requeststatuslogs.Add(requeststatuslog);

            _context.Requests.Add(request);
            Requestclient requestclient = new Requestclient
            {
                Firstname = model.FirstName,
                Lastname = model.LastName,
                Email = model.email,
                Zipcode = model.Zipcode,
                State = model.State,
                City = model.City,
                Street = model.Street,
                Request = request,
                Phonenumber=model.PhoneNumber,
                BirthDate=model.date,
              
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

        }
        public void InsertConciegegeData(ConciegeViewModel model)
        {
           
            Request request = new Request
            {
                Firstname = model.c_FirstName,
                Lastname = model.c_LastName,
                Email = model.c_Email,
                Phonenumber = model.c_PhoneNumber,
                Createddate = DateTime.Now,
                Status = 1,
                Requesttypeid = 3,
              
            };

            _context.Requests.Add(request);
            Requestclient requestclient = new Requestclient
            {
                Firstname = model.FirstName,
                Lastname = model.LastName,
                Email = model.email,
                Zipcode = model.Zipcode,
                State = model.State,
                City = model.City,
                Street = model.Street,
                Request = request,
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

        }
    }
}
