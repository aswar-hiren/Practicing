using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class editProvidervm
    {  public string aspuserid { get; set; }
        public string username {  get; set; }
        public string password { get; set; }    
        public string firstname { get; set; }
        public string lastname { get; set; }    
        public string email { get; set; }
        public string phoneno {  get; set; }
        public string ml { get; set; }
        public string npinumber { get; set; }
        public string semail { get; set; }
        public string adress1 { get; set; }
        public string adress2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string Photo { get; set; }
        public string sign { get; set; }
        public IFormFile signdevice { get; set; }
        public string bname { get; set; }

        public string website { get; set; }
        public string text { get; set; }
        public int phyid { get; set; }
        public int regionid { get; set; }
        public bool agreement { get; set; }
        public bool background { get; set; }
        public bool license { get; set; }

        public bool disclosure { get; set; }
        public bool compilance { get; set;}
        public IFormFile agreementdoc { get; set; }
        public IFormFile backgrounddoc { get; set; }
        public IFormFile disclosuredoc { get; set; }
        public IFormFile licensedoc { get; set; }
        public List<Physicianregion> phyregion { get; set; }
        public List<Region> region { get; set; }
        public List<int> AllCheck { get; set; }


    }
}
