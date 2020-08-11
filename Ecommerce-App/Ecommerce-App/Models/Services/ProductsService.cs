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
        public Collection<Cereal> GetData()
        {
            string path = Environment.CurrentDirectory;
            string newPath = Path.GetFullPath(Path.Combine(path, @"wwwroot\cereal.csv"));

            Collection<Cereal> cerealData = new Collection<Cereal>();

            string[] myFile = File.ReadAllLines(newPath);

            for (int i = 1; i < myFile.Length; i++)
            {
                string[] temp = myFile[i].Split(",");

                Cereal cereal = new Cereal();
                cereal.Name = temp[0];
                cereal.Mfr = char.Parse(temp[1]);
                cereal.Type = char.Parse(temp[2]);
                cereal.Calories = Int32.Parse(temp[3]);
                cereal.Protein = Int32.Parse(temp[4]);
                cereal.Fat = Int32.Parse(temp[5]);
                cereal.Sodium = Int32.Parse(temp[6]);
                cereal.Fiber = decimal.Parse(temp[7]);
                cereal.Carbo = decimal.Parse(temp[8]);
                cereal.Sugars = Int32.Parse(temp[9]);
                cereal.Potass = Int32.Parse(temp[10]);
                cereal.Vitamins = Int32.Parse(temp[11]);
                cereal.Shelf = Int32.Parse(temp[12]);
                cereal.Weight = decimal.Parse(temp[13]);
                cereal.Cups = decimal.Parse(temp[14]);
                cereal.Rating = decimal.Parse(temp[15]);

                cerealData.Add(cereal);
            }

            return cerealData;
        }
    }
}
