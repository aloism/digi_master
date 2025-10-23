using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models.db
{

  [Table("account_balances_history")]

  public class AccountBalanceHistory
  {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [Column("extras_id")]
    public int ExtrasId { get; set; }

    [Required]
    [Column("amount", TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }

    [Required]
    [MaxLength(5)]
    [Column("currency")]
    public string Currency { get; set; } = "$";

    [Required]
    [Column("prev_balance", TypeName = "decimal(10,2)")]
    public decimal PrevBalance { get; set; }

    [Required]
    [Column("new_balance", TypeName = "decimal(10,2)")]
    public decimal NewBalance { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("cb_type")]
    public string CbType { get; set; } = "FLOAT";

    [Required]
    [Column("debit_id")]
    public int DebitId { get; set; } = 0;

    [Required]
    [Column("credit_id")]
    public int CreditId { get; set; } = 0;

    [Required]
    [Column("type")]
    public int Type { get; set; } = 0;

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
    [MaxLength(500)]
    [Column("ref")]
    public string Ref { get; set; } = "0";

    [Required]
    [Column("request_ip")]
    public string RequestIp { get; set; }

    [Required]
    [Column("status")]
    public int Status { get; set; } = 0;

  }
}
