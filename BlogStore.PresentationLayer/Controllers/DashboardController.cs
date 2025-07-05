using BlogStore.BusinessLayer.Abstract;
using BlogStore.EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICommentService _commentService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<AppUser> _userManager;

        public DashboardController(IArticleService articleService, ICommentService commentService, ICategoryService categoryService, UserManager<AppUser> userManager)
        {
            _articleService = articleService;
            _commentService = commentService;
            _categoryService = categoryService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var categoryValue = _categoryService.TGetCategoryWithArticleCount();
            ViewBag.CategoryData = categoryValue.Select(x => new {
                name = x.CategoryName,
                value = x.ArticleCount
            }).ToList();

            // 2. En çok blog yazan yazarlar
            var allAuthors = _articleService.TGetAllAuthorsWithArticles();
            var topAuthors = allAuthors
                .Select(author => new {
                    User = author,
                    ArticleCount = author.Articles?.Count ?? 0
                })
                .Where(x => x.ArticleCount > 0)
                .OrderByDescending(x => x.ArticleCount)
                .Take(5)
                .ToList();
            ViewBag.TopAuthors = topAuthors;


            return View();
        }
    }
}
