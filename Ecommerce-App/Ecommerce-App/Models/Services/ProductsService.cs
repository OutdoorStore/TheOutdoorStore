﻿using Ecommerce_App.Data;
using Ecommerce_App.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Services
{
    public class ProductsService : IProductsService
    {
        private StoreDbContext _storeDbContext;

        public ProductsService(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        /// <summary>
        /// Creates a new entry in the database table
        /// based on the product object parameter
        /// and returns the same object. 
        /// </summary>
        /// <param name="product">An instance of Product</param>
        /// <returns>A specific Product object</returns>
        public async Task<Product> CreateProduct(Product product)
        {
            _storeDbContext.Entry(product).State = EntityState.Added;
            await _storeDbContext.SaveChangesAsync();

            return product;
        }

        /// <summary>
        /// Deletes a product from the products database table
        /// based on the product Id parameter.
        /// </summary>
        /// <param name="id">A unique integer product Id value</param>
        /// <returns>An empty task object</returns>
        public async Task DeleteProduct(int id)
        {
            Product product = await _storeDbContext.Products.FindAsync(id);

            if(product == null)
            {
                return;
            }
            else
            {
                _storeDbContext.Entry(product).State = EntityState.Deleted;
                await _storeDbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Returns a specific product from the database table,
        /// by product Id.
        /// </summary>
        /// <param name="id">A unique integer product Id value</param>
        /// <returns>A specific product</returns>
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

        /// <summary>
        /// Updates a specific product in the database table, 
        /// by product Id, into the new product object passed as parameter.
        /// </summary>
        /// <param name="id">A unique integer product Id value</param>
        /// <param name="product">An instance of Product</param>
        /// <returns>The updated Product object</returns>
        public async Task<Product> UpdateProduct(int id, Product product)
        {
            if(product == null)
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


        /// <summary>
        /// Returns a collection of all the products in the 
        /// database.
        /// </summary>
        /// <returns>A collection of all products</returns>
        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _storeDbContext.Products.ToListAsync();

            List<Product> allProducts = new List<Product>();
            foreach (var item in products)
            {
                allProducts.Add(item);
            };
            return allProducts;
        }
    }
}
