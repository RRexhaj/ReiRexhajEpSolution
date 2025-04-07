using Microsoft.AspNetCore.Mvc;

namespace ReiRexhajEpSolution.Presentation.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                HttpContext.Session.SetString("User", username);
                return RedirectToAction("Index", "Poll");
            }
            ViewBag.Error = "Invalid credentials.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("User");
            return RedirectToAction("Index", "Poll");
        }
    }
}
