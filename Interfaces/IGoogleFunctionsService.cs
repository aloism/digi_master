namespace AspnetCoreMvcFull.Interfaces
{
  public interface IGoogleFunctionsService
  {
    Task<Dictionary<string, string>> DeviceRegOTPAsync(string msisdn);
  }
}
