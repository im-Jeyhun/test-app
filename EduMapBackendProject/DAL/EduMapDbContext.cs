using EduMapBackendProject.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EduMapBackendProject.DAL
{
    public class EduMapDbContext : IdentityDbContext<User>
    {
        public EduMapDbContext(DbContextOptions options):base(options)
        {

        }
        //public DbSet<User> Users { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<HeaderSetting> HeaderSettings { get; set; }
        public DbSet<FooterSetting> FooterSettings { get; set; }
        public DbSet<Icon> Icons { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<EventSpeaker> EventSpeakers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseFeature> CourseFeatures { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Social> Socials { get; set; }
    }
}
