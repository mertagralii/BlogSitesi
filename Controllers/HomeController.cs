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

        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Yazarlar")]
        public IActionResult Authors()
        {
            return View();
        }
        [Route("/Kategoriler")]
        public IActionResult Categories() 
        {
            return View();
        }

        [Route("/Editor")]
        public IActionResult Editor() 
        {
            return View(); 
        }



    }
}
