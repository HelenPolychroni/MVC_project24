using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using p20161_ergasia_MVC.Models;

namespace p20161_ergasia_MVC.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly DBContext _context;

        public ReservationsController(DBContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.Reservations.Include(r => r.CustomersUsernameNavigation).Include(r => r.ScreeningsCinemasNameNavigation).Include(r => r.ScreeningsMoviesNameNavigation);
            return View(await dBContext.ToListAsync());
        }



        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.CustomersUsernameNavigation)
                .Include(r => r.ScreeningsCinemasNameNavigation)
                .Include(r => r.ScreeningsMoviesNameNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        /*public IActionResult Create()
        {
            ViewData["CustomersUsername"] = new SelectList(_context.Customers, "Username", "Username");
            ViewData["ScreeningsCinemasName"] = new SelectList(_context.Cinemas, "Name", "Name");
            ViewData["ScreeningsMoviesName"] = new SelectList(_context.Movies, "Name", "Name");
            ViewData["Dates"] = new SelectList(_context.Screenings, "Time", "Time");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScreeningsMoviesName,ScreeningsCinemasName,CustomersUsername,NumberOfSeats,Id")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomersUsername"] = new SelectList(_context.Customers, "Username", "Username", reservation.CustomersUsername);
            ViewData["ScreeningsCinemasName"] = new SelectList(_context.Cinemas, "Name", "Name", reservation.ScreeningsCinemasName);
            ViewData["ScreeningsMoviesName"] = new SelectList(_context.Movies, "Name", "Name", reservation.ScreeningsMoviesName);
            return View(reservation);
        }*/

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["CustomersUsername"] = new SelectList(_context.Customers, "Username", "Username", reservation.CustomersUsername);
            ViewData["ScreeningsCinemasName"] = new SelectList(_context.Cinemas, "Name", "Name", reservation.ScreeningsCinemasName);
            ViewData["ScreeningsMoviesName"] = new SelectList(_context.Movies, "Name", "Name", reservation.ScreeningsMoviesName);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScreeningsMoviesName,ScreeningsCinemasName,CustomersUsername,NumberOfSeats,Id")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            ViewData["CustomersUsername"] = new SelectList(_context.Customers, "Username", "Username", reservation.CustomersUsername);
            ViewData["ScreeningsCinemasName"] = new SelectList(_context.Cinemas, "Name", "Name", reservation.ScreeningsCinemasName);
            ViewData["ScreeningsMoviesName"] = new SelectList(_context.Movies, "Name", "Name", reservation.ScreeningsMoviesName);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.CustomersUsernameNavigation)
                .Include(r => r.ScreeningsCinemasNameNavigation)
                .Include(r => r.ScreeningsMoviesNameNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }

        public IActionResult Create(string moviesName, string cinemasName, DateTime time)
        {

            ViewBag.MoviesName = moviesName;
            ViewBag.CinemasName = cinemasName;
            ViewBag.Time = time;


            // You can also use a model to pass data to the view
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string moviesName, string cinemasName, DateTime time, int numberOfSeats)
        {
            if (ModelState.IsValid)
            {
                // Create a new Reservation object with the provided values
                Reservation reservation = new Reservation
                {
                    ScreeningsMoviesName = moviesName,
                    ScreeningsCinemasName = cinemasName,
                    //Time = time,
                    NumberOfSeats = numberOfSeats
                    // The Id property will be generated by the database
                };

                // Add the reservation to the database
                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();

                // Redirect to the reservation list or another page
                return RedirectToAction(nameof(Index));
            }

            // If the model state is not valid, handle accordingly
            return View(); // Adjust this based on your requirements
        }





    }
}
