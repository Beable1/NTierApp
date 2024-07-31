using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Service.Services;
using NLayer.Web.Services;

namespace NLayer.Web.Controllers
{
    public class CategoriesController : Controller
    {

        private readonly CategoryApiService _categoryService;
        

        public CategoriesController(CategoryApiService categoryService)
        {
            _categoryService = categoryService;
            
        }

        public async Task<IActionResult> Index()
        {
            var categoriesDto = await _categoryService.GetAllAsync();
            

            return View(categoriesDto);
        }


        public IActionResult Create()
        {
           

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {

                await _categoryService.CreateAsync(categoryDto);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public async Task<IActionResult> Remove(int id)
        {

            await _categoryService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }



        [ServiceFilter(typeof(NotFoundFilter<Category>))]
        public async Task<IActionResult> Update(int id)
        {
            var categoryDto = await _categoryService.GetByIdAsync(id);

           

            return View(categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {

            if (ModelState.IsValid)
            {

                await _categoryService.Update(categoryDto);
                return RedirectToAction(nameof(Index));
            }





            return View(categoryDto);
        }
    }
}
