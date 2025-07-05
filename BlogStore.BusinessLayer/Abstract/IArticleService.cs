﻿using BlogStore.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.BusinessLayer.Abstract
{
	public interface IArticleService: IGenericService<Article>
	{
		public List<Article> TGetArticlesWithCategories();
		public AppUser TGetAppUserByArticleId(int id);
		public List<Article> TGetTop3PopularArticles();
		public List<Article> TGetArticlesByAppUser(string id);
		public Article TGetArticleWithUser(int id); // Giriş yapan kullanıcının makalelerini getiriyoruz
        public List<Article> TGetArticlesByCategory(int categoryId);
        public Article TGetArticleBySlug(string slug);
        public List<AppUser> TGetAllAuthorsWithArticles();

    }
}
