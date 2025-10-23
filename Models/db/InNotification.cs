using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models.db
{
  [Table("in_notifications")]
  public class InNotification
  {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column("company_id")]
    public int CompanyId { get; set; } = 0;

    [Required]
    [Column("user_id")]
    [MaxLength(20)]
    public string UserId { get; set; }

    [Required]
    [Column("user_type")]
    public int UserType { get; set; }

    [Required]
    [Column("title")]
    [MaxLength(50)]
    public string Title { get; set; }

    [Required]
    [Column("message")]
    [MaxLength(800)]
    public string Message { get; set; }

    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = new DateTime(2022, 12, 31, 22, 0, 0);

    [Required]
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = new DateTime(2022, 12, 31, 22, 0, 0);

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
