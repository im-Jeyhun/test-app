namespace EduMapBackendProject.DAL.Entities
{
    public class Speaker
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public List<EventSpeaker> EventSpeakers { get; set; }
    }
}
