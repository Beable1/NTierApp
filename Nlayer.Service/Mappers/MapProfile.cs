﻿using AutoMapper;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Mappers
{
	public class MapProfile:Profile
	{
        public MapProfile()
        {
			CreateMap<Product, ProductDto>().ReverseMap();
			CreateMap<Category, CategoryDto>().ReverseMap();
			CreateMap<ProductFeature, ProductFeatureDto>().ReverseMap();
			CreateMap<ProductUpdateDto, Product>();
			CreateMap<Product, ProductWithCategoryDto>().ReverseMap();
			CreateMap<Category,CategoryWithProductsDto>().ReverseMap();
		}
    }
}
