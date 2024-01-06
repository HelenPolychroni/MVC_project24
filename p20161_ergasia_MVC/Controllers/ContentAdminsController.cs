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
    public class ContentAdminsController : Controller
    {
        private readonly DBContext _context;

        public ContentAdminsController(DBContext context)
        {
            _context = context;
        }

        // GET: ContentAdmins
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContentAdmins.ToListAsync());
        }

        public IActionResult HomePage()
        {
            return View();
        }

        // GET: ContentAdmins/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentAdmin = await _context.ContentAdmins
                .FirstOrDefaultAsync(m => m.Username == id);
            if (contentAdmin == null)
            {
                return NotFound();
            }

            return View(contentAdmin);
        }

        // GET: ContentAdmins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContentAdmins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Fullname,Username,Email,Password")] ContentAdmin contentAdmin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contentAdmin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contentAdmin);
        }

        // GET: ContentAdmins/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentAdmin = await _context.ContentAdmins.FindAsync(id);
            if (contentAdmin == null)
            {
                return NotFound();
            }
            return View(contentAdmin);
        }

        // POST: ContentAdmins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Fullname,Username,Email,Password")] ContentAdmin contentAdmin)
        {
            if (id != contentAdmin.Username)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contentAdmin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentAdminExists(contentAdmin.Username))
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
            return View(contentAdmin);
        }

        // GET: ContentAdmins/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentAdmin = await _context.ContentAdmins
                .FirstOrDefaultAsync(m => m.Username == id);
            if (contentAdmin == null)
            {
                return NotFound();
            }

            return View(contentAdmin);
        }

        // POST: ContentAdmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var contentAdmin = await _context.ContentAdmins.FindAsync(id);
            if (contentAdmin != null)
            {
                _context.ContentAdmins.Remove(contentAdmin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContentAdminExists(string id)
        {
            return _context.ContentAdmins.Any(e => e.Username == id);
        }

        public async Task<IActionResult> Login(string username, string password/*User model*/)
        {
            // Check if model state is valid (username and password are provided)
            if (ModelState.IsValid)
            {
                // Attempt to find a user in the database with the provided username and password
                var content_admin = await _context.ContentAdmins
                    .Where(u => u.Username == username && u.Password == password).FirstOrDefaultAsync();
                //.Select(u => new { u.Username,u.Email,u.Role })*/

                // If a user is found, consider the login successful
                if (content_admin != null)
                {

                    return RedirectToAction("HomePage", "ContentAdmins");
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
