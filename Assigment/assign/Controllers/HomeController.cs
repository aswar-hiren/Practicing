using assign.Models;
using DataLayer.DataContext;
using DataLayer.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace assign.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HellodocPrjContext _context;

        public HomeController(ILogger<HomeController> logger,HellodocPrjContext context)
        {
            _logger = logger;
            _context = context;
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