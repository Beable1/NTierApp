using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Services;
using NLayer.Core;
using NLayer.Core.DTOs;

namespace NLayer.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : CustomBaseController
	{
		private readonly IService<Product> _service;
		private readonly IMapper _mapper;


		public ProductController(IMapper mapper, IService<Product> service)
		{
			_mapper = mapper;
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> All()
		{
			var products = await _service.GetAllAsync();
			var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());
			return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var prdouct = await _service.GetByIdAsync(id);

			var productDto = _mapper.Map<ProductDto>(prdouct);

			return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));
		}


		[HttpPost]
		public async Task<IActionResult> Save(ProductDto productDto)
		{
			var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
			var productsDto = _mapper.Map<ProductDto>(product);
			return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productsDto));
		}

		[HttpPut]
		public async Task<IActionResult> Update(ProductDto productDto)
		{
			await _service.UpdateAsync(_mapper.Map<Product>(productDto));
			
			return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Remove(int id)
		{
			var product = await _service.GetByIdAsync(id);
			await _service.RemoveAsync(product);

			return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
		}

	}
}
