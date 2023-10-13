﻿namespace EduMapBackendProject.DAL.Entities
{
    public class CourseFeature
    {
        public int Id { get; set; }
        public DateTime Starts { get; set; }
        public string Duration { get; set; }
        public string ClassDuration { get; set; }
        public string SkillLevel { get; set; }
        public string Language { get; set; }
        public int StudentsCount { get; set; }
        public string Assesments { get; set; }
        public double Fee { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
