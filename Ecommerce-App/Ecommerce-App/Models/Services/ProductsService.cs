using Ecommerce_App.Data;
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
        public async Task<Product> CreateProduct(Product product)
        {
            _storeDbContext.Entry(product).State = EntityState.Added;
            await _storeDbContext.SaveChangesAsync();

            return product;
        }

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

        //public Collection<Cereal> GetAllProducts()
        //{
        //    Collection<Cereal> cerealData = new Collection<Cereal>();
        //    string path = Environment.CurrentDirectory;
        //    string newPath = Path.GetFullPath(Path.Combine(path, @"wwwroot\cereal.csv"));
        //    string[] myFile = File.ReadAllLines(newPath);

        //    for (int i = 1; i < myFile.Length; i++)
        //    {
        //        string[] temp = myFile[i].Split(",");

        //        Cereal cereal = new Cereal 
        //        {
        //            Name = temp[0],
        //            Mfr = char.Parse(temp[1]),
        //            Type = char.Parse(temp[2]),
        //            Calories = Int32.Parse(temp[3]),
        //            Protein = Int32.Parse(temp[4]),
        //            Fat = Int32.Parse(temp[5]),
        //            Sodium = Int32.Parse(temp[6]),
        //            Fiber = decimal.Parse(temp[7]),
        //            Carbo = decimal.Parse(temp[8]),
        //            Sugars = Int32.Parse(temp[9]),
        //            Potass = Int32.Parse(temp[10]),
        //            Vitamins = Int32.Parse(temp[11]),
        //            Shelf = Int32.Parse(temp[12]),
        //            Weight = decimal.Parse(temp[13]),
        //            Cups = decimal.Parse(temp[14]),
        //            Rating = decimal.Parse(temp[15])
        //        };
                

        //        cerealData.Add(cereal);
        //    }

        //    return cerealData;
        //}

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
