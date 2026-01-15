using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebAppHotel.Data;
using WebAppHotel.Data.Domain;

namespace WebAppHotel.Controllers
{
   
    [Authorize(Roles = "Admin,Employee")]
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var reservations = await _context.Reservations
                .Include(r => r.Client)
                .Include(r => r.Room)
                .Include(r => r.User)
                .ToListAsync();

            return View(reservations);
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var reservation = await _context.Reservations
                .Include(r => r.Client)
                .Include(r => r.Room)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (reservation == null) return NotFound();

            return View(reservation);
        }

        // GET: Reservations/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            LoadDropDowns();
            return View();
        }

        // POST: Reservations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("RoomId,ClientId,DateIn,DateOut")] Reservation reservation)
        {
            LoadDropDowns(reservation.RoomId, reservation.ClientId);

            if (!ModelState.IsValid)
                return View(reservation);

            // UserId = логнатия потребител
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
                return Challenge();

            reservation.UserId = userId;

            // Валидация на дати
            var nights = (reservation.DateOut.Date - reservation.DateIn.Date).Days;
            if (nights <= 0)
            {
                ModelState.AddModelError(nameof(Reservation.DateOut),
                    "Датата на освобождаване трябва да е след датата на настаняване.");
                return View(reservation);
            }

            // Вземи стаята
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == reservation.RoomId);
            if (room == null)
            {
                ModelState.AddModelError(nameof(Reservation.RoomId),
                    "Избраната стая не съществува.");
                return View(reservation);
            }

            // Проверка за застъпване
            var hasOverlap = await _context.Reservations
                .AnyAsync(r =>
                    r.RoomId == reservation.RoomId &&
                    reservation.DateIn < r.DateOut &&
                    reservation.DateOut > r.DateIn);

            if (hasOverlap)
            {
                ModelState.AddModelError(string.Empty,
                    "Стаята е заета за избрания период.");
                return View(reservation);
            }

            // Сметни сума
            reservation.TotalSum = nights * room.PricePerNight;

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return NotFound();

            LoadDropDowns(reservation.RoomId, reservation.ClientId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoomId,ClientId,DateIn,DateOut")] Reservation edited)
        {
            if (id != edited.Id) return NotFound();

            LoadDropDowns(edited.RoomId, edited.ClientId);

            if (!ModelState.IsValid)
                return View(edited);

            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null) return NotFound();

            var nights = (edited.DateOut.Date - edited.DateIn.Date).Days;
            if (nights <= 0)
            {
                ModelState.AddModelError(nameof(Reservation.DateOut),
                    "Датата на освобождаване трябва да е след датата на настаняване.");
                return View(edited);
            }

            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == edited.RoomId);
            if (room == null)
            {
                ModelState.AddModelError(nameof(Reservation.RoomId),
                    "Избраната стая не съществува.");
                return View(edited);
            }

            // Проверка за застъпване (без текущата резервация)
            var hasOverlap = await _context.Reservations
                .AnyAsync(r =>
                    r.RoomId == edited.RoomId &&
                    r.Id != edited.Id &&
                    edited.DateIn < r.DateOut &&
                    edited.DateOut > r.DateIn);

            if (hasOverlap)
            {
                ModelState.AddModelError(string.Empty,
                    "Стаята е заета за избрания период.");
                return View(edited);
            }

            // Копиране на позволените полета
            reservation.RoomId = edited.RoomId;
            reservation.ClientId = edited.ClientId;
            reservation.DateIn = edited.DateIn;
            reservation.DateOut = edited.DateOut;
            reservation.TotalSum = nights * room.PricePerNight;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Reservations/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var reservation = await _context.Reservations
                .Include(r => r.Client)
                .Include(r => r.Room)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (reservation == null) return NotFound();

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private void LoadDropDowns(int? roomId = null, int? clientId = null)
        {
            ViewData["RoomId"] = new SelectList(
                _context.Rooms.AsNoTracking(), "Id", "Number", roomId);

            ViewData["ClientId"] = new SelectList(
                _context.Clients.AsNoTracking(), "Id", "FullName", clientId);
        }
    }
}
