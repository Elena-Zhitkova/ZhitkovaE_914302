using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ScientificMagazine.Data;
using ScientificMagazine.Models;
using System.Security.Claims;

namespace ScientificMagazine.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ApplicationDbContext _db;
        public RegistrationController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        [Route("~/registration")]
        public async Task<IActionResult> Registration(User userlog)
        {

            if (_db.Users.Where(a => a.Email == userlog.Email).Count() != 0) return Redirect("~/registration");
            userlog.Role = "User";
            _db.Users.Add(userlog);
            _db.SaveChanges();
            return Redirect("~/login");
        }
    }
}
