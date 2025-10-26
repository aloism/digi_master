using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models.db
{
  [Table("admin_roles")]
  public class AdminRoles
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int? AdminId { get; set; }

    [Required]
    [DefaultValue(0)]
    public int SuperStatus { get; set; }

    [Required]
    [DefaultValue(0)]
    public int Deposit { get; set; }

    [Required]
    [DefaultValue(0)]
    public int Transfer { get; set; }

    [Required]
    [DefaultValue(0)]
    public int? AddCashier { get; set; }

    [Required]
    [DefaultValue(0)]
    public int Inventory { get; set; }

    [Required]
    [DefaultValue(0)]
    public int? Charges { get; set; }

    [Required]
    [DefaultValue(0)]
    public int? FloatManagement { get; set; }

    [Required]
    [DefaultValue(0)]
    public int? Approvals { get; set; }

    [Required]
    [DefaultValue(0)]
    public int? Floating { get; set; }

    [Required]
    [DefaultValue(0)]
    public int? IsChangePassword { get; set; }

    [Required]
    [DefaultValue(0)]
    public int? Status { get; set; }

    [Required]
    [Column(TypeName = "datetime2")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    [Column(TypeName = "datetime2")]
    public DateTime UpdatedAt { get; set; } = new DateTime(2025, 12, 31, 22, 0, 0);

  }
}
