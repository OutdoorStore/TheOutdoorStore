using Ecommerce_App.Data;
using Ecommerce_App.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Services
{
    public class InventoryManagement : IProductsService
    {
        private StoreDbContext _storeDbContext;

        public InventoryManagement(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }
        public async Task<Product> CreateProduct(Product product)
        {
            _storeDbContext.Entry(product).State = EntityState.Added;
            await _storeDbContext.SaveChangesAsync();

            return product;
        }

        public async Task DeleteProduct(int id)
        {
            Product product = await _storeDbContext.Products.FindAsync(id);

            if (product == null)
            {
                return;
            }
            else
            {
                _storeDbContext.Entry(product).State = EntityState.Deleted;
                await _storeDbContext.SaveChangesAsync();
            }
        }

        public async Task<Product> GetSingleProduct(int id)
        {
            Product product = await _storeDbContext.Products.FindAsync(id);

            if (product == null)
            {
                return null;
            }
            else
            {
                return product;
            }
        }

        public async Task<Product> UpdateProduct(int id, Product product)
        {
            if (product == null)
            {
                return null;
            }
            else
            {
                _storeDbContext.Entry(product).State = EntityState.Modified;

                await _storeDbContext.SaveChangesAsync();

                return product;
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _storeDbContext.Products.ToListAsync();

            return products;
        }
    }
}
