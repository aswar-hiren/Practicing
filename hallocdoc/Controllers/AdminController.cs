using DataLayer.DataContext;
using DataLayer.Models;
using DataLayer.ViewModels;

using LogicLayer.Interface_Admin;
using LogicLayer.Interface_patient;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

using hallocdoc.Helpers;
using System.IO.Compression;
using ClosedXML.Excel;

using Newtonsoft.Json;
using HalloDoc.Auth;
using LogicLayer.Repositary_patient;
using LogicLayer.Interface_Provider;

namespace hallocdoc.Controllers
{
    [CustomAuthorize("admin")]
    public class AdminController : Controller
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;
        private readonly ILogger<HomeController> _logger;
        private readonly HellodocPrjContext _context;
        private readonly IAdminRequest _adminrequest;
        private readonly IEmailsender _emailsender;
        private readonly IPatientRequest _PatientRequest;
        private readonly IProviderPanel _Provider;

        public AdminController(ILogger<HomeController> logger, IProviderPanel providerPanel, IPatientRequest patientRequest, IEmailsender emailsender, HellodocPrjContext context, IAdminRequest adminRequest, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
            _logger = logger;
            _context = context;
            _adminrequest = adminRequest;
            _emailsender = emailsender;
            _PatientRequest = patientRequest;
            _Provider = providerPanel;
        }
        public IActionResult AccessPage()
        {
            return View();
        }
        //[RoleAuthorize(6)]
        public IActionResult MainPage()
        {
            RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
            requestListAdminDash = _adminrequest.getallrequest(0);
            string id = HttpContext.Session.GetString("aspuser");
            var admin = _adminrequest.GetaspUser(id);
            ViewBag.username = admin.Firstname + " " + admin.Lastname;
            return View(requestListAdminDash);
        }
        public IActionResult AdminDashBoardTable()
        {
            return View();
        }
        [RoleAuthorize(6)]
        public IActionResult AdminDashBoard()
        {
            RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
            requestListAdminDash = _adminrequest.getallrequest(0);
            string id = HttpContext.Session.GetString("aspuser");
            var admin =_adminrequest.GetaspUser(id);
            ViewBag.username = admin.Firstname + " " + admin.Lastname;
            return View("AdminDashBoard",requestListAdminDash);
        }
        [RoleAuthorize(5)]
        public IActionResult MyProfile()
        { myProfilevm myProfilevm = new myProfilevm();
         string id=   HttpContext.Session.GetString("aspuser");
            myProfilevm = _adminrequest.getadmin(id);
            var admin = _adminrequest.GetaspUser(id);
            ViewBag.username = admin.Firstname + " " + admin.Lastname;
            return PartialView("MyProfile",myProfilevm);
        }
        public IActionResult EditProvider(int phyid)
        { 
          editProvidervm editProvidervm= new editProvidervm();
          editProvidervm = _adminrequest.getphysician(phyid);
          //ViewBag.username = editProvidervm.Firstname + " " + editProvidervm.Lastname;
          return View("EditProvider",editProvidervm);

        }
        public IActionResult DownloadPro(int reqid)
        {
            var encounter = _Provider.Getencounter(reqid);
            var filepath = Path.Combine(hostingEnvironment.WebRootPath, "Report", encounter.Report);
            return File(System.IO.File.ReadAllBytes(filepath), "multipart/form-data", System.IO.Path.GetFileName(filepath));
        }
        public IActionResult deleteProvider(int phyid)
        {
            try
            {
                _adminrequest.deletePhy(phyid);
                TempData["success"] = "Deleted Successfully";
                return RedirectToAction("Provider");
            }
            catch (Exception)
            {

                TempData["error"] = "Error In Delete";
                return RedirectToAction("EditProvider" ,new {phyid=phyid});
            }
          
         

        }
        public IActionResult New(int page, int pageSize, int requestTypeId, string patientname, int regionId)
        {
            RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
            requestListAdminDash = _adminrequest.GetNewRequest(page, pageSize, requestTypeId, patientname, regionId);
            return PartialView("New", requestListAdminDash);
        }
        public IActionResult PendingTable(int page, int pageSize, int requestTypeId, string patientname, int regionId)
        {
            RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
            requestListAdminDash = _adminrequest.GetPendingRequest(page, pageSize, requestTypeId, patientname, regionId);
            return PartialView("PendingTable", requestListAdminDash);
        }
        public IActionResult Active(int page, int pageSize, int requestTypeId, string patientname, int regionId)
        {
            RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
            requestListAdminDash = _adminrequest.GetActiveRequest(page, pageSize, requestTypeId, patientname, regionId);
            return PartialView("Active", requestListAdminDash);
        }
        public IActionResult ConcludeTable(int page, int pageSize, int requestTypeId, string patientname, int regionId)
        {
            RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
            requestListAdminDash = _adminrequest.GetConcludeRequest(page, pageSize, requestTypeId, patientname, regionId);
            return PartialView("ConcludeTable", requestListAdminDash);
        }
        public IActionResult Close(int page, int pageSize, int requestTypeId, string patientname, int regionId)
        {
            RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
            requestListAdminDash = _adminrequest.GetCloseRequest(page, pageSize, requestTypeId, patientname, regionId);
            return PartialView("Close", requestListAdminDash);
        }
        public IActionResult UnPaidTable(int page, int pageSize, int requestTypeId, string patientname, int regionId)
        {
            RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
            requestListAdminDash = _adminrequest.GetUnPaidRequest(page, pageSize, requestTypeId, patientname, regionId);
            return PartialView("UnPaidTable", requestListAdminDash);
        }
        public IActionResult ViewNotes(int reqid, RequestListAdminDash model, string add)
        {
            RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
            requestListAdminDash = _adminrequest.GetAdminNote(reqid);
            requestListAdminDash.reqid = reqid;
            if (add == "Add")
            {
                try
                {
                    TempData["success"] = "Note Added Successfully";
                    _adminrequest.AddAdminNote(reqid, model);
                }
                catch (Exception)
                {

                    TempData["error"] = "Error In Adding Note";
                }
             
            }
            requestListAdminDash = _adminrequest.GetAdminNote(reqid);
            requestListAdminDash.reqid = reqid;
            return View(requestListAdminDash);
        }

