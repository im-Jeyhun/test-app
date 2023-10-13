using EduMapBackendProject.Areas.Admin.ViewModels.Role;
using EduMapBackendProject.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduMapBackendProject.Areas.EduHomeArea.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly UserManager<User> _userManager;

        public RoleController(RoleManager<IdentityRole> rolemanager, UserManager<User> userManager)
        {
            _rolemanager = rolemanager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(_rolemanager.Roles.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            if (string.IsNullOrEmpty(roleName)) return NotFound();

            await _rolemanager.CreateAsync(new IdentityRole { Name = roleName });

            return RedirectToAction("index", "role");
        }

        public async Task<IActionResult> Update(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            UpdateRoleViewModel editRoleVM = new();

            editRoleVM.UserRoles = await _userManager.GetRolesAsync(user);
            editRoleVM.Roles = _rolemanager.Roles.ToList();
            editRoleVM.User = user;


            return View(editRoleVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var oldRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, oldRoles);
            await _userManager.AddToRolesAsync(user, roles);

            return RedirectToAction("Index", "Role");
        }


    }
}
