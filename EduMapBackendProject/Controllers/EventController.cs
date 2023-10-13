using EduMapBackendProject.DAL;
using EduMapBackendProject.DAL.Entities;
using EduMapBackendProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduMapBackendProject.Controllers
{
    public class EventController : Controller
    {
        private readonly EduMapDbContext _dataContext;

        public EventController(EduMapDbContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IActionResult Index()
        {
            List<Event> events = _dataContext.Events.ToList();
            return View(events);
        }

        public IActionResult Detail(int id)
        {
            EventVM eventVM = new EventVM
            {
                Blogs = _dataContext.Blogs.Take(3).ToList(),

                Event = _dataContext.Events
                .Include(e => e.EventSpeakers)
                .ThenInclude(es => es.Speaker)
                .FirstOrDefault(e => e.Id == id)

            };

            return View(eventVM);
        }
    }
}