        public IActionResult ViewCase(int reqclientid ,int type)
        {
            RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
           var requestclient = _adminrequest.GetRequestclient(reqclientid);
            requestListAdminDash.status = requestclient.Request.Status;
            requestListAdminDash.Requestclient=requestclient;
            requestListAdminDash.type = type;
            return View(requestListAdminDash);
        }

        public IActionResult AssignCase(int reqid, int phyid, string textnote)
        {
            RequestListAdminDash requestListAdminDash=new RequestListAdminDash();
            try
            { if(reqid ==null || phyid ==0 || textnote == null)
                {
                    throw new Exception("Fill All The Details");
                }

            _adminrequest.AssignCaseReq(reqid, phyid, textnote);          
                requestListAdminDash = _adminrequest.getallrequest(0);
                TempData["success"] = "Assign Succesfully";
                return RedirectToAction("AdminDashBoard");

            }
            catch (Exception)
            {
                TempData["error"] = "Fill All the Field All Check Some error ";
            return RedirectToAction("AdminDashBoard");

        }


        }
        public IActionResult TransferCase(int reqid, int phyid, string textnote)
        {
            RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
            try
            {
                if (reqid == null || phyid == 0 || textnote == null)
                {
                    throw new Exception("Fill All The Details");
                }
                _adminrequest.TransferCaseReq(reqid, phyid, textnote);
                requestListAdminDash = _adminrequest.getallrequest(0);
                TempData["success"] = "Tranferd succesfully";
                return RedirectToAction("AdminDashBoard");
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction("AdminDashBoard");

            }
        
        }

        [HttpPost]
        public IActionResult Cancel(int requestid, string firstname)
        {
            RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
            requestListAdminDash.reqid = requestid;
            requestListAdminDash.firstname = firstname;

            return RedirectToAction("MainPage");
        }

        public IActionResult CancelCase(int reqid, CancelModel model)
        {
            RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
            try
            {
                _adminrequest.UpdateStatusAndNote(reqid, model);
                TempData["success"] = "Request Cancel Succesffully";
                return RedirectToAction("MainPage");
            }
            catch (Exception)
            {

                TempData["error"] = "error While Cancel Request";
                return RedirectToAction("MainPage");
            }
         
        }
        public IActionResult BlockRequest(int reqid, BlockView model)
        {
            try
            {
                _adminrequest.BlockRequest(reqid, model);
                TempData["success"] = "Request Blocked In Status 11";
                return RedirectToAction("MainPage");
            }
            catch (Exception)
            {

                TempData["error"] = " Error While Request Blocked";
                return RedirectToAction("MainPage");
            }
            
        }
        public IActionResult GetPhysician(int region)
        {
            AssignModel assignModel = new AssignModel();
            assignModel.Physicians = _adminrequest.GetPhysiciansForRegion(region);
            return PartialView("GetPhysician", assignModel);
        }
        public List<Physician> GetPhysicianforTransfer(int region)
        {
            AssignModel assignModel = new AssignModel();
            assignModel.Physicians = _adminrequest.GetPhysiciansForRegion(region);
            return assignModel.Physicians;
        }



        //models
        public IActionResult cancelModel(int reqid, string name)
        {

            CancelModel cancelModel = new CancelModel();
            cancelModel.reqid = reqid;
            cancelModel.patientname = name;
            return PartialView("Cancel", cancelModel);
        }
        public IActionResult assignModel(int reqid)
        {

            AssignModel assignModel = new AssignModel();
            assignModel.reqid = reqid;
            assignModel.Regions = _adminrequest.regionlist();

            return PartialView("Assign", assignModel);
        }

        public IActionResult ContactProvidermodel(string data,int physicianId)
        {


            AssignModel assignModel = new AssignModel();
            assignModel.email = data;
            assignModel.phy_id = physicianId;
            HttpContext.Session.SetInt32("phy_id", physicianId);
            return PartialView("ProviderPopUp",assignModel);


        }

