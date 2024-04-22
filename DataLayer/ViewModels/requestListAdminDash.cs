using DataLayer.Models;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.ViewModels
{
    public class RequestListAdminDash
    {
       public  List<Requestclient> reqlist { get; set; }
        public List<Region> regionList { get; set; }
        public int phy_id { get; set; }
        public int status { get; set; }
        public string firstname { get; set; }
        public int type { get; set; }
        public Requeststatuslog Requeststatuslog { get; set; }
        public IEnumerable<Requestclient> paginatedRequest { get; set; }

        public string transferNotes { get; set; }
        public int? reqid { get; set; }
        public string requesttype {  get; set; }
        
        public int newreq {  get; set; }

        public int pendingreq { get; set; }

        public int activereq { get; set; }
        public int total { get; set; }    
        public int conclude {  get; set; }  

        public int unpaid { get; set; } 
        public int closereq { get; set; }

        public Requestclient Requestclient { get; set; }
        public string AdminNotes { get; set; }

        public Requestnote RequestNotes { get; set; }
       public int CurrentPage {  get; set; }

        public double TotalPages { get; set; }
        public int PageSize { get; set; }

        public BitArray? isFinalized { get; set; }


    }
}
