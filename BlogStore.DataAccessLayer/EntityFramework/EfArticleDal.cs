using BlogStore.DataAccessLayer.Abstract;
using BlogStore.DataAccessLayer.Context;
using BlogStore.DataAccessLayer.Repositories;
using BlogStore.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.DataAccessLayer.EntityFramework
{
    public class EfArticleDal : GenericRepository<Article>, IArticleDal
    {
        private readonly BlogContext _context;
        public EfArticleDal(BlogContext context) : base(context)
        {
            _context = context; 
        }

        public List<AppUser> GetAllAuthorsWithArticles()
        {
            return _context.Users
                .Where(u => u.Articles.Any())
                .Include(u => u.Articles)
                .ToList();
        }

        public AppUser GetAppUserByArticleId(int id)
        {
            string userId = _context.Articles.Where(x => x.ArticleId == id).Select(y => y.AppUserId).FirstOrDefault();
            var Uservalue = _context.Users.Where(x => x.Id == userId).FirstOrDefault(); // AppUserId ile AppUser tablosundan kullanıcıyı getiriyoruz
            return Uservalue; // AppUser nesnesini döndürüyoruz
        }

        public Article GetArticleBySlug(string slug)
        {
            return _context.Articles
.Include(x => x.AppUser)
.Include(x => x.Category)
.FirstOrDefault(x => x.Slug == slug);
        }

        public List<Article> GetArticlesByAppUser(string id)
        {
            return _context.Articles
         .Include(x => x.Category)
         .Include(x => x.AppUser)
         .Where(x => x.AppUserId == id)
         .ToList(); // Giriş yapan kullanıcının makalelerini getiriyoruz
        }

        public List<Article> GetArticlesByCategory(int categoryId)
        {
            return _context.Articles
     .Include(x => x.AppUser)
     .Include(x => x.Category)
     .Where(x => x.CategoryId == categoryId)
     .ToList();
        }

        public List<Article> GetArticlesWithCategories()
        {
            return _context.Articles.Include(x => x.Category).ToList();  // ilişkili tablo
        }

        public Article GetArticleWithUser(int id)
        {
            return _context.Articles.Include(x => x.AppUser).FirstOrDefault(x => x.ArticleId == id); // Giriş yapan kullanıcının makalelerini getiriyoruz
        }

        public List<Article> GetTop3PopularArticles()
        {
            var values = _context.Articles.OrderByDescending(x => x.ArticleId).Take(3).ToList();
            return values;
        }
    }
}
