using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Blobs;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobStorageController : ControllerBase
    {
        [HttpPost("/uploadfile")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            string blobUri = string.Empty;
            BlobServiceClient blobServiceClient =
                new BlobServiceClient(Environment.GetEnvironmentVariable("imagegalleryblob"));
            BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient("photos");

            using (var stream = file.OpenReadStream())
            {
                BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);
                await blobClient.UploadAsync(stream, true);
                blobUri = blobClient.Uri.ToString();
            }
            return Ok(blobUri);
        }

        [HttpGet("/getimage")]
        public async Task<IActionResult> GetImage(string blobUri)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(blobUri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();

                    return File(content, "image/jpeg");
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
