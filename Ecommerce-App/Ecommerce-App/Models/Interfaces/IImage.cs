using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface IImage
    {
        Task UploadImage(string containerName, string fileName, string filePath);
    }
}
