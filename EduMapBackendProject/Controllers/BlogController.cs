using EduMapBackendProject.DAL;
using EduMapBackendProject.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EduMapBackendProject.Controllers
{
    public class BlogController : Controller
    {
        private readonly EduMapDbContext _dataContext;

        public BlogController(EduMapDbContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IActionResult Index()
        {
            List<Blog> blogs = _dataContext.Blogs.ToList();
            return View(blogs);
        }

        public IActionResult Detail(int id)
        {
            if (id == 0) return NotFound();

            Blog blog = _dataContext!.Blogs!.FirstOrDefault(b => b.Id == id);

            if (blog is null) return NotFound();

            return View(blog);
        }
    }
}
