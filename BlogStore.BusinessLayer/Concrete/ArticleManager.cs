﻿using BlogStore.BusinessLayer.Abstract;
using BlogStore.BusinessLayer.Helpers;
using BlogStore.DataAccessLayer.Abstract;
using BlogStore.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.BusinessLayer.Concrete
{
	public class ArticleManager : IArticleService
	{
		private readonly IArticleDal _articleDal;

		public ArticleManager(IArticleDal articleDal)
		{
			_articleDal = articleDal;
		}

        public AppUser TGetAppUserByArticleId(int id)
        {
            return _articleDal.GetAppUserByArticleId(id);
        }

        public void TDelete(int id)
		{
			_articleDal.Delete(id);
		}

		public List<Article> TGetAll()
		{
			return _articleDal.GetAll();
		}

        public List<Article> TGetArticlesWithCategories()
        {
            return _articleDal.GetArticlesWithCategories();
        }

        public Article TGetById(int id)
		{
			return _articleDal.GetById(id);
		}

		public void TInsert(Article entity)
		{
            if (entity.Title.Length >= 10 && entity.Title.Length <= 100 && entity.Description != "" && entity.ImageUrl.Contains("a"))
            {
                entity.Slug = SlugHelper.GenerateSlug(entity.Title);

                int counter = 1;
                string originalSlug = entity.Slug;
                while (_articleDal.GetAll().Any(x => x.Slug == entity.Slug))
                {
                    entity.Slug = $"{originalSlug}-{counter}";
                    counter++;
                }

                _articleDal.Insert(entity);
            }
            else
            {
                
            }

        }

		public void TUpdate(Article entity)
		{
            entity.Slug = SlugHelper.GenerateSlug(entity.Title);

            int counter = 1;
            string originalSlug = entity.Slug;
            while (_articleDal.GetAll().Any(x => x.Slug == entity.Slug && x.ArticleId != entity.ArticleId))
            {
                entity.Slug = $"{originalSlug}-{counter}";
                counter++;
            }

            _articleDal.Update(entity);
        }

        public List<Article> TGetTop3PopularArticles()
        {
            return _articleDal.GetTop3PopularArticles();
        }

        public List<Article> TGetArticlesByAppUser(string id)
        {
            return _articleDal.GetArticlesByAppUser(id);
        }

        public Article TGetArticleWithUser(int id)
        {
            return _articleDal.GetArticleWithUser(id);
        }

        public List<Article> TGetArticlesByCategory(int categoryId)
        {
            return _articleDal.GetArticlesByCategory(categoryId);
        }

        public Article TGetArticleBySlug(string slug)
        {
            return _articleDal.GetArticleBySlug(slug);
        }

        public List<AppUser> TGetAllAuthorsWithArticles()
        {
            return _articleDal.GetAllAuthorsWithArticles();
        }
    }
}
