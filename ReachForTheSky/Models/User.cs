using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReachForTheSky.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Phone { get; set; }

        [Required, MaxLength(20)]
        public string AccountType { get; set; } = "Personal";

        [MaxLength(200)]
        public string? ADAPreferences { get; set; }

        public decimal Rating { get; set; } = 0;
        public decimal CreditBalance { get; set; } = 0;
        public int? TravelRadius { get; set; }
        public bool IsMinor { get; set; } = false;

        [Required, MaxLength(20)]
        public string AccountStatus { get; set; } = "Active";

        [Required, MaxLength(256)]
        public string PasswordHash { get; set; } = string.Empty;
    }
}