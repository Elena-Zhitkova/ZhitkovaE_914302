using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ScientificMagazine.Data;
using System.Security.Claims;
using System;
using ScientificMagazine.Models;

namespace ScientificMagazine.Controllers
{
    public class LoginController : Controller
    {
        public static string usersa = "";
        private readonly ApplicationDbContext _db;
        public LoginController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("~/logout")]
        public async Task<IActionResult> Logout()
        {
            usersa = "";
            await this.HttpContext.SignOutAsync();
            return Redirect("~/");
        }
        [HttpPost]
        [Route("~/login")]
        public async Task<IActionResult> Login(User userlog)
        {
            
            

            if (_db.Users.Where(a => (a.Email == userlog.Email && a.Password == userlog.Password)).Count() == 0) return Redirect("~/Login");
            var user = _db.Users.Where(a => a.Email == userlog.Email && a.Password == userlog.Password).First();
            usersa = user.Role;
            var claims = new List<Claim>
    {
        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
        new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
    };
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await this.HttpContext.SignInAsync(claimsPrincipal);
            return Redirect("~/");
        }
    }
}
