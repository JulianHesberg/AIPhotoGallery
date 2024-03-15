using infrastructure.entity;
using Microsoft.AspNetCore.Mvc;
using service.services;

namespace api.Controllers;
    
[Route("api/[controller]")]
[ApiController]
public class ImageController : ControllerBase
{

    private readonly ImageService _service;
    public ImageController(ImageService imageService)
    {
        _service = imageService;
    }

    [HttpGet]
    public IActionResult GetAllImages()
    {
        try
        {
            var images = _service.GetAllImages();
            if (images == null)
            {
                return NotFound("There is not images to retrieve");
            }

            return Ok(images);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

} 
    [HttpGet("GetImageById")]
    public IActionResult GetImageById(int id)
    {
        try
        {
            var image = _service.GetImageById(id);
            if (image == null)
            {
                return NotFound();
            }
            return Ok(image);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("AddImage")]
    public IActionResult AddImage([FromBody] AiImages aiImages)
    {
        try
        {
            if (aiImages == null)
            {
                return BadRequest("The image parameters cannot be null");
            }
            _service.CreateImage(aiImages);
            return CreatedAtAction(nameof(GetImageById), new { id = aiImages.ImageId }, aiImages);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateImage(int id, [FromBody] AiImages aiImages)
    {
        try
        {
            if (aiImages == null || id != aiImages.ImageId)
            {
                return BadRequest();
            }
            var existingImage = _service.GetImageById(id);
            if (existingImage == null)
            {
                return NotFound();
            }
            _service.UpdateImage(aiImages);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteImage(int id)
    {
        try
        {
            var existingImage = _service.GetImageById(id);
            if (existingImage == null)
            {
                return NotFound();
            }
            _service.DeleteImage(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}