using EduMapBackendProject.DAL;
using EduMapBackendProject.DAL.Entities;
using EduMapBackendProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduMapBackendProject.ViewComponents
{
    public class HeaderViewComponent: ViewComponent
    {
        private readonly UserManager<User> _userManager;
        public readonly EduMapDbContext _dataContext;
        public HeaderViewComponent(EduMapDbContext dataContext, UserManager<User> userManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            HomeVM vm = new HomeVM
            {
                HeaderSetting = _dataContext.HeaderSettings.Select(hs => new HeaderSettingVM
                {
                    Logo = hs.Logo,
                    LogoPath = hs.LogoPath
                }).FirstOrDefault()
            };

            ViewBag.UserFullName = null;

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                ViewBag.UserFullName = user.FullName;

            }

            return View(await Task.FromResult(vm));
        }
    }
}
