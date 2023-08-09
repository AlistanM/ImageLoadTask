using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Formatting;
using TestTask2.Configuration;
using TestTask2.Interfaces;
using TestTask2.Models;
using TestTask2.Repositories.DB;
using TestTask2.Repositories.DB.ScriptsProvider;
using TestTask2.Repositories.FileServices;

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
            var repositiry = new ImageMetaRepository();
            var a = repositiry.ReadAll().Select(x => APIFileInfo.FromMeta(x)).ToArray();
            return Ok(JsonConvert.SerializeObject(a));
        }
        [Route("delete")]
        [HttpGet]
        public IActionResult DeleteImage([FromQuery] long id)
        {
            var repositiry = new ImageMetaRepository();
            repositiry.Delete(id);
            var image = new ImageRepository();
            image.DeleteImage(id);
            return Ok();
        }
        [Route("save")]
        [HttpPost]
        public IActionResult LoadImage()
        {
            var repositiry = new ImageMetaRepository();
            var rawRequestBody = new StreamReader(Request.Body).ReadToEndAsync().GetAwaiter().GetResult();

            repositiry.Create(JsonConvert.DeserializeObject<MetaInfo>(rawRequestBody));
            return Ok();
        }
        public IActionResult DeleteImage()
        {
            return Json(null);
        }

        [Route("saveimage")]
        [HttpPost]
        public IActionResult SaveImage()
        {
            var repositiry = new ImageRepository();
            var imageRepositiry = new ImageMetaRepository();
            var rawRequestBody = new StreamReader(Request.Body).ReadToEndAsync().GetAwaiter().GetResult();
            var bytes = Convert.FromBase64String(rawRequestBody.Split(',').Last());
            var maxID = imageRepositiry.GetMaxID();
            if(maxID == 0)
            {
                maxID++;
            }
            repositiry.SaveImage(maxID, bytes);
            return Ok();
        }

        [Route("getimage")]
        [HttpGet]
        public IActionResult GetImage([FromQuery] long id)
        {
            var repositiry = new ImageRepository();
            Console.WriteLine(id);
            var pat = repositiry.GetImage(id);
            var byteStr = Convert.ToBase64String(pat);
            return Ok(byteStr);
        }

        [Route("update")]
        [HttpPost]
        public IActionResult UpdateImage()
        {
            var repositiry = new ImageMetaRepository();
            var rawRequestBody = new StreamReader(Request.Body).ReadToEndAsync().GetAwaiter().GetResult();

            repositiry.Update(JsonConvert.DeserializeObject<MetaInfo>(rawRequestBody));
            return Ok();
        }
    }
}
