using System.Text.RegularExpressions;

namespace AspnetCoreMvcFull.Utilities
{
  public static class RegexConvert
  {
    #region STRINGS REGEX

    public static string ToAlphaNumericOnly(this string input)
    {
      Regex rgx = new Regex("[^a-zA-Z0-9]");
      return rgx.Replace(input, "");
    }

    public static string ToAlphaOnly(this string input)
    {
      Regex rgx = new Regex("[^a-zA-Z]");
      return rgx.Replace(input, "");
    }

    public static string ToNumericOnly(this string input)
    {
      Regex rgx = new Regex("[^0-9]");
      return rgx.Replace(input, "");
    }

    #endregion
  }
}
