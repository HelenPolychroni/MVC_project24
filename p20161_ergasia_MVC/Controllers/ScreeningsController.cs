using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using p20161_ergasia_MVC.Models;

namespace p20161_ergasia_MVC.Controllers
{
    public class ScreeningsController : Controller
    {
        private readonly DBContext _context;

        public ScreeningsController(DBContext context)
        {
            _context = context;
        }

        // GET: Screenings
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.Screenings.Include(s => s.CinemasNameNavigation).Include(s => s.ContentAdminsUsernameNavigation).Include(s => s.MoviesNameNavigation);
            return View(await dBContext.ToListAsync());
        }

        public async Task<IActionResult> View4Reservations()
        {
            var dBContext = _context.Screenings.Include(s => s.CinemasNameNavigation).Include(s => s.ContentAdminsUsernameNavigation).Include(s => s.MoviesNameNavigation);
            return View(await dBContext.ToListAsync());
        }



        public IActionResult ViewAccordingToDate(DateTime? startDate, DateTime? endDate)
        {
            //return View();
            var screenings = _context.Screenings
        .Include(s => s.MoviesNameNavigation)
        .Include(s => s.CinemasNameNavigation)
        .AsQueryable();

            if (startDate.HasValue)
            {
                screenings = screenings.Where(s => s.Time >= startDate);
            }

            if (endDate.HasValue)
            {
                screenings = screenings.Where(s => s.Time <= endDate);
            }

            return View(screenings.ToList());
        }

        // GET: Screenings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var screening = await _context.Screenings
                .Include(s => s.CinemasNameNavigation)
                .Include(s => s.ContentAdminsUsernameNavigation)
                .Include(s => s.MoviesNameNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (screening == null)
            {
                return NotFound();
            }

            return View(screening);
        }

        // GET: Screenings/Create
        public IActionResult Create()
        {
            ViewData["CinemasName"] = new SelectList(_context.Cinemas, "Name", "Name");
            ViewData["ContentAdminsUsername"] = new SelectList(_context.ContentAdmins, "Username", "Username");
            ViewData["MoviesName"] = new SelectList(_context.Movies, "Name", "Name");
            ViewData["Time"] = new SelectList(_context.Screenings, "Time", "Time");

            return View();
        }

        // POST: Screenings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MoviesName,CinemasName,Time,ContentAdminsUsername")] Screening screening)
        {
            if (ModelState.IsValid)
            {
                _context.Add(screening);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CinemasName"] = new SelectList(_context.Cinemas, "Name", "Name", screening.CinemasName);
            ViewData["ContentAdminsUsername"] = new SelectList(_context.ContentAdmins, "Username", "Username", screening.ContentAdminsUsername);
            ViewData["MoviesName"] = new SelectList(_context.Movies, "Name", "Name", screening.MoviesName);
            return View(screening);
        }

        public async Task<IActionResult> BuyTickets(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var screening = await _context.Screenings.FindAsync(id);
            if (screening == null)
            {
                return NotFound();
            }

            return RedirectToAction("BuyTickets", "Reservations", new { screening});
        }

        // GET: Screenings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var screening = await _context.Screenings.FindAsync(id);
            if (screening == null)
            {
                return NotFound();
            }
            ViewData["CinemasName"] = new SelectList(_context.Cinemas, "Name", "Name", screening.CinemasName);
            ViewData["ContentAdminsUsername"] = new SelectList(_context.ContentAdmins, "Username", "Username", screening.ContentAdminsUsername);
            ViewData["MoviesName"] = new SelectList(_context.Movies, "Name", "Name", screening.MoviesName);
            return View(screening);
        }


        // POST: Screenings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MoviesName,CinemasName,Time,ContentAdminsUsername")] Screening screening)
        {
            if (id != screening.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(screening);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScreeningExists(screening.Id))
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
            ViewData["CinemasName"] = new SelectList(_context.Cinemas, "Name", "Name", screening.CinemasName);
            ViewData["ContentAdminsUsername"] = new SelectList(_context.ContentAdmins, "Username", "Username", screening.ContentAdminsUsername);
            ViewData["MoviesName"] = new SelectList(_context.Movies, "Name", "Name", screening.MoviesName);
            return View(screening);
        }

        // GET: Screenings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var screening = await _context.Screenings
                .Include(s => s.CinemasNameNavigation)
                .Include(s => s.ContentAdminsUsernameNavigation)
                .Include(s => s.MoviesNameNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (screening == null)
            {
                return NotFound();
            }

            return View(screening);
        }

        // POST: Screenings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var screening = await _context.Screenings.FindAsync(id);
            if (screening != null)
            {
                _context.Screenings.Remove(screening);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScreeningExists(int id)
        {
            return _context.Screenings.Any(e => e.Id == id);
        }
    }
}
