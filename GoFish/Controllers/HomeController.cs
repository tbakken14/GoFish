using Microsoft.AspNetCore.Mvc;

namespace GoFish.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }
    }
}