using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models.db
{
  public class Branch
  {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [DefaultValue(0)]
    public int OwnerId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; }

    [Required]
    [Column(TypeName = "varchar(100)")]
    public string PhysicalLocation { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string AccountNumber { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string Email { get; set; }

    [Required]
    [Column(TypeName = "varchar(200)")]
    public string Balance { get; set; }

    [Required]
    [Column(TypeName = "varchar(5)")]
    [DefaultValue("$")]
    public string Currency { get; set; }

    [Required]
    [DefaultValue(0)]
    public int CompanyId { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = new DateTime(2022, 12, 31, 22, 0, 0);

    [Required]
    public DateTime MonthAdded { get; set; }

    [Required]
    public DateTime DayAdded { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; } = new DateTime(2022, 12, 31, 22, 0, 0);

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

    [DefaultValue(0)]
    public int MainAccSales { get; set; }

    [Required]
    [Column(TypeName = "bigint")]
    [DefaultValue(0)]
    public long Trnnr { get; set; }

    [Required]
    [Column(TypeName = "varchar(200)")]
    [DefaultValue("0")]
    public string MaxFloatLvTrigger { get; set; }

    [Required]
    [Column(TypeName = "varchar(200)")]
    [DefaultValue("0")]
    public string MinFloatLvTrigger { get; set; }

    public DateTime? AdminUpdatedAt { get; set; }

    [Column(TypeName = "text")]
    public string AdminUpdateIp { get; set; }

  }
}
