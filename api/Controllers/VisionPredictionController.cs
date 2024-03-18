using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VisionPredictionController : ControllerBase
{
    private readonly string? _predictionKey = Environment.GetEnvironmentVariable("visionpredictionkey");
    private readonly string? _endpoint = Environment.GetEnvironmentVariable("visionendpoint");
    private readonly string? _projectId = Environment.GetEnvironmentVariable("visionprojectid");
    private readonly string? _publishedName = Environment.GetEnvironmentVariable("visionprojectname");

    [HttpPost]
    public async Task<IActionResult> PredictImage([FromBody] string imageUrl)
    {
        var client = new HttpClient();

        // Request headers - replace this example key with your valid Prediction-Key.
        client.DefaultRequestHeaders.Add("Prediction-Key", _predictionKey);

        // Prediction URL - replace this example URL with your valid Prediction URL.
        string url = $"{_endpoint}/customvision/v3.0/Prediction/{_projectId}/classify/iterations/{_publishedName}/url";

        HttpResponseMessage response;

        // Request body. Try this sample with a locally stored image.
        byte[] byteData = Encoding.UTF8.GetBytes($"{{\"Url\": \"{imageUrl}\"}}");

        using (var content = new ByteArrayContent(byteData))
        {
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            response = await client.PostAsync(url, content);
            var predictionResult = await response.Content.ReadAsStringAsync();

            // Parse the JSON response
            var predictions = JObject.Parse(predictionResult)["predictions"];
            foreach (var prediction in predictions)
            {
                // Check if the probability is greater than 75%
                if ((double)prediction["probability"] > 0.75)
                {
                    var tagName = (string)prediction["tagName"];
                    return Ok(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tagName.ToLower()));
                }
            }

            // If no prediction has a probability greater than 75%, return a default message
            return Ok("No prediction with more than 75% probability.");
        }
    }
}
