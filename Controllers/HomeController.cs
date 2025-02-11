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
        public IActionResult Index() // Ana Sayfa Kýsmý
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

       
        public IActionResult Details(int Id) // Blog Detay Kýsmý
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

        public IActionResult AuthorDetails(int Id)  // Yazar Detay Kýsmý
        {
            var authorDetail = _connection.QuerySingleOrDefault<TBLAuthorsModel>("SELECT * FROM TBLAuthors WHERE Id = @Id", new { Id });
            var authorsPost = _connection.Query<IndexViewModel>
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
                      WHERE b.IsIndex = 1 AND b.IsApproved = 1 AND b.IsDeleted = 0 AND b.AuthorsId = @Id", new {Id}
                ).ToList();

            var model = new AuthorsDetailsViewModel()
            {
                Authors = authorDetail,
                Post = authorsPost,
            };

            return View(model);
        }

        [Route("/Yazarlar")]
        public IActionResult Authors() // Yazarlar Kýsmý
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
        public IActionResult Categories()  // Kategoriler Kýsmý
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
        public IActionResult Editor()  // Blog Yazma Kýsmý
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
        public IActionResult AddPost(TBLBlogModel post) //[POST] Blog Yazma Kýsmý
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
        public IActionResult AddCategory() //  Kategori Ekleme Kýsmý
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult AddCategory(TBLCategoryModel category)  // [POST] Kategori Ekleme Kýsmý
        {
            var addCategory = _connection.Execute(
                @"INSERT INTO TBLCategory
                  (CategoryName) VALUES (@CategoryName)", new
                {
                    @CategoryName = category.CategoryName,
                }
            );
            return RedirectToAction("Categories");
        }

        [HttpGet]

        public IActionResult AddAuthors() // Yazar Ekleme Kýsmý
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult AddAuthors(TBLAuthorsModel authors) // [POST] Yazar Ekleme Kýsmý
        {
            var date = DateTime.Now;
            var addAuthor = _connection.Execute(
                                                @"INSERT INTO TBLAuthors
                                                  (Name,SurName,Age,Birthday,Birthplace,ImageURL,CreatedDate,Description,Summary)
                                                  VALUES (@Name,@SurName,@Age,@Birthday,@Birthplace,@ImageURL,@CreatedDate,@Description,@Summary)", new
                                                {
                                                    Name = authors.Name,
                                                    SurName = authors.SurName,
                                                    Age = authors.Age,
                                                    Birthday = authors.Birthday,
                                                    Birthplace = authors.Birthplace,
                                                    ImageURL = authors.ImageURL,
                                                    CreatedDate = date,
                                                    Description = authors.Description,
                                                    Summary = authors.Summary,

                                                }
                                               );
            return RedirectToAction("Authors");
        }

        [HttpGet]
        public IActionResult Approval() // Onaylama Kýsmý
        {
            var approvalList = _connection.Query<IndexViewModel>
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
                      WHERE (b.IsApproved = 0 OR b.IsIndex = 0) AND b.IsDeleted = 0 "
                ).ToList();
           

           
            return View(approvalList);
        }
        
        public IActionResult ApprovalTrue(int Id) // Blog Yazýsýna Onay verme
        {
            var approvalTrue = _connection.Execute
                                                  (
                                                    @"UPDATE TBLBlog
                                                     SET
                                                     IsApproved = 1
                                                     Where Id = @Id", new {Id}
                                                  );
            return RedirectToAction("Approval");
        }
        
        public IActionResult IsIndexTrue(int Id) // Blog Yazýsýný Öne Çýkarma
        {
            var isIndexTrue = _connection.Execute
                                                  (
                                                    @"UPDATE TBLBlog
                                                     SET
                                                     IsIndex = 1
                                                     Where Id = @Id", new { Id }
                                                  );
            return RedirectToAction("Approval");
        }

        public IActionResult BlogDelete(int Id)  // Blog Yazýsýný Silme
        {
            var blogDelete = _connection.Execute
                                                (
                                                  @"UPDATE TBLBlog
                                                    SET
                                                    IsDeleted = 1
                                                    WHERE Id = @Id",new {Id}
                                                );
            return RedirectToAction("Approval"); 
        }

        [HttpGet]
        public IActionResult AuthorsUpdate(int Id) 
        {
            var updateAuthorsCheck = _connection.QuerySingleOrDefault<TBLAuthorsModel>("SELECT * FROM TBLAuthors WHERE Id = @Id", new {Id});
             return View(updateAuthorsCheck);
        }
        [HttpPost]
        public IActionResult AuthorsUpdate(TBLAuthorsModel author)
        {
            var UpdateDate = DateTime.Now;
            var updateAuthors = _connection.Execute(
                                                    @"UPDATE TBLAuthors
                                                     SET
                                                      Name = @Name,
                                                      SurName = @SurName,
                                                      Age = @Age,
                                                      Birthday = @Birthday,
                                                      Birthplace = @Birthplace,
                                                      ImageURL = @ImageURL,
                                                      Description = @Description,
                                                      Summary = @Summary,
                                                      UpdateDate = @UpdateDate
                                                      WHERE Id = @Id
                                                    ", new {
                                                        Name = author.Name,
                                                        SurName = author.SurName,
                                                        Age = author.Age,
                                                        Birthday = author.Birthday,
                                                        Birthplace = author.Birthplace,
                                                        ImageURL = author.ImageURL,
                                                        Description = author.Description,
                                                        Summary = author.Summary,
                                                        UpdateDate = UpdateDate,
                                                        Id = author.Id
                                                    }
                                                   );
            return RedirectToAction("Authors");
        }


    }
}
