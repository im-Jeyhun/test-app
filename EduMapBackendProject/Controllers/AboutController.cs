using EduMapBackendProject.DAL;
using Microsoft.AspNetCore.Mvc;

namespace EduMapBackendProject.Controllers
{
    public class AboutController : Controller
    {
        private readonly EduMapDbContext _dataContext;

        public AboutController(EduMapDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
