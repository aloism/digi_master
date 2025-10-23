using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models.db
{
  public class Bank
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    public int? BankId { get; set; }

    [Column(TypeName = "varchar(32)")]
    public string AcctNo { get; set; }

    [Column(TypeName = "varchar(max)")]
    public string AcctName { get; set; }

    [Column(TypeName = "varchar(max)")]
    public string AccountType { get; set; }

    [Column(TypeName = "varchar(max)")]
    public string RoutingNumber { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string MobileNumber { get; set; }

    [DefaultValue(0)]
    public int Status { get; set; }

    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; }
  }
}
