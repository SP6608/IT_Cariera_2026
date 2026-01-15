using EventuresApp.Data;
using EventuresApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;

namespace EventuresApp.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext context;
        public EventsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult All()
        {
            var events = context.Events
<<<<<<< HEAD
=======
                .Include(e => e.Owner)
>>>>>>> 551a36c7a89937b4d508c8f107438518b7cac37b
                .Select(e => new EventAllViewModel
                {
                    Name = e.Name,
                    Place = e.Place,
                    Start = e.Start.ToString("dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture),
                    End = e.End.ToString("dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture),
<<<<<<< HEAD
                    Owner = e.OwnerId // или после ще го форматираме правилно
=======
                    Owner = e.Owner.UserName,

                    PricePerTicket = e.PricePerTicket,
                    TotalTickets = e.TotalTickets
>>>>>>> 551a36c7a89937b4d508c8f107438518b7cac37b
                })
                .ToList();

            return View(events);
        }

        //Създавам страница Create Get Create HTTpPOS
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Event model)
        {
            if (!ModelState.IsValid)
            {
<<<<<<< HEAD
                return View(model);
=======
                string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                Event eventForDb = new Event
                {
                    Name = bindingModel.Name,
                    Place = bindingModel.Place,
                    Start = bindingModel.Start,
                    End = bindingModel.End,
                    TotalTickets = bindingModel.TotalTickets,
                    PricePerTicket = bindingModel.PricePerTicket,
                    OwnerId = currentUserId,
                   
                };

                context.Events.Add(eventForDb);
                context.SaveChanges();

                return this.RedirectToAction("All");
>>>>>>> 551a36c7a89937b4d508c8f107438518b7cac37b
            }

            model.OwnerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            context.Events.Add(model);
            context.SaveChanges();

            return RedirectToAction("All"); // или Index
        }


    }
}
