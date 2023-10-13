using EduMapBackendProject.Areas.Admin.ViewModels.FeedBack;
using EduMapBackendProject.DAL;
using EduMapBackendProject.DAL.Entities;
using EduMapBackendProject.Extension;
using Microsoft.AspNetCore.Mvc;

namespace EduMapBackendProject.Areas.Admin.Controllers
{
    [Area("admin")]
    public class FeedBackController : Controller
    {
        private readonly EduMapDbContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FeedBackController(EduMapDbContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = dataContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult List()
        {
            return View(_dataContext.FeedBacks.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(FeedBackCreateVM feedBackCreateVM)
        {
            if (!ModelState.IsValid) return View();

            var feedBack = new FeedBack
            {
                FullName = feedBackCreateVM.FullName,
                Content = feedBackCreateVM.Content,
                Position = feedBackCreateVM.Position
            };

            if (!feedBackCreateVM.Image.CheckImage())
            {
                ModelState.AddModelError("Photo", "Add only photo");
                return View();
            }

            if (feedBackCreateVM.Image.CheckImageSize(1000))
            {
                ModelState.AddModelError("Photo", "Size is high");
                return View();
            }


            string fileName = Guid.NewGuid() + feedBackCreateVM.Image.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                feedBackCreateVM.Image.CopyTo(stream);

            };

            feedBack.ImagePath = fileName;

            _dataContext.FeedBacks.Add(feedBack);
            _dataContext.SaveChanges();
            return RedirectToAction("List", "feedback");
        }

        public IActionResult Update(int id)
        {
            var feedBack = _dataContext.FeedBacks.FirstOrDefault(fb => fb.Id == id);
            if (feedBack == null) return NotFound();

            var feedBackModel = new FeedBackUpdateVM
            {
                Id = feedBack.Id,
                FullName = feedBack.FullName,
                Content = feedBack.Content,
                Position = feedBack.Position
            };

            return View(feedBack);
        }

        [HttpPost]
        public IActionResult Update(FeedBackUpdateVM feedBackUpdateVM)
        {
            var feedBack = _dataContext.FeedBacks.FirstOrDefault(fb => fb.Id == feedBackUpdateVM.Id);
            if (feedBack == null) return NotFound();

            feedBack.FullName = feedBack.FullName;
            feedBack.Content = feedBack.Content;
            feedBack.Position = feedBack.Position;

            if(feedBackUpdateVM.Image != null)
            {
                if (!feedBackUpdateVM.Image.CheckImage())
                {
                    ModelState.AddModelError("Photo", "Only Photo.");
                    return View(feedBackUpdateVM);
                }

                if (feedBackUpdateVM.Image.CheckImageSize(1000))
                {
                    ModelState.AddModelError("Photo", "Size is high.");
                    return View(feedBackUpdateVM);
                }

                var imagePathToDelete = Path.Combine(_webHostEnvironment.WebRootPath, "img", feedBack.ImagePath);
                if (System.IO.File.Exists(imagePathToDelete))
                {
                    System.IO.File.Delete(imagePathToDelete);
                }

                string fileName = Guid.NewGuid() + feedBackUpdateVM.Image.FileName;
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    feedBackUpdateVM.Image.CopyTo(stream);

                };

                feedBack.ImagePath = fileName;
            }

            _dataContext.SaveChanges();

            return RedirectToAction("List", "feedback");
        }

        public IActionResult Delete(int id)
        {
            var feedBack = _dataContext.FeedBacks.FirstOrDefault(fb => fb.Id == id);
            if (feedBack == null) return NotFound();

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", feedBack.ImagePath);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _dataContext.FeedBacks.Remove(feedBack);

            _dataContext.SaveChanges();
            return RedirectToAction("List", "feedback");
        }
    }
}
