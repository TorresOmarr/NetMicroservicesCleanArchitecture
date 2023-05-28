using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infraestructure.Data;
using MongoDB.Driver;
using System.Runtime.InteropServices;

namespace Catalog.Infraestructure.Repositories
{
    public class ProductRepository : IProductRepository, IBrandRepository, ITypesRepository
    {
        private readonly ICatalogContext _context;    
        public ProductRepository(ICatalogContext catalogContext)
        {
            _context = catalogContext;   
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context
                            .Products
                            .Find(p => true)
                            .ToListAsync();
        }
        public async Task<Product> GetProduct(string id)
        {
            var product = await _context.Products
                                        .Find(p => p.Id == id)
                                        .FirstOrDefaultAsync();
            return product;
        }
        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);
            return await _context
                            .Products
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByBrand(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Brands.Name, name);
            return await _context
                            .Products
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
            return product;
        }
        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _context
                                    .Products
                                    .ReplaceOneAsync(p => p.Id == product.Id,product);    

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;   
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            DeleteResult deleteResult = await _context
                                                .Products
                                                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
