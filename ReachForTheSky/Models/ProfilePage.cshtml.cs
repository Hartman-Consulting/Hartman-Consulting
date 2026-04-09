using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class ProfilePageModel : PageModel
{
    [BindProperty]
    public UserProfile User { get; set; }

    private static UserProfile _user = new UserProfile
    {
        Id = 1,
        Name = "Jane Doe",
        Email = "jane@example.com",
        PhoneNumber = "123-456-7890",
        ADAPreferences = "Wheelchair accessible locations preferred",
        Rating = 4.8,
        CreditBalance = 12.5,
        TravelRadius = 10
    };

    public void OnGet()
    {
        User = _user;
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _user.Name = User.Name;
        _user.Email = User.Email;
        _user.PhoneNumber = User.PhoneNumber;
        _user.ADAPreferences = User.ADAPreferences;
        _user.TravelRadius = User.TravelRadius;

        return RedirectToPage();
    }
}
