using DataLayer.DataContext;
using DataLayer.Models;
using DataLayer.ViewModel;
using DataLayer.ViewModels;
using LogicLayer.Interface_patient;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
namespace LogicLayer.Repositary_patient
{
    public class PatientRequest : IPatientRequest
    {
        private readonly HellodocPrjContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;


        public PatientRequest(HellodocPrjContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {

            _context = context;
            this.hostingEnvironment = hostingEnvironment;

        }

        public uservm getUserDat(string search)
        {
            if (search == null)
            {
                uservm providervm = new uservm();
                providervm.user = _context.Users.Include(u => u.CityNavigation).ToList();
                return providervm;
            }
            else
            {
                uservm providervm = new uservm();
              
                providervm.user = _context.Users.Include(us => us.CityNavigation).Where(us => us.FirstName.ToLower().Contains(search.ToLower())).ToList();
                return providervm;
            }
        }
        public void Adduser(uservm model)
        {
         
            try
            { if(model.Gender == null || model.cityid== null)
                {
                    throw new Exception("Select The Gender");
                }
                var city = _context.Cities.Where(c => c.CityId == model.cityid).FirstOrDefault();
                User user = new User()
                {
                    FirstName = model.firstName,
                    LastName = model.lastName,
                    PhoneNumber = model.phonenumber,
                    Gender = model.Gender,
                    City = model.city,
                    Country = model.country,
                    Email = model.email,
                    CityNavigation = city,
                };
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
         
        }
        public void Updateuser(uservm model)
        {
          
            try
            {
                if (model.Gender == null || model.cityid == null)
                {
                    throw new Exception("Select The Gender");
                }
                var city = _context.Cities.Where(c => c.CityId == model.cityid).FirstOrDefault();
                var user = _context.Users.FirstOrDefault(u => u.Userid == model.id);
                if (user == null)
                {
                     user.FirstName = model.firstName;
                user.LastName = model.lastName;
                user.PhoneNumber = model.phonenumber;
                user.Gender = model.Gender;
                user.City = model.city;
                    user.Country = model.country;
                user.Email = model.email;
                user.CityNavigation = city;
              
               
                _context.SaveChanges();
                }
               
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public List<City> getcity()
        {
            return _context.Cities.ToList();
        }
        public void Userdelete(int id) {

            var user= _context.Users.Include(u=>u.CityNavigation).FirstOrDefault(u=>u.Userid==id);   
            _context.Remove(user);
            _context.SaveChanges();


        }
        public uservm getUser(int id)
        {
            var user= _context.Users.Include(u=>u.CityNavigation).FirstOrDefault(u => u.Userid == id);
            uservm uservm = new uservm();

            uservm.firstName = user.FirstName;
            uservm.lastName = user.LastName;
            uservm.email = user.Email;
            uservm.Gender = user.Gender;
            uservm.phonenumber = user.PhoneNumber;
            uservm.cityid = user.CityNavigation.CityId;
            uservm.country = user.Country;
            uservm.id = id;
            return uservm;

        }
    }
}
