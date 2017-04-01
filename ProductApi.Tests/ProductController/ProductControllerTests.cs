using Moq;
using NUnit.Framework;
using ProductsApi.Controllers;
using ProductsApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
	[TestFixture]
	public class ProductControllerTests
	{
        private Mock<IProductRepository> _productRepositoryMock;

		[SetUp]
		public void SetUp()
		{
            this._productRepositoryMock = new Mock<IProductRepository>();
		}

		[Test]
		public void CreateProduct()
		{
            var productController = new ProductsController(this._productRepositoryMock.Object);
            var productCreateModel = TestHelper.GetProductModel();

            System.Web.Http.Results.OkNegotiatedContentResult<ProductsApi.Models.Product> result = (System.Web.Http.Results.OkNegotiatedContentResult<ProductsApi.Models.Product>)productController.Create(productCreateModel);
         
            this._productRepositoryMock.Verify(x => x.Add(productCreateModel), Times.Once);
            Assert.AreSame(productCreateModel, result.Content);
        }

		[Test]
		public void UpdateProduct()
		{
            var productController = new ProductsController(this._productRepositoryMock.Object);
            var productCreateModel = TestHelper.GetProductModel();
            productController.Create(productCreateModel);
            this._productRepositoryMock.Verify(x => x.Add(productCreateModel), Times.Once);
            var productUpdateModel = TestHelper.GetProductUpdateModel(productCreateModel.Name);
            this._productRepositoryMock.Setup(x => x.Find(productUpdateModel.Name)).Returns(productUpdateModel);

            System.Web.Http.Results.OkNegotiatedContentResult<Response> result = (System.Web.Http.Results.OkNegotiatedContentResult<Response>)productController.Update(productUpdateModel);

            this._productRepositoryMock.Verify(x => x.Update(productUpdateModel), Times.Once);
            StringAssert.Contains("Ok", result.Content.Message);
        }

		[Test]
		public void DeleteProduct()
		{
            var productController = new ProductsController(this._productRepositoryMock.Object);
            var productCreateModel = TestHelper.GetProductModel();
            productController.Create(productCreateModel);
            this._productRepositoryMock.Verify(x => x.Add(productCreateModel), Times.Once);
            this._productRepositoryMock.Setup(x => x.Find(productCreateModel.Name)).Returns(productCreateModel);

            System.Web.Http.Results.OkNegotiatedContentResult<ProductsApi.Models.Product> result = (System.Web.Http.Results.OkNegotiatedContentResult<ProductsApi.Models.Product>)productController.Delete(productCreateModel.Name);

            this._productRepositoryMock.Verify(x => x.Remove(productCreateModel.Name), Times.Once);
            Assert.AreSame(productCreateModel, result.Content);
        }

		[Test]
		public void GetAllProducts()
		{
            var productController = new ProductsController(this._productRepositoryMock.Object);
            var productCreateModel1 = TestHelper.GetProductModel();
            var productCreateModel2 = TestHelper.GetProductModel();
            var productCreateModel3 = TestHelper.GetProductModel();
            var productCreateModel4 = TestHelper.GetProductModel();
            var productList = new[] { productCreateModel1, productCreateModel2, productCreateModel3, productCreateModel4 };
            this._productRepositoryMock.Setup(x => x.GetAll())
                .Returns(productList);

            var actualList = productController.GetAll();

            this._productRepositoryMock.Verify(x => x.GetAll(), Times.Once);
            Assert.AreSame(productList, actualList);
        }

		[Test]
		public void GetProduct()
		{
            var productController = new ProductsController(this._productRepositoryMock.Object);     
            var productCreateModel = TestHelper.GetProductModel();
            productController.Create(productCreateModel);
            this._productRepositoryMock.Verify(x => x.Add(productCreateModel), Times.Once);
            this._productRepositoryMock.Setup(x => x.Find(productCreateModel.Name)).Returns(productCreateModel);

            System.Web.Http.Results.OkNegotiatedContentResult<ProductsApi.Models.Product> result = (System.Web.Http.Results.OkNegotiatedContentResult<ProductsApi.Models.Product>)productController.Get(productCreateModel.Name);

            this._productRepositoryMock.Verify(x => x.Find(productCreateModel.Name), Times.Once);
            Assert.AreSame(productCreateModel, result.Content);
        }		
	}
}
