using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using ScientificMagazine.Data;
using ScientificMagazine.Models;

namespace ScientificMagazine.Controllers
{
    public class ArchiveController : Controller
    {

        private readonly ApplicationDbContext _db;

        public ArchiveController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var objArchiveList = _db.Archives.ToList();
            var up = new Upload();
            up.arch = objArchiveList;
            
            return View(up);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("~/Archive")]
        public IActionResult Publish(Upload obj)
        {
            var filePath = Path.Combine(Path.GetTempPath(), obj.file[0].FileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                obj.file[0].CopyTo(stream);
            }
            var arch = new Archive();
            arch.Magazine = filePath;
            arch.Year = DateTime.Now.Year;
            arch.Number = _db.Archives.Max(a => a.Number) + 1;
            _db.Archives.Add(arch);
            _db.SaveChanges();

            return Redirect($"~/Archive");
        }

        public PhysicalFileResult DownloadFile(string fileName)
        {
            string contentType = "";
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);

            return new PhysicalFileResult(fileName, contentType);
        }

    }
}
