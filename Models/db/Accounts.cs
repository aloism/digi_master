using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models.db
{
  [Table("in_accounts")]
  public class Accounts
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int ProductId { get; set; }

    public int? ParentId { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Name { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public decimal Amount { get; set; }

    [Required]
    [DefaultValue(0)]
    public int AccountType { get; set; }

    [Required]
    [DefaultValue(0)]
    public int AccountCategory { get; set; }

    [Column(TypeName = "varchar(10)")]
    public string AccountCode { get; set; }

    [Required]
    [Column(TypeName = "varchar(10)")]
    [DefaultValue("USD")]
    public string Currency { get; set; }

    [Required]
    [Column(TypeName = "varchar(30)")]
    public string MinRate { get; set; }

    [Required]
    [Column(TypeName = "varchar(30)")]
    public string MaxRate { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string MinFlmAmount { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string MaxFlmAmount { get; set; }

    [Required]
    [DefaultValue(0)]
    public int IsBlendedRate { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string BlendedRate { get; set; }

    [Required]
    public DateTime BlendedRDate { get; set; } = new DateTime(2023, 12, 31, 22, 0, 0);

    [Required]
    public DateTime CreatedAt { get; set; } = new DateTime(2023, 12, 31, 22, 0, 0);

    [Required]
    public DateTime UpdatedAt { get; set; } = new DateTime(2023, 12, 31, 22, 0, 0);

    [Required]
    public DateTime MonthAdded { get; set; }

    [Required]
    public DateTime DayAdded { get; set; }

    [Required]
    public DateTime RateDayAdded { get; set; }

    [Required]
    [Column(TypeName = "text")]
    public string RequestIp { get; set; }

    [Required]
    [DefaultValue(0)]
    public int WasApproved { get; set; }

    public int? ApprovedBy { get; set; }

    [Required]
    [DefaultValue(0)]
    public int Status { get; set; }

  }
}
