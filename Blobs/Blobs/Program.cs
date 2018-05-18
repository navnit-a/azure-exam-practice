using System;
using System.IO;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Blobs
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Create a container
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnection"));

            var blobClient = storageAccount.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference("images");

            container.CreateIfNotExists(BlobContainerPublicAccessType.Blob);
            Console.WriteLine("Container Created");

            // Upload a file
            //var blockBlob = container.GetBlockBlobReference("img2.png");

            //using (var fileStream = File.OpenRead(@"c:\img2.png"))
            //{
            //    blockBlob.UploadFromStream(fileStream);
            //}
            //Console.WriteLine("File Uploaded");

            //Download a file
            //var blockBlob = container.GetBlockBlobReference("img2.png");

            // using (var fileStream = File.OpenWrite(@"img2-downloaded.png"))
            // {
            //     blockBlob.DownloadToStream(fileStream);
            // }
            // Console.WriteLine("File Downloaded");

            // List files
            //var blobs = container.ListBlobs();
            //foreach (var blob in blobs)
            //{
            //    Console.WriteLine(blob.Uri);
            //}

            // Copy blobs
            //var blockBlob = container.GetBlockBlobReference("img2.png");
            //var blockBlobCopy = container.GetBlockBlobReference("img3.png");

            //var cb = new AsyncCallback(x => Console.WriteLine("Blob copy completed"));
            //blockBlobCopy.BeginStartCopy(blockBlob.Uri, cb, null);

            // Path
            //var blockBlob = container.GetBlockBlobReference("png-images/img5-meta.png");
            //using (var fileStream = File.OpenRead(@"c:\img2.png"))
            //{
            //    blockBlob.Metadata.Add("ISO", "9001");
            //    blockBlob.Metadata.Add("Author", "Navnit Anuth");
            //    blockBlob.UploadFromStream(fileStream);
            //}
            //blockBlob.FetchAttributes();
            //foreach (var item in blockBlob.Metadata)
            //{
            //    Console.WriteLine($"{item.Key} : {item.Value}");
            //}
            //Console.WriteLine("File Uploaded");

            // Custom metadata
            //var blockBlob = container.GetBlockBlobReference("img-metadata.png");
            //using (var fileStream = File.OpenRead(@"c:\img2.png"))
            //{
            //    blockBlob.Metadata.Add("ISO", "9001");
            //    blockBlob.Metadata.Add("Author", "Navnit Anuth");

            //    blockBlob.UploadFromStream(fileStream);
            //}

            //SetMetaData(container);
            //GetMetaData(container);

            Console.ReadKey();
        }

        static void SetMetaData(CloudBlobContainer container)
        {
            container.Metadata.Clear();
            container.Metadata.Add("owner", "nav");
            container.SetMetadata();
        }

        static void GetMetaData(CloudBlobContainer container)
        {
            container.FetchAttributes();
            foreach (var item in container.Metadata)
            {
                Console.WriteLine($"{item.Key} : {item.Value}");
            }
        }
    }
}