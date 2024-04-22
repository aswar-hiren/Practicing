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
        public int InsertPatientRequestData(PatientInfo model)
        {
            Aspnetuser aspnetuser = _context.Aspnetusers.FirstOrDefault(u => u.Email == model.Email);


            Debug.WriteLine(aspnetuser);

            var userid=1;
            if (aspnetuser == null)
            {
                Aspnetuser aspnetuser1 = new Aspnetuser
                {
                    Id = RandomString(5),
                    Username = model.FirstName + "_" + model.LastName,
                    Email = model.Email,
                    Passwordhash = model.PasswordHash,
                  
                    Roleid=3

                };
                _context.Aspnetusers.Add(aspnetuser1);
                aspnetuser = aspnetuser1;

                _context.SaveChanges();

            }

            var userone = _context.Users.FirstOrDefault(u => u.Aspnetuserid == aspnetuser.Id);
            if (userone == null)
            {

                User user = new User
                {

                    Firstname = model.FirstName,
                    Lastname = model.LastName,
                    Email = model.Email,
                    Mobile = model.PhoneNumber,
                    Zipcode = model.Zipcode,
                    State = model.State,
                    City = model.City,
                    Street = model.Street,
                    Createdby = "patient",

                    Aspnetuserid = aspnetuser.Id,
                    Createddate = DateTime.Now,
                    
                    
                    Aspnetuser = aspnetuser,
                };

                _context.Users.Add(user);
                _context.SaveChanges();
                Aspnetuserrole aspnetuserrole = new Aspnetuserrole
                {
                    Userid = user.Userid,
                    User = user,
                    Roleid = 3,


                };
                _context.Aspnetuserroles.Add(aspnetuserrole);
                _context.SaveChanges();


                Request request = new Request
                {
                    Userid = user.Userid,
                    Firstname = model.FirstName,
                    Lastname = model.LastName,
                    Phonenumber = model.PhoneNumber,
                    Email = model.Email,
                    Createddate = DateTime.Now,
                
                    Status = 1,
                    User = user,
                    Requesttypeid=1,
                    
                   

                };

                _context.Requests.Add(request);
                _context.SaveChanges();
                Requeststatuslog requeststatuslog = new Requeststatuslog
                {
                    Createddate = DateTime.Now,
                    Requestid = request.Requestid,
                    Request = request
                };
                _context.Requeststatuslogs.Add(requeststatuslog);
                _context.SaveChanges();
                Requestclient requestclient = new Requestclient
                {
                    Firstname = model.FirstName,
                    Lastname = model.LastName,
                    Email = model.Email,
                    Zipcode = model.Zipcode,
                    State = model.State,
                    City = model.City,
                    Street = model.Street,
                    Request = request,
                    Phonenumber=model.PhoneNumber,
                    BirthDate=model.date,
           
                    Notes=model.Details,

                   
                   

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
                    requestclient.Region=regionone;
                    requestclient.Regionid = regionone.Regionid;    
                    _context.SaveChanges();
                }
                else
                {
                    requestclient.Regionid = region.Regionid;
                    requestclient.Region = region;
                }
                string? uniquefilename = null;
                if (model.Photo != null)
                {
                    string uploadfolder = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                    uniquefilename = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filepath = Path.Combine(uploadfolder, uniquefilename);
                    model.Photo.CopyTo(new FileStream(filepath, FileMode.Create));

                }
                Requestwisefile requestwisefile = new Requestwisefile
                {
                    Requestid = request.Requestid,
                    Filename = uniquefilename,
                    Request = request,
                    Createddate = DateTime.Now,

                };
                _context.Requestwisefiles.Add(requestwisefile);
                _context.SaveChanges();

                userid = user.Userid;
                
            }


            else
            {
                 userid= userone.Userid;

                
                Request request = new Request
                {
                    Userid = userone.Userid,
                    Firstname = model.FirstName,
                    Lastname = model.LastName,
                    Phonenumber = model.PhoneNumber,
                    Email = model.Email,
                    Createddate = DateTime.Now,
                    Status = 1,
                    User = userone,
                   
                    Requesttypeid=1

                };

                _context.Requests.Add(request);
                _context.SaveChanges();
                Requeststatuslog requeststatuslog = new Requeststatuslog
                {
                    Createddate = DateTime.Now,
                    Requestid = request.Requestid,
                    Request = request
                };
                _context.Requeststatuslogs.Add(requeststatuslog);
                _context.SaveChanges();
                Requestclient requestclient = new Requestclient
                {
                    Firstname = model.FirstName,
                    Lastname = model.LastName,
                    Email = model.Email,
                    Zipcode = model.Zipcode,
                    State = model.State,
                    City = model.City,
                    Street = model.Street,
                    BirthDate=model.date,
                    Request = request,
                    Phonenumber=model.PhoneNumber,
                  
                    Notes=model.Details

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
                string uniquefilename = null;
                if (model.Photo != null)
                {
                    string uploadfolder = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                    uniquefilename = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filepath = Path.Combine(uploadfolder, uniquefilename);
                    model.Photo.CopyTo(new FileStream(filepath, FileMode.Create));

                }
                Requestwisefile requestwisefile = new Requestwisefile
                {
                    Requestid = request.Requestid,
                    Filename = uniquefilename,
                    Request = request,
                    Createddate = DateTime.Now,

                };
                _context.Requestwisefiles.Add(requestwisefile);
                    _context.SaveChanges();
            }
            return userid;
        }
    }
}
