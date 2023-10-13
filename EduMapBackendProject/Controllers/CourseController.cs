using EduMapBackendProject.DAL;
using EduMapBackendProject.DAL.Entities;
using EduMapBackendProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduMapBackendProject.Controllers
{
    public class CourseController : Controller
    {
        private readonly EduMapDbContext _dataContext;
        public CourseController(EduMapDbContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IActionResult Index()
        {
            List<Course> courses = _dataContext.Courses.ToList();

            return View(courses);
        }

        public IActionResult Detail(int id)
        {
            CourseVM courseVM = new CourseVM
            {
                Blogs = _dataContext.Blogs.Take(3).ToList(),

                Course = _dataContext!.Courses!.Include(c => c.Feature)!
                .FirstOrDefault(c => c.Id == id)
            };

            return View(courseVM);
        }

    }
}
