using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModel
{
    public class uservm
    {
       
        public string search {  get; set; }
        public int id { get; set; }
        public List<User> user { get; set; }
        [Required] 
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public DateTime dob { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string phonenumber { get; set; }
        [Required]
        public string country { get; set; }
        [Required]
        public string city { get; set; }
        public List<City> Citylist { get; set; }   
        public int cityid { get; set; }
        public int CurrentPage { get; set; }

        public double TotalPages { get; set; }
        public int PageSize { get; set; }
        public int total { get; set; }
        public IEnumerable<User> paginatedRequest { get; set; }
        public User userone {  get; set; }  
    }
}
