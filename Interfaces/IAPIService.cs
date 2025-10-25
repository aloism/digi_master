namespace AspnetCoreMvcFull.Interfaces
{
  public interface IAPIService
  {
    Task<Dictionary<string, string>> SendSMSNotificationAsync(string msisdn, string message);
  }
}
