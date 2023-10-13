using EduMapBackendProject.Areas.Admin.ViewModels;
using EduMapBackendProject.Areas.Admin.ViewModels.Slider;
using EduMapBackendProject.DAL;
using EduMapBackendProject.DAL.Entities;
using EduMapBackendProject.Extension;
using Microsoft.AspNetCore.Mvc;

namespace EduMapBackendProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly EduMapDbContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SliderController(EduMapDbContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = dataContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult List()
        {
            return View(_dataContext.Sliders.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Create(CreateViewModel sliderViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!sliderViewModel.Photo.CheckImage())
            {
                ModelState.AddModelError("Photo", "Add only photo");
                return View();
            }

            if (sliderViewModel.Photo.CheckImageSize(1000))
            {
                ModelState.AddModelError("Photo", "Size is high");
                return View();
            }

            var slider = new Slider
            {
                Title = sliderViewModel.Title,
                Description = sliderViewModel.Description,
            };

            string fileName = Guid.NewGuid() + sliderViewModel.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                sliderViewModel.Photo.CopyTo(stream);

            };

            slider.Url = fileName;

            _dataContext.Sliders.Add(slider);

            _dataContext.SaveChanges();

            return RedirectToAction("List");
        }
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();

            var sliderEdit = _dataContext.Sliders.FirstOrDefault(s => s.Id == id);

            if (sliderEdit == null) return NotFound();

            var sliderModel = new UpdateViewModel
            {
                Id = sliderEdit.Id,
                Title = sliderEdit.Title,
                Description = sliderEdit.Description
            };

            return View(sliderModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(UpdateViewModel sliderUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                var slider = _dataContext.Sliders.FirstOrDefault(s => s.Id == sliderUpdateViewModel.Id);

                if (slider == null) return NotFound();

                slider.Title = sliderUpdateViewModel.Title;
                slider.Description = sliderUpdateViewModel.Description;

                if (sliderUpdateViewModel.Photo != null)
                {
                    if (!sliderUpdateViewModel.Photo.CheckImage())
                    {
                        ModelState.AddModelError("Photo", "Only Photo.");
                        return View(sliderUpdateViewModel);
                    }

                    if (sliderUpdateViewModel.Photo.CheckImageSize(1000))
                    {
                        ModelState.AddModelError("Photo", "Size is high.");
                        return View(sliderUpdateViewModel);
                    }


                    var imagePathToDelete = Path.Combine(_webHostEnvironment.WebRootPath, "img", slider.Url);
                    if (System.IO.File.Exists(imagePathToDelete))
                    {
                        System.IO.File.Delete(imagePathToDelete);
                    }

                    string fileName = Guid.NewGuid() + sliderUpdateViewModel.Photo.FileName;
                    string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        sliderUpdateViewModel.Photo.CopyTo(stream);

                    };

                    slider.Url = fileName;

                }

                _dataContext.SaveChanges();

            }

            return RedirectToAction("Index");


        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var existSlider = _dataContext.Sliders.FirstOrDefault(s => s.Id == id);
            if (existSlider == null) return NotFound();
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", existSlider.Url);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _dataContext.Sliders.Remove(existSlider);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
