﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Repositories
{
	public interface IProductRepıository:IGenericRepository<Product>
	{

		Task<List<Product>> GetProductsWithCategory();
	}
}
