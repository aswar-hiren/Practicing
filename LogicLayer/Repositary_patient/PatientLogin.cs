using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataContext;
using DataLayer.Models;
using DataLayer.ViewModels;
using LogicLayer.Interface_patient;
using Microsoft.EntityFrameworkCore;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace LogicLayer.Repositary_patient
{
    public class PatientLogin : IPatientLogin
    {
        private readonly HellodocPrjContext _context;
        public PatientLogin(HellodocPrjContext context)
        {
            _context = context;
        }
        public Aspnetuser ValidateLogin(PatientLoginView model)
        {
            return _context.Aspnetusers.FirstOrDefault(Au => Au.Email == model.email && Au.Passwordhash == model.passwordhash);
           
        }

		public Physician Getphy(string ID)
		{
            return _context.Physicians.Where(ph => ph.Aspnetuserid == ID).FirstOrDefault();
		}

	
	}
}
