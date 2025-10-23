using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcFull.Models
{
  public class UserDto
  {
    [Required]
    [StringLength(255)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(255)]
    public string LastName { get; set; }

    [Required]
    [StringLength(15)]
    public string IdNumber { get; set; }

    [Required]
    [StringLength(25, MinimumLength = 3)]
    public string Town { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; }

    [Required]
    [RegularExpression(@"^\d+$", ErrorMessage = "Phone must be numeric.")]
    public string Phone { get; set; }

    public string BusinessName { get; set; }

    public string PaymentType { get; set; }

    public string Country { get; set; }

    // Optional: Include Password if needed
    // [Required]
    // [StringLength(100, MinimumLength = 6)]
    // public string Password { get; set; }
  }
}
