using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models.db
{
  [Table("in_trans_log")]
  public class InTransLog
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    // 'from' is a reserved keyword in C#, so we rename it and map explicitly if using EF
    public string From { get; set; }

    public string Result { get; set; }

    public string Reference { get; set; }
    public string ResponseCode { get; set; }

    public string HttpCode { get; set; }

    public string CAccountNumber { get; set; }

    public int UserId { get; set; }

    public string RequestBy { get; set; } = "0";

    public DateTime CreatedAt { get; set; } = new DateTime(2022, 12, 31, 22, 0, 0);

    public DateTime UpdatedAt { get; set; } = new DateTime(2022, 12, 31, 22, 0, 0);
  }

}
