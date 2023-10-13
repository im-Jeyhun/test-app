using EduMapBackendProject.DAL.Entities;

namespace EduMapBackendProject.ViewModels
{
    public class AboutVM
    {
        public About About { get; set; }
        public List<FeedBack> FeedBacks { get; set; }
        public List<Event> Events { get; set; }
        public List<Teacher> Teachers { get; set; }
    }
}
