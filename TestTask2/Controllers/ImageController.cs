using Microsoft.AspNetCore.Mvc;

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
            List<string> files = new List<string>()
            { "a", "b", "c" };
            return Json(files);
        }
    }
}
