using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestTask2.Configuration;
using TestTask2.Models;
using TestTask2.Repositories.DB;
using TestTask2.Repositories.DB.ScriptsProvider;

namespace TestTask2.Controllers
{
    [ApiController]
    [Route("/api/image")]
    public class ImageController : Controller
    {
        private readonly ILogger<ImageController> _logger;

        public ImageController(ILogger<ImageController> logger)
        {
            _logger = logger;
        }

        [Route("files")]
        [HttpGet]
        public IActionResult GetFiles()
        {
            var res = new ImageMetaRepository();
            res.Update(new MetaInfo { Name = "Новая картинка", Description = "новое описание", Id = 4 });
            var a = res.ReadAll().Select(x => APIFileInfo.FromMeta(x)).ToArray();
            return Ok(JsonConvert.SerializeObject(a));
        }

        public IActionResult LoadImage()
        {
            return Json(null);
        }
        public IActionResult RemoveImage()
        {
            return Json(null);
        }
        public IActionResult GetImageInfo()
        {
            return Json(null);
        }
        public IActionResult GetImage()
        {
            return Json(null);
        }
        public IActionResult UpdateImage()
        {
            return Json(null);
        }
    }
}
