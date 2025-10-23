using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models.db
{
  public class Audit
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int OwnerId { get; set; }

    public int CompanyId { get; set; }

    public int UserId { get; set; }

    [Required]
    [Column(TypeName = "varchar(16)")]
    public string Trx { get; set; }

    [Required]
    [Column(TypeName = "varchar(max)")]
    public string Log { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }
}
}
