using DataLayer.DataContext;
using DataLayer.Models;
using DataLayer.ViewModels;
using LogicLayer.Interface_Provider;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Repo_Provider
{
    public class ProviderPanel : IProviderPanel
    {
        private readonly HellodocPrjContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public ProviderPanel(HellodocPrjContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public RequestListAdminDash GetNewRequest(int page, int pageSize, int requestTypeId, string patientname, int regionId, int? phyid)
        {
            RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
            var allData = _context.Requestclients.Include(rc => rc.Request).Include(rc => rc.Request.Physician).Where(rc => rc.Request.Status == 1 && rc.Request.Physicianid == phyid).OrderByDescending(rc => rc.Request.Createddate).ToList();
            if (requestTypeId != 0)
            {
                allData = _context.Requestclients.Include(rc => rc.Request).Include(rc => rc.Request.Physician).Where(rc => rc.Request.Status == 1 && rc.Request.Requesttypeid == requestTypeId && rc.Request.Physicianid == phyid).OrderByDescending(rc => rc.Request.Createddate).ToList();
            }
            if (regionId != 0)
            {
                allData = allData.Where(a => a.Regionid == regionId).OrderByDescending(rc => rc.Request.Createddate).ToList();
            }
            if (!string.IsNullOrWhiteSpace(patientname))
            {
                var serchTerms = patientname.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                allData = allData.Where(a => serchTerms.All(term => a.Firstname.ToLower().Contains(term) || a.Lastname.ToLower().Contains(term))).OrderByDescending(rc => rc.Request.Createddate).ToList();
            }

            requestListAdminDash.paginatedRequest = allData.Skip((page - 1) * pageSize).Take(pageSize).OrderByDescending(rc => rc.Request.Createddate);
            requestListAdminDash.CurrentPage = page;
            requestListAdminDash.PageSize = pageSize;
            requestListAdminDash.TotalPages = Math.Ceiling((double)allData.Count / pageSize);
            requestListAdminDash.total = allData.Count;
            return requestListAdminDash;

        }
        public RequestListAdminDash GetPendingRequest(int page, int pageSize, int requestTypeId, string patientname, int regionId, int? phyid)
        {
            RequestListAdminDash requestListAdminDash = new RequestListAdminDash();

            var allData = _context.Requestclients.Include(rc => rc.Request).Include(rc => rc.Request.Physician).Where(rc => rc.Request.Status == 2 && rc.Request.Physicianid == phyid).OrderByDescending(rc => rc.Request.Createddate).ToList();

            if (requestTypeId != 0)
            {
                allData = _context.Requestclients.Include(rc => rc.Request).Include(rc => rc.Request.Physician).Where(rc => rc.Request.Status == 2 && rc.Request.Requesttypeid == requestTypeId && rc.Request.Physicianid == phyid).OrderByDescending(rc => rc.Request.Createddate).ToList();
            }
            if (regionId != 0)
            {
                allData = allData.Where(a => a.Regionid == regionId).ToList();
            }
            if (!string.IsNullOrWhiteSpace(patientname))
            {
                var serchTerms = patientname.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                allData = allData.Where(a => serchTerms.All(term => a.Firstname.ToLower().Contains(term) || a.Lastname.ToLower().Contains(term))).OrderByDescending(rc => rc.Request.Createddate).ToList();

            }

            requestListAdminDash.paginatedRequest = allData.Skip((page - 1) * pageSize).Take(pageSize).OrderByDescending(rc => rc.Request.Createddate);
            requestListAdminDash.CurrentPage = page;
            requestListAdminDash.PageSize = pageSize;
            requestListAdminDash.TotalPages = Math.Ceiling((double)allData.Count / pageSize);
            requestListAdminDash.total = allData.Count;
            return requestListAdminDash;

        }



        public RequestListAdminDash GetActiveRequest(int page, int pageSize, int requestTypeId, string patientname, int regionId, int? phyid)
        {
            RequestListAdminDash requestListAdminDash = new RequestListAdminDash();

            var allData = _context.Requestclients.Include(rc => rc.Request).Include(rc => rc.Request.Physician).Where(rc => (rc.Request.Status == 4 || rc.Request.Status == 5) && rc.Request.Physicianid == phyid).OrderByDescending(rc => rc.Request.Createddate).ToList();

            if (requestTypeId != 0)
            {
                allData = _context.Requestclients.Include(rc => rc.Request).Include(rc => rc.Request.Physician).Where(rc => (rc.Request.Status == 4 || rc.Request.Status == 5) && rc.Request.Requesttypeid == requestTypeId && rc.Request.Physicianid == phyid).OrderByDescending(rc => rc.Request.Createddate).ToList();
            }
            if (regionId != 0)
            {
                allData = allData.Where(a => a.Regionid == regionId).ToList();
            }
            if (!string.IsNullOrWhiteSpace(patientname))
            {
                var serchTerms = patientname.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                allData = allData.Where(a => serchTerms.All(term => a.Firstname.ToLower().Contains(term) || a.Lastname.ToLower().Contains(term))).OrderByDescending(rc => rc.Request.Createddate).ToList();

            }

            requestListAdminDash.paginatedRequest = allData.Skip((page - 1) * pageSize).Take(pageSize).OrderByDescending(rc => rc.Request.Createddate);
            requestListAdminDash.CurrentPage = page;
            requestListAdminDash.PageSize = pageSize;
            requestListAdminDash.TotalPages = Math.Ceiling((double)allData.Count / pageSize);
            requestListAdminDash.total = allData.Count;
            return requestListAdminDash;
        }

        public RequestListAdminDash GetConcludeRequest(int page, int pageSize, int requestTypeId, string patientname, int regionId, int? phyid)
        {
            RequestListAdminDash requestListAdminDash = new RequestListAdminDash();

            var allData = _context.Requestclients.Include(rc => rc.Request).Include(rc => rc.Request.Physician).Include(rc => rc.Request.Encounters).Where(rc => rc.Request.Status == 6 && rc.Request.Physicianid == phyid).OrderByDescending(rc => rc.Request.Createddate).ToList();

            if (requestTypeId != 0)
            {
                allData = _context.Requestclients.Include(rc => rc.Request).Include(rc => rc.Request.Physician).Where(rc => rc.Request.Status == 6 && rc.Request.Requesttypeid == requestTypeId && rc.Request.Physicianid == phyid).OrderByDescending(rc => rc.Request.Createddate).ToList();
            }
            if (regionId != 0)
            {
                allData = allData.Where(a => a.Regionid == regionId).ToList();
            }
            if (!string.IsNullOrWhiteSpace(patientname))
            {
                var serchTerms = patientname.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                allData = allData.Where(a => serchTerms.All(term => a.Firstname.ToLower().Contains(term) || a.Lastname.ToLower().Contains(term))).OrderByDescending(rc => rc.Request.Createddate).ToList();

            }

            requestListAdminDash.paginatedRequest = allData.Skip((page - 1) * pageSize).Take(pageSize).OrderByDescending(rc => rc.Request.Createddate);
            requestListAdminDash.CurrentPage = page;
            requestListAdminDash.PageSize = pageSize;
            requestListAdminDash.TotalPages = Math.Ceiling((double)allData.Count / pageSize);
            requestListAdminDash.total = allData.Count;

            return requestListAdminDash;
        }
        public Physician GetaspUser(string id)
        {
            return _context.Physicians.Where(ad => ad.Aspnetuserid == id).FirstOrDefault();
        }
        public bool GetAspUser(string email)
        {
            var aspuser = _context.Aspnetusers.Where(asp => asp.Email == email).FirstOrDefault();
            if (aspuser != null)
            {
                return true;

            }
            else
            {
                return false;
            }

        }
        public void TransferCaseReq(int reqid, string textnote)
        {
            var request = _context.Requests.Where(rc => rc.Requestid == reqid).FirstOrDefault();
            Requeststatuslog requeststatuslog = new Requeststatuslog
            {
                Requestid = reqid,
                Request = request,
                Status = 1,
                Notes = textnote,
                Createddate = DateTime.Now,


            };
            _context.Requeststatuslogs.Add(requeststatuslog);
            _context.SaveChanges();
            request.Status = 1;
            _context.SaveChanges();
        }
        public void Reqaccept(int reqid)
        {
            Request request = _context.Requests.Where(r => r.Requestid == reqid).FirstOrDefault();

            Requeststatuslog requeststatuslog = new Requeststatuslog
            {
                Requestid = reqid,
                Request = request,
                Status = 2,

                Createddate = DateTime.Now,


            };
            _context.Requeststatuslogs.Add(requeststatuslog);
            _context.SaveChanges();


            request.Status = 2;
            _context.SaveChanges();
        }
        public void encounterCase(int requestid, int type)
        {
            if (type == 2)
            {
                Request request = _context.Requests.Where(r => r.Requestid == requestid).FirstOrDefault();

                Requeststatuslog requeststatuslog = new Requeststatuslog
                {
                    Requestid = requestid,
                    Request = request,
                    Status = 6,
                    Createddate = DateTime.Now,
                };
                _context.Requeststatuslogs.Add(requeststatuslog);
                _context.SaveChanges();
                request!.Status = 6;
                _context.SaveChanges();
            }
            else if (type == 1)
            {
                Request request = _context.Requests.Where(r => r.Requestid == requestid).FirstOrDefault();

                Requeststatuslog requeststatuslog = new Requeststatuslog
                {
                    Requestid = requestid,
                    Request = request,
                    Status = 5,

                    Createddate = DateTime.Now,


                };
                _context.Requeststatuslogs.Add(requeststatuslog);
                _context.SaveChanges();
                request!.Status = 5;
                _context.SaveChanges();
            }

        }
        public void forHouseCall(int reqid)
        {
            Request request = _context.Requests.Where(r => r.Requestid == reqid).FirstOrDefault();

            Requeststatuslog requeststatuslog = new Requeststatuslog
            {
                Requestid = reqid,
                Request = request,
                Status = 6,
                Createddate = DateTime.Now,
            };
            _context.Requeststatuslogs.Add(requeststatuslog);
            _context.SaveChanges();
            request!.Status = 6;
            _context.SaveChanges();
        }
        public void AddEncounterDetails(int reqid, EncounterViewModel model)
        {
            Request request = _context.Requests.Where(r => r.Requestid == reqid).FirstOrDefault();
            Encounter encounter = new Encounter
            {
                Firstname = model.firstName,
                Location = model.Location,
                BirthDate = Convert.ToDateTime(model.birthDate),
                CreatedDate = model.Date,
                PhoneNumber = model.phoneNumber,
                Email = model.Email,
                HistoryOfInjury = model.historyOfInjury,
                MedicalHistory = model.medicalHistory,
                Medication = model.medications,
                Allergies = model.allergies,
                Temp = model.temp,
                Hr = model.hr,
                Rr = model.rr,
                BpS = model.bps,
                BpD = model.bpd,
                O2 = model.o2,
                Pain = model.pain,
                Heent = model.heent,
                Cv = model.cv,
                Chest = model.chest,
                Abd = model.abd,
                Extr = model.extr,
                Skin = model.skin,
                Neuro = Convert.ToString(model.neuro),
                Other = model.other,
                Diagonosis = model.diagnosis,
                MedicationDispense = model.medicalDispensed,
                Procedure = model.procedure,
                FollowUp = model.followUp,
                TreatmentPlan = model.plan,
                Requestid = reqid,
                Request = request
            };
            _context.Encounters.Add(encounter);
            _context.SaveChanges();

        }
        public string SendEmail(string email, string body, string subject, string emailto, int phyid)
        {
            string name = "";
            int id = 0;
            DateTime date = DateTime.Now;
            if (phyid != 0)
            {
                var physican = _context.Physicians.Where(rc => rc.Physicianid == phyid).FirstOrDefault();
                if (physican != null)
                {
                    name = physican.Firstname;
                    id = physican.Physicianid;
                    date = Convert.ToDateTime(physican.Createddate);
                }
                else
                {
                    return "false";
                }
            }

            string fromemail = "tatva.dotnet.hirenaswar@outlook.com";
            string frommailpassword = "#Aswar2002";

            MailMessage mailmessage = new MailMessage(new MailAddress(fromemail), new MailAddress("tatva.dotnet.hirenaswar@outlook.com"));
            mailmessage.Subject = subject;
            mailmessage.Body = body;
            mailmessage.IsBodyHtml = true;

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

                Emaillog emaillog = new Emaillog()
                {
                    Recievername = name,
                    Physicianid = phyid,
                    Roleid = 2,
                    Emailid = emailto,
                    Createdate = date,
                    Subjectname = subject,
                    Sentdate = DateTime.Now,
                    Isemailsent = new BitArray(1, true),
                };
                _context.Emaillogs.Add(emaillog);
                _context.SaveChanges();
                smtpClient.Send(mailmessage);

            }
            catch (Exception)
            {

                throw;
            }
            return "true";

        }
        public List<ShiftDetailsmodal> ShiftDetailsmodal(DateTime date, DateTime sunday, DateTime saturday, string type, int phyid)
        {
            var shiftdetails = _context.Shiftdetails.Include(u => u.Shift).Where(u => u.Shiftdate.Month == date.Month && u.Shiftdate.Year == date.Year && u.Shift.Physicianid == phyid);
            switch (type)
            {
                case "month":
                    shiftdetails = _context.Shiftdetails.Include(u => u.Shift).Where(u => u.Shiftdate.Month == date.Month && u.Shift.Physicianid == phyid && u.Shiftdate.Year == date.Year && u.Isdeleted == new System.Collections.BitArray(new[] { false }));
                    break;
                default:
                    break;
            }

            var list = shiftdetails.Select(s => new ShiftDetailsmodal
            {
                Shiftid = s.Shiftid,
                Shiftdetailid = s.Shiftdetailid,
                Shiftdate = s.Shiftdate,
                Startdate = s.Shift.Startdate,
                Starttime = s.Starttime,
                Endtime = s.Endtime,
                Physicianid = s.Shift.Physicianid,
                PhysicianName = s.Shift.Physician.Firstname,
                Status = s.Status,

            });

            return list.ToList();
        }
        public List<Shiftdetail> Getshiftdetail(DateTime date, int phyid)
        {
            return _context.Shiftdetails.Include(m => m.Shift).ThenInclude(m => m.Physician).Where(m => m.Shiftdate == date && m.Isdeleted == new System.Collections.BitArray(new[] { false })).ToList();
        }
        public void FinalizeForm(int reqId)
        {
            var request = _context.Requests.FirstOrDefault(u => u.Requestid == reqId)!;

            request.IsFinalized = new System.Collections.BitArray(1, true);

            _context.SaveChanges();
        }
        public Encounter Getencounter(int reqid)
        {
            return _context.Encounters.Where(e => e.Requestid == reqid).FirstOrDefault();
        }
        public void ReportUpload(ConcludeCarevm model)
        {
            Encounter encounter = _context.Encounters.Where(en => en.Requestid == model.reqid).FirstOrDefault();
            string? uniquefilename = null;
            try
            {
            if (model.photo != null)
            {

                string uploadfolder = Path.Combine(_hostingEnvironment.WebRootPath, "Report");
                uniquefilename = $"{model.concludeId}" + "_medicalReport.pdf";
                string filepath = Path.Combine(uploadfolder, uniquefilename);
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    model.photo.CopyTo(stream);

                }
                encounter!.Isreport = new BitArray(1, true);
                encounter.Report = uniquefilename;
                _context.SaveChanges();

            }
            }
            catch (Exception)
            {

                throw;
            }

          
           


        }
        public void PostConcludeData(int reqid,ConcludeCarevm model)
        {
            Request request = _context.Requests.Where(r => r.Requestid == reqid).FirstOrDefault();
            Requeststatuslog requeststatuslog = new Requeststatuslog
            {
                Requestid = reqid,
                Request = request,
                Status = 8,
                Createddate = DateTime.Now,
            };
            _context.Requeststatuslogs.Add(requeststatuslog);
            _context.SaveChanges();
            request!.Status = 8;
            _context.SaveChanges();
        
          
          
        }
        public void AddAdminNote(int reqid, RequestListAdminDash model)
        {
            Request request = _context.Requests.Where(r => r.Requestid == reqid).FirstOrDefault();
            Requestnote requestNotes = _context.Requestnotes.Where(r => r.Requestid == reqid).FirstOrDefault();
            if (requestNotes == null)
            {
                Requestnote reqnote = new Requestnote
                {
                    Requestid = request.Requestid,
                    Request = request,
                    Physiciannotes = model.AdminNotes,
                    Adminnotes = null,
                    Createdby = "admin",
                    Createddate = DateTime.Now,
                    Modifiedby = "admin",
                    Modifieddate = DateTime.Now,

                };
                _context.Requestnotes.Add(reqnote);
                _context.SaveChanges();
            }
            else
            {
                requestNotes.Physiciannotes = model.AdminNotes;
                _context.SaveChanges();
            }
        }
    }
}
