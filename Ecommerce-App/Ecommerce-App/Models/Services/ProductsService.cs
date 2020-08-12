using Ecommerce_App.Models.Interfaces;
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
        public Task<Product> CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Collection<Cereal> GetAllProducts()
        {
            Collection<Cereal> cerealData = new Collection<Cereal>();
            string path = Environment.CurrentDirectory;
            string newPath = Path.GetFullPath(Path.Combine(path, @"wwwroot\cereal.csv"));
            string[] myFile = File.ReadAllLines(newPath);

            for (int i = 1; i < myFile.Length; i++)
            {
                string[] temp = myFile[i].Split(",");

                Cereal cereal = new Cereal 
                {
                    Name = temp[0],
                    Mfr = char.Parse(temp[1]),
                    Type = char.Parse(temp[2]),
                    Calories = Int32.Parse(temp[3]),
                    Protein = Int32.Parse(temp[4]),
                    Fat = Int32.Parse(temp[5]),
                    Sodium = Int32.Parse(temp[6]),
                    Fiber = decimal.Parse(temp[7]),
                    Carbo = decimal.Parse(temp[8]),
                    Sugars = Int32.Parse(temp[9]),
                    Potass = Int32.Parse(temp[10]),
                    Vitamins = Int32.Parse(temp[11]),
                    Shelf = Int32.Parse(temp[12]),
                    Weight = decimal.Parse(temp[13]),
                    Cups = decimal.Parse(temp[14]),
                    Rating = decimal.Parse(temp[15])
                };
                

                cerealData.Add(cereal);
            }

            return cerealData;
        }

        public Task<Product> GetSingleProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateProduct(int id, Product product)
        {
            throw new NotImplementedException();
        }

        Collection<Product> IProductsService.GetAllProducts()
        {
            throw new NotImplementedException();
        }
    }
}
