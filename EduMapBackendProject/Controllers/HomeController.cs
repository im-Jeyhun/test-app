using EduMapBackendProject.DAL;
using EduMapBackendProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EduMapBackendProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly EduMapDbContext _dataContext;

        public HomeController(EduMapDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            var HomeViewModel = new HomeVM
            {
                Sliders = _dataContext.Sliders.ToList(),
                Events = _dataContext.Events.ToList(),
                Courses = _dataContext.Courses.ToList(),
                FeedBacks = _dataContext.FeedBacks.ToList(),
                Blogs = _dataContext.Blogs.ToList()
            };

            return View(HomeViewModel);
        }
    }
}
