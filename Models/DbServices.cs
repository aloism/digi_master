using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.Models.db;
using Microsoft.AspNetCore.Identity;
using System;

namespace AspnetCoreMvcFull.Models
{
  public class DbServices
  {
    private readonly AspnetCoreMvcFullContext _context;

    public DbServices()
    {
    }

    public DbServices(AspnetCoreMvcFullContext context)
    {
      _context = context;
    }

    public static string hashData(string plainTextPassword)
    {
      var passwordHasher = new PasswordHasher<object>();
      var passwordService = new Utils.PasswordHasher(passwordHasher);

      //// Hash a password to simulate a password stored in the database.
      string hashedPassword = passwordHasher.HashPassword(null, plainTextPassword);
      return hashedPassword;
    }

    public async Task<int> SaveAdminAsync(RegisterViewModel model)
    {

      try
      {

        var passwordHasher = new PasswordHasher<object>();
        var passwordService = new Utils.PasswordHasher(passwordHasher);

        string plainTextPassword = Utils.Utilities.intCodeRandom(8);// "MySecretPassword123";

        //// Hash a password to simulate a password stored in the database.
        string hashedPassword = passwordHasher.HashPassword(null, plainTextPassword);

        //// Verify the provided plain-text password against the hashed password.
        //bool isCorrect = passwordService.VerifyHashedPassword(hashedPassword, "MySecretPassword123");
        //Console.WriteLine($"Is the password correct? {isCorrect}");

        //bool isIncorrect = passwordService.VerifyHashedPassword(hashedPassword, "WrongPassword");
        //Console.WriteLine($"Is the wrong password correct? {isIncorrect}");

        // if (user != null && passwordService.VerifyHashedPassword(user.Password, model.Password))
        // if (user != null && user.Password.Equals(model.Password))
        var admin = new Admin
        {
          FirstName = model.FirstName,
          LastName = model.LastName,
          Username = model.Username,
          Password = hashedPassword,
          Phone = model.Phone,
          Message = 0,
          Deposit = 0,
          Transfer = 0,
          Merchant = 0,
          Charges = 0,
          RememberToken = Guid.NewGuid().ToString("N")
        };

        _context.Admin.Add(admin);
        await _context.SaveChangesAsync();

        return admin.Id;
      }
      catch (Exception ex)
      {
        Console.WriteLine("An error occurred: " + ex.Message);
      }

      return -1;
    }
    public async Task<int> CreateAdminAsync()
    {
      var admin = new Admin
      {
        FirstName = "Alois",
        LastName = "Chikomo",
        Username = "alois123",
        Password = "securePassword123",
        Phone = "0771234567",
        Message = 1,
        Deposit = 100,
        Transfer = 50,
        Merchant = 1,
        Charges = 5,
        RememberToken = Guid.NewGuid().ToString("N")
      };

      _context.Admin.Add(admin);
      await _context.SaveChangesAsync();

      return admin.Id;
    }


  }
}
