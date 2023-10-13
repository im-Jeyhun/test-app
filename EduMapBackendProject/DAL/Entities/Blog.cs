namespace EduMapBackendProject.DAL.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
