namespace EduMapBackendProject.DAL.Entities
{
    public class Social
    {
        public int Id { get; set; }
        public string FaceBook { get; set; }
        public string Pinterest { get; set; }
        public string Vimeo { get; set; }
        public string Twitter { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
