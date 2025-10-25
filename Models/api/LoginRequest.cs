namespace AspnetCoreMvcFull.Models.api
{
  public class LoginRequest
  {
    public string Phone { get; set; }
    public string Password { get; set; }

    public string Username { get; set; }
    public string Pincode { get; set; }
    public string DeviceName { get; set; }
    public string DeviceOem { get; set; }
    public string DeviceType { get; set; }
    public string DeviceModel { get; set; }
    public string DeviceHardwareId { get; set; }
    public string PlatformOs { get; set; }
    public string PlatformOsVersion { get; set; }
    public int UserType { get; set; }
    public int? AppType { get; set; }


  }
}
