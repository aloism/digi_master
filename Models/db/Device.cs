using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models.db
{
  [Table("devices")]
  public class Device
  {
    // Primary Key (id)
    [Key]
    public uint Id { get; set; } // int(11) UNSIGNED maps to uint or int, using uint for unsigned

    // Foreign/Reference Keys
    public int CompanyId { get; set; } // int(11)

    public int UserId { get; set; } // int(11)

    // Device Identification Fields
    [Required]
    [MaxLength(100)]
    public string Msisdn { get; set; } // varchar(100)

    [Required]
    [MaxLength(400)]
    public string DeviceName { get; set; } // varchar(400)

    [Required]
    [MaxLength(200)]
    public string DeviceOem { get; set; } // varchar(200)

    [Required]
    [MaxLength(200)]
    public string DeviceType { get; set; } // varchar(200)

    [Required]
    [MaxLength(200)]
    public string DeviceModel { get; set; } // varchar(200)

    [Required]
    [MaxLength(200)]
    public string DeviceHardwareId { get; set; } // varchar(200)

    [Required]
    [MaxLength(200)]
    public string PlatformOs { get; set; } // varchar(200)

    [Required]
    [MaxLength(200)]
    public string PlatformOsVersion { get; set; } // varchar(200)

    [Required]
    [MaxLength(200)]
    public string UserTypeString { get; set; } // varchar(200) - Renamed to avoid collision with int UserType

    [Required]
    [MaxLength(200)]
    public string VerificationCode { get; set; } // varchar(200)

    [Required]
    [MaxLength(500)]
    public string DeviceId { get; set; } // varchar(500)

    [Required]
    public string AuthToken { get; set; } // text

    public string UserAgent { get; set; } // text (nullable)

    public int UserType { get; set; } // int(11) - Note the collision with UserTypeString

    public string RefreshToken { get; set; } // text (nullable)

    public int SupervisorAuth { get; set; } // int(11)

    public int Status { get; set; } // int(11)

    public int DeviceTypeInt { get; set; } // int(11) - Renamed to avoid collision with string DeviceType

    public int WasVerified { get; set; } // int(11)

    // Timestamp and Date Fields

    // Note: EF Core will handle DEFAULT '2022-12-31 22:00:00' values.
    //[Column("last_auth_at", TypeName = "timestamp")]
    public DateTime LastAuthAt { get; set; } // timestamp

    //[Column("created_at", TypeName = "timestamp")]
    public DateTime CreatedAt { get; set; } // timestamp

    //[Column("updated_at", TypeName = "timestamp")]
    public DateTime UpdatedAt { get; set; } // timestamp

    // datetime fields should be DateTime? if they can be NULL, or DateTime otherwise.
    //[Column("month_added")]
    public DateTime MonthAdded { get; set; } // datetime

    //[Column("day_added")]
    public DateTime DayAdded { get; set; } // datetime

    public DateTime? AdminUpdatedAt { get; set; } // datetime (NULLABLE)

    // IP and Location Fields
    [Required]
    public string RequestIp { get; set; } // text

    public string Location { get; set; } // text (nullable)

    public string AdminUpdateIp { get; set; } // text (nullable)
  }
}
