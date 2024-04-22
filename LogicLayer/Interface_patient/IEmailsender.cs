using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Interface_patient
{
    public interface IEmailsender
    {
        public void SendEmail(string email, string body, string subject,string model,string filepath);
    }
}
