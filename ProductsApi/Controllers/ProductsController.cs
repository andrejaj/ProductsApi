using ProductsApi.Models;
using ProductsApi.Repository;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ProductsApi.Controllers
{
    internal class Response
    {
        public readonly string Message;

        public Response(string message)
        {
            if(message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            this.Message = message;
        }
    }

	public class ProductsController : ApiController
	{
		protected IProductRepository _productRepository;

		public ProductsController(IProductRepository productRepository)
		{
			if (productRepository == null)
			{
				throw new ArgumentNullException(nameof(productRepository));
			}

			this._productRepository = productRepository;
		}

		[Route("api/products")]
		[HttpPost]
		public IHttpActionResult Create([FromBody] Product product)
		{
			if (product == null)
			{
				return BadRequest();
			}

			this._productRepository.Add(product);
			return Ok(product);
		}

		[Route("api/products")]
		[HttpPut]
		public IHttpActionResult Update([FromBody] Product product)
		{
			if (product == null)
			{
				return BadRequest();
			}

			var found = _productRepository.Find(product.Name);
			if (found == null)
			{
				return NotFound();
			}

			_productRepository.Update(product);
			return Ok(new Response("Ok"));
		}

		[Route("api/products/{name}")]
		[HttpDelete]
		public IHttpActionResult Delete(string name)
		{
			var product = _productRepository.Find(name);
			if (product == null)
			{
				return NotFound();
			}

			_productRepository.Remove(name);
			return Ok(product);
		}

		[Route("api/products")]
		[HttpGet]
		public IEnumerable<Product> GetAll()
		{
			return _productRepository.GetAll();
		}

		[Route("api/products/{name}")]
		[HttpGet]
		public IHttpActionResult Get(string name)
		{
			var product = this._productRepository.Find(name);
			if (product == null)
			{
				return NotFound();
			}

			return Ok(product);
		}
	}
}
