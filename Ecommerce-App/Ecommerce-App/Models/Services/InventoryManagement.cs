using Ecommerce_App.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Services
{
    public class InventoryManagement : IProductsService
    {
        public Task<Product> CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Collection<Product> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetSingleProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateProduct(int id, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
