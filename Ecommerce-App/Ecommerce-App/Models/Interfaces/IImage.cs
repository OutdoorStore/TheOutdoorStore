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
        /// Uploads an image to Azure blob storage based on
        /// the unique container name, file name and file path. 
        /// </summary>
        /// <param name="containerName">The specific container the image should be uploaded into</param>
        /// <param name="fileName">The name of the file that is being uploaded</param>
        /// <param name="image">a byte[] stream</param>
        /// <param name="contentType">The type of image file that is being uploaded</param>
        /// <returns>A complete task</returns>
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
