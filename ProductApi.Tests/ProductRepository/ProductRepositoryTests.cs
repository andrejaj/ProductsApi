using FluentAssertions;
using NUnit.Framework;
using ProductsApi.Models;
using ProductsApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
	[TestFixture]
	public class ProductRepositoryTests
	{
		private ProductRepositoryTestable _productRepository;

		[SetUp]
		public void SetUp()
		{
			_productRepository = new ProductRepositoryTestable();
		}

		[Test]
		public void AddProduct()
		{
			var product = TestHelper.GetProductModel();
			_productRepository.Add(product);

			CollectionAssert.Contains(_productRepository.Products, product);
		}

		[Test]
		public void UpdateProduct()
		{
			var productCreateModel = TestHelper.GetProductModel();
			_productRepository.Add(productCreateModel);
			var productUpdateModel = TestHelper.GetProductUpdateModel(productCreateModel.Name);
			_productRepository.Update(productUpdateModel);

			CollectionAssert.Contains(_productRepository.Products, productUpdateModel);
		}

		[Test]
		public void DeleteProduct()
		{
			var product = TestHelper.GetProductModel();
			_productRepository.Add(product);
			CollectionAssert.Contains(_productRepository.Products, product);

			_productRepository.Remove(product.Name);
			CollectionAssert.DoesNotContain(_productRepository.Products, product);
		}

		[Test]
		public void AllProducts()
		{
			var product1 = TestHelper.GetProductModel();
			var product2 = TestHelper.GetProductModel();
			var product3 = TestHelper.GetProductModel();
			_productRepository.Add(product1);
			_productRepository.Add(product2);
			_productRepository.Add(product3);

			_productRepository.Products.Count().Should().BeGreaterOrEqualTo(3);

			_productRepository.Products[0].Name.Should().Be(product1.Name);
			_productRepository.Products[1].Name.Should().Be(product2.Name);
			_productRepository.Products[2].Name.Should().Be(product3.Name);
		}

		[Test]
		public void FindProduct()
		{
			var expectedProduct = TestHelper.GetProductModel();
			_productRepository.Add(expectedProduct);

			var actualProduct = _productRepository.Find(expectedProduct.Name);

			Assert.AreSame(expectedProduct, actualProduct);
		}
	}

	internal class ProductRepositoryTestable : ProductRepository
	{
		public Product[] Products => products.Select(x => x.Value).ToArray();
	}
}
