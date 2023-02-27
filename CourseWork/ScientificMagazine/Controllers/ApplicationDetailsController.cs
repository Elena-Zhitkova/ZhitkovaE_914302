using Microsoft.AspNetCore.Mvc;
using ScientificMagazine.Data;
using Microsoft.EntityFrameworkCore;
using ScientificMagazine.Models;
using ScientificMagazine.Utils;
using ScientificMagazine.Enums;

namespace ScientificMagazine.Controllers
{

    public class ApplicationDetailsController : Controller
    {
        public static Application appl;
        private readonly ApplicationDbContext _db;
        public ApplicationDetailsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("ApplicationDetails/Index/{id}")]
        public IActionResult Index(int id)
        {
            var objApplication = _db.Applications.Where(item => item.Id == id).ToList().First();
            ViewData["statuses"] =  _db.Statuses;
            ViewData["reviewer"] = _db.Users.Where(item => item.Role == "Reviewer").ToList();
            var up = new Upload();
            up.app = objApplication;
            appl = up.app;
            return View( up);
        }

        public IActionResult ChangeStatus(int id, string status)
        {
            var objApplication = _db.Applications.Where(item => item.Id == id).ToList().First();
            objApplication.Status = status;
            
            var reviewer = _db.Users.Where(item => item.FIO == objApplication.Assignee).ToList().First();
            var redactors = _db.Users.Where(item => item.Role == "Redactor").ToList();
            var author = _db.Users.Where(item => item.FIO == objApplication.Author).ToList().First();
            switch (status)
            {
                case "Готов к правкам":
                    foreach(var redactor in redactors)
                    {
                        EmailSender.Send(redactor.Email, string.Format(Messages.ReadyToEdit, $"~/ApplicationDetails/Index/{id}"));
                    }
                    break;
                case "Редактируется":
                        EmailSender.Send(author.Email, Messages.NeedToEdit,new List<string>() {objApplication.RedactorReviewFile, objApplication.CriticReviewFile });
                    break;
                case "Отклонить":
                        EmailSender.Send(author.Email, Messages.Decline);
                    break;
                case "Готов к рецензии":
                    EmailSender.Send(reviewer.Email, string.Format(Messages.Assigned, $"~/ApplicationDetails/Index/{id}"));
                    break;
            }
            _db.Update(objApplication);
            _db.SaveChanges();
            return Redirect($"~/ApplicationDetails/Index/{id}");
        }

        public IActionResult ChangeAssignee(int id, string assignee)
        {
            var reviewer = _db.Users.Where(item => item.FIO == assignee).ToList().First();
            EmailSender.Send(reviewer.Email, string.Format(Messages.Assigned, $"~/ApplicationDetails/Index/{id}"));

            var objApplication = _db.Applications.Where(item => item.Id == id).ToList().First();
            objApplication.Assignee = assignee;
            _db.Update(objApplication);
            _db.SaveChanges();
            return Redirect($"~/ApplicationDetails/Index/{id}");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ApplicationDetails/Index/{id}")]
        public IActionResult ApplicationDetailsUpdate(Upload obj)
        {
            obj.app = appl;
            var filePath = Path.Combine(Path.GetTempPath(), obj.file[0].FileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                obj.file[0].CopyTo(stream);
            }
            obj.app.CriticReviewFile = filePath;
            filePath = Path.Combine(Path.GetTempPath(), obj.file[1].FileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                obj.file[1].CopyTo(stream);
            }
            obj.app.RedactorReviewFile = filePath;
            _db.Applications.Update(obj.app);
            _db.SaveChanges();

            return Redirect($"~/ApplicationDetails/Index/{obj.app.Id}");
        }

        public IActionResult ToMagazine(int id)
        {
            var objApplication = _db.Applications.Where(item => item.Id == id).ToList().First();
            _db.Remove(objApplication);
            _db.SaveChanges();
            var art = new Article();
            art.ArticlePDF = objApplication.ArticleFile;
            art.Annotation = objApplication.Annotation;
            art.ArticleName = objApplication.Name;
            art.Keywords = objApplication.KeyWords;
            art.ArticleAuthor = objApplication.Author;
            art.GradeDowload = 0;
            art.GradeViews = 0;
            art.MagazineId = _db.Archives.OrderBy(a => a.Number).Last().Id;
            _db.Add(art);
            _db.SaveChanges();
            return Redirect($"~/");
        }
    }
}
