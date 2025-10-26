using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Models.db;
using AspnetCoreMvcFull.Services;
using AspnetCoreMvcFull.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace AspnetCoreMvcFull.Interfaces.stub
{
  public class AccountService : IAccountService
  {
    private readonly AspnetCoreMvcFullContext _context;

    public AccountService(AspnetCoreMvcFullContext context)
    {
      _context = context;
    }

    public async Task<Admin> GetAdminUserAsync(string username)
    {
      var admin = await _context.Admin.FirstOrDefaultAsync(i => i.Username == username);
      return admin;
    }

    public async Task<AdminRoles> GetUserLevelAsync(int id)
    {
      var admin = await _context.AdminRoles.FirstOrDefaultAsync(i => i.Id == id);
      return admin;
    }

    public async Task<bool> CheckPasswordSignInAsync(string username, string password)
    {
      var admin = await _context.Admin.FirstOrDefaultAsync(i => i.Username == username);

      var passwordService = new PasswordHasherhandler(DbServices.getPasswordHasher());
       if (admin != null && passwordService.VerifyHashedPassword(admin.Password, password))
      {
        return true;
      }
        return false;
    }
    public async Task<ServiceResult> CheckInventoryAsync(int productId, decimal amount)
    {
      // Equivalent to the PHP block checking InInventory
      /**/
      var inventory = await _context.Accounts
                                    .FirstOrDefaultAsync(i => i.ProductId == productId);

      if (inventory == null)
          return new ServiceResult { Message = "Please Notify Admin, Control Account Float not Declared." };

      if (inventory.Status == 0)
          return new ServiceResult { Message = "Please Notify Admin, Control Account not Active." };

      if (inventory.Amount <= 0 || inventory.Amount < amount)
          return new ServiceResult { Message = "Please Notify Admin, Control Account Float Inventory not sufficient." };
      

      // Assuming check passes for stub:
      return new ServiceResult { IsSuccessful = true };
    }
    public async Task<ServiceResult> ProcessAccountCreationAsync(
    int userId,
    AddAccountFormModel model,
    int productId,
    string currencyCode,
    string accountName,
    string userIp)
    {
      // Ensure you have:
      // using Microsoft.EntityFrameworkCore;
      // using System.Linq; 
      // using YourApp.Data; 
      // using YourApp.Models; // For all your In* and History models

      var now = DateTime.UtcNow;
      // 1. Setup Utility Variables (Date/Time)
      var dateParts = GetDateParts(now);
      //var monthAdded = Utils.Utilities.GetMonthJoinedDateTime(now);
     // var dayAdded = Utils.Utilities.GetDayJoinedDateTime(now);

      // Using a transaction ensures all database operations succeed or fail together.
      using (var transaction = await _context.Database.BeginTransactionAsync())
      {
        try
        {
          // 2. Find InAccounts (Replaces multiple InAccounts::where calls)
          var inAccount = await _context.Accounts
              .FirstOrDefaultAsync(a => a.AccountType == model.AccountType || a.Name == accountName);

          bool isNewAccount = (inAccount == null);

          decimal prevBalance = 0;
          decimal newBalance = 0;
          string reqMessage = "";
          string notificationMessage = "";
          string accountNumber = Utils.Utilities.intCodeRandom(9);

          if (isNewAccount)
          {
            // --- Creation Logic (New Account) ---

            // 2a. Create InAccounts entry (cbook_inventory)
            var newAccount = new Accounts
            {
              AccountNumber = accountNumber,
              ParentId = model.SelectedBranchId,
              ProductId = productId,
              Name = accountName,
              Amount = model.InvAmount,
              AccountType = model.AccountType,
              AccountCategory = 1,
              MinRate = "0",//model.MinAccRate,
              MaxRate = "0", // Hardcoded '0' as in PHP
              Currency = currencyCode,
              MonthAdded = dateParts.MonthAdded,
              DayAdded = dateParts.DayAdded,
              RateDayAdded = Utils.Utilities.GetDayJoinedDateTime(now),
              RequestIp = userIp,
              CreatedAt = DateTime.UtcNow,
              Status = 1
            };
            _context.Accounts.Add(newAccount);
            await _context.SaveChangesAsync();

            // Fetch the created account to get its ID for history (necessary if auto-ID isn't immediately available)
            inAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.ProductId == productId && a.AccountType == model.AccountType);

            prevBalance = 0;
            newBalance = newAccount.Amount;

            reqMessage = "Branch Account was successfully created";
            notificationMessage = $"Alert- Account, Company Branch Account has been declared, Current Balance: {currencyCode}{newAccount.Amount:N2}";
          }
          else
          {
            if (model.InvAmount > 0)
            {
              // --- Update Logic (Existing Account - Top-up) ---
              prevBalance = inAccount.Amount;
              newBalance = inAccount.Amount + model.InvAmount;

              inAccount.Amount = newBalance;
              await _context.SaveChangesAsync();

              reqMessage = "Branch Account was successfully created";
              notificationMessage = $"Alert- Account, Branch Control Account has been updated from {currencyCode}{prevBalance:N2}, Current Balance: {currencyCode}{newBalance:N2}";

            }
          }

          if (model.InvAmount > 0)
          {
            // --- Common History and Rates Logging ---

            // 2b/3c. Create InAccountsHistory entry (inaccount_his)
            var accountHistory = new AccountsHistory
            {
              AccountId = inAccount.Id,
              TransType = productId.ToString(),
              TellerId = userId.ToString(),
              Amount = model.InvAmount,
              PrevBalance = prevBalance,
              NewBalance = newBalance,
              MinRate = "0",//model.MinAccRate,
              MaxRate = "0", // Hardcoded '0' as in PHP
              Currency = currencyCode,
              AccountCategory = (int)inAccount.AccountCategory,
              Description = string.Empty,
              Ref = string.Empty,
              MonthAdded = dateParts.MonthAdded,
              DayAdded = dateParts.DayAdded,
              RequestIp = userIp,
              CreatedAt = now,
              DebitId = 0,
              CreditId = 1,
              Type = 3, // Top up
              Status = 1
            };
            _context.AccountsHistory.Add(accountHistory);

            // 4. Create InRatesHistory entry (rates_his)
            //var ratesHistory = new InRatesHistory
            //{
            //  AccountId = inAccount.Id,
            //  RateType = productId,
            //  MinAmount = 0,
            //  MaxAmount = 0,
            //  MinRate = model.MinAccRate,
            //  MaxRate = 0, // Hardcoded '0' as in PHP
            //  Currency = inAccount.Currency,
            //  MonthAdded = dateParts.MonthAdded,
            //  DayAdded = dateParts.DayAdded,
            //  RequestIp = userIp,
            //  CreatedAt = DateTime.UtcNow,
            //  Status = 1
            //};
            //_context.InRatesHistory.Add(ratesHistory);

            // 5. Update Control Account (Debit the float) and Create AccountsHistory
            var controlAccount = await _context.Accounts.FirstOrDefaultAsync(i => i.ProductId == productId);

            if (controlAccount != null) // Should not be null due to earlier check, but safe guard it
            {
              var invPrevBalance = controlAccount.Amount;
              var invNewBalance = controlAccount.Amount - model.InvAmount;

              controlAccount.Amount = invNewBalance;
              await _context.SaveChangesAsync(); // Save the inventory update

              var inventoryHistory = new AccountsHistory
              {
                AccountId = controlAccount.Id,
                TransType = productId.ToString(),
                TellerId = userId.ToString(),
                Amount = invNewBalance,
                PrevBalance = invPrevBalance,
                NewBalance = newBalance,
                MinRate = "0",//model.MinAccRate,
                MaxRate = "0", // Hardcoded '0' as in PHP
                Currency = currencyCode,
                AccountCategory = (int)controlAccount.AccountCategory,
                Description = string.Empty,
                Ref = string.Empty,
                MonthAdded = dateParts.MonthAdded,
                DayAdded = dateParts.DayAdded,
                RequestIp = userIp,
                CreatedAt = now,
                DebitId = 1,
                CreditId = 0,
                Type = 0, // Transfer Out
                Status = 1
              };
              _context.AccountsHistory.Add(inventoryHistory);
            }
          }
          // 6. Create History entry (his)
          var history = new History
          {
            UserId = userId,
            Type = isNewAccount ? GlobalConstants.CREATE_T_ACCOUNT : GlobalConstants.UPDATE_T_ACCOUNT,
            Amount = model.InvAmount,
            Ref = productId.ToString(), // Ref is the product ID in PHP
            Main = 1,
            StripeId = "0",
            Charge = 0,
            CreatedAt = DateTime.UtcNow
          };
          _context.History.Add(history);

          await _context.SaveChangesAsync();
          await transaction.CommitAsync();

          // 7. Final Return
          return new ServiceResult
          {
            IsSuccessful = true,
            Message = reqMessage, // User-facing success message
            NotificationMessage = notificationMessage // SMS/Alert message
          };
        }
        catch (Exception ex)
        {
          await transaction.RollbackAsync();
          // Log the exception (e.g., using a dedicated logger)
          Console.WriteLine($"Transaction failed: {ex.Message}");

          return new ServiceResult
          {
            IsSuccessful = false,
            Message = "An error occurred during account processing. Please try again or contact support."
          };
        }
      }
    }

    // NOTE: GlobalConstants would be a static class in C# holding your integer constants.
    public async Task<ServiceResult> ProcessAccountCreationAsyncxx(
        int userId,
        AddAccountFormModel model,
        int productId,
        string currencyCode,
        string accountName,
        string userIp)
    {
      // This method replaces all the complex database logic after the authentication check.

      // 1. Setup Utility Variables (Date/Time)
      var dateParts = GetDateParts(DateTime.UtcNow);

      // 2. Find/Create InAccounts (Replaces multiple InAccounts::where calls)
      /*
      */
      var inAccount = await _context.Accounts
          .FirstOrDefaultAsync(a => a.AccountType == model.AccountType || a.Name == accountName);

      bool isNewAccount = (inAccount == null);

      decimal prevBalance = 0;
      decimal newBalance = 0;
      string reqMessage = "";
      string notificationMessage = "";

      if (isNewAccount)
      {
          // --- Creation Logic (New Account) ---
          // 2a. Create InAccounts entry (cbook_inventory)
          // 2b. Create InAccountsHistory entry (inaccount_his)
          // 2c. Set success messages
          // ...
      }
      else
      {
          // --- Update Logic (Existing Account) ---
          // 3a. Calculate new balance: $_new_balance = (inaccount->amount + request->inv_amount)
          // 3b. Update InAccounts amount and Save
          // 3c. Create InAccountsHistory entry (inaccount_his)
          // 3d. Set success messages
          // ...
      }

      // 4. Create InRatesHistory entry (rates_his)
      // 5. Update InInventory (Debit the float) and Create InInventoryHistory
      // 6. Create History entry (his)

      // 7. Final Return
      return new ServiceResult
      {
          IsSuccessful = true,
          Message = reqMessage, // User-facing success message
          NotificationMessage = notificationMessage // SMS/Alert message
      };

      // Placeholder return for stub:
      return new ServiceResult
      {
        IsSuccessful = true,
        Message = "Trading Account was successfully processed (STUB).",
        NotificationMessage = $"Account {accountName} created/updated with {currencyCode}{model.InvAmount}"
      };
    }

    // Helper method to get the custom date formats (replaces PHP Utils methods)
    public static DateParts GetDateParts(DateTime date)
    {
      // Matches PHP's date formatting for the history tables
      return new DateParts
      {
        MonthAdded = Utils.Utilities.GetMonthJoinedDateTime(date),//date.ToString("MMM-yyyy"), // e.g., "Oct-2025"
        DayAdded = Utils.Utilities.GetDayJoinedDateTime(date),//date.Day.ToString()          // e.g., "23"
      };
    }

  }
}
