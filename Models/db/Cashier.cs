using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models.db
{
  [Table("cashiers")]
  public class Cashier
  {

    public uint Id { get; set; }

    public int? CompanyId { get; set; }

    public int? UserId { get; set; }
    public int? AccountType { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? IdNumber { get; set; }

    public string AgentNumber { get; set; } = string.Empty;

    public string Town { get; set; } = string.Empty;
    public string BusinessName { get; set; } = string.Empty;

    public int BranchId { get; set; } = 0;
    //public string CabinId { get; set; } = string.Empty;

    public string? Image { get; set; }

    public string Email { get; set; } = string.Empty;


    public string? Balance { get; set; }

    public string? Country { get; set; }

    public string Password { get; set; } = string.Empty;

    public string? Phone { get; set; }

    public int? Status { get; set; }

    public int? ApprovedId { get; set; }

    public DateTime? AdminUpdatedAt { get; set; }

    public string? AdminUpdateIp { get; set; }

    public int IsDeviceVerificationEnabled { get; set; }

    public int? WorkStatus { get; set; }

    public int? OnlineStatus { get; set; }

    public string? RememberToken { get; set; }

    public int? SupervisorAuth { get; set; }

    public string IpAddress { get; set; } = string.Empty;

    public DateTime? LastLogin { get; set; }

    public int? IsOnline { get; set; }

    public DateTime LastSeen { get; set; }

    public string? OfficeAddress { get; set; }

    public int? Cashbook { get; set; }

    public long? CashbookNumber { get; set; }

    public int? Expenses { get; set; }

    public int? RequestMoney { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string VerificationCode { get; set; } = string.Empty;

    public bool? EmailVerify { get; set; } = false;

    public DateTime EmailTime { get; set; }

    public int? BusinessLevel { get; set; }

    public int? Shipping { get; set; }

    public string DeviceId { get; set; } = "0-0";

    public int? TwoFaVerified { get; set; }

    public DateTime? TwFaVrDate { get; set; }

    public string? AuthToken { get; set; }

    public string? DeviceHardwareId { get; set; }

    public int? PinRetriesCounter { get; set; }

  }
}
