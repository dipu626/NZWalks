using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO.Images;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        // POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromBody] ImageUploadRequestDto imageUploadRequestDto)
        {
            ValidateFileUpload(imageUploadRequestDto);

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }


        }

        private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequestDto)
        {
            string[] allowdExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (allowdExtensions.Contains(Path.GetExtension(imageUploadRequestDto.FileName)) == false)
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if (imageUploadRequestDto.File.Length > 3145728) // 3MB
            {
                ModelState.AddModelError("file", "File size more than 3 MB, please upload a smaller size file");
            }
        }
    }
}
