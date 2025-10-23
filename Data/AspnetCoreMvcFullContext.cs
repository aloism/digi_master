using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Models.db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Data
{
    public class AspnetCoreMvcFullContext : DbContext
    {
    //    public AspnetCoreMvcFullContext (DbContextOptions<AspnetCoreMvcFullContext> options)
    //        : base(options)
    //    {
    //}
    public AspnetCoreMvcFullContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Admin> Admin { get; set; }
    public DbSet<History> History { get; set; }
    public DbSet<Audit> Audit { get; set; }
    public DbSet<InTransLog> TransLogs { get; set; }
    public DbSet<AccountBalance> AccountBalances { get; set; }
    public DbSet<InNotification> Notifications { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<InAuthAudit> InAuthAudits { get; set; }
    public DbSet<Settings> Settings { get; set; }
    public DbSet<AccountBalanceHistory> AccountBalanceHistoryRecords { get; set; }
    public DbSet<AspnetCoreMvcFull.Models.Transactions> Transactions { get; set; } = default!;
    public IQueryable<User> getUser(string userName)
    {
      SqlParameter pContactName = new SqlParameter("@UserName", userName);
      //return this.Users.FromSql("EXECUTE Customers_SearchCustomers @ContactName", pContactName);

      return this.Users.FromSql($"[dbo].[get_account_details] {userName}");
    }
  }
}
