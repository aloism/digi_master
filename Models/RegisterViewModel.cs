using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcFull.Models
{
  public class RegisterViewModel
  {
    [Required]
    [Display(Name = "User Name")]
    public string Username { get; set; }
    [Required]
    [Display(Name = "User FirstName")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "User LastName")]
    public string LastName { get; set; }

    [Required]
    [Display(Name = "Phone")]
    public string Phone { get; set; }

    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
  }
}
