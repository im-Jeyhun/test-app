using Microsoft.AspNetCore.Identity;

namespace EduMapBackendProject.DAL.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}
