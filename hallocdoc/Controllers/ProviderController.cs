using DataLayer.DataContext;
using DataLayer.Models;
using DataLayer.ViewModels;
using hallocdoc.Helpers;
using iTextSharp.text.pdf;
using iTextSharp.text;
using LogicLayer.Interface_Admin;
using LogicLayer.Interface_Provider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;
using System.Data;
using System.IO.Compression;
using LogicLayer.Interface_patient;
using HalloDoc.Auth;

namespace hallocdoc.Controllers
{
    [CustomAuthorize("provider")]
    public class ProviderController : Controller
    {
        private readonly IAdminRequest _adminrequest;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private readonly IProviderPanel _Provider;
        private readonly HellodocPrjContext _context;
        public ProviderController(IAdminRequest adminrequest, HellodocPrjContext context, IProviderPanel providerPanel, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _adminrequest = adminrequest;
            _Provider = providerPanel;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        [RoleAuthorize(25)]
        public IActionResult ProviderDashBoard()
        {
            var phyid = HttpContext.Session.GetInt32("phyid");
            RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
            requestListAdminDash = _adminrequest.getallrequest(phyid);
            string id = HttpContext.Session.GetString("aspuser");
            var provider = _Provider.GetaspUser(id);
            ViewBag.name = provider.Firstname + " " + provider.Lastname;
            return View("ProviderDashBoard", requestListAdminDash);
        }
        public IActionResult New(int page, int pageSize, int requestTypeId, string patientname, int regionId)
        {
            var phyid = HttpContext.Session.GetInt32("phyid");
            RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
            requestListAdminDash = _Provider.GetNewRequest(page, pageSize, requestTypeId, patientname, regionId, phyid);
            ViewBag.state = "New";
            return PartialView("NewProvider", requestListAdminDash);
        }
        public IActionResult PendingTable(int page, int pageSize, int requestTypeId, string patientname, int regionId)
        {
            var phyid = HttpContext.Session.GetInt32("phyid");
            RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
            requestListAdminDash = _Provider.GetPendingRequest(page, pageSize, requestTypeId, patientname, regionId, phyid);
            ViewBag.state = "Pending";
            return PartialView("NewProvider", requestListAdminDash);
        }
        public IActionResult Active(int page, int pageSize, int requestTypeId, string patientname, int regionId)
        {
            var phyid = HttpContext.Session.GetInt32("phyid");
            RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
            requestListAdminDash = _Provider.GetActiveRequest(page, pageSize, requestTypeId, patientname, regionId, phyid);
            ViewBag.state = "Active";
            return PartialView("NewProvider", requestListAdminDash);
        }
        public IActionResult ConcludeTable(int page, int pageSize, int requestTypeId, string patientname, int regionId)
        {
            var phyid = HttpContext.Session.GetInt32("phyid");
            RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
            requestListAdminDash = _Provider.GetConcludeRequest(page, pageSize, requestTypeId, patientname, regionId, phyid);
            ViewBag.state = "Conclude";
            return PartialView("NewProvider", requestListAdminDash);
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

        public IActionResult ViewCase(int reqclientid, int type)
        {
            RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
            var requestclient = _adminrequest.GetRequestclient(reqclientid);
            requestListAdminDash.status = requestclient.Request.Status;
            requestListAdminDash.Requestclient = requestclient;
            requestListAdminDash.type = type;
            return View(requestListAdminDash);
        }
        public IActionResult ViewUploads(int? reqclientid, int? requestid, int type, viewdocumentmodel model)
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
                var filepath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", filename.Filename);
                fpa.Add(filepath);
            }
            var email = "tatva.dotnet.hirenaswar@outlook.com";

            var subject = "Send Mail With Attchment";
            var body = "this msg is for test";
            try
            {
                _adminrequest.SendEmailUp("al", body, subject, email, fpa, requestid);
                TempData["success"] = "Mail Sent Succesfully";
            }
            catch (Exception e)
            {
                TempData["error"] = "Mail Not Sent ";
                Console.WriteLine(e);
            }
            return RedirectToAction("ViewUploads");
        }
        public IActionResult Downloaddoc(int documentid)
        {
            var filename = _adminrequest.GetFile(documentid);
            var filepath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", filename.Filename);
            return File(System.IO.File.ReadAllBytes(filepath), "multipart/form-data", System.IO.Path.GetFileName(filepath));
        }
        public IActionResult DownLoadAllProvider(viewdocumentmodel model, int requestid, int reqclientid)
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

