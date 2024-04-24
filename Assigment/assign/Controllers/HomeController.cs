using DataLayer.Models;
using DataLayer.DataContext;
using DataLayer.ViewModel;
using DataLayer.ViewModels;
using LogicLayer.Interface_patient;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace assign.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HellodocPrjContext _context;
        private readonly IPatientRequest _PatientRequest;

        public HomeController(ILogger<HomeController> logger,HellodocPrjContext context,IPatientRequest patientRequest)
        {
            _logger = logger;
            _context = context;
            _PatientRequest = patientRequest;
        }

        public IActionResult Index()
        { 

           var data =_context.Users.ToList();
           var temp= data.Select(x => new uservm()
            {
                firstname = x.Firstname,
                lastname=x.Lastname
            }).Where(x=>x.firstname=="dvsg").ToList();
            return View(temp);
         
        }
        public IActionResult PatientInfo()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PatientInfoPage(PatientInfo model)
        {
            try
            {
                 _PatientRequest.InsertPatientRequestData(model);
             
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {

                TempData["error"] = "Error while Request ";
                return RedirectToAction("Index", "Home");
            }


        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}