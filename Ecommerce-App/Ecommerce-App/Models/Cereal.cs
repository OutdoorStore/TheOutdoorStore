using Ecommerce_App.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models
{
    public class Cereal 
    {
        public string Name { get; set; }
        public char Mfr { get; set; }
        public char Type { get; set; }
        public int Calories { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
        public int Sodium { get; set; }
        public int Fiber { get; set; }
        public int Carbo { get; set; }
        public int Sugars { get; set; }
        public int Potass { get; set; }
        public int Vitamins { get; set; }
        public int Shelf { get; set; }
        public int Weight { get; set; }
        public decimal Cups { get; set; }
        public decimal Rating { get; set; }

    }

    public enum Manufacturer
    {
        //A: "American Home Food Products",
        //A = "American Home Food Products",
        //G = General Mills
        //K = Kelloggs
        //N = Nabisco
        //P = Post
        //Q = Quaker Oats
        //R = Ralston Purina
    }

    public enum Type
    {

    }



}
