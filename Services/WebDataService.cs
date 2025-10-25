using AspnetCoreMvcFull.Controllers;
using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Models.db;
using AspnetCoreMvcFull.Utils;
using Microsoft.CodeAnalysis.Scripting;
using System;

namespace AspnetCoreMvcFull.Services
{
  public class WebDataService
  {
    internal User getFxAccountDetails(AspnetCoreMvcFullContext db, string userName)
    {
      //  snoopDataContext db = new snoopDataContext();

      //  System.Data.Linq.ISingleResult<get_account_detailsResult> r = db.get_account_details(userName);
      return db.getUser(userName).ToList()[0];
      //  return r.ToList<get_account_detailsResult>()[0];

    }
      public static Dictionary<string, object> AuthTellerLocal(ILogger<ApiController> _logger,Cashier? cashier, string password)
      {
        _logger.LogInformation("authTellerLocal");

        var response = new Dictionary<string, object>
        {
          ["resultStatus"] = GlobalConstants.ON_FAILURE_RES,
          ["res_message"] = "Request Failed"
        };

        if (cashier != null)
        {

        var passwordService = new PasswordHasherhandler(DbServices.getPasswordHasher());
        bool isPasswordValid = passwordService.VerifyHashedPassword(cashier.Password, password);
          // Check password using BCrypt
          //bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, cashier.Password);

          if (isPasswordValid)
          {
            response["resultStatus"] = GlobalConstants.ON_SUCCESS_RES;
            response["res_message"] = "Request Successful";
            return response;
          }
        }
        else
        {
          _logger.LogInformation("Error Login");
          response["resultStatus"] = GlobalConstants.ON_WRONG_PINCODE;
          response["res_message"] = "Auth Not Failed.";
          return response;
        }

        return response;
      }
    
    public static string GenerateRandomString(int strength = 16)
    {
      string input = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

      var random = new Random();
      var result = string.Empty;

      for (int i = 0; i < strength; i++)
      {
        int index = random.Next(0, input.Length);
        result += input[index];
      }

      return result;
    }

    // A simple utility function (Replaces PHP str_random)
    //private static string GenerateRandomString(int length)
    //{
    //  const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    //  var random = new System.Random();
    //  return new string(System.Linq.Enumerable.Repeat(chars, length)
    //      .Select(s => s[random.Next(s.Length)]).ToArray());
    //}
  }
}
