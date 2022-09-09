using Microsoft.AspNetCore.Mvc;

namespace SPaPS.Controllers
{
    public class AccountController : Controller
    {
        public async Task<IActionResult> Register()
        {
            
            return View();
        }
    }
}
