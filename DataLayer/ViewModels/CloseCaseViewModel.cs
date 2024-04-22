using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class CloseCaseViewModel
    {
        public string fullName {  get; set; }
        public List<Requestwisefile> requestWiseFiles { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateOnly? DOB { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public int reqId { get; set; }
        public int reqclientid { get; set; }
        


    }
}
