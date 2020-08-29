using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Auth.AccessControlPolicy;
using Azure.Storage.Blobs;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Ecommerce_App.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce_App.Pages.Dashboard
{
    [Authorize(Policy = "AdminOnly")]
    public class IndexModel : PageModel
    {
        private SignInManager<Customer> _signInManager;
        private UserManager<Customer> _userManager;
        private IOrder _order;
        private IImage _image;
        private readonly IConfiguration _configuration;
        private readonly IProductsService _productService;

        public IndexModel(IImage image, IConfiguration configuration, IProductsService productService, SignInManager<Customer> signInManager, UserManager<Customer> userManager, IOrder order)
        {
            _image = image;
            _configuration = configuration;
            _productService = productService;
            _signInManager = signInManager;
            _userManager = userManager;
            _order = order;
        }

        public List<Order> Orders { get; set; }
        public List<Product> Products { get; set; }

        [BindProperty]
        public ImageViewModel Input { get; set; }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGet()
        {
            // use _product service to get the product by id.
            // set the product property to our service result:
            Products = await _productService.GetAllProducts();

            Orders = await _order.GetAllOrdersForAdmin();

            return Page();
        }

        public async Task<IActionResult> OnPostCreate()
        {
            if (Input.File != null)
            {
                string ext = Path.GetExtension(Input.File.FileName);
                string fileName = Input.File.FileName;
                string contentType = Input.File.ContentType;

                using (var stream = new MemoryStream())
                {
                    // copies the file as a stream to the file location:
                    await Input.File.CopyToAsync(stream);
                    var bytes = stream.ToArray();
                    await _image.UploadImage("images", fileName, bytes, contentType);
                }

            }

            Blob blob = new Blob(_configuration);

            // TODO: Add placeholder image and add conditional logic to use it if no file was added
            var resultBlob = await blob.GetBlob(Input.File.FileName, "images");

            string imageUri = resultBlob.Uri.ToString();
            Product.Image = imageUri;
            Product.Id = 0;
            await _productService.CreateProduct(Product);
            return RedirectToPage("/Dashboard/Index");
        }

        public async Task<IActionResult> OnPostUpdate()
        {
            // Create a local file in the ./data/ directory for uploading and downloading
            // Set the local path to a temp location (folder)
            //string localPath = Path.GetTempFileName();
            if (Input.File != null)
            {
                string ext = Path.GetExtension(Input.File.FileName);
                string fileName = Input.File.FileName;
                string contentType = Input.File.ContentType;

                if (Input.File != null)
                {
                    using (var stream = new MemoryStream())
                    {
                        // copies the file as a stream to the file location:
                        await Input.File.CopyToAsync(stream);
                        var bytes = stream.ToArray();
                        await _image.UploadImage("images", fileName, bytes, contentType);
                    }

                }

                Blob blob = new Blob(_configuration);

                var resultBlob = await blob.GetBlob(Input.File.FileName, "images");

                string imageUri = resultBlob.Uri.ToString();
                Product.Image = imageUri;
            }

            // save to DB: set product.Image to image Uri
            // and update the product in the DB
            //Product = await _productService.GetSingleProduct(Product.Id);
            await _productService.UpdateProduct(Product.Id, Product);

            return RedirectToPage("/Dashboard/Index");
        }

        public async Task<IActionResult> OnPostDelete()
        {
            await _productService.DeleteProduct(Product.Id);
            return RedirectToPage("/Dashboard/Index");
        }

        // refactor, view model not needed on a razor page.
        public class ImageViewModel
        {
            public IFormFile File { get; set; }
        }
    }
}