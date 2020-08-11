using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface IProductsService
    {
        
        /// <summary>
        /// Returns a collection of all the products in the 
        /// database.
        /// </summary>
        /// <returns>A collection of all products</returns>
        Collection<Cereal> GetAllProducts();

        /// <summary>
        /// Returns a specific product from the database table,
        /// by product Id.
        /// </summary>
        /// <param name="id">A unique integer product Id value</param>
        /// <returns>A specific product</returns>
        Task<Cereal> GetSingleProduct(int id);

        /// <summary>
        /// Creates a new entry in the database table
        /// based on the cereal object parameter
        /// and returns the same object. 
        /// </summary>
        /// <param name="cereal">An instance of Product</param>
        /// <returns>A specific Product object</returns>
        Task<Cereal> CreateProduct(Cereal cereal);

        /// <summary>
        /// Updates a specific product in the database table, 
        /// by product Id, into the new product object passed as parameter.
        /// </summary>
        /// <param name="id">A unique integer product Id value</param>
        /// <param name="cereal">An instance of Product</param>
        /// <returns>The updated Product object</returns>
        Task<Cereal> UpdateProduct(int id, Cereal cereal);

        /// <summary>
        /// Deletes a product from the products database table
        /// based on the product Id parameter.
        /// </summary>
        /// <param name="id">A unique integer product Id value</param>
        /// <returns>An empty task object</returns>
        Task DeleteProduct(int id);

    }
}
