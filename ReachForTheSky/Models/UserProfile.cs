public class UserProfile
{
    public int Id { get; set; }

    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public string ADAPreferences { get; set; }

    public double Rating { get; set; }

    public double CreditBalance { get; set; }

    public int TravelRadius { get; set; } // in miles
}
