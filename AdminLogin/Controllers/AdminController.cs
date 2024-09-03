using Microsoft.AspNetCore.Mvc;

namespace AdminLogin.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminLogin()
        {
            return View();
        }
    }
}
