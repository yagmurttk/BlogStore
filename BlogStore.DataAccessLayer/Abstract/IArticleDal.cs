﻿using BlogStore.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.DataAccessLayer.Abstract
{
    public interface IArticleDal: IGenericDal<Article>
    {
        List<Article> GetArticlesWithCategories();
        public AppUser GetAppUserByArticleId(int id);
         List<Article> GetTop3PopularArticles();
        List<Article> GetArticlesByAppUser(string id);
        public Article GetArticleWithUser(int id); // Giriş yapan kullanıcının makalelerini getiriyoruz
        public List<Article> GetArticlesByCategory(int categoryId);
        public Article GetArticleBySlug(string slug);
        public List<AppUser> GetAllAuthorsWithArticles();
    }
}
