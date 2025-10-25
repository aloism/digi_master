namespace AspnetCoreMvcFull.Interfaces
{
  public interface IAuditService
  {
    Task LogAuditAsync(int userId, string transactionRef, string logDetails);
  }
}
