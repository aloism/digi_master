using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models.db
{
  [Table("in_auth_audits")]
  public class InAuthAudit
  {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column("company_id")]
    public int CompanyId { get; set; } = 0;

    [Required]
    [Column("user_id")]
    public int UserId { get; set; }

    [Column("amount")]
    [MaxLength(32)]
    public double Amount { get; set; }

    [Required]
    [Column("prev_float")]
    [MaxLength(200)]
    public string PrevFloat { get; set; } = "0";

    [Column("rate")]
    [MaxLength(32)]
    public double Rate { get; set; }

    [Column("currency")]
    [MaxLength(32)]
    public string Currency { get; set; }

    [Column("charge")]
    [MaxLength(32)]
    public string Charge { get; set; }

    [Required]
    [Column("ref")]
    [MaxLength(100)]
    public string Ref { get; set; }

    [Required]
    [Column("main")]
    public int Main { get; set; }

    [Column("type")]
    public int? Type { get; set; }

    [Required]
    [Column("status")]
    public int Status { get; set; } = 1;

    [Column("notes")]
    public string Notes { get; set; }

    [Column("stripe_id")]
    public string StripeId { get; set; }

    [Required]
    [Column("chargeback")]
    public int Chargeback { get; set; } = 0;

    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = new DateTime(2024, 12, 30, 22, 0, 0);

  }
}
