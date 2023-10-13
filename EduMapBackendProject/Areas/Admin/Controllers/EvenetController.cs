using EduMapBackendProject.Areas.Admin.ViewModels.Event;
using EduMapBackendProject.DAL;
using EduMapBackendProject.DAL.Entities;
using EduMapBackendProject.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduMapBackendProject.Areas.Admin.Controllers
{
    [Area("admin")]
    public class EvenetController : Controller
    {
        private readonly EduMapDbContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EvenetController(EduMapDbContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = dataContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult List()
        {
            var events = _dataContext.Events
                .Include(e => e.EventSpeakers)
                .ThenInclude(ev => ev.Speaker).ToList();
            return View(events);
        }

        public IActionResult Create()
        {
            var eventCreateVM = new EventCreateViewModel
            {
                Speakers = _dataContext.Speakers.ToList()
            };

            return View(eventCreateVM);
        }

        [HttpPost]
        public IActionResult Create(EventCreateViewModel createViewModel)
        {
            if (!ModelState.IsValid) return View(createViewModel);

            var eventModel = new Event
            {
                Title = createViewModel.Tittle,
                Description = createViewModel.Description,
                Address = createViewModel.Address,
                Date = createViewModel.Date,
                StartTime = createViewModel.StartTime,
                EndTime = createViewModel.EndTime
            };
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Photo", "Add photo");
                return View();
            }

            if (!createViewModel.Photo.CheckImage())
            {
                ModelState.AddModelError("Photo", "Add only photo");
                return View();
            }

            if (createViewModel.Photo.CheckImageSize(1000))
            {
                ModelState.AddModelError("Photo", "Size is high");
                return View();
            }

            string fileName = Guid.NewGuid() + createViewModel.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                createViewModel.Photo.CopyTo(stream);

            };

            eventModel.ImagePath = fileName;

            foreach (var eventSpeakerId in createViewModel.SpeakerIds)
            {
                EventSpeaker eventSpeaker = new();
                eventSpeaker.EventId = eventModel.Id;
                eventSpeaker.SpeakerId = eventSpeakerId;
                eventModel.EventSpeakers.Add(eventSpeaker);
            }

            _dataContext.Events.Add(eventModel);

            _dataContext.SaveChanges();

            return RedirectToAction("List");
        }

        public IActionResult Update(int? id)
        {
            var findedEvent = _dataContext.Events.FirstOrDefault(e => e.Id == id);
            if (findedEvent == null) return NotFound();

            var eventUpdateModel = new EventUpdateViewModel
            {
                Id = findedEvent.Id,   
                Tittle = findedEvent.Title,
                Description = findedEvent.Title,
                Address = findedEvent.Address,
                Date = findedEvent.Date,
                StartTime = findedEvent.StartTime,
                EndTime = findedEvent.EndTime,
                Speakers = _dataContext.Speakers.ToList(),
                SpeakerIds = findedEvent.EventSpeakers.Select(es => es.SpeakerId).ToList()
            };

            return View(eventUpdateModel);
        }

        [HttpPost]
        public IActionResult Update(EventUpdateViewModel updateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(updateViewModel);
            }
            var eventModel = _dataContext.Events.Include(e => e.EventSpeakers)
                .FirstOrDefault(e => e.Id == updateViewModel.Id);

            if (eventModel == null) return NotFound();

            eventModel.Title = updateViewModel.Tittle;
            eventModel.Description = updateViewModel.Description;
            eventModel.Address = updateViewModel.Address;
            eventModel.Date = updateViewModel.Date;
            eventModel.StartTime = updateViewModel.StartTime;
            eventModel.EndTime = updateViewModel.EndTime;

            if(updateViewModel.Photo != null)
            {
                if (!updateViewModel.Photo.CheckImage())
                {
                    ModelState.AddModelError("Photo", "Only Photo.");
                    return View(updateViewModel);
                }

                if (updateViewModel.Photo.CheckImageSize(1000))
                {
                    ModelState.AddModelError("Photo", "Size is high.");
                    return View(updateViewModel);
                }


                var imagePathToDelete = Path.Combine(_webHostEnvironment.WebRootPath, "img", eventModel.ImagePath);
                if (System.IO.File.Exists(imagePathToDelete))
                {
                    System.IO.File.Delete(imagePathToDelete);
                }

                string fileName = Guid.NewGuid() + updateViewModel.Photo.FileName;
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    updateViewModel.Photo.CopyTo(stream);

                };

                eventModel.ImagePath = fileName;
            }


            foreach (var speakerId in updateViewModel.SpeakerIds)
            {
                if (!_dataContext.Speakers.Any(bt => bt.Id == speakerId))
                {
                    ModelState.AddModelError(string.Empty, "something went wrong");
                    return View(updateViewModel);
                }
            }

            //event daxilinde var olan speaker id ler
            var speakerIdsInEvent = eventModel.EventSpeakers.Select(es => es.SpeakerId).ToList();

            //event daxilinde var olan speaker id leri silinecekler olaraq ayirmaq
            var speakerIdsToRemove = speakerIdsInEvent.Except(updateViewModel.SpeakerIds).ToList();

            //view modelden gelen speaker id lerle eventin id lerini elave elemek ucun qarsilasdirib ayirmaq 
            var speakerIdToAdd = updateViewModel.SpeakerIds.Except(speakerIdsInEvent).ToList();

            eventModel.EventSpeakers.RemoveAll(es => speakerIdsToRemove.Contains(es.SpeakerId));

            foreach (var speakerId in speakerIdToAdd)
            {
                var eventSpeaker = new EventSpeaker
                {
                    EventId = eventModel.Id,
                    SpeakerId = speakerId,
                };

                _dataContext.EventSpeakers.Add(eventSpeaker);
            }

            _dataContext.SaveChanges();

            return RedirectToAction("List");
        }


        public IActionResult Delete(int? id)
        {
            var events = _dataContext.Events.FirstOrDefault(e => e.Id == id);
            if (events == null) return NotFound();
            _dataContext.Events.Remove(events);
            _dataContext.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
