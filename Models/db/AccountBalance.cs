using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models.db
{
  [Table("account_balances")]
  public class AccountBalance
  {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column("company_id")]
    public int CompanyId { get; set; } = 0;

    [Required]
    [Column("cabin_id")]
    public int CabinId { get; set; }

    [Required]
    [Column("product_id")]
    public int ProductId { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("name")]
    public string Name { get; set; }

    [Required]
    [MaxLength(20)]
    [Column("account_number")]
    public string AccountNumber { get; set; }

    [Required]
    [Column("balance_type")]
    public int BalanceType { get; set; }

    [Required]
    [Column("balance", TypeName = "decimal(10,2)")]
    public decimal Balance { get; set; }

    [Required]
    [MaxLength(5)]
    [Column("currency")]
    public string Currency { get; set; } = "$";

    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = new DateTime(2022, 12, 31, 20, 0, 0);

    [Required]
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = new DateTime(2022, 12, 31, 20, 0, 0);

    [Required]
    [Column("month_added")]
    public DateTime MonthAdded { get; set; }

    [Required]
    [Column("day_added")]
    public DateTime DayAdded { get; set; }

    [Required]
    [Column("request_ip")]
    public string RequestIp { get; set; }

    [Required]
    [Column("status")]
    public int Status { get; set; } = 0;

  }
}
