using EduMapBackendProject.DAL.Entities;

namespace EduMapBackendProject.Areas.Admin.ViewModels.CourseFeatureVM
{
    public class CourseListViewModel
    {
        public int CourseId { get; set; }
        public List<CourseFeature> CourseFeatures { get; set; }
    }
}
