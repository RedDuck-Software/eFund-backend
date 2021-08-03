using Api.Service;
using EFund.Api.Attributes;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFund.Api.Controllers
{
    [Route("[controller]")]
    [EnableCors("AllowAll")]
    public class ImageController : BaseController
    {
        private ImageService ImageService { get; }

        public ImageController(ImageService imageService)
        {
            ImageService = imageService;
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> GetImage(string name)
        {
            var res = await ImageService.GetBytesArrayById(name) ??
                      await ImageService.GetBytesArrayById("default.jpeg");

            return File(res, "image/jpeg");
        }
    }
}
