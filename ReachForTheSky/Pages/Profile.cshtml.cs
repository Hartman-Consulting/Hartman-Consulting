using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ReachForTheSky.Data;

namespace ReachForTheSky.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly AppDbContext _db;
        public ProfileModel(AppDbContext db) => _db = db;

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public string StatusMessage { get; set; } = string.Empty;
        public bool NotFound { get; set; } = false;

        public class InputModel
        {
            public int UserID { get; set; }
            [Required] public string Name { get; set; } = string.Empty;
            [Required, EmailAddress] public string Email { get; set; } = string.Empty;
            public string? Phone { get; set; }
            public string AccountType { get; set; } = "Personal";
            public int? TravelRadius { get; set; }
            public string? ADAPreferences { get; set; }
            public decimal CreditBalance { get; set; }
            public string AccountStatus { get; set; } = string.Empty;
        }

        public void OnGet(int id)
        {
            var sessionID = HttpContext.Session.GetInt32("UserID");
            if (sessionID == null)
            {
                Response.Redirect("/Login");
                return;
            }

            var user = _db.Users.Find(id);
            if (user == null) { NotFound = true; return; }

            Input = new InputModel
            {
                UserID = user.UserID,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                AccountType = user.AccountType,
                TravelRadius = user.TravelRadius,
                ADAPreferences = user.ADAPreferences,
                CreditBalance = user.CreditBalance,
                AccountStatus = user.AccountStatus
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = _db.Users.Find(Input.UserID);
            if (user == null) { NotFound = true; return Page(); }

            user.Name = Input.Name;
            user.Email = Input.Email;
            user.Phone = Input.Phone;
            user.AccountType = Input.AccountType;
            user.TravelRadius = Input.TravelRadius;
            user.ADAPreferences = Input.ADAPreferences;

            await _db.SaveChangesAsync();
            StatusMessage = "Profile updated successfully!";
            return Page();
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }
    }
}