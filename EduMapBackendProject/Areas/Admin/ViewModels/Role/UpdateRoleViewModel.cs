using EduMapBackendProject.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace EduMapBackendProject.Areas.Admin.ViewModels.Role
{
    public class UpdateRoleViewModel
    {
        public List<IdentityRole> Roles { get; set; }
        public IList<string> UserRoles { get; set; }
        public User User { get; set; }
    }
}
