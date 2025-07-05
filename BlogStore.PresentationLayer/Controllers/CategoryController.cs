using BlogStore.BusinessLayer.Abstract;
using BlogStore.EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ICategoryService _categoryService;
		private readonly IArticleService _articleService;

        public CategoryController(ICategoryService categoryService, IArticleService articleService)
        {
            _categoryService = categoryService;
            _articleService = articleService;
        }

        public IActionResult CategoryList()
		{
            var categories = _categoryService.TGetCategoryWithArticleCount();
            return View(categories);
        }
		[HttpGet]
		public IActionResult CreateCategory()
		{
			return View();
		}
		[HttpPost]
		public IActionResult CreateCategory(Category category) //Solid ezildi Dto kullanılmalı (belirli entitileri almak için
		{
			
				_categoryService.TInsert(category);
				return RedirectToAction("CategoryList");
			
			
		}
		public IActionResult DeleteCategory(int id)
		{
			_categoryService.TDelete(id);
			return RedirectToAction("CategoryList");
		}
		[HttpGet]
		public IActionResult UpdateCategory(int id)
		{
			var value = _categoryService.TGetById(id);
			return View(value);
		}
		[HttpPost]
		public IActionResult UpdateCategory(Category category)
		{
			_categoryService.TUpdate(category);
			return RedirectToAction("CategoryList");
		}
        public IActionResult CategoryDetail(int id)
        {
            var category = _categoryService.TGetById(id);
            if (category == null)
            {
                return NotFound();
            }

            var articles = _articleService.TGetArticlesByCategory(id);

            ViewBag.CategoryName = category.CategoryName;
            ViewBag.CategoryDescription = category.CategoryDescription;
            ViewBag.ArticleCount = articles.Count;

            return View(articles);
        }
    }
}
