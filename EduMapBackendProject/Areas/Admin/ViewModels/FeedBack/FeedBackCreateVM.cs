﻿namespace EduMapBackendProject.Areas.Admin.ViewModels.FeedBack
{
    public class FeedBackCreateVM
    {
        public IFormFile Image { get; set; }
        public string Content { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
    }
}
