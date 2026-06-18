using Microsoft.AspNetCore.Mvc;
using Models;
using System.Diagnostics;

namespace WertyEnglish.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
