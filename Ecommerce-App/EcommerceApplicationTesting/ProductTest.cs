using Ecommerce_App.Data;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Ecommerce_App.Models.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;

namespace EcommerceApplicationTesting
{
    public class ProductTest : DatabaseTestBase
    {

        private IProductsService BuildRepository()
        {
            return new InventoryManagement(_db);
        }

        [Fact]
        public async Task CanGetAndSetProduct()
        {
            // Arrange
            Product newProduct = new Product()
            {
                Id = 11,
                Sku = "TEST-BB-PUR-01",
                Name = "Mango",
                Price = 5.00M,
                Description = "Greatest fruit ever",
                Image = "image.png"
            };

            var repository = BuildRepository();

            // Act

            Product saved = await repository.CreateProduct(newProduct);
            Product getProduct = await repository.GetSingleProduct(11);

            // Assert
            Assert.Equal(11, getProduct.Id);
            Assert.Equal("TEST-BB-PUR-01", getProduct.Sku);
            Assert.Equal("Mango", getProduct.Name);
            Assert.Equal(5.00M, getProduct.Price);
            Assert.Equal("Greatest fruit ever", getProduct.Description);
            Assert.Equal("image.png", getProduct.Image);
        }

        [Fact]
        public async Task CanCreateAndGetSingleProduct()
        {
            // Arrange
            Product newProduct = new Product()
            {
                Id = 11,
                Sku = "TEST-BB-PUR-01",
                Name = "Mango",
                Price = 5.00M,
                Description = "Greatest fruit ever",
                Image = "image.png"
            };

            var repository = BuildRepository();

            // Act

            Product saved = await repository.CreateProduct(newProduct);
            Product getProduct = await repository.GetSingleProduct(11);

            // Assert
            Assert.Equal("Mango", getProduct.Name);
        }

        [Fact]
        public async Task CanGetAllProducts()
        {
            // Arrange & Act
            var repository = BuildRepository();

            List<Product> result = await repository.GetAllProducts();

            // Assert
            Assert.Equal(10, result.Count);
        }

        [Fact]
        public async Task CanUpdateAProduct()
        {
            // Arrange

            var repository = BuildRepository();

            Product newProduct = new Product()
            {
                Id = 1,
                Sku = "TEST-BB-PUR-01",
                Name = "Mango",
                Price = 5.00M,
                Description = "Greatest fruit ever",
                Image = "image.png"
            };

            // Act

            Product updateProduct = await repository.UpdateProduct(1, newProduct);
            Product result = await repository.GetSingleProduct(1);

            // Assert
            Assert.Equal("Mango", result.Name);
        }

        [Fact]
        public async Task CanDeleteAProduct()
        {
            // Arrange

            var repository = BuildRepository();

            List<Product> result = await repository.GetAllProducts();

            await repository.DeleteProduct(1);

            List<Product> result2 = await repository.GetAllProducts();

            Product result3 = await repository.GetSingleProduct(1);

            // Assert
            Assert.Equal(10, result.Count);
            Assert.Equal(9, result2.Count);
            Assert.Null(result3);
        }

    }
}
