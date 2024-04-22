using DataLayer.DataContext;
using DataLayer.Models;
using DataLayer.ViewModels;
using LogicLayer.Interface_Admin;
using Microsoft.EntityFrameworkCore;

using System.Net.Mail;

using System.Security.Claims;
using System.Text;

using System.IdentityModel.Tokens.Jwt;

using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Http.Internal;
using System.Collections;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LogicLayer.Repo_admin
{
	public class AdminRequest : IAdminRequest
	{
		private readonly HellodocPrjContext _context;
		private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
		public AdminRequest(HellodocPrjContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
		{
			_context = context;
			_hostingEnvironment = hostingEnvironment;
		}
		public RequestListAdminDash GetNewRequest(int page, int pageSize, int requestTypeId, string patientname, int regionId)
		{
			RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
			var allData = _context.Requestclients.Include(rc => rc.Request).Include(rc => rc.Request.Physician).Where(rc => rc.Request.Status == 1).OrderByDescending(rc => rc.Request.Createddate).ToList();
			if (requestTypeId != 0)
			{
				allData = _context.Requestclients.Include(rc => rc.Request).Include(rc => rc.Request.Physician).Where(rc => rc.Request.Status == 1 && rc.Request.Requesttypeid == requestTypeId).OrderByDescending(rc => rc.Request.Createddate).ToList();
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
		public RequestListAdminDash GetPendingRequest(int page, int pageSize, int requestTypeId, string patientname, int regionId)
		{
			RequestListAdminDash requestListAdminDash = new RequestListAdminDash();

			var allData = _context.Requestclients.Include(rc => rc.Request).Include(rc => rc.Request.Physician).Where(rc => rc.Request.Status == 2).OrderByDescending(rc => rc.Request.Createddate).ToList();

			if (requestTypeId != 0)
			{
				allData = _context.Requestclients.Include(rc => rc.Request).Include(rc => rc.Request.Physician).Where(rc => rc.Request.Status == 2 && rc.Request.Requesttypeid == requestTypeId).OrderByDescending(rc => rc.Request.Createddate).ToList();
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
		public Admin GetaspUser(string id)
		{
			return _context.Admins.Where(ad => ad.Aspnetuserid == id).FirstOrDefault();
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

		public RequestListAdminDash GetActiveRequest(int page, int pageSize, int requestTypeId, string patientname, int regionId)
		{
			RequestListAdminDash requestListAdminDash = new RequestListAdminDash();

			var allData = _context.Requestclients.Include(rc => rc.Request).Include(rc => rc.Request.Physician).Where(rc => rc.Request.Status == 4 || rc.Request.Status == 5).OrderByDescending(rc => rc.Request.Createddate).ToList();

			if (requestTypeId != 0)
			{
				allData = _context.Requestclients.Include(rc => rc.Request).Include(rc => rc.Request.Physician).Where(rc => (rc.Request.Status == 4 || rc.Request.Status == 5) && rc.Request.Requesttypeid == requestTypeId).OrderByDescending(rc => rc.Request.Createddate).ToList();
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

		public RequestListAdminDash GetConcludeRequest(int page, int pageSize, int requestTypeId, string patientname, int regionId)
		{
			RequestListAdminDash requestListAdminDash = new RequestListAdminDash();

			var allData = _context.Requestclients.Include(rc => rc.Request).Include(rc => rc.Request.Physician).Where(rc => rc.Request.Status == 6).OrderByDescending(rc => rc.Request.Createddate).ToList();

			if (requestTypeId != 0)
			{
				allData = _context.Requestclients.Include(rc => rc.Request).Include(rc => rc.Request.Physician).Where(rc => rc.Request.Status == 6 && rc.Request.Requesttypeid == requestTypeId).OrderByDescending(rc => rc.Request.Createddate).ToList();
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
		public RequestListAdminDash GetCloseRequest(int page, int pageSize, int requestTypeId, string patientname, int regionId)
		{
			RequestListAdminDash requestListAdminDash = new RequestListAdminDash();

			var allData = _context.Requestclients.Include(rc => rc.Request).Include(rc => rc.Request.Physician).Where(rc => (rc.Request.Status == 3 || rc.Request.Status == 7 || rc.Request.Status == 8)).OrderByDescending(rc => rc.Request.Createddate).ToList();

			if (requestTypeId != 0)
			{
				allData = _context.Requestclients.Include(rc => rc.Request).Include(rc => rc.Request.Physician).Where(rc => (rc.Request.Status == 3 || rc.Request.Status == 7 || rc.Request.Status == 8) && rc.Request.Requesttypeid == requestTypeId).OrderByDescending(rc => rc.Request.Createddate).ToList();
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
		public RequestListAdminDash GetUnPaidRequest(int page, int pageSize, int requestTypeId, string patientname, int regionId)
		{
			RequestListAdminDash requestListAdminDash = new RequestListAdminDash();

			var allData = _context.Requestclients.Include(rc => rc.Request).Include(rc => rc.Request.Physician).Where(rc => rc.Request.Status == 9).OrderByDescending(rc => rc.Request.Createddate).ToList();

			if (requestTypeId != 0)
			{
				allData = _context.Requestclients.Include(rc => rc.Request).Include(rc => rc.Request.Physician).Where(rc => rc.Request.Status == 9 && rc.Request.Requesttypeid == requestTypeId).OrderByDescending(rc => rc.Request.Createddate).ToList();
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



		public RequestListAdminDash getallrequest(int? type)
		{
			if (type != 0)
			{

				var request = _context.Requests.Where(rc => rc.Physicianid == type).ToList();

				var requestCountByType = request
				  .GroupBy(li => li.Status)
				  .Select(g => new { Status = g.Key, Count = g.Count() })
				  .ToList();
				RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
				requestListAdminDash.regionList = _context.Regions.ToList();
				requestListAdminDash.newreq = request.Where(r => r.Status == 1).ToList()?.Count ?? 0;
				requestListAdminDash.pendingreq = request.Where(r => r.Status == 2).ToList()?.Count ?? 0;
				requestListAdminDash.activereq = request.Where(r => r.Status == 4 || r.Status == 5).ToList()?.Count ?? 0;

				requestListAdminDash.conclude = request.Where(r => r.Status == 6).ToList()?.Count ?? 0;

				return requestListAdminDash;
			}
			else
			{

				var request = _context.Requests.ToList();
				var requestCountByType = request
  .GroupBy(li => li.Status)
  .Select(g => new { Status = g.Key, Count = g.Count() })
  .ToList();

				RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
				requestListAdminDash.regionList = _context.Regions.ToList();
				requestListAdminDash.regionList = _context.Regions.ToList();
				requestListAdminDash.newreq = request.Where(r => r.Status == 1).ToList()?.Count ?? 0;
				requestListAdminDash.pendingreq = request.Where(r => r.Status == 2).ToList()?.Count ?? 0;
				requestListAdminDash.activereq = request.Where(r => r.Status == 4 || r.Status == 5).ToList()?.Count ?? 0;

				requestListAdminDash.conclude = request.Where(r => r.Status == 6).ToList()?.Count ?? 0;
				requestListAdminDash.closereq = request.Where(r => r.Status == 3 || r.Status == 7|| r.Status==8).ToList()?.Count ?? 0;
				requestListAdminDash.unpaid = request.Where(r => r.Status == 9).ToList()?.Count ?? 0;
				return requestListAdminDash;
			}
		}

		public Requestclient GetRequestclient(int reqclientid)
		{
			return _context.Requestclients.Include(rc => rc.Request).Where(rc => rc.Requestclientid == reqclientid).FirstOrDefault();
		}
		public RequestListAdminDash GetRequestclientByRegion(string region, int status)
		{
			RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
			requestListAdminDash.reqlist = _context.Requestclients.Include(rc => rc.Region).Where(rc => rc.Region.Name == region).Include(rc => rc.Request).Where(r => r.Request.Status == status).ToList();
			return (requestListAdminDash);
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
					Physiciannotes = null,
					Adminnotes = model.AdminNotes,
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
				requestNotes.Adminnotes = model.AdminNotes;
				_context.SaveChanges();
			}
		}

		public RequestListAdminDash GetAdminNote(int reqid)
		{
			RequestListAdminDash requestListAdminDash = new RequestListAdminDash();
			requestListAdminDash.RequestNotes = _context.Requestnotes.Where(rn => rn.Requestid == reqid).FirstOrDefault();
			requestListAdminDash.Requeststatuslog = _context.Requeststatuslogs.Include(rc => rc.Physician).Where(req => req.Requestid == reqid && req.Status == 2).OrderByDescending(rc => rc.Createddate).FirstOrDefault();
			return requestListAdminDash;
		}
		public void UpdateStatusAndNote(int reqid, CancelModel model)
		{
			Request request = _context.Requests.Where(u => u.Requestid == reqid).FirstOrDefault();
			Requeststatuslog requeststatuslog = new Requeststatuslog
			{
				Requestid = reqid,
				Request = request,
				Status = 3,
				Notes = model.TransCancelNotes,
				Createddate = DateTime.Now,
			};
			_context.Requeststatuslogs.Add(requeststatuslog);
			_context.SaveChanges();
			request.Status = 3;
			_context.SaveChanges();

		}
		public List<Physician> GetPhysiciansForRegion(int region)
		{
			return _context.Physicians.Where(ph => ph.Regionid == region).ToList();
		}
		public void clearCase(int requestid)
		{
			Request request = _context.Requests.Where(r => r.Requestid == requestid).FirstOrDefault();

			Requeststatuslog requeststatuslog = new Requeststatuslog
			{
				Requestid = requestid,
				Request = request,
				Status = 10,

				Createddate = DateTime.Now,


			};
			_context.Requeststatuslogs.Add(requeststatuslog);
			_context.SaveChanges();
			request!.Status = 10;
			_context.SaveChanges();

		}
		public void AssignCaseReq(int reqid, int phyid, string textnote)
		{
			try
			{
				Request request = _context.Requests.Where(r => r.Requestid == reqid).FirstOrDefault();
				Physician phy = _context.Physicians.Where(ph => ph.Physicianid == phyid).FirstOrDefault();
				Requeststatuslog requeststatuslog = new Requeststatuslog
				{
					Requestid = reqid,
					Request = request,
					Status = 1,
					Notes = textnote,
					Createddate = DateTime.Now,
					Physicianid = phyid,
					Physician = phy
				};
				_context.Requeststatuslogs.Add(requeststatuslog);
				_context.SaveChanges();

				request.Physicianid = phyid;
				request.Physician = phy;
				request.Status = 1;
				_context.SaveChanges();
			}
			catch (Exception)
			{

				throw;
			}


		}
		public void TransferCaseReq(int reqid, int phyid, string textnote)
		{
			Request request = _context.Requests.Where(r => r.Requestid == reqid).FirstOrDefault();
			Physician phy = _context.Physicians.Where(ph => ph.Physicianid == phyid).FirstOrDefault();
			Requeststatuslog requeststatuslog = new Requeststatuslog
			{
				Requestid = reqid,
				Request = request,
				Status = 2,
				Notes = textnote,
				Createddate = DateTime.Now,
				Transtophysicianid = phyid,
				Transtophysician = phy

			};
			_context.Requeststatuslogs.Add(requeststatuslog);
			_context.SaveChanges();

			request.Physicianid = phyid;
			request.Physician = phy;
			request.Status = 2;
			_context.SaveChanges();
		}
		public void BlockRequest(int reqid, BlockView model)
		{
			Request request = _context.Requests.Where(r => r.Requestid == reqid).FirstOrDefault();
			Requeststatuslog requeststatuslog = new Requeststatuslog
			{
				Requestid = reqid,
				Request = request,
				Status = 11,
				Notes = model.TransNotes,
				Createddate = DateTime.Now,
			};
			_context.Requeststatuslogs.Add(requeststatuslog);
			_context.SaveChanges();
			request.Status = 11;

			_context.SaveChanges();
		}
		public List<Region> regionlist()
		{
			return _context.Regions.ToList();
		}
		public Schedulereqvm regionlistschedule()
		{
			Schedulereqvm schedulereqvm = new Schedulereqvm();

			schedulereqvm.region = _context.Regions.ToList();
			return schedulereqvm;
		}
		public viewdocumentmodel ShowDocument(int? reqclientid, int? requestid, viewdocumentmodel model)
		{
			viewdocumentmodel viewdocumentmodel = new viewdocumentmodel();
			Request request = _context.Requests.Where(r => r.Requestid == requestid).FirstOrDefault();
			Requestclient reqclient = _context.Requestclients.Where(rc => rc.Requestclientid == reqclientid).FirstOrDefault();

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
					Requestid = Convert.ToInt32(requestid),
					Filename = uniquefilename,
					Createddate = DateTime.Now,
					Request = request
				};
				_context.Requestwisefiles.Add(requestwisefile);
				_context.SaveChanges();
			}

			viewdocumentmodel.requestwisefiles = _context.Requestwisefiles.Where(u => u.Requestid == requestid).ToList();
			viewdocumentmodel.Uploder = "Admin";
			viewdocumentmodel.firstName = reqclient.Firstname;

			viewdocumentmodel.reqid = requestid;
			viewdocumentmodel.reqclientid = reqclientid;


			return viewdocumentmodel;
		}
		public void deletedoc(int documentid)
		{
			Requestwisefile file = _context.Requestwisefiles.Where(rf => rf.Requestwisefileid == documentid).FirstOrDefault();
			_context.Requestwisefiles.Remove(file);
			_context.SaveChanges();
		}
		public void deleteAllDoc(List<int> documentid, int flag, int requestid)
		{




			foreach (var item in documentid)
			{
				Requestwisefile file = _context.Requestwisefiles.Where(rf => rf.Requestwisefileid == item).FirstOrDefault();
				_context.Requestwisefiles.Remove(file);
				_context.SaveChanges();
			}

		}
		public Requestwisefile getdoc(int documentid)
		{
			return _context.Requestwisefiles.Where(rf => rf.Requestwisefileid == documentid).FirstOrDefault();
		}
		public string Getemail(int reqclientid)
		{
			return _context.Requestclients.Where(rc => rc.Requestclientid == reqclientid).FirstOrDefault().Email;
		}
		public void SendEmailUp(string email, string body, string subject, string emailto, List<string> filepath, int reqid)
		{
			string fromemail = "tatva.dotnet.hirenaswar@outlook.com";
			string frommailpassword = "#Aswar2002";
			string name = "";
			int id = 0;
			DateTime date = DateTime.Now;
			if (reqid != 0)
			{
				var request = _context.Requestclients.Include(rc => rc.Request).Where(rc => rc.Requestid == reqid).FirstOrDefault();
				name = request.Firstname;
				id = request.Requestid;
				date = request.Request.Createddate;

			}
			MailMessage mailmessage = new MailMessage(new MailAddress(fromemail), new MailAddress(emailto));
			mailmessage.Subject = subject;
			mailmessage.Body = body;
			mailmessage.IsBodyHtml = true;
			foreach (var item in filepath)
			{
				Attachment attachment = new Attachment(item);
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
				Emaillog emaillog = new Emaillog()
				{
					Recievername = name,
					Requestid = id,
					Roleid = 1,
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


		}
		public Requestwisefile GetFile(int documentid)
		{
			return _context.Requestwisefiles.FirstOrDefault(u => u.Requestwisefileid == documentid);
		}
		public List<Healthprofessionaltype> GetOrderDetails()
		{
			OrdersModel orderModel = new OrdersModel();
			orderModel.hptype = _context.Healthprofessionaltypes.ToList();
			return orderModel.hptype;
		}
		public int Getstatus(int reqid)
		{
			return _context.Requests.Where(r => r.Requestid == reqid).FirstOrDefault().Status;
		}
		public List<Healthprofessional> GetOrder(int professionId)
		{
			OrdersModel orderModel = new OrdersModel();
			orderModel.hp = _context.Healthprofessionals.Where(hp => hp.Profession == professionId).ToList();
			return orderModel.hp;
		}
		public Healthprofessional GetBusinessData(int businessid)
		{
			OrdersModel ordersModel = new OrdersModel();
			ordersModel.Healthprofessional = _context.Healthprofessionals.Where(hf => hf.Vendorid == businessid).FirstOrDefault();
			return ordersModel.Healthprofessional;
		}
		public void CreateOrder(int? reqid, OrdersModel model)
		{
			Request request = _context.Requests.Where(r => r.Requestid == reqid).FirstOrDefault();
			Orderdetail orderdetail = new Orderdetail
			{
				Requestid = reqid,
				Vendorid = model.vendorid,
				Faxnumber = model.faxnumber,
				Email = model.email,
				Businesscontact = model.contact,
				Noofrefill = model.refill,
				Createddate = DateTime.Now,
				Createdby = "admin",
				Prescription = model.details
			};
			_context.Orderdetails.Add(orderdetail);
			_context.SaveChanges();
		}
		public string SendEmail(string email, string body, string subject, string emailto, int reqid)
		{
			string name = "";
			int id = 0;
			DateTime date = DateTime.Now;
			if (reqid != 0)
			{
				var request = _context.Requestclients.Include(rc => rc.Request).Where(rc => rc.Requestid == reqid).FirstOrDefault();
				if (request != null)
				{
					name = request.Firstname;
					id = request.Requestid;
					date = request.Request.Createddate;
				}
				else
				{
					return "false";
				}
			}

			string fromemail = "tatva.dotnet.hirenaswar@outlook.com";
			string frommailpassword = "#Aswar2002";
			MailMessage mailmessage = new MailMessage(new MailAddress(fromemail), new MailAddress(emailto));
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
					Requestid = id,
					Roleid = 1,
					Emailid = "tatva.dotnet.hirenaswar@outlook.com",
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
		public string SendEmailPhyContact(string email, string body, string subject, string emailto, int phyid)
		{
			string name = "";
			int id = 0;
			DateTime date = DateTime.Now;
			if (phyid != 0)
			{
				var physician = _context.Physicians.Where(rc => rc.Physicianid == phyid).FirstOrDefault();
				if (physician != null)
				{
					name = physician.Firstname;
					id = physician.Physicianid;
					date = Convert.ToDateTime(physician.Createddate);
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
					Requestid = id,
					Roleid = 2,

					Emailid = "tatva.dotnet.hirenaswar@outlook.com",
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
		public string GenerateJwtToken(string secretKey, string issuer, string audience, int reqid, int status)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Sid, Convert.ToString(reqid)),
				new Claim(ClaimTypes.SerialNumber, Convert.ToString(status)),
			};

			var token = new JwtSecurityToken(
				issuer: issuer,
				audience: audience,
				claims: claims,
				expires: DateTime.Now.AddMinutes(30),
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
		public EncounterViewModel getdetails(int reqclientid, int reqid)
		{
			EncounterViewModel encounterViewModel = new EncounterViewModel();
			Encounter client = _context.Encounters.Where(rc => rc.Requestid == reqid).FirstOrDefault();
			if (client != null)
			{
				encounterViewModel.firstName = client.Firstname;
				encounterViewModel.lastName = client.Firstname;
				encounterViewModel.Location = client.Location;
				encounterViewModel.birthDate = client.BirthDate;
				encounterViewModel.Date = client.CreatedDate;
				encounterViewModel.phoneNumber = client.PhoneNumber;
				encounterViewModel.Email = client.Email;
				encounterViewModel.historyOfInjury = client.HistoryOfInjury;
				encounterViewModel.medicalHistory = client.MedicalHistory;
				encounterViewModel.medications = client.Medication;
				encounterViewModel.allergies = client.Allergies;
				encounterViewModel.temp = Convert.ToInt32(client.Temp);
				encounterViewModel.hr = Convert.ToInt32(client.Hr);
				encounterViewModel.rr = Convert.ToInt32(client.Rr);
				encounterViewModel.bps = Convert.ToInt32(client.BpS);
				encounterViewModel.o2 = Convert.ToInt32(client.O2);
				encounterViewModel.hint = client.Heent;
				encounterViewModel.cv = Convert.ToInt32(client.Cv);
				encounterViewModel.chest = Convert.ToInt32(client.Chest);
				encounterViewModel.abd = Convert.ToInt32(client.Abd);
				encounterViewModel.extr = client.Extr;
				encounterViewModel.skin = client.Skin;
				encounterViewModel.other = client.Other;
				encounterViewModel.plan = client.TreatmentPlan;
				encounterViewModel.medicalDispensed = client.MedicationDispense;
				encounterViewModel.procedure = client.Procedure;
				encounterViewModel.followUp = client.FollowUp;
				encounterViewModel.reqid = reqid;

				return encounterViewModel;
			}
			else
			{
				encounterViewModel.reqid = reqid;
				return encounterViewModel;
			}
		}
		public void AddEncounterDetails(int reqid, EncounterViewModel model)
		{
			Request request = _context.Requests.Where(r => r.Requestid == reqid).FirstOrDefault();
			Encounter encounterone = _context.Encounters.Where(e => e.Requestid == reqid).FirstOrDefault();
			if (encounterone == null)
			{
				Encounter encounter = new Encounter()
				{
					Firstname = model.firstName,

					Location = model.Location,
					BirthDate = model.birthDate,
					CreatedDate = model.Date,
					PhoneNumber = model.phoneNumber,
					Email = model.Email,
					HistoryOfInjury = model.historyOfInjury,
					MedicalHistory = model.medicalHistory,
					Medication = model.medications,
					Allergies = model.allergies,
					Temp = Convert.ToInt32(model.temp),
					Hr = Convert.ToInt32(model.hr),
					Rr = Convert.ToInt32(model.rr),
					BpD = Convert.ToInt32(model.bpd),
					BpS = Convert.ToInt32(model.bps),
					O2 = Convert.ToInt32(model.o2),
					Pain = model.pain,
					Heent = model.heent,
					Cv = Convert.ToInt32(model.cv),
					Chest = Convert.ToInt32(model.chest),
					Abd = Convert.ToInt32(model.abd),
					Extr = model.extr,
					Skin = model.skin,
					Neuro = Convert.ToString(model.neuro),
					Other = model.other,


					TreatmentPlan = model.plan,
					MedicationDispense = model.medicalDispensed,
					Procedure = model.procedure,
					FollowUp = model.followUp,
					IsFinalized = new BitArray(1, false),
					Requestid = request.Requestid,
					Request = request
				};
				_context.Add(encounter);
				_context.SaveChanges();
			}
			else
			{
				Encounter encounter = _context.Encounters.Where(r => r.Requestid == reqid).FirstOrDefault();
				encounter.Firstname = model.firstName;
				encounter.Firstname = model.firstName;
				encounter.Location = model.Location;
				encounter.BirthDate = model.birthDate;
				encounter.CreatedDate = model.Date;
				encounter.PhoneNumber = model.phoneNumber;
				encounter.Email = model.Email;
				encounter.HistoryOfInjury = model.historyOfInjury;
				encounter.MedicalHistory = model.medicalHistory;
				encounter.Medication = model.medications;
				encounter.Allergies = model.allergies;
				encounter.Temp = Convert.ToInt32(model.temp);
				encounter.Hr = Convert.ToInt32(model.hr);
				encounter.Rr = Convert.ToInt32(model.rr);
				encounter.BpD = Convert.ToInt32(model.bpd);
				encounter.BpS = Convert.ToInt32(model.bps);
				encounter.O2 = Convert.ToInt32(model.o2);
				encounter.Pain = model.pain;
				encounter.Heent = model.heent;
				encounter.Cv = Convert.ToInt32(model.cv);
				encounter.Chest = Convert.ToInt32(model.chest);
				encounter.Abd = Convert.ToInt32(model.abd);
				encounter.Extr = model.extr;
				encounter.Skin = model.skin;

				encounter.Neuro = Convert.ToString(model.neuro);
				encounter.Other = model.other;


				encounter.TreatmentPlan = model.plan;
				encounter.MedicationDispense = model.medicalDispensed;
				encounter.Procedure = model.procedure;
				encounter.FollowUp = model.followUp;
				_context.SaveChanges();
			}


		}
		public Request GetRequest(SendLinkViewModel model)
		{
			return _context.Requests.Where(r => r.Email == model.email).FirstOrDefault();
		}
		public CloseCaseViewModel getclosedetails(int reqid)
		{
			CloseCaseViewModel closeCaseViewModel = new CloseCaseViewModel();
			closeCaseViewModel.requestWiseFiles = _context.Requestwisefiles.Where(rwf => rwf.Requestid == reqid).ToList();
			var reqclient = _context.Requestclients.Where(rc => rc.Requestid == reqid).FirstOrDefault();
			closeCaseViewModel.reqId = reqid;

			closeCaseViewModel.firstName = reqclient.Firstname;
			closeCaseViewModel.lastName = reqclient.Lastname;
			closeCaseViewModel.phoneNumber = reqclient.Phonenumber;
			closeCaseViewModel.DOB = reqclient.BirthDate;
			closeCaseViewModel.email = reqclient.Email;
			return closeCaseViewModel;
		}
		public void insertCaseClose(CloseCaseViewModel model, int button)
		{
			Request request = _context.Requests.Where(r => r.Requestid == model.reqId).FirstOrDefault();
			Requeststatuslog requeststatuslog = new Requeststatuslog
			{
				Requestid = model.reqId,
				Request = request,
				Status = 9,
				Createddate = DateTime.Now,

			};
			_context.Requeststatuslogs.Add(requeststatuslog);
			request.Status = 9;
			_context.SaveChanges();

		}
		public void updatereqclient(CloseCaseViewModel model)
		{
			Requestclient reqclient = _context.Requestclients.Where(rc => rc.Requestid == model.reqId).FirstOrDefault();
			reqclient.Phonenumber = model.phoneNumber;
			reqclient.Email = model.email;
			_context.SaveChanges();
		}
		public Admin getadminone(string id)
		{
			return _context.Admins.Include(ad => ad.Accessrole).Where(ad => ad.Aspnetuserid == id).FirstOrDefault();
		}
		public myProfilevm getadmin(string id)
		{
			myProfilevm myProfilevm = new myProfilevm();
			var admin = _context.Admins.Where(ad => ad.Aspnetuserid == id).FirstOrDefault();
			var aspuser = _context.Aspnetusers.Where(asp => asp.Id == id).FirstOrDefault();
			var regionId = _context.Regions.FirstOrDefault(u => u.Name == admin.City)!.Regionid;
			myProfilevm.aspuserId = id;
			myProfilevm.username = aspuser.Username;
			myProfilevm.password = aspuser.Passwordhash;
			myProfilevm.email = admin.Email;
			myProfilevm.firstname = admin.Firstname;
			myProfilevm.lastname = admin.Lastname;
			myProfilevm.cemail = admin.Email;
			myProfilevm.region = regionId;
			myProfilevm.adress1 = admin.Address1;
			myProfilevm.adress2 = admin.Address2;
			myProfilevm.city = admin.City;
			myProfilevm.state = "Gujrat";
			myProfilevm.zip = admin.Zip;
			myProfilevm.phoneno = admin.Mobile;
			myProfilevm.regions = _context.Regions.ToList();
			myProfilevm.adminregion = _context.Adminregions.Where(ad => ad.Adminid == admin.Adminid).ToList();
			return myProfilevm;
		}
		public editProvidervm getphysician(int id)
		{
			editProvidervm editProvidervm = new editProvidervm();
			var physician = _context.Physicians.Where(ph => ph.Physicianid == id).Include(ph => ph.Aspnetuser).FirstOrDefault();
			var physregion = _context.Physicianregions.Where(ph => ph.Physicianid == id).ToList();
			var region = _context.Regions.ToList();
			//var regionId = _context.Regions.FirstOrDefault(u => u.Name == physician.City)!.Regionid;
			//var regionId = _context.Regions.FirstOrDefault(r => r.Name == physician.City).Regionid;
			editProvidervm.phyid = id;
			editProvidervm.aspuserid = physician.Aspnetuserid;
			editProvidervm.username = physician.Aspnetuser.Username;
			editProvidervm.password = physician.Aspnetuser.Passwordhash;
			editProvidervm.email = physician.Email;
			editProvidervm.firstname = physician.Firstname;
			editProvidervm.lastname = physician.Lastname;

			//editProvidervm.regionid = regionId;
			editProvidervm.adress1 = physician.Address1;
			editProvidervm.adress2 = physician.Address2;
			editProvidervm.city = physician.City;
			editProvidervm.state = "Gujrat";
			editProvidervm.zip = physician.Zip;
			editProvidervm.phoneno = physician.Mobile;
			editProvidervm.adress1 = physician.Address1;
			editProvidervm.adress2 = physician.Address2;
			editProvidervm.city = physician.City;
			editProvidervm.zip = physician.Zip;
			//editProvidervm.regionid = regionId;
			editProvidervm.ml = physician.Medicallicense;
			editProvidervm.npinumber = physician.Npinumber;
			editProvidervm.semail = physician.Syncemailaddress;
			editProvidervm.agreement = Convert.ToBoolean(physician.Isagreementdoc);
			editProvidervm.background = Convert.ToBoolean(physician.Isbackgrounddoc);
			editProvidervm.disclosure = Convert.ToBoolean(physician.Isnondisclosuredoc);
			editProvidervm.license = Convert.ToBoolean(physician.Islicensedoc);

			editProvidervm.compilance = Convert.ToBoolean(physician.Istrainingdoc);

			editProvidervm.bname = physician.Businessname;
			editProvidervm.website = physician.Businesswebsite;
			editProvidervm.sign = physician.Signature;
			editProvidervm.Photo = physician.Photo;
			editProvidervm.text = physician.Adminnotes;

			editProvidervm.phyregion = physregion;
			editProvidervm.region = region;
			return editProvidervm;
		}
		public void AdminResetPassword(myProfilevm myProfilevm)
		{
			var aspnetuser = _context.Aspnetusers.FirstOrDefault(u => u.Id == myProfilevm.aspuserId);

			aspnetuser.Passwordhash = myProfilevm.admin.Aspnetuser.Passwordhash;

			_context.Update(aspnetuser);
			_context.SaveChanges();
		}


		public void EditAdminDetails(myProfilevm adminDetails)
		{
			var admin = _context.Admins.FirstOrDefault(u => u.Aspnetuserid == adminDetails.aspuserId);
			var aspnetuser = _context.Aspnetusers.FirstOrDefault(u => u.Id == adminDetails.aspuserId);
			var admregion = _context.Adminregions.Where(ph => ph.Adminid == admin.Adminid).ToList();
			foreach (var item in admregion)
			{
				_context.Adminregions.Remove(item);
				_context.SaveChanges();
			}

			foreach (var item in adminDetails.AllCheck)
			{
				Adminregion adminregion = new Adminregion()
				{
					Adminid = admin.Adminid,
					Regionid = item
				};
				_context.Add(adminregion);
				_context.SaveChanges();
			}

			admin.Firstname = adminDetails.firstname;
			admin.Lastname = adminDetails.lastname;
			admin.Email = adminDetails.email;
			admin.Mobile = adminDetails.phoneno;

			_context.Update(admin);
			_context.SaveChanges();

			aspnetuser.Username = adminDetails.firstname + " " + adminDetails.lastname;

			aspnetuser.Email = adminDetails.email;
			aspnetuser.Phonenumber = adminDetails.phoneno;

			_context.Update(aspnetuser);
			_context.SaveChanges();


		}

		public void EditBillingDetailsAdmin(myProfilevm adminDetails)
		{
			var admin = _context.Admins.FirstOrDefault(u => u.Aspnetuserid == adminDetails.aspuserId);
			var regionId = _context.Regions.FirstOrDefault(u => u.Name == adminDetails.city)!.Regionid;


			admin.Address1 = adminDetails.adress1;
			admin.Address2 = adminDetails.adress2;
			admin.City = adminDetails.city;
			admin.Zip = adminDetails.zip;
			admin.Regionid = regionId;

			_context.Update(admin);
			_context.SaveChanges();

		}
		public void AdminResetPasswordPhy(editProvidervm model)
		{
			var aspnetuser = _context.Aspnetusers.FirstOrDefault(u => u.Id == model.aspuserid);

			aspnetuser.Passwordhash = model.password;

			_context.Update(aspnetuser);
			_context.SaveChanges();
		}


		public void EditAdminDetailsPhy(editProvidervm model)
		{
			var phy = _context.Physicians.Include(ph => ph.Aspnetuser).Where(ph => ph.Physicianid == model.phyid).FirstOrDefault();

			var phyregion = _context.Physicianregions.Where(ph => ph.Physicianid == phy.Physicianid).ToList();
			foreach (var item in phyregion)
			{
				_context.Physicianregions.Remove(item);
				_context.SaveChanges();
			}
			foreach (var item in model.AllCheck)
			{
				Physicianregion physicianregion = new Physicianregion()
				{
					Physicianid = model.phyid,
					Regionid = item
				};
				_context.Add(physicianregion);
				_context.SaveChanges();
			}
			phy.Firstname = model.firstname;
			phy.Lastname = model.lastname;
			phy.Email = model.email;
			phy.Mobile = model.phoneno;
			_context.Update(phy);
			_context.SaveChanges();
			phy.Aspnetuser.Username = model.firstname + " " + model.lastname;
			phy.Aspnetuser.Email = model.email;
			phy.Aspnetuser.Phonenumber = model.phoneno;
			_context.Update(phy);
			_context.SaveChanges();
		}

		public void EditBillingDetailsPhy(editProvidervm model)
		{
			var phy = _context.Physicians.Include(ph => ph.Aspnetuser).Where(ph => ph.Physicianid == model.phyid).FirstOrDefault();
			phy.Address1 = model.adress1;
			phy.Address2 = model.adress2;
			phy.City = model.city;
			phy.Zip = model.zip;
			//phy.Regionid = regionId;
			_context.Update(phy);
			_context.SaveChanges();

		}
		public void providerProfilePhy(editProvidervm model)
		{
			Physician phy = _context.Physicians.Where(ph => ph.Physicianid == model.phyid).FirstOrDefault();
			phy.Adminnotes = model.text;
			phy.Businesswebsite = model.website;
			phy.Businessname = model.bname;
			_context.SaveChanges();
		}
		public Providervm getPhysicianData(int regionId)
		{
			if (regionId == 0)
			{
				Providervm providervm = new Providervm();
				providervm.physicians = _context.Physiciannotifications.Include(ph => ph.Physician).Include(ph => ph.Physician.Role).ToList();
				return providervm;
			}
			else
			{
				Providervm providervm = new Providervm();
				providervm.physicians = _context.Physiciannotifications.Include(ph => ph.Physician).Include(ph => ph.Physician.Role).Where(ph => ph.Physician.Regionid == regionId).ToList();
				return providervm;
			}
		}
		public Providervm GetRegion()
		{
			Providervm providervm = new Providervm();
			providervm.regions = _context.Regions.ToList();
			return providervm;
		}
		public List<Region> GetRegionForMd()
		{

			return _context.Regions.ToList();

		}
		public List<Physician> MdStatusDetails(int regionId)
		{
			var data = _context.Physicians
					   .Include(a => a.Shifts)
					   .ThenInclude(sd => sd.Shiftdetails).Where(a => (a.Regionid == regionId || regionId == 0))
					   .ToList();

			return data;
		}
		public List<Region> GetRegionForShift()
		{
			return _context.Regions.ToList();
		}
		public IQueryable<NewStateAdminVm> ExportAllService()
		{

			var data = from r in _context.Requests
					   join rc in _context.Requestclients on r.Requestid equals rc.Requestid
					   select new NewStateAdminVm
					   {
						   Firstname = rc.Firstname,
						   Lastname = rc.Lastname,

						   RequestorFirstname = r.Firstname,
						   RequestorLastname = r.Lastname,
						   Createddate = r.Createddate,
						   Phonenumber = rc.Phonenumber,
						   City = rc.City,
						   State = rc.State,
						   Street = rc.Street,
						   Zipcode = rc.Zipcode,
						   Notes = rc.Notes,
						   Status = r.Status,
						   Email = rc.Email,
						   RequestTypeId = r.Requesttypeid,
						   RequestId = r.Requestid,
						   ConfirmationNumber = r.Confirmationnumber
					   };

			return data;

		}
		public void updateNotificationDetails(int physicianId, bool isChecked)
		{
			if (isChecked)
			{
				var notification = _context.Physiciannotifications.FirstOrDefault(u => u.Physicianid == physicianId);

				notification.Isnotificationstopped = true;
				_context.Update(notification);
				_context.SaveChanges();
			}
			else
			{
				var notification = _context.Physiciannotifications.FirstOrDefault(u => u.Physicianid == physicianId);
				notification.Isnotificationstopped = false;
				_context.Update(notification);
				_context.SaveChanges();
			}
		}
		public void UpdateProviderDoc(editProvidervm editPhysicianvm)
		{
			var physician = _context.Physicians.FirstOrDefault(p => p.Physicianid == editPhysicianvm.phyid);
			string uniquefilename1 = null;
			if (editPhysicianvm.agreementdoc != null && editPhysicianvm.agreementdoc.Length > 0)
			{
				string uploadfolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
				uniquefilename1 = $"{editPhysicianvm.phyid}" + "_AgrrementDoc.pdf";
				string filepath = Path.Combine(uploadfolder, uniquefilename1);
				using (var stream = new FileStream(filepath, FileMode.Create))
				{
					editPhysicianvm.agreementdoc.CopyTo(stream);

				}
				physician!.Isagreementdoc = true;

			}

			if (editPhysicianvm.backgrounddoc != null && editPhysicianvm.backgrounddoc.Length > 0)
			{
				string uploadfolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
				uniquefilename1 = $"{editPhysicianvm.phyid}" + "_BackgroundDoc.pdf";
				string filepath = Path.Combine(uploadfolder, uniquefilename1);
				using (var stream = new FileStream(filepath, FileMode.Create))
				{
					editPhysicianvm.backgrounddoc.CopyTo(stream);

				}
				physician!.Isbackgrounddoc = true;
			}

			if (editPhysicianvm.disclosuredoc != null && editPhysicianvm.disclosuredoc.Length > 0)
			{
				string uploadfolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
				uniquefilename1 = $"{editPhysicianvm.phyid}" + "_NonDisclosureDoc.pdf";
				string filepath = Path.Combine(uploadfolder, uniquefilename1);
				using (var stream = new FileStream(filepath, FileMode.Create))
				{
					editPhysicianvm.disclosuredoc.CopyTo(stream);

				}
				physician!.Isnondisclosuredoc = true;
			}

			if (editPhysicianvm.licensedoc != null && editPhysicianvm.licensedoc.Length > 0)
			{
				string uploadfolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
				uniquefilename1 = $"{editPhysicianvm.phyid}" + "_LicenseDoc.pdf";
				string filepath = Path.Combine(uploadfolder, uniquefilename1);
				using (var stream = new FileStream(filepath, FileMode.Create))
				{
					editPhysicianvm.licensedoc.CopyTo(stream);
				}
				physician!.Islicensedoc = true;
			}
			_context.SaveChanges();
		}
		public AccessPagevm GetAccessPagevm()
		{
			var accessPage = new AccessPagevm()
			{
				roles = _context.Roles.ToList(),
			};
			return accessPage;
		}
		public AccessPagevm GetMenuDetails(int accountType)
		{
			var menu = accountType == 0 ? _context.Menus.ToList() : _context.Menus.Where(m => m.Accounttype == accountType).ToList();
			AccessPagevm accessPagevm = new()
			{
				MenuList = menu,
			};
			return accessPagevm;
		}
		public void UpdateRolemenus(AccessPagevm accessPagevm)
		{
			var role = new Role()
			{
				Name = accessPagevm.RoleName!,
				Accounttype = (short)accessPagevm.accountType!,
				Createdby = "Admin",
				Createddate = DateTime.Now,
				Isdeleted = new BitArray(1, false),
			};
			_context.Add(role);
			_context.SaveChanges();
			foreach (var item in accessPagevm.menuItems)
			{
				var rolemenu = new RoleMenu()
				{
					Roleid = role.Roleid,
					Menuid = item
				};
				_context.Update(rolemenu);

			}
			_context.SaveChanges();
		}
		public AccessPagevm GetEditRoleDetails(int roleId)
		{
			var role = _context.Roles.FirstOrDefault(r => r.Roleid == roleId);
			var accesspagevm = new AccessPagevm()
			{
				RoleName = role!.Name,
				accountType = role.Accounttype,
				MenuList = _context.Menus.Where(m => m.Accounttype == role.Accounttype).ToList(),
				menuItems = _context.RoleMenus.Where(r => r.Roleid == roleId).Select(r => r.Menuid).ToList(),
				RoleId = roleId,
			};
			return accesspagevm;

		}
		public void EditRole(AccessPagevm accessPagevm)
		{
			var role = _context.Roles.FirstOrDefault(r => r.Roleid == accessPagevm.RoleId);
			role!.Name = accessPagevm.RoleName!;
			_context.SaveChanges();
			var rolemenus = _context.RoleMenus.Where(rm => rm.Roleid == accessPagevm.RoleId).ToList();
			if (accessPagevm.menuItems != null)
			{
				foreach (var item in rolemenus)
				{
					_context.RoleMenus.Remove(item);
				}
				foreach (var item in accessPagevm.menuItems)
				{
					RoleMenu roleMenu = new RoleMenu();
					roleMenu.Roleid = role.Roleid;
					roleMenu.Menuid = item;
					_context.Add(roleMenu);
				}
				_context.SaveChanges();
			}
			else
			{
				foreach (var item in rolemenus)
				{
					_context.RoleMenus.Remove(item);
				}
				_context.SaveChanges();
			}
		}
		public void DeleteRole(int roleid)
		{
			var role = _context.Roles.Include(r => r.RoleMenus).Include(r => r.Physicians).Include(r => r.Admins).FirstOrDefault(r => r.Roleid == roleid);
			if (role != null)
			{
				_context.Roles.Remove(role);
				_context.SaveChanges();
			}


		}
		public CreateProvidervm createProviderDetails()
		{
			CreateProvidervm createProvidervm = new()
			{
				Regions = _context.Regions.ToList(),
				RoleList = _context.Roles.Where(r => r.Accounttype == 2).ToList(),
			};
			return createProvidervm;

		}

		public void insertProviderData(CreateProvidervm model)
		{


			var aspnetuser = new Aspnetuser
			{
				Id = Guid.NewGuid().ToString("N"),
				Username = model.Lastname + " " + model.Firstname,
				Passwordhash = model.Password,
				Email = model.Email,
				Phonenumber = model.PhoneNumber,
				Roleid = 2
			};
			_context.Add(aspnetuser);
			_context.SaveChanges();
			var physician = new Physician()
			{
				Aspnetuserid = aspnetuser.Id,
				Firstname = model.Firstname,
				Lastname = model.Lastname,
				Email = model.Email,
				Mobile = model.PhoneNumber,
				Medicallicense = model.MedicalLicense,
				Npinumber = model.NpiNumber,
				Address1 = model.Address1,
				Address2 = model.Address2,
				Regionid = model.RegionId,
				City = model.City,
				Zip = model.Zipcode,
				Businessname = model.BusinessName,
				Businesswebsite = model.BusinessWebsite,
				Adminnotes = model.AdminNotes,
				Createdby = aspnetuser.Id,
				Status = 1,
				Createddate = DateTime.Now,
				Roleid = model.RoleId,
				Isdeleted = new BitArray(1, false),


			};
			_context.Add(physician);

			_context.SaveChanges();
			foreach (var item in model.region)
			{
				Physicianregion physicianregion = new Physicianregion()
				{
					Physicianid = physician.Physicianid,
					Regionid = item,


				};
				_context.Add(physicianregion);
				_context.SaveChanges();
			}


			Physiciannotification physiciannotification = new Physiciannotification()
			{
				Isnotificationstopped = false,
				Physicianid = physician.Physicianid,
				Physician = physician
			};
			_context.Add(physiciannotification);

			_context.SaveChanges();
			var physcianLocation = new Physicianlocation()
			{
				Physicianid = physician.Physicianid,
				Physicianname = physician.Firstname + " " + physician.Lastname,
				Address = physician.Address1 + "," + physician.Address2,
				Latitude = model.Latitude,
				Longitude = model.Longitude,
				Createddate = DateTime.Now,

			};
			_context.Add(physcianLocation);

			var aspnetuserrole = new Aspnetuserrole()
			{
				Userid = 37,
				Roleid = 3
			};
			_context.Add(aspnetuserrole);

			_context.SaveChanges();
			string uniquefilename1 = null;
			if (model.AgreementDoc != null && model.AgreementDoc.Length > 0)
			{
				string uploadfolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
				uniquefilename1 = $"{physician.Physicianid}" + "_AgrrementDoc.pdf";
				string filepath = Path.Combine(uploadfolder, uniquefilename1);

				using (var stream = new FileStream(filepath, FileMode.Create))
				{
					model.AgreementDoc.CopyTo(stream);

				}
				physician!.Isagreementdoc = true;
			}

			if (model.BackgroundDoc != null && model.BackgroundDoc.Length > 0)
			{
				string uploadfolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
				uniquefilename1 = $"{physician.Physicianid}" + "_BackgroundDoc.pdf";
				string filepath = Path.Combine(uploadfolder, uniquefilename1);

				using (var stream = new FileStream(filepath, FileMode.Create))
				{
					model.BackgroundDoc.CopyTo(stream);

				}

				physician!.Isbackgrounddoc = true;
			}

			if (model.NonDisclosureDoc != null && model.NonDisclosureDoc.Length > 0)
			{
				string uploadfolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
				uniquefilename1 = $"{physician.Physicianid}" + "_NonDisclosureDoc.pdf";
				string filepath = Path.Combine(uploadfolder, uniquefilename1);

				using (var stream = new FileStream(filepath, FileMode.Create))
				{
					model.NonDisclosureDoc.CopyTo(stream);

				}
				physician!.Isnondisclosuredoc = true;
			}

			if (model.LicienceDc != null && model.LicienceDc.Length > 0)
			{
				string uploadfolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
				uniquefilename1 = $"{physician.Physicianid}" + "_LicenseDoc.pdf";
				string filepath = Path.Combine(uploadfolder, uniquefilename1);

				using (var stream = new FileStream(filepath, FileMode.Create))
				{
					model.LicienceDc.CopyTo(stream);

				}
				physician!.Islicensedoc = true;
			}
			_context.SaveChanges();
		}
		public List<ShiftDetailsmodal> ShiftDetailsmodal(DateTime date, DateTime sunday, DateTime saturday, string type)
		{
			var shiftdetails = _context.Shiftdetails.Where(u => u.Shiftdate.Month == date.Month && u.Shiftdate.Year == date.Year);
			switch (type)
			{
				case "month":
					shiftdetails = _context.Shiftdetails.Where(u => u.Shiftdate.Month == date.Month && u.Shiftdate.Year == date.Year && u.Isdeleted == new System.Collections.BitArray(new[] { false }));
					break;

				case "week":
					shiftdetails = _context.Shiftdetails.Where(u => u.Shiftdate >= sunday && u.Shiftdate <= saturday && u.Isdeleted == new System.Collections.BitArray(new[] { false }));
					break;

				case "day":
					shiftdetails = _context.Shiftdetails.Where(u => u.Shiftdate.Month == date.Month && u.Shiftdate.Year == date.Year && u.Shiftdate.Day == date.Day && u.Isdeleted == new System.Collections.BitArray(new[] { false }));
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
		public List<Physician> physicians(int regionid)
		{
			if (regionid == 0)
				return _context.Physicians.ToList();
			else
			{
				return _context.Physicians.Where(ph => ph.Regionid == regionid).ToList();
			}

		}


		public List<JObject> GetPhysicianForCreateShift(int RegionId)
		{
			var physicianRegion = _context.Physicians.Where(m => m.Regionid == RegionId).Select(m => new JObject() { { "PhysicianId", m.Physicianid }, { "PhysicianName", m.Firstname } }).ToList();

			return physicianRegion;
		}
		public async Task<bool> CreateShift(CreateShift modal, int AdminId)
		{
			try
			{
				string WeekDays = "";
				if (modal.RepeatDays != null)
				{
					foreach (var item in modal.RepeatDays)
					{
						WeekDays += item + ",";
					}
					WeekDays = WeekDays.Substring(0, WeekDays.Length - 1);
				}
				Shift shift = new Shift()
				{
					Physicianid = modal.PhysicianId,
					Startdate = DateOnly.Parse(modal.ShiftDate),
					Isrepeat = new System.Collections.BitArray(new[] { modal.IsRepeat }),
					Weekdays = WeekDays,
					Repeatupto = modal.RepeatUpto,
					Createdby = "6YS6H",
					Createddate = DateTime.Now//by deafult for now
				};
				_context.Shifts.Add(shift);
				await _context.SaveChangesAsync();

				if (modal.RepeatDays == null)
				{
					modal.RepeatDays = new List<int>();
				}

				DateTime ShiftDate = DateTime.Parse(modal.ShiftDate.ToString());

				DateTime NexttDate = ShiftDate;
				int j = 0;

				for (int i = 0; i <= modal.RepeatUpto * modal.RepeatDays.Count(); i++)
				{
					Shiftdetail shiftDetail = new Shiftdetail()
					{
						Shiftid = shift.Shiftid,
						Shiftdate = NexttDate,
						Regionid = modal.PhysicianRegion,
						Starttime = modal.StartTime,
						Endtime = modal.EndTime,
						Status = 1,
						Isdeleted = new System.Collections.BitArray(new[] { false }),
					};
					_context.Shiftdetails.Add(shiftDetail);

					if (modal.RepeatDays.Count() > 0)
					{
						int skipDay = (7 - (int)ShiftDate.DayOfWeek - 1 + modal.RepeatDays[j]);
						if (skipDay > 7)
						{
							skipDay = skipDay % 7;
						}

						NexttDate = ShiftDate.AddDays(skipDay);
						ShiftDate = NexttDate;

						if (i % modal.RepeatUpto == modal.RepeatUpto - 1 && i != modal.RepeatUpto * modal.RepeatDays.Count() - 1)
						{
							j++;
							ShiftDate = DateTime.Parse(modal.ShiftDate.ToString());
						}

					}
					await _context.SaveChangesAsync();
				}
				return true;
			}
			catch { return false; }
		}
		public EditViewShift GetViewShift(int ShiftDetailId)
		{
			try
			{
				Shiftdetail shiftDetail = _context.Shiftdetails.Include(m => m.Shift).ThenInclude(m => m.Physician).FirstOrDefault(m => m.Shiftdetailid == ShiftDetailId);
				if (shiftDetail != null)
				{
					EditViewShift editViewShift = new EditViewShift()
					{
						ShiftDetailId = ShiftDetailId,
						PhysicianRegionVS = (int)shiftDetail.Regionid,
						PhysicianRegionName = _context.Regions.FirstOrDefault(m => m.Regionid == shiftDetail.Regionid).Name,
						PhysicianName = shiftDetail.Shift.Physician.Firstname,
						ShiftDateVS = shiftDetail.Shiftdate.ToString("yyyy-MM-dd"),
						StartTimeVS = shiftDetail.Starttime,
						EndTimeVS = shiftDetail.Endtime,
					};
					return editViewShift;
				}
				return new EditViewShift();
			}
			catch { return new EditViewShift(); }
		}
		public bool ReturnViewShift(int ShiftDetailId)
		{
			try
			{
				Shiftdetail shiftDetail = _context.Shiftdetails.FirstOrDefault(m => m.Shiftdetailid == ShiftDetailId);
				if (shiftDetail != null)
				{
					if (shiftDetail.Status == 1)
					{
						shiftDetail.Status = 2;
					}
					else
					{
						shiftDetail.Status = 1;
					}
					_context.SaveChanges();
					return true;
				}
				return false;
			}
			catch { return false; }
		}
		public bool DeleteViewShift(int ShiftDetailId)
		{
			try
			{
				Shiftdetail shiftDetail = _context.Shiftdetails.FirstOrDefault(m => m.Shiftdetailid == ShiftDetailId);
				if (shiftDetail != null)
				{
					shiftDetail.Isdeleted = new System.Collections.BitArray(new[] { true });
					_context.SaveChanges();
					return true;
				}
				return false;
			}
			catch { return false; }
		}
		public async Task<bool> EditViewShift(EditViewShift Shift)
		{
			try
			{
				Shiftdetail shiftDetail = _context.Shiftdetails.FirstOrDefault(m => m.Shiftdetailid == Shift.ShiftDetailId);
				if (shiftDetail != null)
				{
					shiftDetail.Shiftdate = DateTime.Parse(Shift.ShiftDateVS);
					shiftDetail.Starttime = Shift.StartTimeVS;
					shiftDetail.Endtime = Shift.EndTimeVS;

					await _context.SaveChangesAsync();
					return true;
				}
				return false;
			}
			catch { return false; }
		}
		public Schedulereqvm Shiftrequest(int page, int pageSize, int regionid)
		{
			Schedulereqvm schedulereqvm = new Schedulereqvm();
			List<Shiftdetail> shiftdetail = null;
			if (regionid == 0)
			{
				shiftdetail = _context.Shiftdetails.Include(m => m.Shift).ThenInclude(m => m.Physician).Where(m => m.Isdeleted == new System.Collections.BitArray(new[] { false })).OrderByDescending(m => m.Shiftdate).ToList();
			}
			else
			{
				shiftdetail = _context.Shiftdetails.Include(m => m.Shift).ThenInclude(m => m.Physician).Where(m => m.Shift.Physician.Regionid == regionid && m.Isdeleted == new System.Collections.BitArray(new[] { false })).OrderByDescending(m => m.Shiftdate).ToList();
			}
			schedulereqvm.region = _context.Regions.ToList();
			schedulereqvm.paginatedRequest = shiftdetail.Skip((page - 1) * pageSize).Take(pageSize);
			schedulereqvm.CurrentPage = page;
			schedulereqvm.PageSize = pageSize;
			schedulereqvm.TotalPages = Math.Ceiling((double)shiftdetail.Count / pageSize);
			schedulereqvm.total = shiftdetail.Count;

			return schedulereqvm;
		}

		public bool requestapprove(Schedulereqvm model)
		{
			try
			{
				foreach (var item in model.allshift)
				{
					Shiftdetail shiftDetail = _context.Shiftdetails.FirstOrDefault(m => m.Shiftdetailid == item);
					if (shiftDetail != null)
					{
						if (shiftDetail.Status == 1)
						{
							shiftDetail.Status = 2;
						}
						else
						{
							shiftDetail.Status = 1;
						}
						_context.SaveChanges();

					}

				}
				return true;
			}
			catch (Exception)
			{

				return false;
			}


		}
		public bool requestdelete(Schedulereqvm model)
		{
			try
			{
				if (model.allshift == null)
				{
					return false;
				}
				foreach (var item in model.allshift)
				{
					Shiftdetail shiftDetail = _context.Shiftdetails.FirstOrDefault(m => m.Shiftdetailid == item);
					if (shiftDetail != null)
					{
						shiftDetail.Isdeleted = new System.Collections.BitArray(new[] { true });
						_context.SaveChanges();

					}

				}
				return true;
			}
			catch (Exception)
			{

				return false;
			}


		}
		public List<Physicianlocation> GetPhysicianLocations()
		{
			return _context.Physicianlocations.ToList();
		}
		public List<Shiftdetail> Getshiftdetail(DateTime date)
		{
			return _context.Shiftdetails.Include(m => m.Shift).ThenInclude(m => m.Physician).Where(m => m.Shiftdate == date && m.Isdeleted == new System.Collections.BitArray(new[] { false })).ToList();
		}
		public CreateAdminAccountvm CreateAdminAccountDetails()
		{
			CreateAdminAccountvm createAdminAccountvm = new()
			{
				regions = _context.Regions.ToList(),
				Rolelist = _context.Roles.Where(r => r.Accounttype == 1).ToList(),

			};

			return createAdminAccountvm;
		}
		public void CreateAdminAccount(CreateAdminAccountvm createAdminAccountvm, List<int> regions)
		{
			var aspnetuser = new Aspnetuser
			{
				Id = Guid.NewGuid().ToString("N"),
				Username = createAdminAccountvm.Lastname + " " + createAdminAccountvm.Firstname,
				Passwordhash = createAdminAccountvm.Password,
				Email = createAdminAccountvm.Email,
				Phonenumber = createAdminAccountvm.Phonenumber,
				Roleid = 1


			};
			_context.Add(aspnetuser);
			_context.SaveChanges();
			var admin = new Admin
			{
				Aspnetuserid = aspnetuser.Id,
				Firstname = createAdminAccountvm.Firstname,
				Lastname = createAdminAccountvm.Lastname,
				Email = createAdminAccountvm.Email,
				Mobile = createAdminAccountvm.Phonenumber,
				Address1 = createAdminAccountvm.Address1,
				Address2 = createAdminAccountvm.Address2,
				City = createAdminAccountvm.City,
				Regionid = createAdminAccountvm.RegionId,
				Zip = createAdminAccountvm.Zipcode,
				Altphone = createAdminAccountvm.AltPhone,
				Createdby = "Admin",
				Createddate = DateTime.Now,
				Status = 1,
				Isdeleted = new BitArray(1, false),
			};
			_context.Add(admin);
			_context.SaveChanges();
			foreach (var item in regions)
			{
				var adminRegions = new Adminregion
				{
					Adminid = admin.Adminid,
					Regionid = item,
				};
				_context.Add(adminRegions);
			}
			_context.SaveChanges();
		}
		public Vendorvm Getvenderdata(int hptypeid, string Searchname, int requestype)
		{
			Vendorvm vendorvm = new Vendorvm();

			var alldata = _context.Healthprofessionals.Where(hp => hp.Isdeleted == new System.Collections.BitArray(new[] { false })).Include(hp => hp.ProfessionNavigation).ToList();
			vendorvm.hptype = _context.Healthprofessionaltypes.ToList();
			if (Searchname != null)
			{
				alldata = alldata.Where(hp => hp.Vendorname.ToLower() == Searchname.ToLower()).ToList();
			}
			if (hptypeid != 0)
			{
				alldata = alldata.Where(hp => hp.ProfessionNavigation.Healthprofessionalid == hptypeid).ToList();
			}
			vendorvm.healthprofessionalList = alldata;
			return vendorvm;

		}
		public AddBusiness getHptype()
		{
			AddBusiness addBusiness = new AddBusiness();
			addBusiness.hptype = _context.Healthprofessionaltypes.ToList();
			return addBusiness;


		}
		public void AddVendor(AddBusiness model)
		{

			var prof = _context.Healthprofessionaltypes.Where(hptype => hptype.Healthprofessionalid == model.proffesion).FirstOrDefault();
			Healthprofessional healthprofessional = new Healthprofessional()
			{
				Vendorname = model.bname,
				ProfessionNavigation = prof,
				Profession = prof.Healthprofessionalid,
				Faxnumber = model.faxno,
				Phonenumber = model.contact,
				Email = model.email,
				Businesscontact = model.bcontact,
				State = model.state,
				City = model.city,
				Zip = model.pincode,
				Isdeleted = new BitArray(1, false)


			};
			_context.Healthprofessionals.Add(healthprofessional);
			_context.SaveChanges();

		}
		public AddBusiness getVendor(int vendorid)
		{
			var vendor = _context.Healthprofessionals.Where(hp => hp.Vendorid == vendorid).FirstOrDefault();
			var hpttype = _context.Healthprofessionaltypes.ToList();
			AddBusiness addBusiness = new AddBusiness()
			{
				bname = vendor.Vendorname,
				proffesion = vendor.Profession,
				faxno = vendor.Faxnumber,
				contact = vendor.Phonenumber,
				email = vendor.Email,
				bcontact = vendor.Businesscontact,
				street = "assa",
				city = vendor.City,
				state = vendor.State,
				vendorid = vendor.Vendorid,
				pincode = vendor.Zip,
				hptype = hpttype


			};
			return addBusiness;


		}
		public void updateVendor(AddBusiness model)
		{
			Healthprofessional vendor = _context.Healthprofessionals.Where(hp => hp.Vendorid == model.vendorid).FirstOrDefault();
			var prof = _context.Healthprofessionaltypes.Where(hptype => hptype.Healthprofessionalid == model.proffesion).FirstOrDefault();
			vendor.Vendorname = model.bname;
			vendor.Profession = model.proffesion;
			vendor.ProfessionNavigation = prof;
			vendor.Email = model.email;
			vendor.Phonenumber = model.contact;
			vendor.City = model.city;
			vendor.State = model.state;
			vendor.Zip = model.pincode;
			_context.SaveChanges();


		}
		public void deleteBusiness(int vendorid)
		{
			Healthprofessional vendor = _context.Healthprofessionals.Where(hp => hp.Vendorid == vendorid).FirstOrDefault();
			vendor.Isdeleted = new System.Collections.BitArray(new[] { true });
			_context.SaveChanges();
		}
		public List<SearchRecordvm> GetSearchRecorddata(int page, int pageSize, string patientname, string statusofrequest, int requesttype, string email, DateOnly fromdate, DateOnly todate, string providername, string phoneNumber)
		{

			int[] statusrequest = null;

			if (!string.IsNullOrEmpty(statusofrequest))
			{
				statusrequest = statusofrequest.Split(',').Select(s => int.Parse(s)).ToArray();
			}
			var list = (from r in _context.Requests
						join rc in _context.Requestclients on r.Requestid equals rc.Requestid into requestGroup
						from result in requestGroup.DefaultIfEmpty()
						join p in _context.Physicians on r.Physicianid equals p.Physicianid into physicianGroup
						from logresult in physicianGroup.DefaultIfEmpty()
						join rn in _context.Requestnotes on r.Requestid equals rn.Requestid into notesGroup
						from noteresult in notesGroup.DefaultIfEmpty()

						select new
						{
							requestid = r.Requestid,
							requestType = r.Requesttypeid,
							patientName = result.Firstname != null ? result.Firstname : "No Name",
							email = result.Email != null ? result.Email : "a@gmail.com",
							Phone = result.Phonenumber != null ? result.Phonenumber : "123456789",
							address = result.Address,
							zip = result.Zipcode,
							requestStatus = r.Status,
							physician = logresult.Firstname != null ? logresult.Firstname : "No Name",
							physicianNotes = noteresult.Physiciannotes,
							adminNotes = noteresult.Adminnotes,
							patientNotes = result.Notes,
							dateOfService = r.Createddate, // take accepted date instend of created date
						}).OrderByDescending(r => r.dateOfService).ToList();

			if (patientname != null)
			{

				var serchTerms = patientname.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				list = list.Where(a => serchTerms.All(term => a.patientName.ToLower().Contains(term))).ToList();
			}

			if (requesttype != 0)
			{
				list = list.Where(x => x.requestType == requesttype).ToList();
			}

			/*if (searchRecordvm.fromDateofService != DateOnly.MinValue && searchRecordvm.toDateofService != DateOnly.MinValue)
            {
                list = list.Where(x => DateOnly.FromDateTime(x.dateOfService ?? DateTime.MinValue) >= fromdate && DateOnly.FromDateTime(x.dateOfService ?? DateTime.MinValue) <= todate).ToList();
            }*/

			if (statusofrequest != null && statusofrequest != "0")
			{
				if (statusrequest.Length == 1)
				{
					list = list.Where(x => x.requestStatus == statusrequest[0]).ToList();
				}
				if (statusrequest.Length == 2)
				{
					list = list.Where(x => x.requestStatus == statusrequest[0] || x.requestStatus == statusrequest[1]).ToList();
				}
				if (statusrequest.Length == 3)
				{
					list = list.Where(x => x.requestStatus == statusrequest[0] || x.requestStatus == statusrequest[1] || x.requestStatus == statusrequest[2]).ToList();
				}
			}

			if (email != null)
			{
				var serchTerms = email.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				list = list.Where(a => serchTerms.All(term => a.email.ToLower().Contains(term))).ToList();
			}
			if (providername != null)
			{

				var serchTerms = providername.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				list = list.Where(a => serchTerms.All(term => a.physician.ToLower().Contains(term))).ToList();
			}
			if (phoneNumber != null)
			{
				var serchTerms = phoneNumber.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				list = list.Where(a => serchTerms.All(term => a.Phone.ToLower().Contains(term))).ToList();
			}


			Dictionary<int, string> requestTypeMap = new Dictionary<int, string>()
			{
				{ 1, "Patient" },
				{ 2, "Family/Friend" },
				{ 3, "Concierge" },
				{ 4, "Business" },
				{ 5, "VIP" },

			};

			Dictionary<int, string> requestStatusMap = new Dictionary<int, string>()
			{
				{ 1, "New" },
				{ 2, "Pending" },
				{ 3, "Cancelled" },
				{ 4, "MDEnRoute" },
				{ 5, "MDONSite" },
				{ 6, "Conclude" },
				{ 7, "CancelledByPatient" },
				{ 8, "Close" },
				{ 9, "Unpaid" },
				{ 10, "Clear" },
				 { 11, "Block" },

			};


			var data = list.Select(x => new SearchRecordvm()
			{
				requestid = x.requestid,
				PatientName = x.patientName,
				RequestorName = requestTypeMap[x.requestType],
				Email = x.email,
				Phonenumber = x.Phone,
				Address = x.address,
				Zip = x.zip,
				RequestStatus = requestStatusMap[x.requestStatus],
				PhysicianName = x.physician,
				PhysicianNotes = x.physicianNotes,
				AdminNotes = x.adminNotes,
				PatientNotes = x.patientNotes,
				DateofService = x.dateOfService.ToString()
			}).ToList();



			return data;

		}
		public PatientRecordvm GetPatientRecordDetails(int page, int pageSize, string FirstName, string LastName, string Email, string PhoneNumber)
		{
			var list = _context.Users.ToList();
			PatientRecordvm patientRecordvm = new PatientRecordvm();

			list = list.Where(a =>
				(string.IsNullOrEmpty(FirstName) || a.Firstname.ToLower().Contains(FirstName.ToLower())) &&
				(string.IsNullOrEmpty(LastName) || a.Lastname!.ToLower().Contains(LastName.ToLower())) &&
				(string.IsNullOrEmpty(Email) || a.Email.ToLower().Contains(Email.ToLower())) &&
			   (string.IsNullOrEmpty(PhoneNumber) || a.Mobile!.Replace(" ", "").Contains(PhoneNumber)
				)).ToList();
			patientRecordvm.User = list;


			int count = patientRecordvm.User.Count();
			int TotalPage = (int)Math.Ceiling(count / (double)pageSize);
			patientRecordvm.User = patientRecordvm.User.Skip((page - 1) * pageSize).Take(pageSize).ToList();
			patientRecordvm.CurrentPage = page;
			patientRecordvm.TotalPages = TotalPage;
			patientRecordvm.total = count;
			patientRecordvm.PageSize = pageSize;




			return patientRecordvm;
		}
		public PatientHistoryvm getPatientHistoryDetails(int userid, int page, int pageSize, string FirstName, string LastName, string Email, string PhoneNumber)
		{
			PatientHistoryvm patientHistoryvm = new PatientHistoryvm();
			patientHistoryvm.userid = userid;
			var list = _context.Requestclients.Include(rc => rc.Request).Include(rc => rc.Request.Physician).Include(rc => rc.Request.Requestwisefiles).Where(rc => rc.Request.Userid == userid).OrderByDescending(rc => rc.Request.Createddate).ToList();

			list = list.Where(a =>
				(string.IsNullOrEmpty(FirstName) || a.Firstname.ToLower().Contains(FirstName.ToLower())) &&
				(string.IsNullOrEmpty(LastName) || a.Lastname!.ToLower().Contains(LastName.ToLower())) &&
				(string.IsNullOrEmpty(Email) || a.Email.ToLower().Contains(Email.ToLower())) &&
			   (string.IsNullOrEmpty(PhoneNumber) || a.Phonenumber!.Replace(" ", "").Contains(PhoneNumber)
				)).ToList();
			patientHistoryvm.clients = list;
			int count = patientHistoryvm.clients.Count();
			int TotalPage = (int)Math.Ceiling(count / (double)pageSize);
			patientHistoryvm.clients = patientHistoryvm.clients.Skip((page - 1) * pageSize).Take(pageSize).ToList();
			patientHistoryvm.CurrentPage = page;
			patientHistoryvm.TotalPages = TotalPage;
			patientHistoryvm.total = count;
			patientHistoryvm.PageSize = pageSize;
			return patientHistoryvm;
		}

		public BlockHistoryvm getPatientBlockDetails(int page, int pageSize, string FirstName, string LastName, string Email, string PhoneNumber)
		{
			BlockHistoryvm blockHistoryvm = new BlockHistoryvm();

			var list = _context.Requestclients.Include(rc => rc.Request).Include(rc => rc.Request.Physician).Include(rc => rc.Request.Requestwisefiles).Where(rc => rc.Request.Status == 11).OrderByDescending(rc => rc.Request.Createddate).ToList();

			list = list.Where(a =>
				(string.IsNullOrEmpty(FirstName) || a.Firstname.ToLower().Contains(FirstName.ToLower())) &&
				(string.IsNullOrEmpty(LastName) || a.Lastname!.ToLower().Contains(LastName.ToLower())) &&
				(string.IsNullOrEmpty(Email) || a.Email.ToLower().Contains(Email.ToLower())) &&
			   (string.IsNullOrEmpty(PhoneNumber) || a.Phonenumber!.Replace(" ", "").Contains(PhoneNumber)
				)).ToList();
			blockHistoryvm.clients = list;
			int count = blockHistoryvm.clients.Count();
			int TotalPage = (int)Math.Ceiling(count / (double)pageSize);
			blockHistoryvm.clients = blockHistoryvm.clients.Skip((page - 1) * pageSize).Take(pageSize).ToList();
			blockHistoryvm.CurrentPage = page;
			blockHistoryvm.TotalPages = TotalPage;
			blockHistoryvm.total = count;
			blockHistoryvm.PageSize = pageSize;
			return blockHistoryvm;
		}
		public void unBlockreq(int requestid)
		{
			Request request = _context.Requests.Where(r => r.Requestid == requestid).FirstOrDefault();
			request.Status = 1;
			_context.SaveChanges();
		}
		public void deletereq(int requestid)
		{
			Requestclient requestclient = _context.Requestclients.Where(r => r.Requestid == requestid).FirstOrDefault();
			Request request = _context.Requests.Where(r => r.Requestid == requestid).FirstOrDefault();
			var reqslog = _context.Requeststatuslogs.Where(r => r.Requestid == requestid).ToList();
			if (reqslog != null)
			{
				foreach (var item in reqslog)
				{
					_context.Requeststatuslogs.Remove(item);
					_context.SaveChanges();
				}
			}

			var wisefile = _context.Requestwisefiles.Where(r => r.Requestid == requestid).ToList();
			if (wisefile != null)
			{
				foreach (var item in wisefile)
				{
					_context.Requestwisefiles.Remove(item);
					_context.SaveChanges();
				}
			}
			var notes = _context.Requestnotes.Where(r => r.Requestid == requestid).ToList();
			if (notes != null)
			{
				foreach (var item in notes)
				{
					_context.Requestnotes.Remove(item);
					_context.SaveChanges();
				}
			}
			if (requestclient != null)
			{
				_context.Requestclients.Remove(requestclient);
				_context.SaveChanges();
			}

			_context.Requests.Remove(request);
			_context.SaveChanges();

		}
		public EmailLogvm getEmailLogDetails(int page, int pageSize, string FirstName, int roleid, string Email, DateTime createdate, DateTime sendate)
		{
			EmailLogvm emaillogvm = new EmailLogvm();

			var list = _context.Emaillogs.ToList();
			if (FirstName != null)
			{

				var serchTerms = FirstName.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				list = list.Where(a => serchTerms.All(term => a.Recievername.ToLower().Contains(term))).ToList();
			}

			if (roleid != 0)
			{
				list = list.Where(x => x.Roleid == roleid).ToList();
			}

			/*if (searchRecordvm.fromDateofService != DateOnly.MinValue && searchRecordvm.toDateofService != DateOnly.MinValue)
            {
                list = list.Where(x => DateOnly.FromDateTime(x.dateOfService ?? DateTime.MinValue) >= fromdate && DateOnly.FromDateTime(x.dateOfService ?? DateTime.MinValue) <= todate).ToList();
            }*/



			if (Email != null)
			{
				var serchTerms = Email.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				list = list.Where(a => serchTerms.All(term => a.Emailid.ToLower().Contains(term))).ToList();
			}
			if (createdate != null && createdate != DateTime.MinValue)
			{


				list = list.Where(a => a.Createdate.Date == createdate.Date).ToList();
			}
			if (sendate != null && sendate != DateTime.MinValue)
			{
				list = list.Where(a => a.Sentdate == sendate.Date).ToList();
			}

			emaillogvm.clients = list;
			int count = emaillogvm.clients.Count();
			int TotalPage = (int)Math.Ceiling(count / (double)pageSize);
			emaillogvm.clients = emaillogvm.clients.Skip((page - 1) * pageSize).Take(pageSize).ToList();
			emaillogvm.CurrentPage = page;
			emaillogvm.TotalPages = TotalPage;
			emaillogvm.total = count;
			emaillogvm.PageSize = pageSize;
			return emaillogvm;
		}
		public EmailLogvm getSmsLogDetails(int page, int pageSize, string FirstName, int roleid, string Phonenumber, DateTime createdate, DateTime sendate)
		{
			EmailLogvm emaillogvm = new EmailLogvm();

			var list = _context.Smslogs.ToList();
			if (FirstName != null)
			{

				var serchTerms = FirstName.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				list = list.Where(a => serchTerms.All(term => a.Recievername.ToLower().Contains(term))).ToList();
			}

			if (roleid != 0)
			{
				list = list.Where(x => x.Roleid == roleid).ToList();
			}

			if (Phonenumber != null)
			{
				var serchTerms = Phonenumber.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				list = list.Where(a => serchTerms.All(term => a.Mobilenumber.Contains(term))).ToList();
			}
			if (createdate != null && createdate != DateTime.MinValue)
			{


				list = list.Where(a => a.Createdate.Date == createdate.Date).ToList();
			}
			if (sendate != null && sendate != DateTime.MinValue)
			{
				list = list.Where(a => a.Sentdate == sendate.Date).ToList();
			}

			emaillogvm.smslog = list;
			int count = emaillogvm.smslog.Count();
			int TotalPage = (int)Math.Ceiling(count / (double)pageSize);
			emaillogvm.smslog = emaillogvm.smslog.Skip((page - 1) * pageSize).Take(pageSize).ToList();
			emaillogvm.CurrentPage = page;
			emaillogvm.TotalPages = TotalPage;
			emaillogvm.total = count;
			emaillogvm.PageSize = pageSize;
			return emaillogvm;
		}

        public Admin GetadminOne(string id)
        {
			return _context.Admins.Include(ad=>ad.Accessrole).FirstOrDefault(ad => ad.Aspnetuserid == id);
        }
    }
}
