using BlogStore.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [Route("Article/ArticleDetail/{slug}")]
        public IActionResult ArticleDetail(string slug)
        {
            var article = _articleService.TGetArticleBySlug(slug);

            if (article == null)
            {
                return NotFound();
            }
            //article.ViewCount++;
            //_articleService.TUpdate(article);
            ViewBag.i = article.ArticleId;
            return View(article);
        }
    }
}