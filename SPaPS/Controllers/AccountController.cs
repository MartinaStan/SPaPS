using SPaPS.Models.AccountModels;
using Microsoft.AspNetCore.Mvc;
using SPaPS.Models;
using Microsoft.AspNetCore.Identity;
using SPaPS.Data;

namespace SPaPS.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly SPaPSContext _context;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, SPaPSContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
           var result = await _signInManager.PasswordSignInAsync(userName: model.Email, password: model.Password, isPersistent:false, lockoutOnFailure:true) ;

            if(!result.Succeeded || result.IsLockedOut || result.IsNotAllowed)
            {
                ModelState.AddModelError("Error", "Погрешно корисничко име или лозинка!");
                return View(model);

            }

            return RedirectToAction(nameof(HomeController.Index), "Home");  
        }

        [HttpGet]
        public IActionResult Register()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            IdentityUser user = new IdentityUser()
            {
                UserName =  model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            var createUser = await _userManager.CreateAsync(user, model.Password);

            if (!createUser.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            user = await _userManager.FindByEmailAsync(model.Email);

            Client client = new Client()
            {
                UserId = user.Id,
                Name = model.Name,
                Address = model.Address,
                IdNo = model.IdNo,
                ClientTypeId = model.ClientTypeId,
                CityId = model.CityId,
                CountryId = model.CountryId
            };

            await _context.AddAsync(client);
            await _context.SaveChangesAsync();


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
