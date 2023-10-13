using EduMapBackendProject.DAL.Entities;

namespace EduMapBackendProject.Areas.Admin.ViewModels.Event
{
    public class EventUpdateViewModel
    {
        public int Id { get; set; }
        public List<Speaker>? Speakers { get; set; }
        public string Tittle { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<int> SpeakerIds { get; set; }
        public IFormFile Photo { get; set; }
    }
}
