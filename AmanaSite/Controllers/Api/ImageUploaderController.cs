using System.Threading.Tasks;
using AmanaSite.Models.VM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmanaSite.Controllers.Api
{
    public class ImageUploaderController : ApiBaseController
    {
        [HttpPost("upload-image")]
        public ActionResult<ImageDto> UploadImage(IFormFile image)
        {
            return Ok(new ImageDto
            {
                Status=true,
                Msg="image uploaded successfully",
                ImageUrl="https://i.stack.imgur.com/M0le5.jpg",
                GeneratedName="demoImage.jpg",
                OriginalName="demoImage.jpg"
            });
        }
    }
}