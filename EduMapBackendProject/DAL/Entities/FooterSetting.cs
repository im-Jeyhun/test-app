
namespace EduMapBackendProject.DAL.Entities
{
    public class FooterSetting 
    {
        public int Id { get; set; }
        public List<Icon> Icons { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
    }
}
