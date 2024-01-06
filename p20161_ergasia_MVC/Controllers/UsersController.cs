using Microsoft.AspNetCore.Mvc;
using p20161_ergasia_MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace p20161_ergasia_MVC.Controllers
{
    public class UsersController : Controller
    {
       
        private readonly DBContext _context;   // 4 database

        public UsersController(DBContext context)
        {
            _context = context;
        }

       


        // Action method to display the login form
        public IActionResult Login()
        {
            return View();
        }



        // POST: Users/LogIn - POST action for handling login form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password/*User model*/)
        {
            // Check if model state is valid (username and password are provided)
            if (ModelState.IsValid)
            {
                if (username.StartsWith("cu"))  // customer
                {
                    return RedirectToAction("Login", "Customers", new { username, password });
                }
                else if (username.StartsWith("ca"))  // content administrator
                {
                    return RedirectToAction("Login", "ContentAdmins", new { username, password });
                }
                else if (username.StartsWith("ad")) // application administrator
                {
                    return RedirectToAction("Login", "Admins", new { username, password });
                }
            }
            // If model state is not valid or authentication fails, return to the login form with errors
            return View("Login");
        }
    }
}