        [HttpPost]
        public IActionResult TransferModel(int reqid)
        {
            AssignModel assignModel = new AssignModel();
            assignModel.reqid = reqid;
            assignModel.Regions = _adminrequest.regionlist();
            return PartialView("Transfer", assignModel);
        }
        public IActionResult blockModel(int reqid, string name)
        {

            BlockView blockView = new BlockView();
            blockView.reqid = reqid;
            blockView.Patientname = name;

            return PartialView("BlockModel", blockView);
        }
        public IActionResult clearModel(int reqid)
        {
            BlockView blockView = new BlockView();
            blockView.reqid = reqid;
            return PartialView("Clear", blockView);
        }
        [HttpPost]
        public IActionResult sendagreementModel(int reqid, int reqtypeid)
        {
            BlockView blockView = new BlockView();
            blockView.reqid = reqid;
            blockView.reqtypeid = reqtypeid;
            return PartialView("sendAgreeMent", blockView);
        }
        public void EditProviderSign(int physicianid, string base64string)
        {
            var physician = _context.Physicians.FirstOrDefault(u => u.Physicianid == physicianid);
            physician.Signature = base64string;
            _context.Physicians.Update(physician);
            _context.SaveChanges();
        }
        public void EditProviderPhoto(int physicianid, string base64string)
        {
            var physician = _context.Physicians.FirstOrDefault(u => u.Physicianid == physicianid);
            physician.Photo = base64string;
            _context.Physicians.Update(physician);
            _context.SaveChanges();
        }
        public IActionResult SignPad(int phyid)
        {
            editProvidervm editProvidervm = new editProvidervm();
            editProvidervm.phyid = phyid;
            return PartialView("SignPad",editProvidervm);
        }
        public IActionResult CloseCase(int reqid )
        {           
          
            var  closeCaseViewModel =   _adminrequest.getclosedetails(reqid);
            return View(closeCaseViewModel);
        }
        [HttpPost]
        public IActionResult CloseCaseDetails(int button, CloseCaseViewModel model)
        {
            if (button==1)
            {
                try
                {
                    _adminrequest.insertCaseClose(model, button);
                    TempData["success"] = "Inserted Successfully";
                    return RedirectToAction("MainPage");

                }
                catch (Exception)
                {
                    TempData["error"] = "Error While Inserting";
                    return RedirectToAction("MainPage");
                }
             
            }
            if(button==2)
            {
                try
                {
                    _adminrequest.updatereqclient(model);
                    TempData["success"] = "Update Succesfully";
                    return RedirectToAction("CloseCase", new { reqid = model.reqId });
                }
                catch (Exception)
                {

                    TempData["error"] = "Error while Update";
                    return RedirectToAction("CloseCase", new { reqid = model.reqId });
                }
      
            }
            return RedirectToAction("CloseCase");
        }
        public IActionResult Encounter(int reqclientid,int reqid)
        {
            EncounterViewModel encounterViewModel = new EncounterViewModel();
            encounterViewModel = _adminrequest.getdetails(reqclientid,reqid);         
            return View(encounterViewModel);
        }
        public IActionResult PostEncounterData(int reqid,EncounterViewModel model)
        {
            try
            {
                _adminrequest.AddEncounterDetails(reqid, model);
                TempData["success"] = "Encounter Data Inserted";
                return RedirectToAction("MainPage");
            }
            catch (Exception)
            {

                TempData["error"] = "error while Data Inserted";
                return RedirectToAction("MainPage");
            }
           
        }
        public IActionResult SendMailOfAgreement(int reqid, BlockView model)
        {
            try
            {
                var status = _adminrequest.Getstatus(reqid);
                var token = _adminrequest.GenerateJwtToken("this is my custom Secret key for authentication", "localhost", "http://localhost:5202/", reqid, status);
                var resetLink = "<a href=" + Url.Action("ReviewPatient", "Patient", new { token = token }, "http") + ">For Agreement</a>";
                var subject = "agreement for the request";
                var body = "Hi" + "USER" + "Click on link below to review Agreement " + resetLink;

             string data=   _adminrequest.SendEmail("al", body, subject, model.email, reqid);
                if(data=="true")
                { 
                TempData["success"] = "Mail Send Succesfully";
                return RedirectToAction("MainPage");
                }
                else
                {
                    TempData["error"] = "There Is no such email exist";
                    return RedirectToAction("MainPage");
                }
            }
            catch (Exception)
            {
                TempData["error"] = "Error While Sending A mail";
                return RedirectToAction("MainPage");
            }
          
        }
        public IActionResult ClearCaseDetail(BlockView model)
        {
            RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
            try
            {
                _adminrequest.clearCase(model.reqid);
                TempData["success"] = "Cleared successFully Status 10";
                return RedirectToAction("MainPage");
            }
            catch (Exception)
            {
                TempData["error"] = "Error";

                return RedirectToAction("MainPage");
               
            }
             

        }

