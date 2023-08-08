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
            var res = new ImageMetaRepository();
            var a = res.ReadAll().Select(x => APIFileInfo.FromMeta(x)).ToArray();
            return Ok(JsonConvert.SerializeObject(a));
        }
        [Route("delete")]
        [HttpGet]
        public IActionResult DeleteImage([FromQuery] long id)
        {
            var res = new ImageMetaRepository();
            res.Delete(id);
            var image = new ImageRepository();
            image.DeleteImage(id);
            return Ok();
        }
        [Route("save")]
        [HttpPost]
        public IActionResult LoadImage()
        {
            var res = new ImageMetaRepository();
            var rawRequestBody = new StreamReader(Request.Body).ReadToEndAsync().GetAwaiter().GetResult();

            res.Create(JsonConvert.DeserializeObject<MetaInfo>(rawRequestBody));
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
            var res = new ImageRepository();
            var q = new ImageMetaRepository();
            var rawRequestBody = new StreamReader(Request.Body).ReadToEndAsync().GetAwaiter().GetResult();
            var bytes = Convert.FromBase64String(rawRequestBody.Split(',').Last());
            var i = q.GetMaxID();
            if(i==0)
            {
                i++;
            }
            res.SaveImage(i, bytes);
            return Ok();
        }

        [Route("getimage")]
        [HttpGet]
        public IActionResult GetImage([FromQuery] long id)
        {
            var res = new ImageRepository();
            Console.WriteLine(id);
            var pat = res.GetImage(id);
            var q = Convert.ToBase64String(pat);
            return Ok(q);
        }

        [Route("update")]
        [HttpPost]
        public IActionResult UpdateImage()
        {
            var res = new ImageMetaRepository();
            var rawRequestBody = new StreamReader(Request.Body).ReadToEndAsync().GetAwaiter().GetResult();

            res.Update(JsonConvert.DeserializeObject<MetaInfo>(rawRequestBody));
            return Ok();
        }
    }
}
