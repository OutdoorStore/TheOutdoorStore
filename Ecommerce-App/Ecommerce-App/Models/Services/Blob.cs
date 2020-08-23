using Ecommerce_App.Models.Interfaces;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Services
{
    public class Blob : IImage
    {
        public CloudStorageAccount CloudStorageAccount { get; set; }

        public CloudBlobClient CloudBlobClient { get; set; }

        public Blob(IConfiguration configuration)
        {
            var storageCreds = new StorageCredentials(configuration["StorageAccountName"], configuration["BlobKey"]);

            CloudStorageAccount = new CloudStorageAccount(storageCreds, true);

            CloudBlobClient = CloudStorageAccount.CreateCloudBlobClient();
        }

        /// <summary>
        /// Gets or Creates a container for the blob to be uploaded into
        /// and setting its permissions.
        /// </summary>
        /// <param name="containerName">A unique container name</param>
        /// <returns>The CloudBlobcontainer object</returns>
        public async Task<CloudBlobContainer> GetContainer(string containerName)
        {
            CloudBlobContainer container = CloudBlobClient.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions 
            { 
                PublicAccess = BlobContainerPublicAccessType.Blob 
            });

            return container;
        }

        /// <summary>
        /// Creates a blob within the container
        /// </summary>
        /// <param name="fileName">A unique file name</param>
        /// <param name="containerName">A unique container name</param>
        /// <returns>A CloudBlob object</returns>
        public async Task<CloudBlob> GetBlob(string fileName, string containerName)
        {
            var container = await GetContainer(containerName);

            CloudBlob blob = container.GetBlobReference(fileName);

            return blob;
        }

        /// <summary>
        /// Uploads an image to Azure blob storage based on
        /// the unique container name, file name and file path. 
        /// </summary>
        /// <param name="containerName">The specific container the image should be uploaded into</param>
        /// <param name="fileName">The name of the file that is being uploaded</param>
        /// <param name="image">a byte[] stream</param>
        /// <param name="contentType">The type of image file that is being uploaded</param>
        /// <returns>A complete task</returns>
        public async Task UploadImage(string containerName, string fileName, byte[] image, string contentType)
        {
            var container = await GetContainer(containerName);

            var blobFile =  container.GetBlockBlobReference(fileName);

            blobFile.Properties.ContentType = contentType;
            
            await blobFile.UploadFromByteArrayAsync(image, 0, image.Length);
        }
    }
}
