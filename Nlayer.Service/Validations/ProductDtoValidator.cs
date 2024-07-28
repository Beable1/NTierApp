using FluentValidation;
using NLayer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Validations
{
	public class ProductDtoValidator:AbstractValidator<ProductDto>
	{
		public ProductDtoValidator()
		{
			RuleFor(x => x.Name).NotNull().WithMessage("{propertyName} is required").NotEmpty().WithMessage("{propertyName} is required");

			RuleFor(x=>x.Price).InclusiveBetween(1,int.MaxValue).WithMessage("{propertyName} must be greater than 0");
			RuleFor(x => x.Stock).InclusiveBetween(1, int.MaxValue).WithMessage("{propertyName} must be greater than 0");
			RuleFor(x => x.Id).InclusiveBetween(1, int.MaxValue).WithMessage("{propertyName} must be greater than 0");
		}
	}
}
