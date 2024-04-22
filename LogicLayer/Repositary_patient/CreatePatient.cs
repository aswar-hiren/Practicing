using DataLayer.DataContext;
using DataLayer.Models;
using DataLayer.ViewModels;
using LogicLayer.Interface_patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Repositary_patient
{
    public class CreatePatient : ICreatePatientReq
    {   private readonly HellodocPrjContext _context;
        public CreatePatient(HellodocPrjContext context)
        {
            _context = context;
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public void AddPatient(CreatePatientModel model)
        {
             var aspnetuser=_context.Aspnetusers.Where(u=>u.Email==model.Email).FirstOrDefault();
            if (aspnetuser==null) {
                var userdata = _context.Requestclients.Where(u => u.Email == model.Email).FirstOrDefault();
                Aspnetuser aspnetuser1 = new Aspnetuser
                {
                    Id = RandomString(5),
                    Username = userdata.Firstname+ "_" + userdata.Lastname,
                    Email = model.Email,
                    Passwordhash = model.passwordhash,
                   Roleid=3,


                };
                _context.Aspnetusers.Add(aspnetuser1);
                _context.SaveChanges();
                aspnetuser = aspnetuser1;
            }
            var userone = _context.Users.FirstOrDefault(u => u.Aspnetuserid == aspnetuser.Id);
            if (userone==null)
            {
                
                var userdata = _context.Requestclients.Where(u => u.Email == model.Email).FirstOrDefault();
                var request = _context.Requests.Where(u => u.Requestid==userdata.Requestid).FirstOrDefault();
                User user = new User
                {

                    Firstname = userdata.Firstname,
                    Lastname = userdata.Lastname,
                    Email = userdata.Email,
                    Mobile = userdata.Phonenumber,
                    Zipcode = userdata.Zipcode,
                    State = userdata.State,
                    City = userdata.City,
                    Street = userdata.Street,
                    Createdby = "patient",

                    Aspnetuserid = aspnetuser.Id,
                    Createddate = DateTime.Now,


                    Aspnetuser = aspnetuser,
                };

                _context.Users.Add(user);
                _context.SaveChanges();


                request.User = user;
                _context.SaveChanges();
            }
        }
    }
}
