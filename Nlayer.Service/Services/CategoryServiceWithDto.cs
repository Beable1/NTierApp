using AutoMapper;
using Microsoft.AspNetCore.Http;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class CategoryServiceWithDto : ServiceWithDto<Category, CategoryDto>, ICategoryServiceWithDto
    {
        private readonly ICategoryRepository _categoryRepository;


        public CategoryServiceWithDto(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, IMapper mapper, ICategoryRepository categoryRepository) : base(repository, unitOfWork, mapper)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CustomResponseDto<CategoryWithProductsDto>> GetSingleCategoryByIdWithProductsAsync(int categoryId)
        {
            var category = await _categoryRepository.GetSingleCategoryByIdWithProductsAsync(categoryId);
            var categoryDto = _mapper.Map<CategoryWithProductsDto>(category);
            return CustomResponseDto<CategoryWithProductsDto>.Success(200, categoryDto);
        }


       

    }   
}
