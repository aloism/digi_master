using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcFull.Models
{
  public class LoginViewModel
  {
    [Required]
    [Display(Name = "User Name")]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
  }
}
