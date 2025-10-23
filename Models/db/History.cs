using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models.db
{
  public class History
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public int UserId { get; set; }

    public decimal Amount { get; set; }

    [Required]
    public decimal PrevFloat { get; set; }

    public decimal Rate { get; set; }

    [Column(TypeName = "varchar(32)")]
    public string Currency { get; set; }

    public decimal Charge { get; set; }

    [Required]
    [Column(TypeName = "varchar(100)")]
    public string Ref { get; set; }

    public int Main { get; set; }

    public int? Type { get; set; }

    public int Status { get; set; }

    [Column(TypeName = "text")]
    public string Notes { get; set; }

    [Column(TypeName = "text")]
    public string StripeId { get; set; }

    public int Chargeback { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }
  
}
}
