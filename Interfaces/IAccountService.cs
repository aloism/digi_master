using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Models.db;

namespace AspnetCoreMvcFull.Interfaces
{
  public interface IAccountService
  {// Checks if the trading account float inventory is sufficient.
    Task<Admin> GetAdminUserAsync(string username);
    Task<bool> CheckPasswordSignInAsync(string username,string password);
    Task<ServiceResult> CheckInventoryAsync(int productId, decimal amount);

    // Handles the core logic of creating/updating the account, history, rates, and inventory.
    Task<ServiceResult> ProcessAccountCreationAsync(
        int userId,
        AddAccountFormModel model,
        int productId,
        string currencyCode,
        string accountName,
        string userIp
    );
  }
}
