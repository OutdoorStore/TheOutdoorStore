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
        /// Returns a list of all the products in the 
        /// database.
        /// </summary>
        /// <returns>A list of all products</returns>
        Task<List<Product>> GetAllProducts();

        /// <summary>
        /// Returns a specific product from the database table,
        /// by product Id.
        /// </summary>
        /// <param name="id">A unique integer product Id value</param>
        /// <returns>A specific product</returns>
        Task<Product> GetSingleProduct(int id);

        /// <summary>
        /// Creates a new entry in the database table
        /// based on the product object parameter
        /// and returns the same object. 
        /// </summary>
        /// <param name="product">An instance of Product</param>
        /// <returns>A specific Product object</returns>
        Task<Product> CreateProduct(Product product);

        /// <summary>
        /// Updates a specific product in the database table, 
        /// by product Id, into the new product object passed as parameter.
        /// </summary>
        /// <param name="id">A unique integer product Id value</param>
        /// <param name="product">An instance of Product</param>
        /// <returns>The updated Product object</returns>
        Task<Product> UpdateProduct(int id, Product product);

        /// <summary>
        /// Deletes a product from the products database table
        /// based on the product Id parameter.
        /// </summary>
        /// <param name="id">A unique integer product Id value</param>
        /// <returns>An empty task object</returns>
        Task DeleteProduct(int id);

        /// <summary>
        /// Returns a list of all products ordered by the specific property.
        /// </summary>
        /// <param name="orderProp">Which property is being used for ordering.</param>
        /// <param name="ascending">Whether the ordering will be acsending or descending.</param>
        /// <returns>Correctly ordered list of all products.</returns>
        Task<List<Product>> GetOrderedProducts(string sort);


    }
}
