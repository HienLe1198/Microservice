using Catalog.Microservice.Data.Interface;
using Catalog.Microservice.Entities;
using MongoDB.Driver;
using Product.Microservice.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Microservice.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Item>> GetProducts()
        {
            return await _context
                            .Products
                            .Find(p => true)
                            .ToListAsync();
        }

        public async Task<Item> GetProduct(string id)
        {
            return await _context
                           .Products
                           .Find(p => p.Id == id)
                           .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetProductByName(string name)
        {
            FilterDefinition<Item> filter = Builders<Item>.Filter.ElemMatch(p => p.Name, name);

            return await _context
                            .Products
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Item>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Item> filter = Builders<Item>.Filter.Eq(p => p.Category, categoryName);

            return await _context
                            .Products
                            .Find(filter)
                            .ToListAsync();
        }


        public async Task CreateProduct(Item product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> UpdateProduct(Item product)
        {
            var updateResult = await _context
                                        .Products
                                        .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Item> filter = Builders<Item>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                                .Products
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}
