using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface IImage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Task UploadImage(string containerName, string fileName, byte[] image, string contentType);

        /// <summary>
        /// Gets or Creates a container for the blob to be uploaded into
        /// and setting its permissions.
        /// </summary>
        /// <param name="containerName">A unique container name</param>
        /// <returns>The CloudBlobcontainer object</returns>
        Task<CloudBlobContainer> GetContainer(string containerName);

        /// <summary>
        /// Creates a blob within the container
        /// </summary>
        /// <param name="fileName">A unique file name</param>
        /// <param name="containerName">A unique container name</param>
        /// <returns>A CloudBlob object</returns>
        Task<CloudBlob> GetBlob(string fileName, string containerName);


    }
}
