using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.Models;
using System;

namespace AspnetCoreMvcFull.Utils
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
  }
}
