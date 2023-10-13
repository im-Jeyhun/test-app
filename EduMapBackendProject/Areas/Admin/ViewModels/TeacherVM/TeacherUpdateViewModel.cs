namespace EduMapBackendProject.Areas.Admin.ViewModels.TeacherVM
{
    public class TeacherUpdateViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public IFormFile Image { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string Degree { get; set; }
        public string Experience { get; set; }
        public string Faculty { get; set; }
        public string Mail { get; set; }
        public string CallNumber { get; set; }
        public int Language { get; set; }
        public int TeamLeader { get; set; }
        public int Development { get; set; }
        public int Design { get; set; }
        public int Innovation { get; set; }
        public int Communication { get; set; }
        public string FaceBook { get; set; }
        public string Pinterest { get; set; }
        public string Vimeo { get; set; }
        public string Twitter { get; set; }
    }
}
