using System.Data;
using System.Diagnostics;
using BlogSitesi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogSitesi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDbConnection _connection;

        public HomeController(IDbConnection connection)
        {
            _connection = connection;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Authors()
        {
            return View();
        }

        public IActionResult Categories() 
        {
            return View();
        }

        public IActionResult Editor() 
        {
            return View(); 
        }



    }
}
