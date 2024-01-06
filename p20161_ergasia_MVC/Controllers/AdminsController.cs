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
    public class AdminsController : Controller
    {
        private readonly DBContext _context;

        public AdminsController(DBContext context)
        {
            _context = context;
        }

        // GET: Admins
        public async Task<IActionResult> Index1()
        {
            return View(await _context.Admins.ToListAsync());
        }

        public IActionResult HomePage()
        {
            return View();
        }

        // GET: Admins/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .FirstOrDefaultAsync(m => m.Username == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // GET: Admins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Fullname,Username,Email,Password")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Fullname,Username,Email,Password")] Admin admin)
        {
            if (id != admin.Username)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.Username))
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
            return View(admin);
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .FirstOrDefaultAsync(m => m.Username == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(string id)
        {
            return _context.Admins.Any(e => e.Username == id);
        }

        public async Task<IActionResult> Login(string username, string password)
        {
            // Check if model state is valid (username and password are provided)
            if (ModelState.IsValid)
            {
                // Attempt to find a user in the database with the provided username and password
                var admin = await _context.Admins
                    .Where(u => u.Username == username && u.Password == password).FirstOrDefaultAsync();
                //.Select(u => new { u.Username,u.Email,u.Role })*/

                // If a user is found, consider the login successful
                if (admin != null)
                {

                    return RedirectToAction("HomePage", "Admins");
                }

                // Authentication successful

                // Authentication successful (for simplicity, redirect to the home page)
                //return RedirectToAction("Index", "Home");
            }
            else
            {
                // If no user is found, add a model error for displaying an error message
                ModelState.AddModelError(string.Empty, "Invalid username or password");
            }

            // If model state is not valid or authentication fails, return to the login form with errors
            return View("Login", "Users");
        }
    }
}
