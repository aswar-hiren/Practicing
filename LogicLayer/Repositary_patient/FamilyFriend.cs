    using DataLayer.DataContext;
using DataLayer.Models;
using DataLayer.ViewModels;
using LogicLayer.Interface_patient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace LogicLayer.Repositary_patient
{


    public class FamilyFriend : IFamilyFriend
    {

        private readonly HellodocPrjContext _context;
        public FamilyFriend(HellodocPrjContext context)
        {

            _context = context;


        }
        public void InsertFriendReq(FriendViewModel model)
        {
         
            Request request = new Request
            {
                Firstname = model.f_FirstName,
                Lastname = model.f_LastName,
                Email = model.f_Email,
                Phonenumber = model.f_PhoneNumber,
                Createddate = DateTime.Now,
                Status = 1,
                Requesttypeid=2

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
                Phonenumber = model.PhoneNumber,
                Zipcode = model.Zipcode,
                State = model.State,
                City = model.City,
                Street = model.Street,
                Request = request,
                BirthDate=model.birthdate,
         
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
    }
}
