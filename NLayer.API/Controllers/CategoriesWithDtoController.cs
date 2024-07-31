using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Services;
using NLayer.Service.Services;

namespace NLayer.API.Controllers
{
    public class CategoriesWithDtoController : CustomBaseController
    {

        private readonly ICategoryServiceWithDto _categoryServiceWithDto;

        public CategoriesWithDtoController(ICategoryServiceWithDto categoryServiceWithDto)
        {
            _categoryServiceWithDto = categoryServiceWithDto;
        }



        [HttpGet]
        public async Task<IActionResult> All()
        {

            return CreateActionResult(await _categoryServiceWithDto.GetAllAsync());
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSingleCategoryByIdWithProductsAsync(int id)
        {

            return CreateActionResult(await _categoryServiceWithDto.GetSingleCategoryByIdWithProductsAsync(id));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            return CreateActionResult(await _categoryServiceWithDto.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryDto categoryDto)
        {

            return CreateActionResult(await _categoryServiceWithDto.AddAsync(categoryDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {


            return CreateActionResult(await _categoryServiceWithDto.UpdateAsync(categoryDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {

            return CreateActionResult(await _categoryServiceWithDto.RemoveAsync(id));
        }


        [HttpDelete("RemoveAll")]
        public async Task<IActionResult> RemoveAll(List<int> ids)
        {
            return CreateActionResult(await _categoryServiceWithDto.RemoveRangeAsync(ids));
        }


        [HttpDelete("Any/{id}")]
        public async Task<IActionResult> Any(int id)
        {
            return CreateActionResult(await _categoryServiceWithDto.AnyAsync(x => x.Id == id));
        }

    }
}
