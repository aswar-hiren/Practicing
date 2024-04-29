using DataLayer.Models;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO.Compression;
using hallocdoc.Helpers;
using LogicLayer.Interface_patient;
using DataLayer.DataContext;
using Microsoft.Extensions.Caching.Memory;
using LogicLayer.Interface_Admin;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace hallocdoc.Controllers
{
   
    public class PatientController : Controller
    {
        private readonly ILogger<PatientController> _logger;
        private readonly HellodocPrjContext context;     
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;
        private readonly IPatientLogin _PatientLogin;
        private readonly IPatientRequest _PatientRequest;
        private readonly IFamilyFriend _FamilyFriend;
        private readonly IBusiness _Business;
        private readonly IConcierge _Concierge;
        private readonly IPatientDashBoard _patientDashBoard;
        private readonly IPatientDashForm _patientDashForm;
        private readonly IViewDocument _viewDocument;
        private readonly IDownlod _downlod;
        private readonly IReqWiseFiles _reqWise;
        private readonly IUpdatePatientProfile _updatePatientProfile;
        private readonly IEmailsender _emailsender;
        private readonly IMemoryCache _memoryCache;
        private readonly IResetPassword _resetPassword;
        private readonly ICreatePatientReq _createPatientReq;
        private readonly IAdminRequest _adminrequest;

        public PatientController(ILogger<PatientController> logger, IAdminRequest adminRequest, ICreatePatientReq createPatientReq, IResetPassword resetPassword, IMemoryCache memoryCache, IEmailsender emailsender, IUpdatePatientProfile updatePatientProfile,  IReqWiseFiles reqWise, IDownlod downlod,  IViewDocument viewDocument, IPatientDashForm patientDashForm, IPatientDashBoard patientDashBoard, IConcierge concierge, HellodocPrjContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,IPatientLogin patientLogin,IPatientRequest patientRequest,IFamilyFriend familyFriend,IBusiness business)
        {
            _logger = logger;
            this.context = context;
            this.hostingEnvironment = hostingEnvironment;
            _PatientLogin= patientLogin;
            _PatientRequest= patientRequest;
            _FamilyFriend= familyFriend;
            _Business = business;
            _Concierge = concierge; 
            _patientDashBoard = patientDashBoard;
            _patientDashForm = patientDashForm;
            _viewDocument = viewDocument;
            _downlod = downlod;
            _reqWise = reqWise;
            _adminrequest = adminRequest;
            _updatePatientProfile = updatePatientProfile;
            _emailsender = emailsender;
            _memoryCache = memoryCache;
            _resetPassword = resetPassword;
            _createPatientReq = createPatientReq;
        }
     
        public IActionResult forgotPassword(forgetpassmodel model)
        {
            string AspId = _resetPassword.getaspuser(model.email);
            if (AspId != "notexist")
            {
                var emailKey = Guid.NewGuid().ToString();
               HttpContext.Session.SetString("aspid", AspId);
                _memoryCache.Set(AspId, emailKey, TimeSpan.FromMinutes(30));
                var resetLink = "<a href=" + Url.Action("Resetpass", "Patient",new { uid = AspId }, "http") + ">Reset Password</a>";
                var subject = "Request to Reset Password";
                var body = "Hi" + "USER" + "Click on link below to reset your password " + resetLink;
                try
                {
                    _emailsender.SendEmail(AspId, body, subject, model.email, "not");
                    var tmp = _memoryCache.Get(AspId);
                    TempData["success"] = "Mail Send Succesfully";
                    return RedirectToAction("PatientLoginPage");
                }
                catch (Exception)
                {

                    TempData["error"] = "Error WHile Send Mail";
                    return RedirectToAction("PatientLoginPage");
                }
             
            }
            else
            {
                TempData["msg"] = "<script>alert('Change succesfully');</script>";              
                return RedirectToAction("ForgetpassPatient");
            }
          
        }
        public IActionResult BackToWeb()
        {
            return View();
        }
     
        public IActionResult submitreqtype()
        {
            return View();
        }
        public IActionResult Resetpass()
        {
            return View();
        }
      
      
     
        public IActionResult Reset_pwd(ResetPassViewModel model)
        {
            var Aspid = HttpContext.Session.GetString("aspid");
            try
            {
                if(model.pass != model.confirmpass)
                {
                    throw new Exception("Password Not Matched");
                }
                _resetPassword.ResetThepass(model, Aspid);
                TempData["success"] = "password reset successfully";
                return RedirectToAction("ResetPass", "Patient");
            }
            catch (Exception ex)
            {

                TempData["error"] = ex.Message;
                return RedirectToAction("ResetPass", "Patient");
            }
     
        }

        [Route("/Patient/PatientInfoPage/checkemail/{email}")]
        [HttpGet]
        public IActionResult CheckEmail(PatientLoginView model)
        {
            var emailExists = context.Aspnetusers.Any(u => u.Email == model.email);
            return Json(new { exists = emailExists });
        }
        [HttpPost]
      
        public IActionResult PatientLogin(PatientLoginView model)
        {
            var aspuser = _PatientLogin.ValidateLogin(model); 
            
            if (aspuser != null)
            {

                if (aspuser.Roleid == 3)
                {

                    var token = JwtHelper.GenerateJwtToken("this is my custom Secret key for authentication", "localhost", "http://localhost:5202/", model.email, aspuser.Roleid);
                    var user = context.Users.FirstOrDefault(u => u.Aspnetuserid == aspuser.Id);
                    HttpContext.Session.SetInt32("userid", user.Userid);
                    HttpContext.Session.SetString("aspuserid", aspuser.Id);
                    HttpContext.Session.SetString("username", user.Firstname); 
                    Response.Cookies.Append("jwt", token);
                    TempData["success"] = "User LogIn Successfully";
                    return RedirectToAction("Patient_dashboard", "Patient" ,new {page=1,pageSize=8});
                   
                }
                if (aspuser.Roleid == 1)
                {
                    var admin = _adminrequest.GetadminOne(aspuser.Id);
                    var roleCookie = new Cookie()
                    {
                        Name = "RoleMenu",
                        Value = admin!.Accessrole.Roleid.ToString()
                    };
                    Response.Cookies.Append(roleCookie.Name, roleCookie.Value!);
                    var token = JwtHelper.GenerateJwtToken("this is my custom Secret key for authentication", "localhost", "http://localhost:5202/", model.email, aspuser.Roleid);
                    HttpContext.Session.SetString("token", token);
                    Response.Cookies.Append("jwt", token);
                    HttpContext.Session.SetString("aspuser",aspuser.Id );
                    TempData["success"] = "User LogIn Successfully";
                    return RedirectToAction("MainPage", "Admin");
                }
                if (aspuser.Roleid == 2)
                {
                    var token = JwtHelper.GenerateJwtToken("this is my custom Secret key for authentication", "localhost", "http://localhost:5202/", model.email, aspuser.Roleid);
                    HttpContext.Session.SetString("token", token);
                    Response.Cookies.Append("jwt", token);
                    HttpContext.Session.SetString("aspuser", aspuser.Id);
                    var phy= _PatientLogin.Getphy(aspuser.Id);
              
                    var roleCookie = new Cookie()
                    {
                        Name = "RoleMenu",
                        Value = phy.Roleid.ToString(),
                    };
                    Response.Cookies.Append(roleCookie.Name, roleCookie.Value!);
                    HttpContext.Session.SetInt32("phyid", phy.Physicianid);
                    TempData["success"] = "User LogIn Successfully";
                    return RedirectToAction("ProviderDashBoard", "Provider");
                }
                else
                {
                    return RedirectToAction("PatientLoginPage", "Patient");
                }
            }
            else
            {
                TempData["error"] = "Username or Password is Incorrect";
                ModelState.AddModelError(string.Empty, "Invalid email or Password");
                return View("PatientLoginPage");
            }

        }
        public IActionResult PatientLoginPage()
        {
            Response.Cookies.Delete("jwt");
            return View();
        }
        public IActionResult LogOut()
        {
            Response.Cookies.Delete("jwt");
            return RedirectToAction("PatientLoginPage","Patient");
        }
        public IActionResult ForgetpassPatient()
        {
            return View();
        }
        public IActionResult PatientInfo()
        {
            return View();
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [HttpPost]
        public IActionResult PatientInfoPage(PatientInfo model)
        {
            try
            {
                if(model.PasswordHash != null)
                {
                   if(model.PasswordHash != model.cPasswordHash)
                    {
                        throw new Exception("Password Not Matched");
                    }
                }
                var userid = _PatientRequest.InsertPatientRequestData(model);
                HttpContext.Session.SetInt32("userid", userid);
                TempData["success"] = "Request Insert Successfully";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {

                TempData["error"] = ex.Message;
                return RedirectToAction("PatientInfo");
            }
          

        }
        public IActionResult FamilyFriendPage(FriendViewModel model)
        {
            try
            {
                _FamilyFriend.InsertFriendReq(model);
                TempData["success"] = "Inserted Successfully";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {

                TempData["error"] = "Error While Insert";
                return RedirectToAction("Index", "Home");
            }
                   
            }

        public IActionResult FamilyFriend()
        {
            return View();
        }

        public IActionResult Business()
        {
            return View();
        }
        public IActionResult Businessinfo(BusinessViewModel model)
        {
            string AspId = _resetPassword.getaspuser(model.Email);
            if (AspId != "notexist")
            {
                try
                {
                    _Business.InsertBusinessData(model, AspId);
                    TempData["success"] = "Request Added";
                }
                catch (Exception)
                {
                    TempData["error"] = "Error While Request Added";

                }
                
                 }
          
            if (AspId == "notexist")
            {
                try
                {
                    _Business.InsertBusinessData(model);
                    TempData["success"] = "Request Added";
                }
                catch (Exception)
                {
                    TempData["error"] = "Error While Request Added";

                }
               
                var emailKey = Guid.NewGuid().ToString();              
                _memoryCache.Set(AspId, emailKey, TimeSpan.FromMinutes(30));
                var resetLink = "<a href=" + "http://localhost:5202/Patient/CreatePatient" + ">Request For Patient</a>";
                var subject = "Request to Create Account";
                var body = "Hi" + "USER" + "Click on link below to reset your password " + resetLink;
                try
                {
                    _emailsender.SendEmail("al", body, subject, model.Email, "not");
                    TempData["success"] = "Mail Send Succesfully";
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception)
                {

                    TempData["error"] = "Error While Mail Send ";
                    return RedirectToAction("Index", "Home");
                }
               
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

           
        }
        public IActionResult CreatePatientData(CreatePatientModel model)
        {
            try
            {   if(model.passwordhash != model.confirmpass)
                {
                    throw new Exception("Password Not Matched");
                }

                _createPatientReq.AddPatient(model);
                TempData["success"] = "Added Successfully";
                return RedirectToAction("CreatePatient");
            }
            catch (Exception ex)
            {

                TempData["error"] = ex.Message;
                return RedirectToAction("CreatePatient");
            }
          
        }
        public IActionResult CreatePatient()
        {          
            return View();
        }
        public IActionResult ConciegePage(ConciegeViewModel model)
        {
            string AspId = _resetPassword.getaspuser(model.email);
            if (AspId != "notexist")
            {
                try
                {
                    _Concierge.InsertConciegegeData(model, AspId);
                    TempData["success"] = "Added Successfully";
                }
                catch (Exception)
                {

                    TempData["error"] = "Error While Added ";
                }
          
            
            }          
            if (AspId == "notexist")
            {
                try
                {
                    _Concierge.InsertConciegegeData(model);
                    TempData["success"] = "Added Successfully";
                }
                catch (Exception)
                {


                    TempData["error"] = "Error While Added ";
                }
             
                var emailKey = Guid.NewGuid().ToString();
                _memoryCache.Set(AspId, emailKey, TimeSpan.FromMinutes(30));
                var resetLink = "<a href=" + "http://localhost:5202/Patient/CreatePatient" + ">Request For Patient</a>";
                var subject = "Request to Create Account";
                var body = "Hi" + "USER" + "Click on link below to reset your password " + resetLink;
                try
                {
                    _emailsender.SendEmail("al", body, subject, model.email, "not");
                    TempData["success"] = "Mail Send Succesfully";
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception)
                {

                    TempData["error"] = "Error While Mail Send ";
                    return RedirectToAction("Index", "Home");
                }
              
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
        }
        public IActionResult Concierge()
        {
            return View();
        }
        [CustomAuthorize("user")]
        public IActionResult Patient_dash_formpage(Patient_dash_info model)
        {    
                PatientDashboard patientDashboard = new PatientDashboard();
         
                if (HttpContext.Session.GetInt32("userid") != null)
                {
                    var temp = HttpContext.Session.GetInt32("userid");
                    var userone = context.Users.Where(u => u.Userid == temp).FirstOrDefault();
                    patientDashboard.userName = userone.Firstname;
                try
                {
                    _patientDashForm.InsertPatientDashForm(model, temp);
                    TempData["success"] = "Added Successfully";
                    return RedirectToAction("Patient_dashboard", new { page = 1, pageSize = 8 });
                }
                catch (Exception)
                {
                    TempData["error"] = " Error While Added ";
                    return RedirectToAction("Patient_dashboard", new { page = 1, pageSize = 8 });

                }
                    
                }
            return RedirectToAction("Patient_dashboard", new { page = 1, pageSize = 8 });

        }
        
        [CustomAuthorize("user")]
        public IActionResult Patient_dashboard(int page, int pageSize)
        { 
            PatientDashboard patientDashboard = new PatientDashboard();
            var patientDashboardone=patientDashboard;
            if (HttpContext.Session.GetInt32("userid") != null)
            {
                int? userone = HttpContext.Session.GetInt32("userid");
                var username= context.Users.Where(u=>u.Userid==userone).FirstOrDefault();
                patientDashboardone = _patientDashBoard.ShowRequest(userone);
                var request = patientDashboardone.requests;
                patientDashboardone.count = patientDashboardone.wiseFiles.Count;
                patientDashboardone.paginatedRequest = request.Skip((page - 1) * pageSize).Take(pageSize);
                ViewBag.CurrentPage = page;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalPages = Math.Ceiling((double)request.Count / pageSize);
                patientDashboard.userName = username.Firstname;
            }
            return View(patientDashboardone);           
        }
        [CustomAuthorize("user")]
        public IActionResult Patient_dash_form()
        {
            Patient_dash_info patient_Dash_Info = new Patient_dash_info();
            var user = HttpContext.Session.GetInt32("userid");
            var userone=context.Users.Where(u=>u.Userid==user).FirstOrDefault();
            patient_Dash_Info.username = userone.Firstname;
            ViewBag.data1 = context.Requestwisefiles.ToList();           
            return View(patient_Dash_Info);
        }
        

        [CustomAuthorize("user")]
        public IActionResult ViewDocument(int reqid, viewdocumentmodel model)
        {
            int? user = HttpContext.Session.GetInt32("userid");
            var aspuser = HttpContext.Session.GetInt32("userid");
            var userone = _viewDocument.Getuser(user);
            var requestdata = _viewDocument.Getrequest(reqid);         
            ViewBag.FirstName = requestdata.Firstname;
            ViewBag.LastName = userone.Lastname;
            ViewBag.Uploder = requestdata.Firstname;
            ViewBag.requestid = reqid;
            ViewBag.documents = _viewDocument.ShowDocument(model,reqid,requestdata);                   
            return View();
        }
        [CustomAuthorize("user")]
        [HttpGet("download")]
        public IActionResult Download(int documentid)
        {
                var filename = _downlod.GetFile(documentid);
                var filepath = Path.Combine(hostingEnvironment.WebRootPath, "uploads", filename.Filename);
                return File(System.IO.File.ReadAllBytes(filepath), "multipart/form-data", System.IO.Path.GetFileName(filepath)); 
        }
        [CustomAuthorize("user")]   
        public IActionResult DownLoadAll(viewdocumentmodel model, int requestid, int reqclientid)
        {

            HttpContext.Session.SetInt32("reqid", requestid);
            HttpContext.Session.SetInt32("reqclientid", reqclientid);
            var zipName = $"filename.zip";

            using (MemoryStream ms = new MemoryStream())
            {
                //required: using System.IO.Compression;  
                using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
                {
                    //QUery the Products table and get all image content  
                    List<Requestwisefile> reqfiles = new List<Requestwisefile>();
                    if(model.allfile==null)
                    {
                        TempData["error"] = "Select Any One File";
                        return RedirectToAction("ViewDocument", new { reqid = requestid });
                    }
                    foreach (var item in model.allfile)
                    {
                        Requestwisefile file = _adminrequest.GetFile(item);
                        reqfiles.Add(file);
                    }
                    reqfiles.ForEach(file =>
                    {
                        byte[] fileContent = null;

                        var filepath = Path.Combine(hostingEnvironment.WebRootPath, "uploads", file.Filename);

                        var entry = zip.CreateEntry(file.Filename);

                        using (FileStream fileone = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read))

                        using (var entryStream = entry.Open())
                        {
                            fileone.CopyTo(entryStream);
                        }
                    });
                }


                return File(ms.ToArray(), System.Net.Mime.MediaTypeNames.Application.Zip, zipName);
            }
        }
        [CustomAuthorize("user")]
        public IActionResult UpdatePatientProfilePage( PatientDashboard model)
        {
            //PatientDashboard patientDashboard = new PatientDashboard();
            //patientDashboard.PatientProfile = model.PatientProfile;

            int? user = HttpContext.Session.GetInt32("userid");
            var aspuser = HttpContext.Session.GetString("aspuserid");
            var userone = _viewDocument.Getuser(user);
            var aspuserone = _viewDocument.Getaspuser(aspuser);
            try
            {
                _updatePatientProfile.Updateprofile(model, userone, aspuserone);
                TempData["success"] = "Updated Succesfully";
                return RedirectToAction("Patient_dashboard", new { page = 1, pageSize = 8 });
            }
            catch (Exception)
            {

                TempData["error"] = "Error While Updated";
                return RedirectToAction("Patient_dashboard", new { page = 1, pageSize = 8 });
            }
           
        }
        [CustomAuthorize("user")]
        public IActionResult Patient_dash_someone()
        {  
            return View();
        }
 
        public IActionResult ReviewPatient(string token)
        {
            HttpContext.Session.SetString("tokenagree",token);
            JwtData data = _updatePatientProfile.DecodeToken(token);
            BlockView blockView = new BlockView();
            blockView.reqid = data.reqid;
            blockView.status = _updatePatientProfile.Getstatus(data.reqid);


            return View(blockView);
        }
        public IActionResult cancelPatientModel(int reqid)
        {  
           BlockView blockView = new BlockView();
            blockView.reqid = reqid;
            return PartialView("_cancel", blockView);
        }
        public IActionResult CancelCaseByPatient(int reqid,BlockView model)
        {
            string token = HttpContext.Session.GetString("tokenagree");
            _updatePatientProfile.CancelCasePatient(reqid, model);
            return RedirectToAction("ReviewPatient", new { token = token });
        }
        public IActionResult AgreeRequest()
        {
            string token=  HttpContext.Session.GetString("tokenagree");
            JwtData data=  _updatePatientProfile.DecodeToken(token);
            _updatePatientProfile.AgreePatient(data.reqid);
            return RedirectToAction("ReviewPatient", new {token=token });                                                                                                                                                                                                                 
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}