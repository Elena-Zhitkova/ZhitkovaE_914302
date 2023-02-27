using Microsoft.AspNetCore.Mvc;

namespace ScientificMagazine.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
