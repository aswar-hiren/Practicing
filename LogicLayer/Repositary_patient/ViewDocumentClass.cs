    using DataLayer.DataContext;
using DataLayer.Models;
using DataLayer.ViewModels;
using LogicLayer.Interface_patient;
using Microsoft.AspNetCore.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace LogicLayer.Repositary_patient
{
    public class ViewDocumentClass : IViewDocument
    {
        private readonly HellodocPrjContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public ViewDocumentClass(HellodocPrjContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
                _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public Request Getrequest(int reqid)
        {
            return _context.Requests.FirstOrDefault(u => u.Requestid == reqid);
        }

        public User Getuser(int? userid)
        {
            return _context.Users.FirstOrDefault(u => u.Userid == userid);

        }
        public Aspnetuser Getaspuser(String aspuser)
        {
            return _context.Aspnetusers.FirstOrDefault(u => u.Id == aspuser); 

        }

        public List<Requestwisefile> ShowDocument(viewdocumentmodel model, int reqid, Request requestdata)
        {
            string uniquefilename = null;

            if (model.Photo != null)
            {
                string uploadfolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                uniquefilename = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filepath = Path.Combine(uploadfolder, uniquefilename);
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    model.Photo.CopyTo(stream);
                }
                Requestwisefile requestwisefile = new Requestwisefile
                {
                    Requestid = reqid,
                    Filename = uniquefilename,
                    Createddate = DateTime.Now,
                    Request= requestdata


                };
                _context.Requestwisefiles.Add(requestwisefile);
                _context.SaveChanges();
            }
            var documents = _context.Requestwisefiles.Where(u => u.Requestid == reqid).ToList();
            return documents;
        }
    }
}
