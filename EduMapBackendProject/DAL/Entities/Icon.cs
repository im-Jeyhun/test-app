namespace EduMapBackendProject.DAL.Entities
{
    public class Icon 
    {
        public int Id { get; set; }
        public string IconClass { get; set; }
        public string IconPath { get; set; }
        public int FooterSettingId { get; set; }
        public FooterSetting FooterSetting { get; set; }
    }
}
