namespace EduMapBackendProject.Areas.Admin.ViewModels.Slider
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
    }
}
