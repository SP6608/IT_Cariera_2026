using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppHotelFinal.Data;
using WebAppHotelFinal.Models;

namespace WebAppHotelFinal.Controllers
{
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
                .AsNoTracking()
                .Include(r => r.Room)
                .Include(r => r.Client)
                .OrderByDescending(r => r.DateIn)
                .ToListAsync();

            return View(reservations);
        }

        // GET: Reservations/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var reservation = await _context.Reservations
                .AsNoTracking()
                .Include(r => r.Room)
                .Include(r => r.Client)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (reservation == null) return NotFound();

            return View(reservation);
        }

        // GET: Reservations/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            await LoadDropDownsAsync();
            return View(new Reservation
            {
                DateIn = DateTime.Today,
                DateOut = DateTime.Today.AddDays(1)
            });
        }

        // POST: Reservations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Reservation reservation)
        {
            ValidateDates(reservation);

            bool overlap = await _context.Reservations.AnyAsync(r =>
                r.RoomId == reservation.RoomId &&
                r.DateIn < reservation.DateOut &&
                reservation.DateIn < r.DateOut);

            if (overlap)
                ModelState.AddModelError("", "Стаята не е свободна за избрания период.");

            if (!ModelState.IsValid)
            {
                await LoadDropDownsAsync(reservation.RoomId, reservation.ClientId);
                return View(reservation);
            }

            var room = await _context.Rooms.AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == reservation.RoomId);

            if (room == null)
            {
                ModelState.AddModelError(nameof(Reservation.RoomId), "Невалидна стая.");
                await LoadDropDownsAsync(reservation.RoomId, reservation.ClientId);
                return View(reservation);
            }

            int nights = (reservation.DateOut.Date - reservation.DateIn.Date).Days;
            reservation.TotalPrice = nights * room.Price;

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Reservations/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return NotFound();

            await LoadDropDownsAsync(reservation.RoomId, reservation.ClientId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Reservation reservation)
        {
            if (id != reservation.Id) return NotFound();

            ValidateDates(reservation);

            bool overlap = await _context.Reservations.AnyAsync(r =>
                r.Id != reservation.Id &&                      // важно: да не сравнява със себе си
                r.RoomId == reservation.RoomId &&
                r.DateIn < reservation.DateOut &&
                reservation.DateIn < r.DateOut);

            if (overlap)
                ModelState.AddModelError("", "Стаята не е свободна за избрания период.");

            if (!ModelState.IsValid)
            {
                await LoadDropDownsAsync(reservation.RoomId, reservation.ClientId);
                return View(reservation);
            }

            var room = await _context.Rooms.AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == reservation.RoomId);

            if (room == null)
            {
                ModelState.AddModelError(nameof(Reservation.RoomId), "Невалидна стая.");
                await LoadDropDownsAsync(reservation.RoomId, reservation.ClientId);
                return View(reservation);
            }

            int nights = (reservation.DateOut.Date - reservation.DateIn.Date).Days;
            reservation.TotalPrice = nights * room.Price;

            _context.Update(reservation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Reservations/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var reservation = await _context.Reservations
                .AsNoTracking()
                .Include(r => r.Room)
                .Include(r => r.Client)
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
            if (reservation == null) return RedirectToAction(nameof(Index));

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private void ValidateDates(Reservation reservation)
        {
            if (reservation.DateOut <= reservation.DateIn)
            {
                ModelState.AddModelError(nameof(Reservation.DateOut),
                    "Дата на освобождаване трябва да е след дата на настаняване.");
            }

            // допълнителна защита: поне 1 нощувка
            int nights = (reservation.DateOut.Date - reservation.DateIn.Date).Days;
            if (nights <= 0)
            {
                ModelState.AddModelError("", "Броят нощувки трябва да е поне 1.");
            }
        }

        private async Task LoadDropDownsAsync(int? selectedRoomId = null, int? selectedClientId = null)
        {
            ViewData["RoomId"] = new SelectList(
                await _context.Rooms.AsNoTracking().OrderBy(r => r.NumberRoom).ToListAsync(),
                "Id", "NumberRoom", selectedRoomId);

            ViewData["ClientId"] = new SelectList(
                await _context.Clients.AsNoTracking().OrderBy(c => c.FullName).ToListAsync(),
                "Id", "FullName", selectedClientId);
        }
    }
}
