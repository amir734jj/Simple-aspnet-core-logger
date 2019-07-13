using Microsoft.AspNetCore.Mvc;

namespace TestLogger.Api.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Hello world!");
        }
    }
}