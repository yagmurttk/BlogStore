using BlogStore.DataAccessLayer.Abstract;
using BlogStore.DataAccessLayer.Context;
using BlogStore.DataAccessLayer.Dtos;
using BlogStore.DataAccessLayer.Repositories;
using BlogStore.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.DataAccessLayer.EntityFramework
{
	public class EfCategoryDal:GenericRepository<Category>, ICategoryDal
	{
        private readonly BlogContext _context;
		public EfCategoryDal(BlogContext context) : base(context)
		{
            _context = context;
		}

        public List<GetCategoryWithArticleCountDto> GetCategoryWithArticleCount()
        {
            var result=_context.Categories
                .Select(c => new GetCategoryWithArticleCountDto
                {
                    CategoryId=c.CategoryId,
                    CategoryName = c.CategoryName,
                    ArticleCount = _context.Articles.Count(a=>a.CategoryId==c.CategoryId)
                }).ToList();
            return result;
        }
    }
}
