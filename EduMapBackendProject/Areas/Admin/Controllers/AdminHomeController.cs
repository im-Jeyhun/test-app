using Microsoft.AspNetCore.Mvc;

namespace EduMapBackendProject.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class AdminHomeController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
