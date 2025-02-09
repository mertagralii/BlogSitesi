using System;
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
                   @"SELECT
                        b.Id AS BlogId,  
                        b.Title, 
                        b.Summary, 
                        b.Description, 
                        b.CreatedDate, 
                        b.UpdateDate, 
                        b.IsDeleted, 
                        b.IsApproved, 
                        b.IsIndex,
                        c.Id AS CategoryId,
                        c.CategoryName,
                        a.Id AS AuthorId,
                        a.Name,
                        a.SurName
                        FROM TBLBlog b
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
                    @"SELECT
                        b.Id AS BlogId,  
                        b.Title, 
                        b.Summary, 
                        b.Description, 
                        b.CreatedDate, 
                        b.UpdateDate, 
                        b.IsDeleted, 
                        b.IsApproved, 
                        b.IsIndex,
                        c.Id AS CategoryId,
                        c.CategoryName,
                        a.Id AS AuthorId,
                        a.Name,
                        a.SurName
                        FROM TBLBlog b
                      LEFT JOIN TBLCategory c ON b.CategoryId = c.Id
                      LEFT JOIN TBLAuthors a ON b.AuthorsId = a.Id
                      WHERE b.Id=@Id", new {Id}
                );

            return View(blogDetails); 
        }

        public IActionResult AuthorDetails(int Id) 
        {
            var authorDetail = _connection.QuerySingleOrDefault<TBLAuthorsModel>("SELECT * FROM TBLAuthors WHERE Id = @Id", new {Id});

            return View(authorDetail);
        }

        [Route("/Yazarlar")]
        public IActionResult Authors()
        {
            var authorsListAndPostCount = _connection.Query<AuthorsViewModel>
            (
             @"SELECT
              a.Id AS AuthorId,
              a.Name,
              a.SurName,
              COUNT(b.Id) AS PostCount
              FROM TBLAuthors a
             LEFT JOIN TBLBlog b ON a.Id = b.AuthorsId 
             GROUP BY a.Id, a.Name, a.SurName
             ORDER BY a.Name;"
            ).ToList();

            return View(authorsListAndPostCount);
        }
        [Route("/Kategoriler")]
        public IActionResult Categories() 
        {
            var categoryListAndPostCount = _connection.Query<CategoryViewModel>
                (
                    @"SELECT 
                    c.Id AS CategoryId,
                    c.CategoryName,
                    COUNT(b.Id) AS BlogCount
                    FROM TBLCategory c
                    LEFT JOIN TBLBlog b ON c.Id = b.CategoryId
                    GROUP BY c.Id, c.CategoryName
                    ORDER BY c.CategoryName;"
                ).ToList();
            return View(categoryListAndPostCount);
        }

        [Route("/Editor")]
        public IActionResult Editor() 
        {
            var categoryList = _connection.Query<TBLCategoryModel>("SELECT * FROM TBLCategory").ToList();
            var authorsList = _connection.Query<TBLAuthorsModel>("SELECT * FROM TBLAuthors").ToList();
            var model = new EditorViewModel()
            {
                Authors = authorsList,
                Category = categoryList
            };
            return View(model); 
        }

        [HttpPost]
        public IActionResult AddPost(TBLBlogModel post)
        {
            var date = DateTime.Now;
            var addPost = _connection.Execute
                (
                 @"INSERT INTO TBLBlog
                   (Title, Summary, Description, CreatedDate, AuthorsId, CategoryId, IsDeleted, IsApproved, IsIndex) 
                   VALUES (@Title, @Summary,@Description,@CreatedDate,@AuthorsId,@CategoryId,@IsDeleted,@IsApproved,@IsIndex);
                  "
                , new
                {
                    Title = post.Title,
                    Summary = post.Summary,
                    Description = post.Description,
                    CreatedDate = date,
                    AuthorsId = post.AuthorsId,
                    CategoryId = post.CategoryId,
                    IsDeleted = post.IsDeleted,
                    IsApproved = post.IsApproved,
                    IsIndex = post.IsIndex,
                });
            return RedirectToAction("Index"); 
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult AddCategory(TBLCategoryModel category) 
        {
            return View(); 
        }

        [HttpGet]

        public IActionResult AddAuthors() 
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult AddAuthors(TBLAuthorsModel authors) 
        {
            return View(); 
        }



    }
}
