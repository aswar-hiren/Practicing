using DataLayer.Models;

using DataLayer.ViewModels;
using Microsoft.AspNetCore.Http;
namespace DataLayer.ViewModels
{
    public class PatientDashboard
    {
        public List<Request> requests { get; set; }

        public List<Requestwisefile> wiseFiles { get; set; }

        public string? UploaderName { get; set; }
        public string? userName { get; set; }
        public int count { get; set; }
        public IFormFile Photo { get; set; }
        public IEnumerable<Request> paginatedRequest { get; set; }
        public UpdatePatientProfile? PatientProfile { get; set; }

        public User User { get; set; }
    }
}
