using BlogStore.BusinessLayer.Abstract;
using BlogStore.EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogStore.PresentationLayer.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<AppUser> _userManager;

        public AuthorController(IArticleService articleService, ICategoryService categoryService, UserManager<AppUser> userManager)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _userManager = userManager;
        }

        public async Task<IActionResult> GetProfile()
        {
            var userProfile = await _userManager.FindByNameAsync(User.Identity.Name);
            if (userProfile == null)
            {
                return NotFound();
            }

            var userArticles = _articleService.TGetArticlesByAppUser(userProfile.Id);

            ViewBag.ArticleCount = userArticles.Count;
            return View(userProfile);
        }
        [HttpGet]
        public IActionResult CreateArticle()
        {
            List<SelectListItem> values = (from x in _categoryService.TGetAll()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryId.ToString()
                                           }).ToList();
            ViewBag.v = values; // Kategorileri ViewBag'e atar
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateArticle(Article article)
        {
            var userProfile = await _userManager.FindByNameAsync(User.Identity.Name); // Giriş yapan kullanıcının bilgilerini alır
            article.AppUserId = userProfile.Id; // Makalenin yazarını giriş yapan kullanıcı olarak ayarlar
            article.CreatedDate = DateTime.Now; // Makale durumu aktif olarak ayarlanır
            _articleService.TInsert(article); // Makaleyi veritabanına ekler
            return RedirectToAction("Index", "Default"); // Profil sayfasına yönlendirir
        }
        public async  Task<IActionResult> MyArticleList()
        {
            var userProfile = await _userManager.FindByNameAsync(User.Identity.Name); // Giriş yapan kullanıcının makalelerini alır
            var values = _articleService.TGetArticlesByAppUser(userProfile.Id) ?? new List<Article>(); // Kullanıcının makalelerini alır
            return View(values); // Makaleleri görüntüler
        }
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var userProfile = await _userManager.FindByNameAsync(User.Identity.Name);
            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(AppUser model)
        {
            try
            {
                var userProfile = await _userManager.FindByNameAsync(User.Identity.Name);
                if (userProfile == null)
                {
                    return NotFound();
                }

                userProfile.Name = model.Name;
                userProfile.Surname = model.Surname;
                userProfile.Email = model.Email;
                userProfile.PhoneNumber = model.PhoneNumber;
                userProfile.Description = model.Description;
                userProfile.ImageUrl = model.ImageUrl;

                var result = await _userManager.UpdateAsync(userProfile);

                if (result.Succeeded)
                {
                    ViewBag.Success = "Profiliniz başarıyla güncellendi!";
                    return View(userProfile);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Bir hata oluştu: " + ex.Message;
                return View(model);
            }


        }


        public IActionResult AuthorList()
        {
            var authors = _articleService.TGetAllAuthorsWithArticles();
            return View(authors);
        }

        public async Task<IActionResult> AuthorDetail(string id)
        {
            var author = await _userManager.FindByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            var articles = _articleService.TGetArticlesByAppUser(id);
            ViewBag.AuthorName = author.Name + " " + author.Surname;
            ViewBag.AuthorDescription = author.Description;
            ViewBag.AuthorImage = author.ImageUrl;
            ViewBag.ArticleCount = articles.Count;
            return View(articles);
        }
    }
}
