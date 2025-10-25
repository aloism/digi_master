using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models.db
{
  public class Product
  {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [DefaultValue(0)]
    public int CompanyId { get; set; }

    [Required]
    public int UserId { get; set; }

    public int? CatId { get; set; }

    [Required]
    [Column(TypeName = "varchar(255)")]
    public string Name { get; set; }

    [Required]
    [Column(TypeName = "varchar(32)")]
    public string Amount { get; set; }

    [Required]
    [DefaultValue(0)]
    public int CashExchange { get; set; }

    [Required]
    [DefaultValue(0)]
    public int HasInventory { get; set; }

    [Column(TypeName = "varchar(32)")]
    public string Rate { get; set; }

    [Column(TypeName = "varchar(32)")]
    public string Currency { get; set; }

    [Required]
    public int Quantity { get; set; }

    [DefaultValue(0)]
    public int Sold { get; set; }

    public int? Rq { get; set; }

    public int? Charge { get; set; }

    [Column(TypeName = "text")]
    public string Address { get; set; }

    [Column(TypeName = "text")]
    public string Note { get; set; }

    [Required]
    [DefaultValue(0)]
    public int AddStatus { get; set; }

    [Required]
    [DefaultValue(0)]
    public int QuantityStatus { get; set; }

    [Required]
    [DefaultValue(0)]
    public int NoteStatus { get; set; }

    [Column(TypeName = "text")]
    public string Description { get; set; }

    [Required]
    [DefaultValue(1)]
    public int Status { get; set; }

    [DefaultValue(1)]
    public int Active { get; set; }

    [Required]
    [Column(TypeName = "varchar(16)")]
    public string RefId { get; set; }

    [DefaultValue(0)]
    public int New { get; set; }

    [Required]
    [DefaultValue(0)]
    public int ShippingStatus { get; set; }

    public int? ShippingFee { get; set; }

    [Required]
    [DefaultValue(0)]
    public int InStatus { get; set; }

    [Required]
    [DefaultValue(0)]
    public int OutStatus { get; set; }

    [Required]
    [DefaultValue(0)]
    public int SpecialStatus { get; set; }

    [Required]
    [DefaultValue(0)]
    public int AllocationStatus { get; set; }

    [Required]
    [DefaultValue(0)]
    public int InventoryStatus { get; set; }

    [Required]
    [Column(TypeName = "varchar(10)")]
    [DefaultValue("0")]
    public string ExchangeRate { get; set; }

    [Required]
    [Column(TypeName = "varchar(10)")]
    [DefaultValue("0")]
    public string OutPaymentMethod { get; set; }

    [Required]
    [DefaultValue(0)]
    public int IsMultiCurrency { get; set; }

    [Required]
    [DefaultValue(0)]
    public int MainDefloatEnabled { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }

  }
}
