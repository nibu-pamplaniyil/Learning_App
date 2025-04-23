using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[Route("api/profile")]
[ApiController]

public class ProfileController:Controller
{
    private readonly IProfileServicer _profileServicer;

    public ProfileController(IProfileServicer profileServicer)
    {
        _profileServicer=profileServicer;
    }

    [HttpPost]
    public async Task<IActionResult> AddProfile([FromForm] Profile profile, IFormFile image)
    {
        try
        {
            if (image != null && image.Length > 0)
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var uniqueFileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                var filePath = Path.Combine(folderPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                profile.imageURL = $"{Request.Scheme}://{Request.Host}/images/{uniqueFileName}";
            }

            var result = await _profileServicer.addProfile(
                profile.name,
                profile.description,
                profile.experience,
                profile.mobile,
                profile.email,
                profile.likedin,
                profile.github,
                profile.imageURL
            );

            return CreatedAtAction(nameof(getProfile), new { id = result.id }, result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "An unexpected error occurred." });
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> updateProfile(Guid id, [FromForm] Profile profile, IFormFile? image)
    {
        try
        {
            if (image != null && image.Length > 0)
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var uniqueFileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                var filePath = Path.Combine(folderPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                profile.imageURL = $"{Request.Scheme}://{Request.Host}/images/{uniqueFileName}";
            }

            var updated = await _profileServicer.updateProfile(
                id,
                profile.name,
                profile.description,
                profile.experience,
                profile.mobile,
                profile.email,
                profile.likedin,
                profile.github,
                profile.imageURL
            );

            return Ok(new { message = "Profile updated successfully", data = updated });
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "An unexpected error occurred." });
        }
    }

    [HttpGet]
    public async Task<IActionResult> getProfile()
    {
        try
        {
            var profiles = await _profileServicer.getProfile();
            return Ok(new { message = "Profiles retrieved successfully", data = profiles });
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "An unexpected error occurred while fetching profiles." });
        }
    }
}