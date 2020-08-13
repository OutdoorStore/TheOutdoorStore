using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Ecommerce_App.Models.Interfaces;
using Ecommerce_App.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Ecommerce_App.Pages.Dashboard
{
    public class PictureModel : PageModel
    {
        private IImage _image;
        private readonly IConfiguration _configuration;

        public PictureModel(IImage image, IConfiguration configuration)
        {
            _image = image;
            _configuration = configuration;
        }

        [BindProperty]
        public ImageViewModel Input { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            // Create a local file in the ./data/ directory for uploading and downloading
            // Set the local path to a temp location (folder)
            string localPath = Path.GetTempFileName();
            // Setting the file name to a GUID

            using (var stream = System.IO.File.Create(localPath))
            {
                // copies the file as a stream to the file location:
                await Input.File.CopyToAsync(stream);
            }

            Blob blob = new Blob(_configuration);

            string fileName = Guid.NewGuid().ToString();

            await blob.UploadImage("images", Input.File.FileName, localPath);

            var resultBlob = await blob.GetBlob(Input.File.FileName, "images");

            string imageUri = resultBlob.Uri.ToString();

            return RedirectToAction("Index", "Home");
        }

        public class ImageViewModel
        {
            public IFormFile File { get; set; }
        }
    }
}