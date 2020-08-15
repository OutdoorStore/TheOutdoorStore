using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Ecommerce_App.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using static System.Net.Mime.MediaTypeNames;

namespace Ecommerce_App.Pages.Dashboard
{
    public class PictureModel : PageModel
    {
        private IImage _image;
        private readonly IConfiguration _configuration;
        private readonly IProductsService _productService;

        public PictureModel(IImage image, IConfiguration configuration, IProductsService productService)
        {
            _image = image;
            _configuration = configuration;
            _productService = productService;
        }

        [BindProperty]
        public ImageViewModel Input { get; set; }
      
        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            // use _product service to get the product by id.
            // set the product property to our service result:
            Product = await _productService.GetSingleProduct(id);
            
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            // Create a local file in the ./data/ directory for uploading and downloading
            // Set the local path to a temp location (folder)
            //string localPath = Path.GetTempFileName();

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

            // save to DB: set product.Image to image Uri
            // and update the product in the DB
            Product = await _productService.GetSingleProduct(Product.Id);
            Product.Image = imageUri;
            await _productService.UpdateProduct(Product.Id, Product);

            return RedirectToAction("Index", "Home");
        }

        public class ImageViewModel
        {
            public IFormFile File { get; set; }
        }
    }
}