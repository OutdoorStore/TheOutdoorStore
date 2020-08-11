using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.ViewModels
{
    public class CerealVM
    {
        public static Collection<Cereal> GetData()
        {
            string path = Environment.CurrentDirectory;
            string newPath = Path.GetFullPath(Path.Combine(path, @"wwwroot\cereal.csv"));

            Collection<Cereal> cerealData = new Collection<Cereal>();

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
                    Fiber = Int32.Parse(temp[7]),
                    Carbo = Int32.Parse(temp[8]),
                    Sugars = Int32.Parse(temp[9]),
                    Potass = Int32.Parse(temp[10]),
                    Vitamins = Int32.Parse(temp[11]),
                    Shelf = Int32.Parse(temp[12]),
                    Weight = Int32.Parse(temp[13]),
                    Cups = decimal.Parse(temp[14]),
                    Rating = decimal.Parse(temp[15]),
                };

                cerealData.Add(cereal);
            }

            return cerealData;
        }
    }
}
