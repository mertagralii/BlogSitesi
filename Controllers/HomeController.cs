using System.Data;
using System.Diagnostics;
using BlogSitesi.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Slugify;

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
            var blogPosts = _connection.Query<IndexViewModel>
                (
                    @"SELECT b.*,c.CategoryName,a.Name,a.SurName FROM TBLBlog b
                      LEFT JOIN TBLCategory c ON b.CategoryId = c.Id
                      LEFT JOIN TBLAuthors a ON b.AuthorsId = a.Id
                      WHERE b.IsIndex = 1 AND b.IsApproved = 1 AND b.IsDeleted = 0"
                ).ToList();
            return View(blogPosts);
        }

       
        public IActionResult Details(int Id)
        {
            var blogDetails = _connection.QuerySingleOrDefault<IndexViewModel>
                (
                    @"SELECT b.*,c.CategoryName,a.Name,a.SurName FROM TBLBlog b
                      LEFT JOIN TBLCategory c ON b.CategoryId = c.Id
                      LEFT JOIN TBLAuthors a ON b.AuthorsId = a.Id
                      WHERE b.IsIndex = 1 AND b.IsApproved = 1 AND b.IsDeleted = 0 AND b.Id=@Id", new {Id}
                );

            return View(blogDetails); 
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
