using DataLayer.Models;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Interface_Provider
{
    public interface IProviderPanel
    {
        public void AddAdminNote(int reqid, RequestListAdminDash model);
        public RequestListAdminDash GetNewRequest(int page, int pageSize, int requestTypeId, string patientname, int regionId,int? phyid);
        public RequestListAdminDash GetPendingRequest(int page, int pageSize, int requestTypeId, string patientname, int regionId, int? phyid);
        public RequestListAdminDash GetActiveRequest(int page, int pageSize, int requestTypeId, string patientname, int regionId, int? phyid);
        public RequestListAdminDash GetConcludeRequest(int page, int pageSize, int requestTypeId, string patientname, int regionId, int? phyid);
        public Physician GetaspUser(string id);
        public bool GetAspUser(string email);
        public void TransferCaseReq(int reqid, string textnote);
        public void Reqaccept(int reqid);
        public void encounterCase(int requestid,int type);
        public void forHouseCall(int reqid);
        public void AddEncounterDetails(int reqid, EncounterViewModel model);
        public string SendEmail(string email, string body, string subject, string emailto, int phyid);
        public List<ShiftDetailsmodal> ShiftDetailsmodal(DateTime date, DateTime sunday, DateTime saturday, string type, int phyid);
        public List<Shiftdetail> Getshiftdetail(DateTime date, int phyid);
        public void FinalizeForm(int reqId);
        public Encounter Getencounter(int reqid);
        public void ReportUpload(ConcludeCarevm model);
        public void PostConcludeData(int reqid,ConcludeCarevm model);
    }
}
