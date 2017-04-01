using ProductsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductsApi.Repository
{
    public interface IProductRepository
    {
        void Add(Product product);

        Product Find(string name);

        void Update(Product product);

        void Remove(string name);

        IEnumerable<Product> GetAll();
    }

    public class ProductRepository : IProductRepository
    {
        protected IDictionary<string, Product> products;

        public ProductRepository()
        {
            products = new Dictionary<string, Product>();
        }

        public void Add(Product product)
        {
            products.Add(product.Name.ToLower(), product);
        }

        public Product Find(string name)
        {
            Product product = null;
            this.products.TryGetValue(name.ToLower(), out product);
            return product;
        }

        public void Update(Product product)
        {
            products[product.Name.ToLower()] = product;
        }

        public void Remove(string name)
        {
            products.Remove(name.ToLower());
        }

        public IEnumerable<Product> GetAll()
        {
            return products.Select(x => x.Value).ToList();
        }
    }
}