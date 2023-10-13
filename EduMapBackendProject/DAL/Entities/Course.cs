namespace EduMapBackendProject.DAL.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public CourseFeature Feature { get; set; }
    }
}
