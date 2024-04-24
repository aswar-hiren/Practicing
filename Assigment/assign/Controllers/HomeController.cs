using DataLayer.Models;
using DataLayer.DataContext;
using DataLayer.ViewModel;
using DataLayer.ViewModels;
using LogicLayer.Interface_patient;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace assign.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HellodocPrjContext _context;
        private readonly IPatientRequest _PatientRequest;

        public HomeController(ILogger<HomeController> logger, HellodocPrjContext context, IPatientRequest patientRequest)
        {
            _logger = logger;
            _context = context;
            _PatientRequest = patientRequest;
        }

        public IActionResult Index()
        {


            return View();

        }


        public IActionResult usertable(string search, int page, int pageSize)
        {
            uservm uservm = new uservm();
            uservm = _PatientRequest.getUserDat(search);
            uservm.paginatedRequest = uservm.user.Skip((page - 1) * pageSize).Take(pageSize);
            uservm.CurrentPage = page;
            uservm.PageSize = pageSize;
            uservm.TotalPages = Math.Ceiling((double)uservm.user.Count / pageSize);
            uservm.total = uservm.user.Count;
            return View(uservm);
        }
        public IActionResult Model(int id)
        {
            uservm uservm = new uservm();
            if (id != 0) { uservm = _PatientRequest.getUser(id); }

            uservm.Citylist = _PatientRequest.getcity();
            return PartialView("_model", uservm);
        }
        public IActionResult CreateUser(uservm model)
        {
            try
            {
                if (model.id != 0)
                {
                    _PatientRequest.Updateuser(model);
                }
                _PatientRequest.Adduser(model);
                TempData["success"] = "User Created";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");

            }



        }
        public IActionResult deleteuser(int userid)
        {
            try
            {
                _PatientRequest.Userdelete(userid);
                TempData["success"] = "Deleted";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                TempData["error"] = "Error";
                return RedirectToAction("Index");
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