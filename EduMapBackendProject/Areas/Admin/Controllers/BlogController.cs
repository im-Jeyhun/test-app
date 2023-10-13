using EduMapBackendProject.Areas.Admin.ViewModels.Blog;
using EduMapBackendProject.DAL;
using EduMapBackendProject.DAL.Entities;
using EduMapBackendProject.Extension;
using Microsoft.AspNetCore.Mvc;

namespace EduMapBackendProject.Areas.Admin.Controllers
{
    [Area("admin")]
    public class BlogController : Controller
    {
        private readonly EduMapDbContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BlogController(EduMapDbContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = dataContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult List()
        {
            return View(_dataContext.Blogs.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BlogCreateViewModel blogCreateViewModel)
        {
            if (!ModelState.IsValid) return View();

            var blog = new Blog
            {
                Author = blogCreateViewModel.Author,
                Title = blogCreateViewModel.Title,
                Description = blogCreateViewModel.Description,
                Date = blogCreateViewModel.Date,
            };

            if (!blogCreateViewModel.Image.CheckImage())
            {
                ModelState.AddModelError("Photo", "Add only photo");
                return View();
            }

            if (blogCreateViewModel.Image.CheckImageSize(1000))
            {
                ModelState.AddModelError("Photo", "Size is high");
                return View();
            }


            string fileName = Guid.NewGuid() + blogCreateViewModel.Image.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                blogCreateViewModel.Image.CopyTo(stream);

            };

            blog.ImagePath = fileName;

            _dataContext.Blogs.Add(blog);
            _dataContext.SaveChanges();
            return RedirectToAction("List", "blog");
        }

        public IActionResult Update(int id)
        {
            var blog = _dataContext.Blogs.FirstOrDefault(b => b.Id == id);
            if (blog == null) return NotFound();

            var fblogViewModel = new BlogUpdateViewModel
            {
                Id = blog.Id,
                Author = blog.Author,
                Title = blog.Title,
                Description = blog.Description,
                Date = blog.Date,
            };

            return View(fblogViewModel);
        }

        [HttpPost]
        public IActionResult Update(BlogUpdateViewModel blogUpdateViewModel)
        {
            if (!ModelState.IsValid) return View(blogUpdateViewModel);

            var blog = _dataContext.Blogs.FirstOrDefault(b => b.Id == blogUpdateViewModel.Id);
            if (blog == null) return NotFound();

            blog.Title = blogUpdateViewModel.Title;
            blog.Author = blogUpdateViewModel.Author;
            blog.Description = blogUpdateViewModel.Author;
            blog.Date = blogUpdateViewModel.Date;

            if (blogUpdateViewModel.Image != null)
            {
                if (!blogUpdateViewModel.Image.CheckImage())
                {
                    ModelState.AddModelError("Photo", "Only Photo.");
                    return View(blogUpdateViewModel);
                }

                if (blogUpdateViewModel.Image.CheckImageSize(1000))
                {
                    ModelState.AddModelError("Photo", "Size is high.");
                    return View(blogUpdateViewModel);
                }

                var imagePathToDelete = Path.Combine(_webHostEnvironment.WebRootPath, "img", blog.ImagePath);
                if (System.IO.File.Exists(imagePathToDelete))
                {
                    System.IO.File.Delete(imagePathToDelete);
                }

                string fileName = Guid.NewGuid() + blogUpdateViewModel.Image.FileName;
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    blogUpdateViewModel.Image.CopyTo(stream);

                };

                blog.ImagePath = fileName;
            }

            _dataContext.SaveChanges();

            return RedirectToAction("List", "blog");
        }

        public IActionResult Delete(int id)
        {
            var blog = _dataContext.Blogs.FirstOrDefault(b => b.Id == id);
            if (blog == null) return NotFound();

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", blog.ImagePath);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _dataContext.Blogs.Remove(blog);

            _dataContext.SaveChanges();
            return RedirectToAction("List", "blog");
        }
    }
}
