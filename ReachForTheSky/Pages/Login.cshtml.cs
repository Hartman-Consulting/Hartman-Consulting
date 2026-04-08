using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using ReachForTheSky.Data;

namespace ReachForTheSky.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _db;
        public LoginModel(AppDbContext db) => _db = db;

        [BindProperty, Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [BindProperty, Required]
        public string Password { get; set; } = string.Empty;

        public string StatusMessage { get; set; } = string.Empty;

        public IActionResult OnGet()
        {
            // If already logged in, go straight to profile
            if (HttpContext.Session.GetInt32("UserID") != null)
                return RedirectToPage("/Profile", new { id = HttpContext.Session.GetInt32("UserID") });
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(Password)));
            var user = _db.Users.FirstOrDefault(u => u.Email == Email && u.PasswordHash == hash);

            if (user == null)
            {
                StatusMessage = "Invalid email or password.";
                return Page();
            }

            HttpContext.Session.SetInt32("UserID", user.UserID);
            HttpContext.Session.SetString("UserName", user.Name);

            return RedirectToPage("/Profile", new { id = user.UserID });
        }
    }
}