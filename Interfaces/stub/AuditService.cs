using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.Models.db;

namespace AspnetCoreMvcFull.Interfaces.stub
{
  public class AuditService : IAuditService
  {
    private readonly AspnetCoreMvcFullContext _context;

    public AuditService(AspnetCoreMvcFullContext context)
    {
      _context = context;
    }

    public async Task LogAuditAsync(int userId, string transactionRef, string logDetails)
    {
      // 1. Create the Audit entry object
      /**/
      var audit = new Audit
      {
          UserId = userId,
          Trx = transactionRef,
          Log = logDetails,
          CreatedAt = DateTime.UtcNow // Use UTC for timestamps
      };

      // 2. Save to the database
      _context.Audits.Add(audit);
      await _context.SaveChangesAsync();

      // Placeholder implementation:
      Console.WriteLine($"AUDIT LOG: UserID={userId}, Ref={transactionRef}, Log='{logDetails}'");
      await Task.CompletedTask;
    }
  }
}
