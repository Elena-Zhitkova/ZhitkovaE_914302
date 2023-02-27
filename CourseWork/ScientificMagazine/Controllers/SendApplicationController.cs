using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScientificMagazine.Data;
using ScientificMagazine.Models;
using System.Drawing;

namespace ScientificMagazine.Controllers
{
    public class SendApplicationController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SendApplicationController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
