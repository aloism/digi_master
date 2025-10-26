using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcFull.Models
{
  public class AddCashierFormModel
  {

    [Required]
    [Display(Name = "Branch ID")]
    public int SelectedBranchId { get; set; }
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
    [Display(Name = "User IdNumber")]
    public string IdNumber { get; set; }

    [Required]
    [Display(Name = "Phone")]
    public string Phone { get; set; }

    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; }

    // The select element has an ID "currency" and its values are strings ("$", "R", "ZWG").
    [Required]
    [Display(Name = "Currency")]
    public string Currency { get; set; }

    [Display(Name = "AgentNumber")]
    public string? AgentNumber { get; set; }

    [Display(Name = "Account Type")]
    public int? AccountType { get; set; }

  }
}
