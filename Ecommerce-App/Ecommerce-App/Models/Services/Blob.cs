﻿using Ecommerce_App.Models.Interfaces;
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

        public async Task<CloudBlob> GetBlob(string fileName, string containerName)
        {
            var container = await GetContainer(containerName);

            CloudBlob blob = container.GetBlobReference(fileName);

            return blob;
        }

        public async Task UploadImage(string containerName, string fileName, byte[] image, string contentType)
        {
            var container = await GetContainer(containerName);

            var blobFile =  container.GetBlockBlobReference(fileName);

            blobFile.Properties.ContentType = contentType;
            
            await blobFile.UploadFromByteArrayAsync(image, 0, image.Length);
        }
    }
}