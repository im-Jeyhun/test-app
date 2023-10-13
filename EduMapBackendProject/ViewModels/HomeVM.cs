using EduMapBackendProject.DAL.Entities;
namespace EduMapBackendProject.ViewModels
{
    public class HomeVM
    {
        public List<Slider>? Sliders { get; set; }
        public List<Blog>? Blogs { get; set; }
        public List<Event>? Events { get; set; }
        public List<Course>? Courses { get; set; }
        public List<FeedBack>? FeedBacks { get; set; }
        public HeaderSettingVM? HeaderSetting { get; set; }
        public FooterSettingVM FooterSetting { get; set; }

    }

    public class HeaderSettingVM
    {
        public string? Logo { get; set; }
        public string? LogoPath { get; set; }
    }

    public class FooterSettingVM
    {
        public List<IconVm> Icons { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
    }

    public class IconVm
    {
        public string IconPath { get; set; }
        public string IconClass { get; set; }
    }
}
