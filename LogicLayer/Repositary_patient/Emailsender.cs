using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.Interface_patient;
using DataLayer.ViewModels;
using DataLayer.Models;
using DataLayer.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace LogicLayer.Repositary_patient
{
    public class Emailsender :IEmailsender
    {
        private readonly HellodocPrjContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public Emailsender(HellodocPrjContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public void SendEmail(string email, string body, string subject,string emailto,string filepath)
        {


          
            string fromemail = "tatva.dotnet.hirenaswar@outlook.com";
            string frommailpassword = "#Aswar2002";
            var data= _context.Requestclients.Include(rc=>rc.Request).Where(asp=>asp.Email == emailto).FirstOrDefault();
            MailMessage mailmessage = new MailMessage(new MailAddress(fromemail), new MailAddress(emailto));
            mailmessage.Subject = subject;
            mailmessage.Body = body;
            mailmessage.IsBodyHtml = true;
            if (filepath != "not") {
                Attachment attachment= new Attachment(filepath);
                mailmessage.Attachments.Add(attachment);
            }

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.office365.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            System.Net.NetworkCredential credential = new System.Net.NetworkCredential();
            smtpClient.UseDefaultCredentials = false;
            credential.UserName = fromemail;
            credential.Password = frommailpassword;

            smtpClient.Credentials = credential;
            try
            {
                smtpClient.Send(mailmessage);
                Emaillog emaillog = new Emaillog()
                {
                    Recievername=data.Firstname,
                    Requestid=data.Requestid,
                    Roleid=3,
                    Emailid=emailto,
                    Createdate=data.Request.Createddate,
                    Subjectname=subject,
                    Sentdate = DateTime.Now,
                    Isemailsent = new BitArray(1, true),
                };
                _context.Emaillogs.Add(emaillog);
                _context.SaveChanges();
               
            }
            catch (Exception)
            {

                throw;
            }
        

        }
    
    }
}
