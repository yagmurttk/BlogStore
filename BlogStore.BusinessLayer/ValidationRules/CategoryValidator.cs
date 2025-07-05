using BlogStore.EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.BusinessLayer.ValidationRules
{
	public class CategoryValidator:AbstractValidator<Category>
	{
		public CategoryValidator()
		{
			RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Kategori Adı boş geçilemez");
			RuleFor(x => x.CategoryName).MinimumLength(3).WithMessage("Lütfen en az 3 karakter veri girişi yapınız");
			RuleFor(x => x.CategoryName).MaximumLength(50).WithMessage("Lütfen en fazla 50 karakter veri girişi yapınız");
			
		}
	}
}
