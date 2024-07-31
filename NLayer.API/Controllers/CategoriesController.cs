using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Service.Services;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return CreateActionResult(CustomResponseDto<CategoryDto>.Success(200, categoryDto));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto _categoryDto)
        {
            

            var category=await _categoryService.AddAsync(_mapper.Map<Category>(_categoryDto));
            var categoryDto = _mapper.Map<CategoryDto>(category);



            return CreateActionResult(CustomResponseDto<CategoryDto>.Success(201, categoryDto));
        }


        [HttpPut]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
            await _categoryService.UpdateAsync(_mapper.Map<Category>(categoryDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _categoryService.GetByIdAsync(id);
            await _categoryService.RemoveAsync(product);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

    }
}
