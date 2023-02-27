using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using ScientificMagazine.Data;
using ScientificMagazine.Models;
using ScientificMagazine.Utils;
using System.IO;
using System.Text;

namespace ScientificMagazine.Controllers
{
    public class Upload
    {
        public Application app { get; set; } = new Application();
        public List<Archive> arch { get; set; } = new List<Archive>();
        public List<IFormFile> file { get; set; }
    }
    public class ApplicationController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ApplicationController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Application> objApplicationList = _db.Applications;
            return View(objApplicationList);
        }
        //get
        public IActionResult ApplicationCreate()
        {
            
            return View();
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ApplicationCreate(Upload obj)
        {
            obj.app.Status = "Обработать";

            var filePath = Path.Combine(Path.GetTempPath(), obj.file[0].FileName);
            var text1 = "";
            var text2 = "";
            using (var stream = System.IO.File.Create(filePath))
            {
                obj.file[0].CopyTo(stream);
                stream.Close();
                var stream2 = System.IO.File.OpenRead(filePath);
                byte[] buffer = new byte[stream2.Length];
                // считываем данные
                stream2.Read(buffer, 0, buffer.Length);
                // декодируем байты в строку
                text1 = Encoding.Default.GetString(buffer);
                stream2.Close();
                stream2 = System.IO.File.OpenRead("data.txt");
                buffer = new byte[stream2.Length];
                // считываем данные
                stream2.Read(buffer, 0, buffer.Length);
                text2 = Encoding.Default.GetString(buffer);
                stream2.Close();
                var s = new FileStream("data.txt", FileMode.Append);
                buffer = Encoding.Default.GetBytes(text1);
                // запись массива байтов в файл
                 s.Write(buffer, 0, buffer.Length);
                s.Close();
              
            }
            
            var dmp = new diff_match_patch();
            var listDiff = dmp.diff_main(text2, text1);
            var resLen = 0;
            foreach (var diff in listDiff)
            {
                if(diff.operation  == Operation.EQUAL && diff.text!= " ") resLen += diff.text.Length;
            }
            obj.app.GradeAntiplagiat = (100.0 * (text1.Length - resLen)  /(  text1.Length)).ToString();
            obj.app.ArticleFile = filePath;
            _db.Applications.Add(obj.app);
            _db.SaveChanges();

            return Redirect($"~/");
        }
    }
}
