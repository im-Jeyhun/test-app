using EduMapBackendProject.DAL.Entities;
using EduMapBackendProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduMapBackendProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM account)
        {
            if (!ModelState.IsValid) return View();

            User user = new User
            {
                FullName = account.FullName,
                UserName = account.UserName,
                Email = account.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(user, account.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(account);
            }

            await _userManager.AddToRoleAsync(user, "Admin");

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login, string? ReturnUrl)
        {
            if (!ModelState.IsValid) return View();

            User user = await _userManager.FindByEmailAsync(login.UserNameOrEmail);

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(login.UserNameOrEmail);

                if (user == null)
                {
                    ModelState.AddModelError("", "Username and Email or password is incorrect");
                    return View(login);
                }

            }

            var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Due to overtrying your account has been blocked for 5 minute");
                    return View();
                }

                ModelState.AddModelError("", "Username and Email or password is incorrect");
                return View();
            }

            await _signInManager.SignInAsync(user, login.RememberMe);

            if (ReturnUrl != null)
            {
                return Redirect(ReturnUrl);
            }

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var item in roles)
            {
                if (item == "Admin")
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "EduHomeArea" });
                }
            }
            return RedirectToAction("index", "home");
        }
    }
}
