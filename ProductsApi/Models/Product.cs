using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsApi.Models
{
	public class Product
	{
		public readonly string Id;
		public string Name { get; set; }
		public int Quantity { get; set; }

		public Product()
		{
			this.Id = "prod_" + Guid.NewGuid().ToString();
		}
	}
}