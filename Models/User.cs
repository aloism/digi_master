using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models
{
  [Table("users")]
  public class User
  {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int OwnerId { get; set; } = 0;
    public int CompanyId { get; set; } = 0;
    public int ClientCode { get; set; } = 0;
    public int? ParentId { get; set; }

    public string StripeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [Required]
    [MaxLength(15)]
    public string IdNumber { get; set; } = "0";

    [Required]
    public string BusinessName { get; set; }

    [Required]
    [MaxLength(200)]
    public string TradingName { get; set; } = "0";

    public string Image { get; set; }

    [Required]
    [MaxLength(100)]
    public string Email { get; set; }

    public string SupportEmail { get; set; }
    public string Balance { get; set; }
    public string Country { get; set; }
    public string PaySupport { get; set; }

    [Required]
    [MaxLength(100)]
    public string Password { get; set; }

    public string Phone { get; set; }

    public int Status { get; set; } = 0;
    public int ApiEnabled { get; set; } = 0;

    public DateTime LastChangePasswordAt { get; set; } = new DateTime(2024, 12, 31, 22, 0, 0);
    public int SupervisorAuth { get; set; } = 0;

    [Required]
    [MaxLength(10)]
    public string InvoiceFlatFee { get; set; } = "0";

    [Required]
    [MaxLength(10)]
    public string PercentageBasedFee { get; set; } = "0";

    [Required]
    [MaxLength(10)]
    public string InvoicingType { get; set; } = "0";

    [Required]
    [MaxLength(10)]
    public string PaymentType { get; set; } = "0";

    [Required]
    [MaxLength(20)]
    public string Town { get; set; }

    [Required]
    [MaxLength(10)]
    public string UserCode { get; set; }

    public int IsOnline { get; set; } = 0;
    public DateTime LastSeen { get; set; } = new DateTime(2023, 12, 31, 22, 0, 0);

    public int MainAdmin { get; set; } = 0;
    public int SubCompany { get; set; } = 0;
    public int SignOffs { get; set; } = 0;
    public int Approvals { get; set; } = 0;
    public int Invoices { get; set; } = 0;
    public int IsAdmin { get; set; } = 0;
    public int IdAdmin { get; set; } = 0;
    public int Trips { get; set; } = 0;
    public int Director { get; set; } = 0;
    public int CollectionsLimit { get; set; } = 0;
    public int IsChangePassword { get; set; } = 0;
    public int Downloads { get; set; } = 0;

    [Required]
    [MaxLength(32)]
    public string IpAddress { get; set; }

    public string LastLogin { get; set; }
    public string KycLink { get; set; }
    public int KycStatus { get; set; } = 0;
    public string RememberToken { get; set; }
    public string OfficeAddress { get; set; }
    public string WebsiteLink { get; set; }
    public int Developer { get; set; } = 0;

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    [Required]
    [MaxLength(191)]
    public string VerificationCode { get; set; }

    public bool EmailVerify { get; set; }
    public DateTime EmailTime { get; set; }

    public string GoogleFaSecret { get; set; }
    public int FaStatus { get; set; } = 0;

    public string Facebook { get; set; }
    public string Twitter { get; set; }
    public string Instagram { get; set; }
    public string Linkedin { get; set; }
    public string Youtube { get; set; }
    public string FaExpiring { get; set; }

    [Required]
    [MaxLength(64)]
    public string PublicKey { get; set; }

    [Required]
    [MaxLength(64)]
    public string SecretKey { get; set; }

    public int BusinessLevel { get; set; } = 1;
    public int Shipping { get; set; } = 0;

    [Required]
    [MaxLength(15)]
    public string QbStatus { get; set; } = "0";

    [Required]
    [MaxLength(15)]
    public string QbId { get; set; } = "0";

    [Required]
    [MaxLength(500)]
    public string DeviceId { get; set; } = "0-0";

    public int TwoFaVerified { get; set; } = 0;
    public DateTime TwFaVrDate { get; set; } = new DateTime(2025, 1, 1);

    public string AuthToken { get; set; }
    public string DeviceHardwareId { get; set; }

    public int PinRetriesCounter { get; set; } = 0;
    public int CashbookModule { get; set; } = 0;
    public int HrmModule { get; set; } = 0;
    public int InventoryModule { get; set; } = 0;
    public int PosModule { get; set; } = 0;
    public int CitModule { get; set; } = 0;
    public int QuickBooksModule { get; set; } = 0;
    public int TasksModule { get; set; } = 0;
    public int ProcurementModule { get; set; } = 0;
    public int DigitalisationModule { get; set; } = 0;
    public int ApprovalsModule { get; set; } = 0;
    public int AppLogin { get; set; } = 0;
    public int RequestMoney { get; set; } = 0;
    public int Expenses { get; set; } = 0;
    public int RentalsModule { get; set; } = 0;
    public int BookingsModule { get; set; } = 0;
    public int InvoicesModule { get; set; } = 0;
    public int AssetsManagementModule { get; set; } = 0;


  }
}
