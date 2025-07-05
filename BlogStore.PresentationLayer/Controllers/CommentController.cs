using BlogStore.BusinessLayer.Abstract;
using BlogStore.EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.Controllers
{
   // [Route("[controller]/[action]/{id}")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IToxicityDetectionService _toxicityDetectionService;
        private readonly ITranslationService _translationService;

        public CommentController(ICommentService commentService, UserManager<AppUser> userManager, IToxicityDetectionService toxicityDetectionService, ITranslationService translationService)
        {
            _commentService = commentService;
            _userManager = userManager;
            _toxicityDetectionService = toxicityDetectionService;
            _translationService = translationService;
        }

        public IActionResult CommentList()
        {
            var values = _commentService.TGetAll();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateComment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(Comment comment)
        {

            try
            {
                if (comment == null || string.IsNullOrWhiteSpace(comment.CommentDetail))
                    return Json(new { status = "error", message = "Yorum boş olamaz." });

                var translatedCommentDetail = await _translationService.TranslateToEnglishAsync(comment.CommentDetail);

                var detectionResult = await _toxicityDetectionService.DetectToxicityAsync(translatedCommentDetail);
                comment.IsToxic = detectionResult.IsToxic;
                comment.ToxicityScore = (float)detectionResult.Score;
                comment.AppUserId = _userManager.GetUserId(User);
                comment.UserNameSurname = _userManager.GetUserName(User);
                comment.CommentDate = DateTime.Now;
                _commentService.TInsert(comment);
                if (detectionResult.IsToxic)
                    return Json(new { status = "toxic", message = "Yorumunuz toksik içerik barındırıyor." });
                return Json(new { status = "success", message = "Yorumunuz başarıyla eklendi." });

            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = $"Bir hata oluştu: {ex.Message}" });
            }


        }

        public IActionResult DeleteComment(int id)
        {
            _commentService.TDelete(id);
            return RedirectToAction("CommentList");
        }
        [HttpGet]
        public IActionResult UpdateComment(int id)
        {
            var value = _commentService.TGetById(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateComment(Comment comment)
        {
            comment.CommentDate = DateTime.Now;
            comment.IsValid = false; // Assu
            _commentService.TUpdate(comment);
            return RedirectToAction("CommentList");
        }
        [HttpPost]
        [Authorize] // Sadece login olan kullanıcılar
        public async Task<IActionResult> AddCommentAjax(int articleId, string commentDetail)
        {
            try
            {
                if (string.IsNullOrEmpty(commentDetail) || commentDetail.Length < 3)
                {
                    return Json(new { success = false, message = "Yorum en az 3 karakter olmalıdır." });
                }

                // Login olan kullanıcıyı al
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                if (user == null)
                {
                    return Json(new { success = false, message = "Kullanıcı bulunamadı." });
                }

                // Yeni yorum oluştur
                var comment = new Comment
                {
                    ArticleId = articleId,
                    CommentDetail = commentDetail,
                    CommentDate = DateTime.Now,
                    AppUserId = user.Id,
                    UserNameSurname = user.Name + " " + user.Surname,
                    IsValid = true // Login olan kullanıcılar için otomatik onay
                };

                _commentService.TInsert(comment);

                // Başarılı response döndür
                return Json(new
                {
                    success = true,
                    message = "Yorumunuz başarıyla eklendi!",
                    comment = new
                    {
                        userNameSurname = comment.UserNameSurname,
                        commentDetail = comment.CommentDetail,
                        commentDate = comment.CommentDate.ToString("dd-MMM-yyyy"),
                        userImageUrl = user.ImageUrl
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Bir hata oluştu: " + ex.Message });
            }
        }
    }
    
}
