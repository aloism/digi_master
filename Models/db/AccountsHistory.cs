using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models.db
{
  [Table("in_accounts_history")]
  public class AccountsHistory
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    public int? AccountId { get; set; }

    public int? ParentId { get; set; }

    public int? CustomerId { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string TellerId { get; set; }

    [Required]
    public decimal PrevBalance { get; set; }

    [Required]
    public decimal NewBalance { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string TransType { get; set; }

    [DefaultValue(0)]
    public int Type { get; set; }

    [DefaultValue(0)]
    public int DebitId { get; set; }

    [DefaultValue(0)]
    public int CreditId { get; set; }

    [Required]
    public decimal Amount { get; set; }

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

    [Required]
    [Column(TypeName = "varchar(200)")]
    public string Description { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = new DateTime(2023, 12, 31, 22, 0, 0);

    [Required]
    public DateTime MonthAdded { get; set; }

    [Required]
    public DateTime DayAdded { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; } = new DateTime(2023, 12, 31, 22, 0, 0);

    [Required]
    [Column(TypeName = "text")]
    public string RequestIp { get; set; }

    [Column(TypeName = "text")]
    public string Location { get; set; }

    [Required]
    [DefaultValue(0)]
    public int Status { get; set; }

    [Required]
    [DefaultValue(0)]
    public int ApprovedId { get; set; }

    [Required]
    [DefaultValue(0)]
    public int AccountCategory { get; set; }

    public DateTime? AdminUpdatedAt { get; set; }

    [Column(TypeName = "text")]
    public string AdminUpdateIp { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Ref { get; set; }

  }
}
