using EduMapBackendProject.DAL;
using EduMapBackendProject.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduMapBackendProject.Controllers
{
    public class TeacherController : Controller
    {
        private readonly EduMapDbContext _dataContext;

        public TeacherController(EduMapDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            List<Teacher> teachers = _dataContext.Teachers.ToList();

            return View(teachers);
        }

        public IActionResult Detail(int id)
        {
            if (id == 0) return NotFound();

            Teacher teacher = _dataContext.Teachers
                .Include(t => t.TeacherSkill)
                .Include(t => t.TeacherSocial)
                .FirstOrDefault(t => t.Id == id);

            return View(teacher);
        }
    }
}
