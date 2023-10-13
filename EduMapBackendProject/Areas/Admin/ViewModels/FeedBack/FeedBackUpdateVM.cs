namespace EduMapBackendProject.Areas.Admin.ViewModels.FeedBack
{
    public class FeedBackUpdateVM
    {
        public int Id { get; set; }
        public IFormFile? Image { get; set; }
        public string Content { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
    }
}
