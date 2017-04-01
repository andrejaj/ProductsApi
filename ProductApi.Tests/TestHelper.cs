using ProductsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
	public class TestHelper
	{
		#region Product Helpers

		public static Product GetProductModel()
		{
			return new Product()
			{
				Name = Faker.Name.FullName(),
				Quantity = Faker.RandomNumber.Next(1, 100)
			};
		}

		public static Product GetProductUpdateModel(string name)
		{
			return new Product()
			{
				Name = name,
				Quantity = Faker.RandomNumber.Next(1, 100)
			};
		}

		#endregion
	}
}
