using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcFull.Models
{
  public class AddAccountFormModel
  {// The select element has an ID "country", but it represents the "Account Type".
    // I will name the property 'AccountType' for clarity based on the label.
    // The values are integers (0, 1, 2) in the HTML, so int is appropriate.
    [Required]
    [Display(Name = "Account Type")]
    public int AccountType { get; set; } 

    [Required]
    [Display(Name = "Branch ID")]
    public int SelectedBranchId { get; set; }
    // The select element has an ID "currency" and its values are strings ("$", "R", "ZWG").
    [Required]
    [Display(Name = "Currency")]
    public string Currency { get; set; }

    [Display(Name = "AuthCode")]
    public string? AuthCode { get; set; }

    // The text input has 'name="AccountName"'.
    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 3)]
    [Display(Name = "Account Name")]
    public string AccountName { get; set; }

    // The text input has 'name="Descriptipon"'.
    // NOTE: I've kept the spelling "Descriptipon" to match your HTML exactly,
    // but you should probably fix the spelling to "Description" in both the HTML and the model.
    [Required]
    [StringLength(500)]
    [Display(Name = "Description")] // Display name corrects the label for the user
    public string Descriptipon { get; set; }
    [Range(0.00, double.MaxValue, ErrorMessage = "The top up amount must be a positive number.")]
    public decimal InvAmount { get; set; } = 0.00M;

    // --- Optional: Properties for Populating Dropdowns ---

    // In a real application, you'd use these properties to pass the list of options 
    // from your controller/page model to the view for generating the <option> tags.

    public List<SelectListItem> AccountTypeOptions { get; set; }
    public List<SelectListItem> CurrencyOptions { get; set; }
  }
}
