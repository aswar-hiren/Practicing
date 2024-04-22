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
    public class ResetPassword : IResetPassword
    {
        private readonly HellodocPrjContext _context;

        public ResetPassword(HellodocPrjContext context)
        {
              _context = context;
        }

        public string getaspuser(string email)
        {
            Aspnetuser emaildata= _context.Aspnetusers.Where(u => u.Email == email).FirstOrDefault();
            if(emaildata  == null)
            {
                return "notexist";
            }
            else
            {
                return emaildata.Id;
            }
        }
     
        public void ResetThepass(ResetPassViewModel model, string aspid)
        {
            Aspnetuser aspuser= _context.Aspnetusers.FirstOrDefault(u => u.Id == aspid);
            
            aspuser.Passwordhash = model.pass;

            _context.SaveChanges();

        }
    }
}