                        var filepath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", file.Filename);

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
        public IActionResult TransferModel(int reqid)
        {
            AssignModel assignModel = new AssignModel();
            assignModel.reqid = reqid;
            assignModel.Regions = _adminrequest.regionlist();
            return PartialView("Transfer", assignModel);
        }
        public IActionResult TransferCase(int reqid, string textnote)
        {
            RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
            _Provider.TransferCaseReq(reqid, textnote);
            requestListAdminDash = _adminrequest.getallrequest(0);
            return RedirectToAction("ProviderDashBoard");
        }
        [HttpPost]
        public IActionResult sendagreementModel(int reqid, int reqtypeid)
        {
            BlockView blockView = new BlockView();
            blockView.reqid = reqid;
            blockView.reqtypeid = reqtypeid;
            return PartialView("sendAgreeMent", blockView);
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

                string data = _adminrequest.SendEmail("al", body, subject, model.email, reqid);
                if (data == "true")
                {
                    TempData["success"] = "Mail Send Succesfully";
                    return RedirectToAction("ProviderDashBoard");
                }
                else
                {
                    TempData["error"] = "There Is no such email exist";
                    return RedirectToAction("ProviderDashBoard");
                }
            }
            catch (Exception)
            {
                TempData["error"] = "Error While Sending A mail";
                return RedirectToAction("ProviderDashBoard");
            }

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
        public IActionResult acceptreq(int reqid)
        {
            try
            {
                _Provider.Reqaccept(reqid);
                TempData["success"] = "Succesfully Accepted";
                return RedirectToAction("ProviderDashBoard");
            }
            catch (Exception)
            {
                TempData["error"] = " Error In Accept";
                return RedirectToAction("ProviderDashBoard");
            }

        }
        public IActionResult Encountermodel(int reqid)
        {
            BlockView blockView = new BlockView();
            blockView.reqid = reqid;
            return PartialView("Encounter", blockView);
        }
        public IActionResult forencounter(BlockView model, int type)
        {
            RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
            try
            {
                if (type == 0)
                {
                    TempData["error"] = "Select Any one Of Them";

                    return RedirectToAction("ProviderDashBoard");
                }
                _Provider.encounterCase(model.reqid, type);
                TempData["success"] = "Encounter successFully";
                return RedirectToAction("ProviderDashBoard");
            }
            catch (Exception)
            {
                TempData["error"] = "Error";

                return RedirectToAction("ProviderDashBoard");

            }

        }
        public IActionResult Housecall(int reqid)
        {
            try
            {
                _Provider.forHouseCall(reqid);
                TempData["success"] = "Encounter successFully";
                return RedirectToAction("ProviderDashBoard");
            }
            catch (Exception)
            {

                TempData["error"] = "Error";

                return RedirectToAction("ProviderDashBoard");
            }
        }
        public IActionResult EncounterForm(int reqclientid, int reqid)
        {
            EncounterViewModel encounterViewModel = new EncounterViewModel();
            return View(encounterViewModel);
        }
        public IActionResult PostEncounterData(int reqid, EncounterViewModel model)
        {
            try
            {
                _adminrequest.AddEncounterDetails(reqid, model);
                TempData["success"] = "Encounter Data Inserted";
                return RedirectToAction("EncounterForm", new { reqid = reqid });
            }
            catch (Exception)
            {

                TempData["error"] = "error while Data Inserted";
                return RedirectToAction("EncounterForm", new { reqid = reqid });
            }

        }
        [RoleAuthorize(27)]
        public IActionResult EditProvider()
        {
            int phyid = Convert.ToInt32(HttpContext.Session.GetInt32("phyid"));
            editProvidervm editProvidervm = new editProvidervm();
            editProvidervm = _adminrequest.getphysician(phyid);
            //ViewBag.username = editProvidervm.Firstname + " " + editProvidervm.Lastname;
            return View("EditProvider", editProvidervm);

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
            return PartialView("SignPad", editProvidervm);
        }
        public IActionResult cancelModel(int phyid)
        {

            CancelModel cancelModel = new CancelModel();
            cancelModel.phyid = phyid;

            return PartialView("AdminRequest", cancelModel);
        }
        public IActionResult Sendmailrequest(CancelModel model)
        {

            var subject = "Request To Admin From Provider";
            var body = "Hi" + "USER" + "Edit My Profile" + "==" + model.TransCancelNotes;

            string data = _Provider.SendEmail("al", body, subject, "tatva.dotnet.hirenaswar@outlook.com", model.phyid);
            if (data == "true")
            {
                TempData["success"] = "Mail Send Succesfully";
                return RedirectToAction("ProviderDashBoard");
            }
            else
            {
                TempData["error"] = "There is no such mail exist";
                return RedirectToAction("ProviderDashBoard");
            }
        }
        [RoleAuthorize(26)]
        public IActionResult Scheduling()
        {
            int phyid = Convert.ToInt32(HttpContext.Session.GetInt32("phyid"));
            CreateShift createShift = new CreateShift();
            createShift.Region = _adminrequest.GetRegionForShift();
            createShift.PhysicianId = phyid;
            return View(createShift);
        }
        public IActionResult loadshift(string datestring, string sundaystring, string saturdaystring, string shifttype, int regionid)
        {
            DateTime date = DateTime.Parse(datestring);
            DateTime sunday = DateTime.Parse(sundaystring);
            DateTime saturday = DateTime.Parse(saturdaystring);
            int phyid = Convert.ToInt32(HttpContext.Session.GetInt32("phyid"));
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
                    monthShift.shiftDetailsmodals = _Provider.ShiftDetailsmodal(date, sunday, saturday, "month", phyid);
                    return PartialView("_monthWise", monthShift);


                default:
                    return PartialView("_monthWise");
            }

        }
        public string GetPhysicianForCreateShift(int RegionId)
        {
            return JsonConvert.SerializeObject(_adminrequest.GetPhysicianForCreateShift(RegionId), Formatting.Indented);
        }
        public IActionResult AllShift(DateTime datestring)
        {
            int phyid = Convert.ToInt32(HttpContext.Session.GetInt32("phyid"));
            var data = _Provider.Getshiftdetail(datestring, phyid);
            return View("_AllShift", data);
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

        public IActionResult FinalizeEncounterForm(int reqId)
        {
            try
            {
                _Provider.FinalizeForm(reqId);
                TempData["success"] = "Form Finalized Successfully";
                return RedirectToAction("ProviderDashboard");
            }
            catch
            {
                TempData["error"] = "Form not Finallized";
                return RedirectToAction("EncounterForm", new { reqId });
            }

        }
        public IActionResult ConcludeCare(int reqId)
        {
            ConcludeCarevm concludeCarevm = new ConcludeCarevm();

            concludeCarevm.reqid = reqId;
            concludeCarevm.encounter = _Provider.Getencounter(reqId);

            return View(concludeCarevm);
        }
        public IActionResult DownlodEncounter(int reqid)
        {
            CancelModel cancelModel = new CancelModel();
            cancelModel.reqid = reqid;
            return PartialView("_DownlodEncounter", cancelModel);
        }

        public ActionResult GeneratePdf(int reqid)
        {

            var rowData = _Provider.Getencounter(reqid);

            if (rowData != null)
            {
                // Create a new PDF document
                Document document = new Document();
                MemoryStream ms = new MemoryStream();
                PdfWriter.GetInstance(document, ms);
                document.Open();

                // Add table for header and data
                PdfPTable mainTable = new PdfPTable(1);
                mainTable.WidthPercentage = 100; // Set table width to 100%

                // Add header row
                PdfPCell headerCell = new PdfPCell(new Phrase("Table Data"));
                headerCell.HorizontalAlignment = Element.ALIGN_CENTER; // Center alignment
                headerCell.Border = PdfPCell.NO_BORDER; // Remove border
                mainTable.AddCell(headerCell);

                // Add data row
                PdfPTable dataTable = new PdfPTable(2); // Assuming 15 columns
                                                         // Add column headers
                foreach (var property in rowData.GetType().GetProperties())
                {
                    PdfPCell cell = new PdfPCell(new Phrase(property.Name));
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY; // Background color for header
                    dataTable.AddCell(cell);
                    dataTable.AddCell(new Phrase(property.GetValue(rowData)?.ToString()));
                    dataTable.CompleteRow();

                }


                // Add data table to main table
                mainTable.AddCell(dataTable);

                // Add the main table to the document
                document.Add(mainTable);

                // Close the document
                document.Close();

                // Convert MemoryStream to byte array
                byte[] bytes = ms.ToArray();
                ms.Close();

                // Return the PDF as a FileResult
                return File(bytes, "application/pdf", "table_data.pdf");
            }
            else
            {
                // Handle the case where no data is found
                return Content("No data found.");
            }
        }
        public IActionResult UploadReport(ConcludeCarevm model)
        {
            try
            {
                _Provider.ReportUpload(model);
                TempData["success"] = "File Uploaded Succesfully";
                return RedirectToAction("ConcludeCare", new { reqid = model.reqid });
            }
            catch (Exception)
            {

                return RedirectToAction("ConcludeCare", new { reqId = model.reqid });
            }

        }
        public IActionResult DownloadPro(int reqid)
        {
            var encounter = _Provider.Getencounter(reqid);
            var filepath = Path.Combine(_hostingEnvironment.WebRootPath, "Report", encounter.Report);
            return File(System.IO.File.ReadAllBytes(filepath), "multipart/form-data", System.IO.Path.GetFileName(filepath));
        }
        public IActionResult ConcludeData(int reqid, ConcludeCarevm model)
        {
            try
            {
                _Provider.PostConcludeData(reqid, model);
                TempData["success"] = "Request Moved To Closed";
                return RedirectToAction("ProviderDashBoard");
            }
            catch (Exception)
            {



                return RedirectToAction("ConcludeCare", new { reqId = reqid });
            }

        }
    }
}
