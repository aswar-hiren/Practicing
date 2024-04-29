using DataLayer.Models;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Interface_Admin
{
    public interface IAdminRequest
    {

        public RequestListAdminDash GetNewRequest(int page, int pageSize, int requestTypeId, string patientname, int regionId);
        public void DeleteRole(int roleid);


        public RequestListAdminDash GetPendingRequest(int page, int pageSize, int requestTypeId, string patientname, int regionId);


        public RequestListAdminDash GetActiveRequest(int page, int pageSize, int requestTypeId, string patientname, int regionId);

        public RequestListAdminDash GetConcludeRequest(int page, int pageSize, int requestTypeId, string patientname, int regionId);

        public RequestListAdminDash GetCloseRequest(int page, int pageSize, int requestTypeId, string patientname, int regionId);


        public RequestListAdminDash GetUnPaidRequest(int page, int pageSize, int requestTypeId, string patientname, int regionId);
        public RequestListAdminDash getallrequest(int? type);
        public CloseCaseViewModel getclosedetails(int reqid);

        public Requestclient GetRequestclient(int reqclientid);
        public RequestListAdminDash GetRequestclientByRegion(string region, int status);

        public void AddAdminNote(int reqid, RequestListAdminDash model);

        public RequestListAdminDash GetAdminNote(int reqid);

        public void UpdateStatusAndNote(int reqid, CancelModel model);

        public List<Physician> GetPhysiciansForRegion(int region);
        public string SendEmailPhyContact(string email, string body, string subject, string emailto, int phyid);
        public int Getstatus(int reqid);
        public void AssignCaseReq(int reqid, int phyid, string textnote);
        public void TransferCaseReq(int reqid, int phyid, string textnote);
        public void BlockRequest(int reqid, BlockView model);
        public void clearCase(int requestid);
        public List<Region> regionlist();
        public viewdocumentmodel ShowDocument(int? reqclientid, int? requestid, viewdocumentmodel model);

        public void deletedoc(int documentid);
        public void deleteAllDoc(List<int> documentid, int flag, int requestid);
        public Requestwisefile getdoc(int documentid);
        public string Getemail(int reqclientid);
        public void SendEmailUp(string email, string body, string subject, string emailto, List<string> filepath,int reqid);
        public Requestwisefile GetFile(int documentid);
        public List<Healthprofessionaltype> GetOrderDetails();
        public List<Healthprofessional> GetOrder(int professionId);
        public Healthprofessional GetBusinessData(int businessid);
        public void CreateOrder(int? reqid, OrdersModel model);
        public string SendEmail(string email, string body, string subject, string emailto,int reqid);
        public string GenerateJwtToken(string secretKey, string issuer, string audience, int reqid, int status);
        public EncounterViewModel getdetails(int reqclientid, int reqid);
        public void AddEncounterDetails(int reqid, EncounterViewModel model);
        public Request GetRequest(SendLinkViewModel model);
        public void insertCaseClose(CloseCaseViewModel model, int button);
        public void updatereqclient(CloseCaseViewModel model);
        public myProfilevm getadmin(string id);
        public void AdminResetPassword(myProfilevm myProfilevm);
        public void EditAdminDetails(myProfilevm adminDetails);
        public void EditBillingDetailsAdmin(myProfilevm adminDetails);
        public Admin GetaspUser(string id);
        public Providervm getPhysicianData(int regionId);
        public IQueryable<NewStateAdminVm> ExportAllService();
        public Providervm GetRegion();
        public void updateNotificationDetails(int physicianId, bool isChecked);
        public editProvidervm getphysician(int id);
        public void AdminResetPasswordPhy(editProvidervm model);
        public void EditAdminDetailsPhy(editProvidervm model);
        public void EditBillingDetailsPhy(editProvidervm model);
        public void providerProfilePhy(editProvidervm model);
        public void UpdateProviderDoc(editProvidervm editPhysicianvm);
        public AccessPagevm GetAccessPagevm();
        public AccessPagevm GetMenuDetails(int accountType);
        public void UpdateRolemenus(AccessPagevm accessPagevm);
        public void EditRole(AccessPagevm accessPagevm);
        public AccessPagevm GetEditRoleDetails(int roleId);
        public CreateProvidervm createProviderDetails();
        public Admin GetadminOne(string id);
        public bool GetAspUser(string email);
        public void insertProviderData(CreateProvidervm model);
        public List<ShiftDetailsmodal> ShiftDetailsmodal(DateTime date, DateTime sunday, DateTime saturday, string type);
        public List<Physician> physicians(int regionid);
        public List<JObject> GetPhysicianForCreateShift(int RegionId);
        public List<Region> GetRegionForShift();
        public Task<bool> CreateShift(CreateShift modal, int AdminId);
        public EditViewShift GetViewShift(int ShiftDetailId);
        public bool ReturnViewShift(int ShiftDetailId);
        public bool DeleteViewShift(int ShiftDetailId);
        public Task<bool> EditViewShift(EditViewShift Shift);

        public Schedulereqvm Shiftrequest(int page, int pageSize, int regionid);

        public bool requestapprove(Schedulereqvm model);
        public bool requestdelete(Schedulereqvm model);
        public Schedulereqvm regionlistschedule();
        public List<Physicianlocation> GetPhysicianLocations();
        public List<Shiftdetail> Getshiftdetail(DateTime date);
        public CreateAdminAccountvm CreateAdminAccountDetails();
        public void CreateAdminAccount(CreateAdminAccountvm createAdminAccountvm, List<int> regions);
        public Vendorvm Getvenderdata(int hptypeid, string Searchname, int requestype);
        public AddBusiness getHptype();
        public void AddVendor(AddBusiness model);
        public AddBusiness getVendor(int vendorid);
        public void updateVendor(AddBusiness model);
        public void deleteBusiness(int vendorid);
        public List<SearchRecordvm> GetSearchRecorddata(int page,int pageSize,string patientname, string statusofrequest, int requesttype, string email, DateOnly fromdate, DateOnly todate, string providername, string phoneNumber);
        public PatientRecordvm GetPatientRecordDetails(int page, int pageSize, string FirstName, string LastName, string Email, string PhoneNumber);
        public PatientHistoryvm getPatientHistoryDetails(int userid, int page, int pageSize, string FirstName, string LastName, string Email, string PhoneNumber);
        public BlockHistoryvm getPatientBlockDetails(int page, int pageSize, string FirstName, string LastName, string Email, string PhoneNumber);
        public void unBlockreq(int requestid);
        public void deletereq(int requestid);
        public EmailLogvm getEmailLogDetails(int page, int pageSize, string FirstName, int roleid, string Email, DateTime createdate, DateTime sendate);
        public EmailLogvm getSmsLogDetails(int page, int pageSize, string FirstName, int roleid, string Phonenumber, DateTime createdate, DateTime sendate);

        public Admin getadminone(string id);
        public List<Region> GetRegionForMd();
        public List<Physician> MdStatusDetails(int regionId);
        public void deletePhy(int phyid);


    }
}