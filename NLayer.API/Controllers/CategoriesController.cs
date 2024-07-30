using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
	
	public class CategoriesController : CustomBaseController
	{
		private readonly ICategoryService _categoryService;
		private readonly IMapper _mapper;
        public CategoriesController(ICategoryService categoryService, IService<Category> service, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
		[HttpGet]
        public async Task<IActionResult> GetAll() 
		{
			var categories =await _categoryService.GetAllAsync();
			var categoriesDto = _mapper.Map<List<Category>>(categories.ToList());


            return CreateActionResult(CustomResponseDto<List<Category>>.Success(200,categoriesDto));
		}

		[HttpGet("[action]/{categoryId}")]
		public async Task<IActionResult> GetSingleCategoryByIdWithProductsAsync(int categoryId) 
		{
			return CreateActionResult(await _categoryService.GetSingleCategoryByIdWithProductsAsync(categoryId));
		}

	}
}
