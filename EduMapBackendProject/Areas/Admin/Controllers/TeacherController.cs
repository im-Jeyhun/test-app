using EduMapBackendProject.Areas.Admin.ViewModels.TeacherVM;
using EduMapBackendProject.DAL;
using EduMapBackendProject.DAL.Entities;
using EduMapBackendProject.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduMapBackendProject.Areas.Admin.Controllers
{
    [Area("admin")]
    public class TeacherController : Controller
    {
        private readonly EduMapDbContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TeacherController(EduMapDbContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = dataContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult List()
        {

            var teachers = _dataContext.Teachers.Include(t => t.TeacherSkill)
                                                .Include(t => t.TeacherSocial)
                                                .ToList();
            return View(teachers);

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TeacherCreateViewModel teacherCreateViewModel)
        {

            if (!ModelState.IsValid) return View();

            Teacher teacher = new()
            {
                FullName = teacherCreateViewModel.FullName,
                Position = teacherCreateViewModel.Position,
                Description = teacherCreateViewModel.Description,
                Degree = teacherCreateViewModel.Degree,
                Experience = teacherCreateViewModel.Experience,
                Faculty = teacherCreateViewModel.Faculty,
                Mail = teacherCreateViewModel.Mail,
                CallNumber = teacherCreateViewModel.CallNumber
            };

            Skill skill = new()
            {
                Language = teacherCreateViewModel.Language,
                TeamLeader = teacherCreateViewModel.TeamLeader,
                Development = teacherCreateViewModel.Development,
                Design = teacherCreateViewModel.Design,
                Innovation = teacherCreateViewModel.Innovation,
                Communication = teacherCreateViewModel.Communication,
                Teacher = teacher

            };

            Social social = new()
            {
                FaceBook = teacherCreateViewModel.FaceBook,
                Pinterest = teacherCreateViewModel.Pinterest,
                Vimeo = teacherCreateViewModel.Vimeo,
                Twitter = teacherCreateViewModel.Twitter,
                Teacher = teacher

            };

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Photo", "Add photo");
                return View();
            }

            if (!teacherCreateViewModel.Photo.CheckImage())
            {
                ModelState.AddModelError("Photo", "Add only photo");
                return View();
            }

            if (teacherCreateViewModel.Photo.CheckImageSize(1000))
            {
                ModelState.AddModelError("Photo", "Size is high");
                return View();
            }

            string fileName = Guid.NewGuid() + teacherCreateViewModel.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                teacherCreateViewModel.Photo.CopyTo(stream);

            };

            teacher.ImagePath = fileName;
            teacher.TeacherSkill = skill;
            teacher.TeacherSocial = social;


            _dataContext.Socials.Add(social);
            _dataContext.Skills.Add(skill);
            _dataContext.Teachers.Add(teacher);

            _dataContext.SaveChanges();
            return RedirectToAction("List");

        }
       
        public IActionResult Update(int? id)
        {
            var teacher = _dataContext.Teachers
                .Include(t => t.TeacherSkill)
                .Include(t => t.TeacherSocial)
                .FirstOrDefault(b => b.Id == id);

            if (teacher == null) return NotFound();

            var teacherUpdateModel = new TeacherUpdateViewModel
            {
                Id = teacher.Id,
                FullName = teacher.FullName,
                Position = teacher.Position,
                Description = teacher.Description,
                Degree = teacher.Degree,
                Experience = teacher.Experience,
                Faculty = teacher.Faculty,
                Mail = teacher.Mail,
                CallNumber = teacher.CallNumber,
                Language = teacher.TeacherSkill.Language,
                TeamLeader = teacher.TeacherSkill.TeamLeader,
                Development = teacher.TeacherSkill.Development,
                Design = teacher.TeacherSkill.Design,
                Innovation = teacher.TeacherSkill.Innovation,
                Communication = teacher.TeacherSkill.Communication,
                FaceBook = teacher.TeacherSocial.FaceBook,
                Pinterest = teacher.TeacherSocial.Pinterest,
                Vimeo = teacher.TeacherSocial.Vimeo,
                Twitter = teacher.TeacherSocial.Twitter,
            };

            return View(teacherUpdateModel);

        }

        [HttpPost]
        public IActionResult Update(TeacherUpdateViewModel teacherUpdateViewModel)
        {
            if (!ModelState.IsValid) return View();

            var teacher = _dataContext.Teachers
                .Include(t => t.TeacherSkill)
                .Include(t => t.TeacherSocial)
                .FirstOrDefault(t => t.Id == teacherUpdateViewModel.Id);

            if (teacher == null) return NotFound();

            teacher.FullName = teacherUpdateViewModel.FullName;
            teacher.Position = teacherUpdateViewModel.Position;
            teacher.Description = teacherUpdateViewModel.Description;
            teacher.Degree = teacherUpdateViewModel.Degree;
            teacher.Experience = teacherUpdateViewModel.Experience;
            teacher.Faculty = teacherUpdateViewModel.Faculty;
            teacher.Mail = teacherUpdateViewModel.Mail;
            teacher.CallNumber = teacherUpdateViewModel.CallNumber;
            teacher.TeacherSkill.Language = teacherUpdateViewModel.Language;
            teacher.TeacherSkill.TeamLeader = teacherUpdateViewModel.TeamLeader;
            teacher.TeacherSkill.Development = teacherUpdateViewModel.Development;
            teacher.TeacherSkill.Design = teacherUpdateViewModel.Design;
            teacher.TeacherSkill.Innovation = teacherUpdateViewModel.Innovation;
            teacher.TeacherSkill.Communication = teacherUpdateViewModel.Communication;
            teacher.TeacherSocial.FaceBook = teacherUpdateViewModel.FaceBook;
            teacher.TeacherSocial.Pinterest = teacherUpdateViewModel.Pinterest;
            teacher.TeacherSocial.Vimeo = teacherUpdateViewModel.Vimeo;
            teacher.TeacherSocial.Twitter = teacherUpdateViewModel.Twitter;


            if (teacherUpdateViewModel.Image != null)
            {
                if (!teacherUpdateViewModel.Image.CheckImage())
                {
                    ModelState.AddModelError("ImagePath", "Only Photo.");
                    return View(teacherUpdateViewModel);
                }

                if (teacherUpdateViewModel.Image.CheckImageSize(1000))
                {
                    ModelState.AddModelError("ImagePath", "Size is high.");
                    return View(teacherUpdateViewModel);
                }

                var imagePathToDelete = Path.Combine(_webHostEnvironment.WebRootPath, "img", teacher.ImagePath);
                if (System.IO.File.Exists(imagePathToDelete))
                {
                    System.IO.File.Delete(imagePathToDelete);
                }

                string fileName = Guid.NewGuid() + teacherUpdateViewModel.Image.FileName;
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    teacherUpdateViewModel.Image.CopyTo(stream);

                };

                teacher.ImagePath = fileName;
            }


            _dataContext.SaveChanges();
            return RedirectToAction("List");

        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var teacher = _dataContext.Teachers.FirstOrDefault(t => t.Id == id);

            if (teacher == null) return NotFound();

            _dataContext.Teachers.Remove(teacher);

            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }

}
