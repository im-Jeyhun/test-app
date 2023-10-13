using EduMapBackendProject.Areas.Admin.ViewModels.CourseFeatureVM;
using EduMapBackendProject.DAL;
using EduMapBackendProject.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EduMapBackendProject.Areas.Admin.Controllers
{
    [Area("admin")]
    public class CourseFeatureController : Controller
    {
        private readonly EduMapDbContext _dataContext;

        public CourseFeatureController(EduMapDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult List(int courseId)
        {
            if (!_dataContext.Courses.Any(c => c.Id == courseId)) return NotFound();

            var courseFeutureListViewModel = new CourseListViewModel
            {
                CourseId = courseId,
                CourseFeatures = _dataContext.CourseFeatures.Where(cf => cf.CourseId == courseId).ToList()
            };

            return View(courseFeutureListViewModel);
        }

        public IActionResult Create(int courseId)
        {
            return View(new CourseFeatureCreateVM
            {
                CourseId = courseId
            });
        }

        [HttpPost]
        public IActionResult Create(CourseFeatureCreateVM courseFeatureCreateVM)
        {
            var courseFeuture = new CourseFeature
            {
                Starts = courseFeatureCreateVM.Starts,
                Duration = courseFeatureCreateVM.Duration,
                ClassDuration = courseFeatureCreateVM.ClassDuration,
                SkillLevel = courseFeatureCreateVM.SkillLevel,
                Language = courseFeatureCreateVM.Language,
                StudentsCount = courseFeatureCreateVM.StudentsCount,
                Assesments = courseFeatureCreateVM.Assesments,
                Fee = courseFeatureCreateVM.Fee,
                CourseId = courseFeatureCreateVM.CourseId,
            };

            _dataContext.CourseFeatures.Add(courseFeuture);

            _dataContext.SaveChanges();

            return RedirectToAction("List",new {courseId = courseFeatureCreateVM.CourseId});
        }

        public IActionResult Update(int id , int courseId)
        {
            var courseFeature = _dataContext.CourseFeatures.FirstOrDefault(cf => cf.Id == id && cf.CourseId == courseId);

            if (courseFeature == null) return NotFound();

            var courseFeuterModel = new CourseFeatureUpdateVM
            {
                Id = courseFeature.Id,
                Starts = courseFeature.Starts,
                Duration = courseFeature.Duration,
                ClassDuration = courseFeature.ClassDuration,
                SkillLevel = courseFeature.SkillLevel,
                Language = courseFeature.Language,
                StudentsCount = courseFeature.StudentsCount,
                Assesments = courseFeature.Assesments,
                Fee = courseFeature.Fee,
                CourseId = courseFeature.CourseId,
            };

            return View(courseFeuterModel);
        }

        [HttpPost]
        public IActionResult Update(CourseFeatureUpdateVM courseFeatureUpdateVM)
        {
            var courseFeature = _dataContext.CourseFeatures.FirstOrDefault(cf => cf.Id == courseFeatureUpdateVM.Id && cf.CourseId == courseFeatureUpdateVM.CourseId);

            if (courseFeature == null) return NotFound();

            courseFeature.Starts = courseFeatureUpdateVM.Starts;
            courseFeature.Duration = courseFeatureUpdateVM.Duration;
            courseFeature.ClassDuration = courseFeatureUpdateVM.ClassDuration;
            courseFeature.SkillLevel = courseFeatureUpdateVM.SkillLevel;
            courseFeature.Language = courseFeatureUpdateVM.Language;
            courseFeature.StudentsCount = courseFeatureUpdateVM.StudentsCount;
            courseFeature.Assesments = courseFeatureUpdateVM.Assesments;
            courseFeature.Fee = courseFeatureUpdateVM.Fee;

            _dataContext.SaveChanges();

            return RedirectToAction("List", new { courseId = courseFeatureUpdateVM.CourseId });

        }

        public IActionResult Delete(int id, int courseId)
        {
            var courseFeature = _dataContext.CourseFeatures.FirstOrDefault(cf => cf.Id == id && cf.CourseId == courseId);

            if (courseFeature == null) return NotFound();

            _dataContext.CourseFeatures.Remove(courseFeature);

            _dataContext.SaveChanges();

            return RedirectToAction("List", new { courseId = courseId });

        }


    }
}
