using Microsoft.AspNetCore.Identity;

namespace AspnetCoreMvcFull.Utils
{
  public class PasswordHasherhandler
  {
    private readonly IPasswordHasher<object> _passwordHasher;

    /// <summary>
    /// Initializes a new instance of the PasswordService.
    /// In a real application, the IPasswordHasher would be injected via dependency injection.
    /// </summary>
    public PasswordHasherhandler(IPasswordHasher<object> passwordHasher)
    {
      _passwordHasher = passwordHasher;
    }

    /// <summary>
    /// Verifies if a plain-text password matches a stored hashed password.
    /// </summary>
    /// <param name="hashedPassword">The hashed password retrieved from the database.</param>
    /// <param name="providedPassword">The plain-text password provided by the user.</param>
    /// <returns>True if the passwords match, otherwise false.</returns>
    public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
      // The first argument to VerifyHashedPassword is the user object, which is not
      // required for this simple verification and can be null.
      var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);

      // Return true if the verification was successful.
      return result == PasswordVerificationResult.Success;
    }
  }
}
