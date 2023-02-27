using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using ScientificMagazine.Data;

namespace ScientificMagazine.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ArticleController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Route("~/Article/{id}")]
        public IActionResult Index(int id)
        {
            var art = _db.Articles.Where(a => a.Id == id).First();
            art.GradeViews++;
            _db.Update(art);
            _db.SaveChanges();
            return View(art);
        }

        [Route("~/Article/DownloadFile")]
        public PhysicalFileResult DownloadFile(string fileName, int id)
        {
            var art = _db.Articles.Where(a => a.Id == id).First();
            art.GradeDowload++;
            _db.Update(art);
            _db.SaveChanges();
            string contentType = "";
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);

            return new PhysicalFileResult(fileName, contentType);
        }
    }
}
