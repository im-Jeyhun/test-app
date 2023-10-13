namespace EduMapBackendProject.Areas.Admin.ViewModels.Course
{
    public class CourseUpdateViewModel
    {
        public int Id { get; set; }
        public IFormFile? Photo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
