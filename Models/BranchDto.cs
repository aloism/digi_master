using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcFull.Models
{
  public class BranchDto
  {
    [Required]
    [Display(Name = "Branch Name")]
    public string Name { get; set; }
    [Required]
    [Display(Name = "Physical Location Name")]
    public string PhysicalLocationName { get; set; }
    [Required]
    [Display(Name = "Geo Location")]
    public string GeoLocation { get; set; }
    [Required]
    [Display(Name = "Town")]
    public string Town { get; set; }
  }
}
