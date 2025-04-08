using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ReiRexhajEpSolution.Presentation.Controllers
{
    public class USELESSFORNOW : Controller
    {
        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(string username)
        {
            if (!string.IsNullOrWhiteSpace(username))
            {
                // Save the username in session for a simple login.
                HttpContext.Session.SetString("Username", username);
                return RedirectToAction("Index", "Poll");
            }

            ViewBag.Error = "Please enter a username.";
            return View();
        }

        // GET: /Account/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
