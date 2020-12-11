using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using NewYork_BackEnd.Data;

namespace NewYork_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        [HttpPost]
        public async Task<IActionResult> UploadTeamImage(IFormFile file)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(AppConfiguration.GetConfiguration("AccessKey"));
            CloudBlobClient BlobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = BlobClient.GetContainerReference("newyork-thebigapp");
            await container.CreateIfNotExistsAsync();
            CloudBlockBlob blob = container.GetBlockBlobReference(file.FileName);
            await blob.UploadFromStreamAsync(file.OpenReadStream());
            return Ok("File uploaded");
        }

        [HttpGet("{filename}")]
        public async Task<Uri> GetPhoto(string filename)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(AppConfiguration.GetConfiguration("AccessKey"));
            CloudBlobClient BlobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = BlobClient.GetContainerReference("newyork-thebigapp");
            CloudBlockBlob blob = container.GetBlockBlobReference(filename);
            bool exists = await blob.ExistsAsync();
            if (exists)
            {
                return blob.Uri;
            }
            else
            {
                return null;
            }
        }
    }
}
