namespace EduMapBackendProject.DAL.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<EventSpeaker> EventSpeakers { get; set; }
    }
}
