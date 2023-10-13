namespace EduMapBackendProject.Areas.Admin.ViewModels.Blog
{
    public class BlogCreateViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public IFormFile Image { get; set; }
        public DateTime Date { get; set; }
    }
}
