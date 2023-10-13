using EduMapBackendProject.Areas.Admin.ViewModels.Course;
using EduMapBackendProject.DAL;
using EduMapBackendProject.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EduMapBackendProject.Areas.Admin.Controllers
{
    [Area("admin")]
    public class CourseController : Controller
    {
        private readonly EduMapDbContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CourseController(EduMapDbContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = dataContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult List()
        {
            return View(_dataContext.Courses.ToList());
        }

        public IActionResult Create()
        {
            return View(new CourseCreateViewModel());
        }

        [HttpPost]
        public IActionResult Create(CourseCreateViewModel courseCreateViewModel)
        {
            if (!ModelState.IsValid) return View(courseCreateViewModel);

            var course = new Course
            {
                Title = courseCreateViewModel.Title,
                Description = courseCreateViewModel.Description
            };

            string fileName = Guid.NewGuid() + courseCreateViewModel.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                courseCreateViewModel.Photo.CopyTo(stream);

            };

            course.ImagePath = fileName;

            _dataContext.Courses.Add(course);

            _dataContext.SaveChanges();

            return RedirectToAction("List");
        }

        public IActionResult Update(int? id)
        {
            var course = _dataContext.Courses.FirstOrDefault(c => c.Id == id);

            if (course == null) return NotFound();

            var courseModel = new CourseUpdateViewModel
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description
            };

            return View(courseModel);
        }

        [HttpPost]
        public IActionResult Update(CourseUpdateViewModel courseUpdateViewModel)
        {
            var course = _dataContext.Courses.FirstOrDefault(c => c.Id == courseUpdateViewModel.Id);

            if (course == null) return NotFound();

            course.Title = courseUpdateViewModel.Title;
            course.Description = courseUpdateViewModel.Description;

            if(courseUpdateViewModel.Photo != null)
            {
                var imagePathToDelete = Path.Combine(_webHostEnvironment.WebRootPath, "img", course.ImagePath);
                if (System.IO.File.Exists(imagePathToDelete))
                {
                    System.IO.File.Delete(imagePathToDelete);
                }

                string fileName = Guid.NewGuid() + courseUpdateViewModel.Photo.FileName;
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    courseUpdateViewModel.Photo.CopyTo(stream);

                };

                course.ImagePath = fileName;
            }

            _dataContext.Courses.Add(course);

            _dataContext.SaveChanges();

            return View("List");

        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var course = _dataContext.Courses.FirstOrDefault(c => c.Id == id);

            if (course == null) return NotFound();

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", course.ImagePath);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _dataContext.Courses.Remove(course);

            _dataContext.SaveChanges();

            return RedirectToAction("List");
        }
    }
}
