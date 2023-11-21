using Catalog.Microservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Microservice.Repositories.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Item>> GetProducts();
        Task<Item> GetProduct(string id);
        Task<IEnumerable<Item>> GetProductByName(string name);
        Task<IEnumerable<Item>> GetProductByCategory(string categoryName);

        Task CreateProduct(Item product);
        Task<bool> UpdateProduct(Item product);
        Task<bool> DeleteProduct(string id);
    }
}
