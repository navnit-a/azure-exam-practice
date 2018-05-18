using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using SharedAccessKey.Models;

namespace SharedAccessKey.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnection"));

            var blobClient = storageAccount.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference("images");

            var blobs = new List<BlobImage>();
            foreach (var blob in container.ListBlobs())
                if (blob.GetType() == typeof(CloudBlockBlob))
                {
                    //var sas = GetSasToken(storageAccount);
                    var sas = container.GetSharedAccessSignature(null, "MySAP");

                    blobs.Add(new BlobImage {BlobUri = blob.Uri + sas});
                }

            return View(blobs);
        }

        /// <summary>
        ///     If you want to generate a link, like a video stream an let the stream expire after a given period of time
        /// </summary>
        /// <param name="storageAccount"></param>
        /// <returns></returns>
        public static string GetSasToken(CloudStorageAccount storageAccount)
        {
            var policy = new SharedAccessAccountPolicy
            {
                Permissions = SharedAccessAccountPermissions.Read | SharedAccessAccountPermissions.Write,
                Services = SharedAccessAccountServices.Blob,
                ResourceTypes = SharedAccessAccountResourceTypes.Object,
                SharedAccessExpiryTime = DateTime.Now.AddMinutes(30),
                Protocols = SharedAccessProtocol.HttpsOnly
            };

            return storageAccount.GetSharedAccessSignature(policy);
        }
    }
}