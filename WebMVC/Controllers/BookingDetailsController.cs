using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PHAMDANGXUANDUY_NET1601_ASS01.Domain.Entity;

namespace WebMVC.Controllers
{
    public class BookingDetailsController : Controller
    {
        private readonly FUMiniHotelManagementContext _context;

        public BookingDetailsController(FUMiniHotelManagementContext context)
        {
            _context = context;
        }

        // GET: BookingDetails
        public async Task<IActionResult> Index()
        {
            var fUMiniHotelManagementContext = _context.BookingDetails.Include(b => b.BookingReservation).Include(b => b.Room);
            return View(await fUMiniHotelManagementContext.ToListAsync());
        }

        // GET: BookingDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BookingDetails == null)
            {
                return NotFound();
            }

            var bookingDetail = await _context.BookingDetails
                .Include(b => b.BookingReservation)
                .Include(b => b.Room)
                .FirstOrDefaultAsync(m => m.BookingReservationId == id);
            if (bookingDetail == null)
            {
                return NotFound();
            }

            return View(bookingDetail);
        }

        // GET: BookingDetails/Create
        public IActionResult Create()
        {
            ViewData["BookingReservationId"] = new SelectList(_context.BookingReservations, "BookingReservationId", "BookingReservationId");
            ViewData["RoomId"] = new SelectList(_context.RoomInformations, "RoomId", "RoomNumber");
            return View();
        }

        // POST: BookingDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingReservationId,RoomId,StartDate,EndDate,ActualPrice")] BookingDetail bookingDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookingDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingReservationId"] = new SelectList(_context.BookingReservations, "BookingReservationId", "BookingReservationId", bookingDetail.BookingReservationId);
            ViewData["RoomId"] = new SelectList(_context.RoomInformations, "RoomId", "RoomNumber", bookingDetail.RoomId);
            return View(bookingDetail);
        }

        // GET: BookingDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BookingDetails == null)
            {
                return NotFound();
            }

            var bookingDetail = await _context.BookingDetails.FindAsync(id);
            if (bookingDetail == null)
            {
                return NotFound();
            }
            ViewData["BookingReservationId"] = new SelectList(_context.BookingReservations, "BookingReservationId", "BookingReservationId", bookingDetail.BookingReservationId);
            ViewData["RoomId"] = new SelectList(_context.RoomInformations, "RoomId", "RoomNumber", bookingDetail.RoomId);
            return View(bookingDetail);
        }

        // POST: BookingDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingReservationId,RoomId,StartDate,EndDate,ActualPrice")] BookingDetail bookingDetail)
        {
            if (id != bookingDetail.BookingReservationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingDetailExists(bookingDetail.BookingReservationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingReservationId"] = new SelectList(_context.BookingReservations, "BookingReservationId", "BookingReservationId", bookingDetail.BookingReservationId);
            ViewData["RoomId"] = new SelectList(_context.RoomInformations, "RoomId", "RoomNumber", bookingDetail.RoomId);
            return View(bookingDetail);
        }

        // GET: BookingDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BookingDetails == null)
            {
                return NotFound();
            }

            var bookingDetail = await _context.BookingDetails
                .Include(b => b.BookingReservation)
                .Include(b => b.Room)
                .FirstOrDefaultAsync(m => m.BookingReservationId == id);
            if (bookingDetail == null)
            {
                return NotFound();
            }

            return View(bookingDetail);
        }

        // POST: BookingDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BookingDetails == null)
            {
                return Problem("Entity set 'FUMiniHotelManagementContext.BookingDetails'  is null.");
            }
            var bookingDetail = await _context.BookingDetails.FindAsync(id);
            if (bookingDetail != null)
            {
                _context.BookingDetails.Remove(bookingDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingDetailExists(int id)
        {
          return (_context.BookingDetails?.Any(e => e.BookingReservationId == id)).GetValueOrDefault();
        }
    }
}
