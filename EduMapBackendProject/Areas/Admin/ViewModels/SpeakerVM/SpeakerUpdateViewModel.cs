namespace EduMapBackendProject.Areas.Admin.ViewModels.SpeakerVM
{
    public class SpeakerUpdateViewModel
    {
        public int Id { get; set; }
        public IFormFile Photo { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }

    }
}
