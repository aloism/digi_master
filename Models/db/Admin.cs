using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models.db
{
  public class Admin
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Password { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int? Message { get; set; }
    public int? Deposit { get; set; }
    public int? Transfer { get; set; }
    public int Inventory { get; set; } = 0;
    public int? Merchant { get; set; }
    public int? Charges { get; set; }
    public int FloatManagement { get; set; } = 0;
    public int Approvals { get; set; } = 0;
    public int Quickbooks { get; set; } = 0;
    public int CabinFloating { get; set; } = 0;
    public string? RememberToken { get; set; }
    public int Status { get; set; } = 0;
    public int IsChangePassword { get; set; } = 0;
    public DateTime LastChangePasswordAt { get; set; } = new DateTime(2024, 12, 31, 22, 0, 0);
    public DateTime LastLogin { get; set; } = new DateTime(2024, 12, 30, 22, 0, 0);
    public int SuperStatus { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = new DateTime(2022, 12, 31, 22, 0, 0);
  }

}
