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
    public class BusinessData : IBusiness
    {
        private readonly HellodocPrjContext _context;
        public BusinessData(HellodocPrjContext context) {
            _context = context;
        }   
        public void InsertBusinessData(BusinessViewModel model, string AspId)
        {

            User user = _context.Users.Where(u=>u.Aspnetuserid==AspId).FirstOrDefault();
            Request request = new Request
            {
                Firstname = model.b_FirstName,
                Lastname = model.b_LastName,
                Email = model.b_Email,
                Createddate = DateTime.Now,
                Status = 1,
                Requesttypeid= 4,
                Phonenumber=model.b_PhoneNumber,
                User=user,

            };
            _context.Requests.Add(request);
            Requeststatuslog requeststatuslog = new Requeststatuslog
            {
                Createddate=DateTime.Now,
                Requestid=request.Requestid,
                Request=request
            };
            _context.Requeststatuslogs.Add(requeststatuslog);
            Requestclient requestclient = new Requestclient
            {
                Firstname = model.FirstName,
                Lastname = model.LastName,
                Email = model.Email,
                Zipcode = Convert.ToString(model.Zipcode),
                State = model.State,
                City = model.City,
                Street = model.Street,
                Phonenumber = model.b_PhoneNumber,
                BirthDate = model.date,
                Requestid = request.Requestid,
                Request = request,


                Notes = model.details

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
        public void InsertBusinessData(BusinessViewModel model)
        {



         

            Request request = new Request
            {
                Firstname = model.b_FirstName,
                Lastname = model.b_LastName,
                Email = model.b_Email,
                Createddate = DateTime.Now,
                Status = 1,
                Requesttypeid = 4,
                Phonenumber = model.b_PhoneNumber,
             




            };
            _context.Requests.Add(request);
            Requestclient requestclient = new Requestclient
            {
                Firstname = model.FirstName,
                Lastname = model.LastName,
                Email = model.Email,
                Zipcode = Convert.ToString(model.Zipcode),
                State = model.State,
                City = model.City,
                Street = model.Street,
                Phonenumber = model.b_PhoneNumber,
                BirthDate = model.date,
                Requestid = request.Requestid,
                Request = request,


                Notes = model.details

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
