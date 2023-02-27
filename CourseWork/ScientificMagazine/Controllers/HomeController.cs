using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using ScientificMagazine.Data;
using ScientificMagazine.Models;
using ScientificMagazine.Utils;
using System.Diagnostics;

namespace ScientificMagazine.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var archives = _db.Archives.OrderBy(a => a.Number).ToList();
            var archiveId = archives[archives.Count - 2].Id;
            var articles = _db.Articles.Where(a => a.MagazineId == archiveId).ToList();
            return View(articles);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}