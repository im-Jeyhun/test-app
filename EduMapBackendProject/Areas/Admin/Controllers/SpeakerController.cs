using EduMapBackendProject.Areas.Admin.ViewModels.SpeakerVM;
using EduMapBackendProject.DAL;
using EduMapBackendProject.DAL.Entities;
using EduMapBackendProject.Extension;
using Microsoft.AspNetCore.Mvc;

namespace EduMapBackendProject.Areas.Admin.Controllers
{
    [Area("admin")]
    public class SpeakerController : Controller
    {
        private readonly EduMapDbContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SpeakerController(EduMapDbContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = dataContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult List()
        {
            return View(_dataContext.Speakers.ToList());
        }

        public IActionResult Create()
        {
            return View(new SpeakerCreateViewModel());
        }

        [HttpPost]
        public IActionResult Create(SpeakerCreateViewModel speakerCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(speakerCreateViewModel);
            }
            if (!speakerCreateViewModel.Photo.CheckImage())
            {
                ModelState.AddModelError("Photo", "Add only photo");
                return View();
            }

            if (speakerCreateViewModel.Photo.CheckImageSize(1000))
            {
                ModelState.AddModelError("Photo", "Size is high");
                return View();
            }
            
            var speaker = new Speaker
            {
                FullName = speakerCreateViewModel.FullName,
                Position = speakerCreateViewModel.Position
            };

            string fileName = Guid.NewGuid() + speakerCreateViewModel.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                speakerCreateViewModel.Photo.CopyTo(stream);

            };

            speaker.ImagePath = fileName;

            _dataContext.Speakers.Add(speaker);
            _dataContext.SaveChanges();

            return RedirectToAction("List");
        }

        public IActionResult Update(int? id)
        {
            var speaker = _dataContext.Speakers.FirstOrDefault(s => s.Id == id);

            if (speaker == null) return NotFound();

            var speakerUpdateModel = new SpeakerUpdateViewModel
            {
                Id = speaker.Id,
                FullName = speaker.FullName,
                Position = speaker.Position
            };

            return View(speakerUpdateModel);
        }

        [HttpPost]
        public IActionResult Update(SpeakerUpdateViewModel speakerUpdateViewModel)
        {
            if (!ModelState.IsValid) return View(speakerUpdateViewModel);

            var speaker = _dataContext.Speakers.FirstOrDefault(s => s.Id == speakerUpdateViewModel.Id);

            if (speaker == null) return NotFound();

            speaker.FullName = speakerUpdateViewModel.FullName;
            speaker.Position = speakerUpdateViewModel.Position;

            if(speakerUpdateViewModel.Photo != null)
            {

                if (!speakerUpdateViewModel.Photo.CheckImage())
                {
                    ModelState.AddModelError("ImagePath", "Only Photo.");
                    return View(speakerUpdateViewModel);
                }

                if (speakerUpdateViewModel.Photo.CheckImageSize(1000))
                {
                    ModelState.AddModelError("ImagePath", "Size is high.");
                    return View(speakerUpdateViewModel);
                }

                var imagePathToDelete = Path.Combine(_webHostEnvironment.WebRootPath, "img", speaker.ImagePath);
                if (System.IO.File.Exists(imagePathToDelete))
                {
                    System.IO.File.Delete(imagePathToDelete);
                }

                string fileName = Guid.NewGuid() + speakerUpdateViewModel.Photo.FileName;
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    speakerUpdateViewModel.Photo.CopyTo(stream);

                };

                speaker.ImagePath = fileName;
            }

            _dataContext.SaveChanges();

            return RedirectToAction("List");
        }

        public IActionResult Delete(int id)
        {
            var speaker = _dataContext.Speakers.FirstOrDefault(s => s.Id == id);

            if (speaker == null) return NotFound();

            _dataContext.Speakers.Remove(speaker);

            _dataContext.SaveChanges();

            return RedirectToAction("List");

        }

    }
}
