using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce_App.Pages.Shared
{
    public class _ErrorModel : PageModel
    {
        public string Message { get; set; }
        public void OnPost()
        {
        }
    }
}