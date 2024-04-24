using DataLayer.DataContext;
using DataLayer.Models;
using DataLayer.ViewModels;
using LogicLayer.Interface_patient;
using Microsoft.AspNetCore.Http;
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
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public void InsertPatientRequestData(PatientInfo model)
        {
            User user = new User()
            {
                Firstname = model.FirstName,
                Lastname = model.LastName,
            };
            _context.Users.Add(user);
            _context.SaveChanges();

        }
            
    }
}
