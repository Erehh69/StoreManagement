using Microsoft.AspNetCore.Mvc;

namespace StoreManagementApp.Controllers
{
    public class HomeController : Controller
    {
        // Home page action
        public IActionResult Index()
        {
            ViewData["Title"] = "Home";
            return View();
        }

        // About Us page action
        public IActionResult AboutUs()
        {
            ViewData["Title"] = "About Us";
            return View();
        }

        // FAQ page action
        public IActionResult FAQ()
        {
            ViewData["Title"] = "FAQ";
            return View();
        }

        // Privacy page action
        public IActionResult Privacy()
        {
            ViewData["Title"] = "Privacy";
            return View();
        }
    }
}
