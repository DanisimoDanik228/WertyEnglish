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
        public IActionResult Dictionary(long id)
        {
            return View(id);
        }
        public IActionResult Learning(long id)
        {
            return View(id);
        }
    }
}
