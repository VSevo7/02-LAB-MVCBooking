using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCBooking.Data;
using MVCBooking.Models;

namespace MVCBooking.Controllers
{
    [Route("Booking")] // Prilagođava osnovnu rutu na "Booking"
    public class HotelBookingsController : Controller
    {
        private readonly MVCBookingContext _context;

        public HotelBookingsController(MVCBookingContext context)
        {
            _context = context;
        }

        // GET: Booking
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.HotelBooking.ToListAsync());
        }

        // GET: Booking/Details/5
        [HttpGet("Details/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelBooking = await _context.HotelBooking
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hotelBooking == null)
            {
                return NotFound();
            }

            return View(hotelBooking);
        }

        // GET: Booking/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Booking/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GuestName,RoomNumber")] HotelBooking hotelBooking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotelBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hotelBooking);
        }

        // GET: Booking/Edit/5
        [HttpGet("Edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelBooking = await _context.HotelBooking.FindAsync(id);
            if (hotelBooking == null)
            {
                return NotFound();
            }
            return View(hotelBooking);
        }

        // POST: Booking/Edit/5
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GuestName,RoomNumber")] HotelBooking hotelBooking)
        {
            if (id != hotelBooking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotelBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelBookingExists(hotelBooking.Id))
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
            return View(hotelBooking);
        }

        // GET: Booking/Delete/5
        [HttpGet("Delete/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelBooking = await _context.HotelBooking
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hotelBooking == null)
            {
                return NotFound();
            }

            return View(hotelBooking);
        }

        // POST: Booking/Delete/5
        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotelBooking = await _context.HotelBooking.FindAsync(id);
            if (hotelBooking != null)
            {
                _context.HotelBooking.Remove(hotelBooking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HotelBookingExists(int id)
        {
            return _context.HotelBooking.Any(e => e.Id == id);
        }
    }
}
