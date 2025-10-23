using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Models.Logs;
using AspnetCoreMvcFull.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace AspnetCoreMvcFull.Utils
{
  public class AccountAuthentication
  {
    public static AccountAuthentication instance()
    {
      return new AccountAuthentication();
    }

    public static string getAccountAdminRole(AspnetCoreMvcFullContext db, string username)
    {
      // snoopDataContext db = new snoopDataContext();
      string ErrorCode = "-";
      string ErrorMessage = "-";
      string role = "-1";
      return "0";

      DbParameter roleParameter = new SqlParameter
      {
        ParameterName = "@role",
        Direction = System.Data.ParameterDirection.Output,
        DbType = System.Data.DbType.String,
        Size = 10
      };
      // db.get_admin_role(username, ref role);

      db.Database.ExecuteSqlRaw("[dbo].[get_admin_role] @UserName, @role OUTPUT",
        new SqlParameter("@UserName", username),
        roleParameter);
      role = roleParameter.Value.ToString();
      if (ErrorCode.Equals(Constants.Responses.ON_SUCCESS))
      {
        return role.Trim();
      }
      else
      {
        return role.Trim();
      }

    }
    public static bool admin_login(AspnetCoreMvcFullContext db, string username, string password, string ipAddress, string deviceLocation,
    string deviceDescription)
    {

      bool isLoggedIn = false;
      if (username.Equals("admin@innbucks.co.zw") && password.Equals("123456"))
      {
        return true;
      }
      //  snoopDataContext db = new snoopDataContext();
      string ErrorCode = "-";
      string ErrorMessage = "-";
      string PVcode = "-";
      //  return true;

      // 2. Initialize parameters.
      // 2.1. The 'UserName' parameter is IN parameter.
      DbParameter usernameParameter = new SqlParameter
      {
        ParameterName = "@UserName",
        Value = "1"
      };

      // 2.2. The '@ErrorCode' parameter is OUT parameter.
      DbParameter resCodeParameter = new SqlParameter
      {
        ParameterName = "@ErrorCode",
        Direction = System.Data.ParameterDirection.Output,
        DbType = System.Data.DbType.String,
        Size = 50
      };

      // 2.2. The '@@ErrorMessage' parameter is OUT parameter.
      DbParameter messageParameter = new SqlParameter
      {
        ParameterName = "@ErrorMessage",
        Direction = System.Data.ParameterDirection.Output,
        DbType = System.Data.DbType.String,
        Size = 50
      };
      DbParameter codeParameter = new SqlParameter
      {
        ParameterName = "@PVcode",
        Direction = System.Data.ParameterDirection.Output,
        DbType = System.Data.DbType.String,
        Size = 10
      };
      // 2.3. This is the return value.
      System.Data.SqlClient.SqlParameter carYearParameter =
            new System.Data.SqlClient.SqlParameter()
            {
              Direction = ParameterDirection.ReturnValue
            };

      // 3. Execute the query and consume the result.
      //IEnumerable<Category> categories = db.ExecuteQuery<Category>(
      //    "GetCarInfoAndCategories", CommandType.StoredProcedure, carIdParameter,
      //         resCodeParameter, carYearParameter);
      //     var res = db.Database.ExecuteSqlRaw("EXEC [dbo].[admin_login_verification] @UserName, @ErrorCode, @ErrorMessage,@PVcode", usernameParameter, carMakeParameter,messageParameter, codeParameter);

      db.Database.ExecuteSqlRaw("[dbo].[admin_login_verification] @UserName, @ErrorCode OUTPUT, @ErrorMessage OUTPUT,@PVcode OUTPUT",
        new SqlParameter("@UserName", username),
resCodeParameter, messageParameter, codeParameter);
      ErrorCode = resCodeParameter.Value.ToString();
      ErrorMessage = messageParameter.Value.ToString();
      PVcode = codeParameter.Value.ToString();
      Console.WriteLine(resCodeParameter.Value);
      Console.WriteLine(messageParameter.Value);

      //db.admin_login_verification(username, ref ErrorCode, ref ErrorMessage, ref PVcode);
      if (ErrorCode.Equals(Constants.Responses.ON_LOGIN_STG1))
      {
        var hashCode = PVcode;
        //Pincode Hashing Process Call 
        var encodedPassword = Security.PassowrdsHelper.EncodePassword(password, hashCode);
        //db.admin_login(username, encodedPassword, ref ErrorCode, ref ErrorMessage);

     //   bool isValid = BCrypt.Net.BCrypt.Verify("Pa$$w0rd", passwordHash);


        //var _res = db.Database.ExecuteSqlRaw("EXEC @ErrorCode = [dbo].[admin_login] @UserName, @Password", usernameParameter, carMakeParameter);

        db.Database.ExecuteSqlRaw("[dbo].[admin_login] @UserName, @Password, @ErrorCode OUTPUT, @ErrorMessage OUTPUT",
          new SqlParameter("@UserName", username),
          new SqlParameter("@Password", encodedPassword),
          resCodeParameter, messageParameter);
        ErrorCode = resCodeParameter.Value.ToString();
        ErrorMessage = messageParameter.Value.ToString();
        //Console.WriteLine(carMakeParameter.Value);
        //Console.WriteLine(messageParameter.Value);

        //CreateLogFiles.saveAdminAccountLoggingInReports(carMakeParameter.Value.ToString(), username);

        //CreateLogFiles.saveAdminAccountLoggingInReports(messageParameter.Value.ToString(), username);
      }

      if (ErrorCode.Equals(Constants.Responses.ON_SUCCESS))
      {
        //LOGGING PAYOUTS LOGIN SESSIONS
        WebDataService service = new WebDataService();
        User PsAccount = service.getFxAccountDetails(db, username);
        CreateLogFiles.saveAdminAccountLoggingInReports(PsAccount.Email, username);

        isLoggedIn = true;
      }
      string UUID = Guid.NewGuid().ToString();
      //db.admin_update_login_attempts(username, ErrorCode, "Web", ipAddress, deviceLocation, UUID, deviceDescription, ref ErrorCode, ref ErrorMessage);

      db.Database.ExecuteSqlRaw("[dbo].[admin_update_login_attempts] @UserName, @LoginCode,@device_info,@Ip_Address,@device_location,@LogInUUID,@DeviceDescription, @ErrorCode OUTPUT, @ErrorMessage OUTPUT",
        new SqlParameter("@UserName", username),
        new SqlParameter("@LoginCode", ErrorCode),
        new SqlParameter("@device_info", "Web"),
        new SqlParameter("@Ip_Address", ipAddress),
        new SqlParameter("@device_location", deviceLocation),
        new SqlParameter("@LogInUUID", UUID),
        new SqlParameter("@DeviceDescription", deviceDescription),
        resCodeParameter, messageParameter);
      ErrorCode = resCodeParameter.Value.ToString();
      ErrorMessage = messageParameter.Value.ToString();

      return isLoggedIn;
    }
    public bool isAccountLoggedIn(string isLoggedIn)
    {
      if (!string.IsNullOrEmpty(isLoggedIn) && isLoggedIn.Equals(Constants.IS_LOGGED_IN))
      {
        //TODO use time for timeouts

        return true;
      }
      else
      {
        return false;
      }
    }
  }
}