        //models
        public IActionResult ViewUploads(int? reqclientid, int? requestid,int type, viewdocumentmodel model)
        {
            if (requestid == null || reqclientid == null)
            {
                requestid = HttpContext.Session.GetInt32("reqid");
                reqclientid = HttpContext.Session.GetInt32("reqclientid");
            } 
            viewdocumentmodel viewdocumentmodel = new viewdocumentmodel();
            viewdocumentmodel = _adminrequest.ShowDocument(reqclientid, requestid, model);
            viewdocumentmodel.type = type;
            return View(viewdocumentmodel);
        } 
        public IActionResult sendmodel()
        {
            return PartialView("SendLink");
        }
        public IActionResult SendLinkAdminDetails(SendLinkViewModel model)
        {
            Request request = _adminrequest.GetRequest(model);
            var token = _adminrequest.GenerateJwtToken("this is my custom Secret key for authentication", "localhost", "http://localhost:5202/", request.Requestid, 1);
            var resetLink = "<a href=" + Url.Action("submitreqtype", "Patient", new { token = token }, "http") + ">Request For Patient</a>";
            var subject = "Request For Patient";
            var body = "Hi" + "USER" + "Click on link below to submit request" + resetLink;

          string data=  _adminrequest.SendEmail("al", body, subject, model.email,request.Requestid);
           if(data=="true")
            {
                TempData["success"] = "Mail Send Succesfully";
                return RedirectToAction("MainPage");
            }
            else
            {
                TempData["error"] = "There is no such mail exist";
                return RedirectToAction("MainPage");
            }
            
        }
        public IActionResult adminCreateRequestt()
        {
            return View();
        }
        public IActionResult DeleteFile(int documentid, int reqclientid, int requestid)
        {
            HttpContext.Session.SetInt32("reqid", requestid);
            HttpContext.Session.SetInt32("reqclientid", reqclientid);
            try
            {
                _adminrequest.deletedoc(documentid);
                TempData["success"] = "Deleted Succesfully";
                return RedirectToAction("ViewUploads");
            }
            catch (Exception)
            {

                TempData["error"] = "Deleted Not Succesfully";
                return RedirectToAction("ViewUploads");
            }
         
           
        }
        public IActionResult deleteAllFile(List<int> documentid, int requestid, int reqclientid, int flag)
        {
            HttpContext.Session.SetInt32("reqid", requestid);
            HttpContext.Session.SetInt32("reqclientid", reqclientid);
            try
            {
                _adminrequest.deleteAllDoc(documentid, flag, requestid);
                TempData["success"] = "Deleted Succesfully";
                return RedirectToAction("ViewUploads");
                
            }
            catch (Exception)
            {

                TempData["error"] = "Deleted Not Succesfully";
                return RedirectToAction("ViewUploads");

            }
         
        }
        public IActionResult SendEmailDoc(List<int> documentid, int requestid, int reqclientid)
        {
            HttpContext.Session.SetInt32("reqid", requestid);
            HttpContext.Session.SetInt32("reqclientid", reqclientid);
            List<string> fpa = new List<string>();
            foreach (var item in documentid)
            {
                var filename = _adminrequest.getdoc(item);
                var filepath = Path.Combine(hostingEnvironment.WebRootPath, "uploads", filename.Filename);
                fpa.Add(filepath);
            }
            var email = "tatva.dotnet.hirenaswar@outlook.com";

            var subject = "Send Mail With Attchment";
            var body = "this msg is for test";
            try
            {
                _adminrequest.SendEmailUp("al", body, subject, email, fpa,requestid);
                TempData["success"] = "Mail Sent Succesfully";
            }
            catch (Exception e)
            {
                TempData["error"] = "Mail Not Sent ";
                Console.WriteLine(e);
            }
            return RedirectToAction("ViewUploads");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Orders(int reqid)
        {
            HttpContext.Session.SetInt32("orderreqid", reqid);
            OrdersModel orderModel = new OrdersModel();
            orderModel.hptype = _adminrequest.GetOrderDetails();
            return View(orderModel);
        }
        public List<Healthprofessional> GetBusinessData(int professionId)
        {
            OrdersModel orderModel = new OrdersModel();
            orderModel.hp = _adminrequest.GetOrder(professionId);
            return orderModel.hp;
        }
        public Healthprofessional GetOtherData(int businessId)
        {
            OrdersModel orderModel = new OrdersModel();
            orderModel.Healthprofessional = _adminrequest.GetBusinessData(businessId);
            return orderModel.Healthprofessional;
        }
        public IActionResult SendOrder(OrdersModel model)
        {
            int? reqid = HttpContext.Session.GetInt32("orderreqid");
            try
            {
                _adminrequest.CreateOrder(reqid, model);
                TempData["success"] = "Order Created";
                return RedirectToAction("Orders");
            }
            catch (Exception)
            {

               TempData["error"] = "Error While Order Created";
                return RedirectToAction("Orders");
            }
            

        }


        public IActionResult Downloaddoc(int documentid)
        {
            var filename = _adminrequest.GetFile(documentid);
            var filepath = Path.Combine(hostingEnvironment.WebRootPath, "uploads", filename.Filename);
            return File(System.IO.File.ReadAllBytes(filepath), "multipart/form-data", System.IO.Path.GetFileName(filepath));
        }
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
                    if (model.allfile == null)
                    {
                        TempData["error"] = "Select Any One File";
                        return RedirectToAction("ViewUploads");
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
        public IActionResult AdminProfile(int command,myProfilevm model)
        {
            if(command==1)
            {
                try
                {
                    _adminrequest.AdminResetPassword(model);
                    ViewBag.username = model.firstname + " " + model.lastname;
                    TempData["success"] = "password Reset";
                    return RedirectToAction("MyProfile");
                }
                catch (Exception)
                {

                    TempData["error"] = "error while password Reset";
                    return RedirectToAction("MyProfile");
                }
                
            }
            if(command==2)
            {
                try
                {
                    if(model.AllCheck==null)
                    {
                        TempData["error"] = "select any One Region";
                        return RedirectToAction("MyProfile");
                    }
                    _adminrequest.EditAdminDetails(model);

                    ViewBag.username = model.firstname + " " + model.lastname;
                    TempData["success"] = "Updated Succesfully";
                    return RedirectToAction("MyProfile");
                }
                catch (Exception)
                {
                    TempData["error"] = "Error While Update";
                    ViewBag.username = model.firstname + " " + model.lastname;
                    return RedirectToAction("MyProfile");
                }
              
            }
            if(command==3)
            {
                try
                {
                    _adminrequest.EditBillingDetailsAdmin(model);
                    ViewBag.username = model.firstname + " " + model.lastname;
                    TempData["success"] = "Updated Succesfully";
                    return RedirectToAction("MyProfile");
                }
                catch (Exception)
                {
                    TempData["error"] = "Error While Updated";
                    return RedirectToAction("MyProfile");
                }
                
            }
            return RedirectToAction("MyProfile");
        }
        public IActionResult ProviderProfile(int command, editProvidervm model)
        {
            if (command == 1)
            {   
                _adminrequest.AdminResetPasswordPhy(model);
                //ViewBag.username = model.firstname + " " + model.lastname;
                return RedirectToAction("EditProvider",new {phyid=model.phyid});
            }
            if (command == 2)
            {
                if (model.AllCheck == null)
                {
                    TempData["error"] = "Select Any One Region";
                    return RedirectToAction("EditProvider", new { phyid = model.phyid });
                }
                _adminrequest.EditAdminDetailsPhy(model);
                //ViewBag.username = model.firstname + " " + model.lastname;
                return RedirectToAction("EditProvider", new { phyid=model.phyid });
            }
            if (command == 3)
            {
                 _adminrequest.EditBillingDetailsPhy(model);
                //ViewBag.username = model.firstname + " " + model.lastname;
                return RedirectToAction("EditProvider", new { phyid=model.phyid });
            }
            if(command== 4)
            {              
                _adminrequest.providerProfilePhy(model);
                //ViewBag.username = model.firstname + " " + model.lastname;
                return RedirectToAction("EditProvider", new { phyid = model.phyid });
            }
            return RedirectToAction("MyProfile");
        }
        [RoleAuthorize(8)]
        public IActionResult Provider()
        {          
            Providervm providervm = new Providervm();
            providervm = _adminrequest.GetRegion();
            return View(providervm);
        }
        [HttpPost]
        public IActionResult providertable(int regionId)
        {   
                Providervm providervm = new Providervm();
                providervm = _adminrequest.getPhysicianData(regionId);
                return View(providervm);         
        }
        public IActionResult UpdateProviderDocument(editProvidervm model)
        {
            try
            {
                _adminrequest.UpdateProviderDoc(model);
                TempData["success"] = "Photo updated";
                return RedirectToAction("EditProvider", new { phyid = model.phyid });
            }
            catch (Exception)
            {

                TempData["error"] = "Error while Photo updated";
                return RedirectToAction("EditProvider", new { phyid = model.phyid });
            }
           
        }
        [HttpPost]
        public IActionResult ExportToCsv(string csv)
        {
            //var bytes = Encoding.UTF8.GetBytes(csv);
            //var result = new FileContentResult(bytes, "text/csv") { FileDownloadName = "table.csv" };
            //return result;
            // Save the CSV file on the server
            System.IO.File.WriteAllText("wwwroot/downloadCsvAdmin/file.csv", csv);
            // Create a URL to download the file
            var fileDownloadUrl = Url.Action("DownloadCsv", "Admin", new { fileName = "file.csv" });
           // Return the URL in a JSON object
            return Json(new { fileDownloadUrl = fileDownloadUrl });
        }

        [HttpGet]
        public IActionResult DownloadCsv(string fileName)
        {
            var path = "wwwroot/downloadCsvAdmin/" + fileName;
            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "text/csv", "table.csv");
        }
        public IActionResult ExportAll()
        {
            try
            {
                List<NewStateAdminVm> data;
                var query = _adminrequest.ExportAllService();
                data = query.ToList();
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Export All");
                worksheet.Cell(1, 1).Value = "Name";
                worksheet.Cell(1, 2).Value = "Date Of Birth";
                worksheet.Cell(1, 3).Value = "Requestor";
                worksheet.Cell(1, 4).Value = "Physician Name";
                worksheet.Cell(1, 5).Value = "Date of Service";
                worksheet.Cell(1, 6).Value = "Requested Date";
                worksheet.Cell(1, 7).Value = "Phone Number";
                worksheet.Cell(1, 8).Value = "Address";
                worksheet.Cell(1, 9).Value = "Notes";
                int row = 2;
                foreach (var item in data)
                {
                    var statusClass = "";
                    var dos = "";
                    var notes = "";
                    if (item.RequestTypeId == 1)
                    {
                        statusClass = "patient";
                    }
                    else if (item.RequestTypeId == 3)
                    {
                        statusClass = "concierge";
                    }
                    else if (item.RequestTypeId == 2)
                    {
                        statusClass = "friend";
                    }
                    else
                    {
                        statusClass = "business";
                    }
                    worksheet.Cell(row, 1).Value = string.Concat(item.Firstname + ',' + item.Lastname);
                    worksheet.Cell(row, 2).Value = string.Concat(item.Strmonth + ',' + item.Intdate + ',' + item.Intyear);
                    worksheet.Cell(row, 3).Value = statusClass.Substring(0, 1).ToUpper() + statusClass.Substring(1).ToLower() + " " + item.RequestorFirstname + item.RequestorLastname;
                    worksheet.Cell(row, 4).Value = "Physician";
                    worksheet.Cell(row, 5).Value = item.Createddate.ToString("MMMM dd,yyyy");
                    worksheet.Cell(row, 6).Value = "DoS";
                    worksheet.Cell(row, 7).Value = item.Phonenumber + "(Patient)";
                    worksheet.Cell(row, 8).Value = (item.Address != null ? string.Concat(item.Address, ',', item.Street, ',', item.City, ',', item.State, ',', item.Zipcode) : string.Concat(item.Street, ',', item.City, ',', item.State, ',', item.Zipcode));
                    worksheet.Cell(row, 9).Value = item.Notes;
                    row++;
                }
                worksheet.Columns().AdjustToContents();
                var memoryStream = new MemoryStream();
                workbook.SaveAs(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "All Data.xlsx");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }
        public IActionResult UpdatedNotification(int physicianId, bool isChecked)
        {
            try
            {
                _adminrequest.updateNotificationDetails(physicianId, isChecked);
                TempData["success"]="Notification Updated";
                return Json(new { isChecked });
            }
            catch (Exception)
            {

                TempData["error"] = "Error While Notification Updated";
                return Json(new { isChecked });
            }
         
        }

        public IActionResult mailsend(string email,string text)
        {
         int phyid= Convert.ToInt32(  HttpContext.Session.GetInt32("phy_id") );
            var subject = "agreement for the request";
            var body = text;
            try
            {
                _adminrequest.SendEmailPhyContact("al", body, subject, email,phyid);
                TempData["success"] = "Mail Send Successfully";
                return RedirectToAction("Provider");
            }
            catch (Exception)
            {

                TempData["error"] = "Error While Mail Send ";
                return RedirectToAction("Provider");
            }
            
        }
        //[RoleAuthorize(7)]
        public IActionResult Access()
        {
            var details = _adminrequest.GetAccessPagevm();
            return View(details);
        }
        public IActionResult EditRole(int roleId)
        {
            var data = _adminrequest.GetEditRoleDetails(roleId);
            return View(data);
        }

        public IActionResult DeleteRole(int roleId)
        {
            try
            {
                _adminrequest.DeleteRole(roleId);
                TempData["success"] = "Edited";
                return RedirectToAction("Access");
            }
            catch (Exception)
            {

                TempData["error"] = "Error";
                return RedirectToAction("Access");
            }

        }
        public IActionResult EditRoleDetails(AccessPagevm accessPagevm)
        {
            try
            {
                _adminrequest.EditRole(accessPagevm);
                TempData["success"] = "Edited";
                return RedirectToAction("EditRole", new { accessPagevm.RoleId });
            }
            catch (Exception)
            {

                TempData["error"] = "Error";
                return RedirectToAction("EditRole", new { accessPagevm.RoleId });
            }
           
        }
        public IActionResult RoleList(int accountType)
        {
            var data = _adminrequest.GetMenuDetails(accountType);
            return PartialView("RoleList", data);
        }
        public IActionResult CreateRole()
        {
            return View(); 
        }
        public IActionResult UpdatedRoleMenu(AccessPagevm accessPagevm)
        {
            try
            {
                _adminrequest.UpdateRolemenus(accessPagevm);
                TempData["success"] = "Rolemenu Updated Successfully";
                return RedirectToAction("Access");
            }
            catch (Exception)
            {

                TempData["error"] = "Error While Rolemenu Updated Successfully";
                return RedirectToAction("Access");
            }
            
        }
        public IActionResult CreateProviderAccount()
        {
            var token = HttpContext.Session.GetString("token");
            
            if (token == null)
            {
                return RedirectToAction("PatientLoginPage", "Patient");
            }
            var adminId = HttpContext.Session.GetString("aspuser");

            var details = _adminrequest.GetadminOne(adminId);
            ViewBag.Username = details.Firstname + " " + details.Lastname;
            var data = _adminrequest.createProviderDetails();
            return View(data);
        }
        [Route("/Admin/CreateAdminAccount/CheckEmail/{email}")]
        [Route("/Admin/CreateProviderAccount/CheckEmail/{email}")]
        [HttpGet]
        public IActionResult CheckEmail(string email)
        {
            var emailExist = false;
            if (email != null)
            {
                emailExist= _adminrequest.GetAspUser(email);
            }

            return Json(new { exists = emailExist });
        }
        [HttpPost]
        public IActionResult CreateProviderAccount(CreateProvidervm model)
        {
            try
            { 
                if(model.region==null)
                {
          
                    TempData["error"] = "Select Any One Region";
                    return RedirectToAction("CreateProviderAccount");
                }
                _adminrequest.insertProviderData(model);
                TempData["success"] = "Provider Created Successfully";
            }
            catch (Exception)
            {
               
                TempData["error"] = "Provider Not Created Successfully";

            }           
            return RedirectToAction("Provider");
        }
        [RoleAuthorize(2)]
        public IActionResult Scheduling()
        {
            CreateShift createShift = new CreateShift();
            createShift.Region = _adminrequest.GetRegionForShift();
            return View(createShift);
        }
    
        public IActionResult loadshift(string datestring, string sundaystring, string saturdaystring, string shifttype,int regionid)
        {
            DateTime date = DateTime.Parse(datestring);
            DateTime sunday = DateTime.Parse(sundaystring);
            DateTime saturday = DateTime.Parse(saturdaystring);
            switch (shifttype)
            {
                case "month":

                    MonthShiftModal monthShift = new MonthShiftModal();
                    var totalDays = DateTime.DaysInMonth(date.Year, date.Month);
                    var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                    var startDayIndex = (int)firstDayOfMonth.DayOfWeek;
                    var dayceiling = (int)Math.Ceiling((float)(totalDays + startDayIndex) / 7);
                    monthShift.daysLoop = (int)dayceiling * 7;
                    monthShift.daysInMonth = totalDays;
                    monthShift.firstDayOfMonth = firstDayOfMonth;
                    monthShift.startDayIndex = startDayIndex;
                    monthShift.shiftDetailsmodals = _adminrequest.ShiftDetailsmodal(date, sunday, saturday, "month");
                    return PartialView("_monthWise", monthShift);

                case "week":

                    WeekShiftModal weekShift = new WeekShiftModal();

                    weekShift.Physicians = _adminrequest.physicians(regionid);
                    weekShift.shiftDetailsmodals = _adminrequest.ShiftDetailsmodal(date, sunday, saturday, "week");

                    List<int> dlist = new List<int>();

                    for (var i = 0; i < 7; i++)
                    {
                        var date12 = sunday.AddDays(i);
                        dlist.Add(date12.Day);
                    }

                    weekShift.datelist = dlist.ToList();
                    weekShift.dayNames = new string[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

                    return PartialView("_weekWise", weekShift);

                case "day":

                    DayShiftModal dayShift = new DayShiftModal();
                    dayShift.Physicians = _adminrequest.physicians(regionid);
                    dayShift.shiftDetailsmodals = _adminrequest.ShiftDetailsmodal(date, sunday, saturday, "day");
                    dayShift.currentDate= date;
                    return PartialView("_DayWise", dayShift);

                default:
                    return PartialView("_DayWise");
            }

        }
        public string GetPhysicianForCreateShift(int RegionId)
        {
            return JsonConvert.SerializeObject(_adminrequest.GetPhysicianForCreateShift(RegionId), Formatting.Indented); 
        }
        public IActionResult ViewShift(int shiftdetailid)
        {  EditViewShift editViewShift = new EditViewShift();   

            editViewShift=_adminrequest.GetViewShift(shiftdetailid);
            return View("ViewShift",editViewShift);
        }
        public bool ReturnViewShift(int ShiftDetailId)
        {
            try
            {
                return _adminrequest.ReturnViewShift(ShiftDetailId);
            }
            catch { return false; }
        }
        public bool DeleteViewShift(int ShiftDetailId)
        {
            try
            {
                return _adminrequest.DeleteViewShift(ShiftDetailId);
            }
            catch { return false; }
        }
        public bool EditViewShift(EditViewShift ShiftDetail)
        {
            try
            {
                return _adminrequest.EditViewShift(ShiftDetail).Result;
            }
            catch { return false; }
        }
        [HttpPost]
        public bool CreateShift(CreateShift modal)
        {
            try
            {
                int AdminId = 1;
                return _adminrequest.CreateShift(modal, AdminId).Result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IActionResult SchedulingReq() {

            var data = _adminrequest.regionlistschedule();
            return View(data);         
        }
      
        public IActionResult SchedulingReqTable(int page, int pageSize, int regionid)
        {
            var dataone = _adminrequest.Shiftrequest(page, pageSize, regionid);
            return PartialView("_ScheduleReqTable", dataone);
          
        }
	
		
		public IActionResult MdOnCall()
		{
			
			var data = _adminrequest.GetRegionForMd();
			return View(data);
		}
        public IActionResult MdCallStatus(int regionId)
        {
            var data = _adminrequest.MdStatusDetails(regionId);
            return PartialView("_MdOnCallStatus", data);
        }
        public IActionResult requestApproveDelete(Schedulereqvm model, int type)
        {
            if(type==1)
            {
                if(model.allshift==null)
                {
                    TempData["error"] = "Select Alleast One";
                    return RedirectToAction("SchedulingReq");
                }
               bool flag= _adminrequest.requestapprove(model);
                if (flag)
                {
                    TempData["success"] = "Approved Succesfully";
                    return RedirectToAction("SchedulingReq");
                }
                else
                {
                    TempData["error"] = "Approved not Succesfully";
                    return RedirectToAction("SchedulingReq");
                }
             
            }
            if(type==2)
            {
              bool flag=  _adminrequest.requestdelete(model);
                if (flag)
                {
                    TempData["success"] = "Delete Succesfully";
                }
                else
                {
                    TempData["error"] = "Delete Not Succesfully";
                }
                return RedirectToAction("SchedulingReq");
            }
            return RedirectToAction("SchedulingReq");
        }
        [RoleAuthorize(17)]
        public IActionResult ProviderLocation()
        {    
            var data = _adminrequest.GetPhysicianLocations();
            return View(data);
        }
        public IActionResult AllShift(DateTime datestring)
        {
            
            var data = _adminrequest.Getshiftdetail(datestring);
            return View("_AllShift",data);
        }
        public IActionResult CreateAdminAccount()
        {

            var token = HttpContext.Session.GetString("token");

            if (token == null)
            {
                return RedirectToAction("PatientLoginPage", "Patient");
            }
            var adminId = HttpContext.Session.GetString("aspuser");

            var details = _adminrequest.GetadminOne(adminId);
            ViewBag.Username = details.Firstname + " " + details.Lastname;

            var data = _adminrequest.CreateAdminAccountDetails();
            return View(data);
        }
        [HttpPost]
        public IActionResult CreateAdminAccount(CreateAdminAccountvm adminAccount, List<int>? regions)
        {
            try
            { 
                if(adminAccount.regions==null)
                {
                    TempData["error"] = " Error ";
                    return RedirectToAction("CreateAdminAccount");
                }
                _adminrequest.CreateAdminAccount(adminAccount, regions!);
                TempData["success"] = "Admin Data Created Succesfully";
                return RedirectToAction("CreateAdminAccount");
            }
            catch (Exception)
            {
                TempData["eroor"] = " Error ";
                return RedirectToAction("CreateAdminAccount");
            }
      
            
        }
        [RoleAuthorize(11)]
        public IActionResult Vendor(int hptypeid, string Searchname, int requestype)
        {
            var data = _adminrequest.Getvenderdata(0, Searchname, 0);
            return View(data);
        }
        public IActionResult VendorTable(int hptypeid,string Searchname,int requestype)
        {

            var data = _adminrequest.Getvenderdata(hptypeid, Searchname, requestype);
            return View("_Vendortable", data);
        }
        public IActionResult AddBusiness()
        {
            var data = _adminrequest.getHptype();
            return View(data);
        }
        [HttpPost]
        public IActionResult AddBusiness(AddBusiness model)
        {
            try
            {
               _adminrequest.AddVendor(model);
                TempData["success"] = "Business Added Successfully";
                return RedirectToAction("AddBusiness");
            }
            catch (Exception)
            {

                TempData["error"] = "Error In Add Business";
                return RedirectToAction("AddBusiness");
            }
           
        }
        public IActionResult UpdateBusiness(int vendorid)
        {
            var data = _adminrequest.getVendor(vendorid);
            return View(data);
        }
        [HttpPost]
        public IActionResult UpdateBusiness(AddBusiness model)
        {
            try
            {
                _adminrequest.updateVendor(model);
                TempData["success"] = "Updated Succesfully";
                return RedirectToAction("UpdateBusiness", new { vendorid = model.vendorid });
            }
            catch (Exception)
            {
                TempData["error"] = "Error In Update";
                return RedirectToAction("UpdateBusiness", new { vendorid = model.vendorid });
            }
        
        }
        public IActionResult deleteBusiness(int vendorid)
        {
            try
            {
                _adminrequest.deleteBusiness(vendorid);
                TempData["success"] = "Business Get Deleted";
                return RedirectToAction("Vendor");
            }
            catch (Exception)
            {

                TempData["error"] = "Error ";
                return RedirectToAction("Vendor");
            }
          
        }
        public IActionResult SearchRecord()
        {
            return View();
        }

        public IActionResult SearchRecordDetails(int page, int pageSize, string patientname, string statusofrequest, int requesttype, string email, DateOnly fromdate, DateOnly todate, string providername, string phoneNumber)
        {
            var data = _adminrequest.GetSearchRecorddata(page,pageSize,patientname, statusofrequest, requesttype, email, fromdate, todate, providername, phoneNumber);
            var paginatedRequest = data.Skip((page - 1) * pageSize).Take(pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = Math.Ceiling((double)data.Count / pageSize);
            ViewBag.total = data.Count;
            return PartialView("_searchRecordDetail", paginatedRequest);
        }
        [RoleAuthorize(21)]
        public IActionResult PatientRecord()
        {           
            return View();
        }
        public IActionResult PatientRecordDetails(int page, int pageSize, string FirstName, string LastName, string Email, string PhoneNumber)
        {
            var data = _adminrequest.GetPatientRecordDetails(page, pageSize,FirstName, LastName, Email, PhoneNumber);
            return PartialView("_PatientDetail", data);
        }
        public IActionResult PatientHistory(int userid)
        {
            ViewBag.userid = userid;
            return View();
        }
        public IActionResult PatientHistoryDetails(int userid, int page, int pageSize, string FirstName, string LastName, string Email, string PhoneNumber)
        {
            var data = _adminrequest.getPatientHistoryDetails(userid,page, pageSize, FirstName, LastName, Email, PhoneNumber);
            return PartialView("_patientHistory", data);
        }
        [RoleAuthorize(22)]
        public IActionResult BlockHistory()
        {
            return View();
        }
        public IActionResult PatientBlockDetails(int page, int pageSize, string FirstName, string LastName, string Email, string PhoneNumber)
        {
            var data = _adminrequest.getPatientBlockDetails(page, pageSize, FirstName, LastName, Email, PhoneNumber);
            return PartialView("_blockHistory", data);
        }
        public IActionResult unblockRequest(int requestid)
        {
            try
            {
                _adminrequest.unBlockreq(requestid);
                TempData["success"] = "Request unblocked";
                return RedirectToAction("BlockHistory");
            }
            catch (Exception)
            {

                TempData["error"] = "Error in Request unblocked";
                return RedirectToAction("BlockHistory");
            }
        }
		public IActionResult deleteRequest(int requestid)
		{
			try
			{
				_adminrequest.deletereq(requestid);
				TempData["success"] = "Request Deleted";
				return RedirectToAction("SearchRecord");
			}
			catch (Exception)
			{

				TempData["error"] = "Error in Request Deleted";
				return RedirectToAction("SearchRecord");
			}
		}
        [RoleAuthorize(13)]
            public IActionResult EmailLog()
            {
                return View();
            }
        public IActionResult emailLogDetails(int page, int pageSize, string FirstName, int roleid, string Email, DateTime createdate, DateTime sendate)
        {
            var data = _adminrequest.getEmailLogDetails(page, pageSize, FirstName, roleid, Email, createdate,sendate);
            return PartialView("_emailLogTable", data);
        }
        [RoleAuthorize(24)]
        public IActionResult SmsLog()
        {
            return View();
        }
        public IActionResult smsLogDetails(int page, int pageSize, string FirstName, int roleid, string Phonenumber, DateTime createdate, DateTime sendate)
        {
            var data = _adminrequest.getSmsLogDetails(page, pageSize, FirstName, roleid, Phonenumber, createdate, sendate);
            return PartialView("_smsLogTable", data);
        }
        public IActionResult PatientInfoPage(PatientInfo model)
        {
            try
            {   

                var userid = _PatientRequest.InsertPatientRequestData(model);
                HttpContext.Session.SetInt32("userid", userid);
                TempData["success"] = "Request Insert Successfully";
                return RedirectToAction("MainPage", "Admin");
            }
            catch (Exception)
            {
                TempData["error"] = "Error while Request ";
                return RedirectToAction("adminCreateRequest", "Admin");
            }


        }
    }
    

}

//public IActionResult FetchRegion(string region,int status)


//{
//    if (region == "All")
//    {
//        RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
//        requestListAdminDash = _adminrequest.GetNewRequest();
//        return PartialView("New", requestListAdminDash);
//    }
//    else
//    {

//        RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
//       requestListAdminDash = _adminrequest.GetRequestclientByRegion(region,status);
//          return PartialView("New", requestListAdminDash);
//    }

//}