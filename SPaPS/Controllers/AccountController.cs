using SPaPS.Models.AccountModels;
using Microsoft.AspNetCore.Mvc;
using SPaPS.Models;
using Microsoft.AspNetCore.Identity;
using SPaPS.Data;
using DataAccess.Services;
using SPaPS.Helpers;
using Microsoft.AspNetCore.Authorization;
using NuGet.Common;
using Postal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace SPaPS.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SPaPSContext _context;
        private readonly IEmailSenderEnhance _emailService;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, SPaPSContext context, IEmailSenderEnhance emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Login()
        {

            if (TempData["Success"] != null)
                ModelState.AddModelError("Success", Convert.ToString(TempData["Success"]));


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            var result = await _signInManager.PasswordSignInAsync(userName: model.Email, password: model.Password, isPersistent: false, lockoutOnFailure: true);

            if (!result.Succeeded || result.IsLockedOut || result.IsNotAllowed)
            {
                ModelState.AddModelError("Error", "Погрешно корисничко име или лозинка!");
                return View(model);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.ClientTypes = new SelectList(_context.References.Where(x => x.ReferenceTypeId == 1).ToList(), "ReferenceId", "Description");
            ViewBag.Cities = new SelectList(_context.References.Where(x => x.ReferenceTypeId == 3).ToList(), "ReferenceId", "Description");
            ViewBag.Countries = new SelectList(_context.References.Where(x => x.ReferenceTypeId == 4).ToList(), "ReferenceId", "Description");
            ViewBag.Roles = new SelectList(_roleManager.Roles.ToList(), "Name", "Name"); 
            ViewBag.Activities = new SelectList(_context.Activities.ToList(), "ActivityId", "Name");
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }

            var userExists = await _userManager.FindByEmailAsync(model.Email);

            if (userExists != null)
            {
                ModelState.AddModelError("Error", "Корисникот веќе постои!");
                return View(model);
            }

            IdentityUser user = new IdentityUser()
            {
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var newPassword = Shared.GeneratePassword(8);

            var createUser = await _userManager.CreateAsync(user, newPassword);

            if (!createUser.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            await _userManager.AddToRoleAsync(user, model.Role);

            Client client = new Client()
            {
                UserId = user.Id,
                Name = model.Name,
                Address = model.Address,
                IdNo = model.IdNo,
                ClientTypeId = model.ClientTypeId,
                CityId = model.CityId,
                CountryId = model.CountryId,
                NoOfEmployees=model.NoOfEmployees,
                DateEstablished=model.DateOfEstablishment
                
            };
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();

            //ClientActivity clientActivity = new ClientActivity()
            //{
            //    ActivityId=model.Activities,
  
            //};
            //await _context.ClientActivities.AddAsync(clientActivity);
            //await _context.SaveChangesAsync();




            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callback = Url.Action(action: "ResetPassword", controller: "Account", values: new { token, email = user.Email }, HttpContext.Request.Scheme);

            /* https://localhost:5001/Account/ResetPassword?token=123asdrew123&email=nikola.stankovski@foxit.mk */

            EmailSetUp emailSetUp = new EmailSetUp()
            {
                To = user.Email,
                Template = "Register",
                Username = user.Email,
                Callback = callback,
                Token = token,
                RequestPath = _emailService.PostalRequest(Request),
            };

            await _emailService.SendEmailAsync(emailSetUp);

            TempData["Success"] = "Успешно креиран корисник!";

            return RedirectToAction(nameof(Login));
        }



        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]

        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            var user = await _userManager.FindByEmailAsync(model.Email);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callback = Url.Action(action: "ResetPassword", controller: "Account", values: new { token, email = user.Email }, HttpContext.Request.Scheme);


            EmailSetUp emailSetUp = new EmailSetUp()
            {
                To = user.Email,
                Template = "ResetPassword",
                Username = user.Email,
                Callback = callback,
                Token = token,
                RequestPath = _emailService.PostalRequest(Request),
            };

            await _emailService.SendEmailAsync(emailSetUp);

            TempData["Success"] = "Ве молиме проверете го вашето влезно сандаче!";

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {

            ResetPasswordModel model = new ResetPasswordModel()
            {
                Email = email,
                Token = token
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            var user = await _userManager.FindByEmailAsync(model.Email);

            var resetPassword = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if (!resetPassword.Succeeded)
            {
                ModelState.AddModelError("Error", "Се случи грешка. Обидете се повторно!");
                return View();
            }

            TempData["Success"] = "Успешно промената лозинка!";

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            return RedirectToAction(nameof(Login));


            var loggedInUserEmail = User.Identity.Name;

            var user = await _userManager.FindByEmailAsync(loggedInUserEmail);

            var changePassword = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!changePassword.Succeeded)
            {
                ModelState.AddModelError("Error", "Се случи грешка. Обидете се повторно!");
                return View();
            }

            ModelState.AddModelError("Success", "Успешно променета лозинка!");

            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditProfileInfo()
        {
            var loggedInUserEmail = User.Identity.Name;

            var appUser = await _userManager.FindByEmailAsync(loggedInUserEmail);
            var userRole = await _userManager.GetRolesAsync(appUser);
            var clientUser = await _context.Clients.Where(x => x.UserId == appUser.Id).FirstOrDefaultAsync();
            var activity = await _context.ClientActivities.Where(a => a.ClientId == clientUser.ClientId).Select(x => x.ActivityId).ToListAsync();


            ViewBag.ClientTypes = new SelectList(_context.References.Where(x => x.ReferenceTypeId == 1).ToList(), "ReferenceId", "Description");
            ViewBag.Cities = new SelectList(_context.References.Where(x => x.ReferenceTypeId == 3).ToList(), "ReferenceId", "Description");
            ViewBag.Countries = new SelectList(_context.References.Where(x => x.ReferenceTypeId == 4).ToList(), "ReferenceId", "Description");
            ViewBag.Activities = new SelectList(_context.Activities.ToList(), "ActivityId", "Name");



            EditProfileInfoModel model = new EditProfileInfoModel()
            {
                Role = String.Join(", ", userRole),
                Name = clientUser.Name,
                Address = clientUser.Address,
                CityId = clientUser.CityId,
                CountryId = clientUser.CountryId,
                ClientTypeId = clientUser.ClientTypeId,
                IdNo = clientUser.IdNo,
                PhoneNumber = appUser.PhoneNumber,
                NoOfEmployees= clientUser.NoOfEmployees,
                DateOfEstablishment = clientUser.DateEstablished,
                Activities = activity
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditProfileInfo(EditProfileInfoModel model)
        {
            ViewBag.ClientTypes = new SelectList(_context.References.Where(x => x.ReferenceTypeId == 1).ToList(), "ReferenceId", "Description");
            ViewBag.Cities = new SelectList(_context.References.Where(x => x.ReferenceTypeId == 3).ToList(), "ReferenceId", "Description");
            ViewBag.Countries = new SelectList(_context.References.Where(x => x.ReferenceTypeId == 4).ToList(), "ReferenceId", "Description");
            ViewBag.Activities = new SelectList(_context.Activities.ToList(), "ActivityId", "Name");


            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Се случи грешка. Обидете се повторно.");

                return View(model);
            }

            var loggedInUserEmail = User.Identity.Name;

            var appUser = await _userManager.FindByEmailAsync(loggedInUserEmail);
            var clientUser = await _context.Clients.Where(x => x.UserId == appUser.Id).FirstOrDefaultAsync();
            var activity = await _context.ClientActivities.Where(a => a.ClientId == clientUser.ClientId).Select(x => x.ActivityId).ToListAsync();

            appUser.PhoneNumber = model.PhoneNumber;
            activity = model.Activities;

            var appUserResult = await _userManager.UpdateAsync(appUser);
            //var activityS =  _context.Update(clientUser);
            if (!appUserResult.Succeeded)
            {
                ModelState.AddModelError("Error", "Се случи грешка. Обидете се повторно.");

                return View(model);
            }

            clientUser.Address = model.Address;
            clientUser.Name = model.Name;
            clientUser.CityId = model.CityId;
            clientUser.CountryId = model.CountryId;
            clientUser.ClientTypeId = model.ClientTypeId;
            clientUser.IdNo = model.IdNo;
            clientUser.UpdatedOn = DateTime.Now;
            clientUser.NoOfEmployees = model.NoOfEmployees;
            clientUser.DateEstablished = model.DateOfEstablishment;
            //activity = model.Activities;
            

            try
            {
                _context.Clients.Update(clientUser);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error", "Се случи грешка. Обидете се повторно.");

                return View(model);
            }


            ModelState.AddModelError("Success", "Успешно променети информации");

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}