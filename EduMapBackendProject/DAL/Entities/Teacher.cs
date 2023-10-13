namespace EduMapBackendProject.DAL.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ImagePath { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string Degree { get; set; }
        public string Experience { get; set; }
        public string Faculty { get; set; }
        public string Mail { get; set; }
        public string CallNumber { get; set; }
        public Skill TeacherSkill { get; set; }
        public Social TeacherSocial { get; set; }
    }
}
