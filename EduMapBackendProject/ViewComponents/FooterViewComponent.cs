using EduMapBackendProject.DAL;
using EduMapBackendProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EduMapBackendProject.ViewComponents
{
    public class FooterViewComponent: ViewComponent
    {
        public readonly EduMapDbContext _dataContext;
        public FooterViewComponent(EduMapDbContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            HomeVM vm = new HomeVM
            {
                FooterSetting = _dataContext!.FooterSettings!.Select(fs => new FooterSettingVM
                {
                    Icons = _dataContext!.Icons.Where(i => i.FooterSettingId == fs.Id)!.Select(i => new IconVm
                    {
                        IconClass = i.IconClass,
                        IconPath = i.IconPath
                    }).ToList(),
                    Address = fs.Address,
                    Email = fs.Email,
                    TelephoneNumber = fs.TelephoneNumber

                })!.FirstOrDefault()
            };

            return View(await Task.FromResult(vm));
        }
    }
}
