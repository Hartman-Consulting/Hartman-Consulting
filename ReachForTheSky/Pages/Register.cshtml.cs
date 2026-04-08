using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using ReachForTheSky.Data;
using ReachForTheSky.Models;

namespace ReachForTheSky.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly AppDbContext _db;
        public RegisterModel(AppDbContext db) => _db = db;

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public string StatusMessage { get; set; } = string.Empty;

        public class InputModel
        {
            [Required] public string Name { get; set; } = string.Empty;
            [Required, EmailAddress] public string Email { get; set; } = string.Empty;
            [Required, MinLength(6)] public string Password { get; set; } = string.Empty;
            public string? Phone { get; set; }
            public string AccountType { get; set; } = "Personal";
            public int? TravelRadius { get; set; }
            public string? ADAPreferences { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // Check for duplicate email
            if (_db.Users.Any(u => u.Email == Input.Email))
            {
                ModelState.AddModelError(string.Empty, "Email already registered.");
                return Page();
            }

            var user = new User
            {
                Name = Input.Name,
                Email = Input.Email,
                Phone = Input.Phone,
                AccountType = Input.AccountType,
                TravelRadius = Input.TravelRadius,
                ADAPreferences = Input.ADAPreferences,
                PasswordHash = HashPassword(Input.Password)
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            StatusMessage = "Account created successfully!";
            return Page();
        }

        private static string HashPassword(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes);
        }
    }
}