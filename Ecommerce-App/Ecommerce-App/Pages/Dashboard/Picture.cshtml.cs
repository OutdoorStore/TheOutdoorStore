using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce_App.Pages.Dashboard
{
    public class PictureModel : PageModel
    {
        private IImage _image;

        public PictureModel(IImage image)
        {
            _image = image;
        }

        public void OnGet()
        {

        }
    }
}