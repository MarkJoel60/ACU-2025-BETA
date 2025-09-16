// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Extensions.StringExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.Common.Extensions;

public static class StringExtensions
{
  public static string Capitalize(this string text)
  {
    return string.IsNullOrEmpty(text) ? text : char.ToUpper(text[0]).ToString() + text.Substring(1);
  }

  /// <summary>
  /// Capitalizes the first character of the string and converts the remaining characters to lowercase.
  /// </summary>
  /// <param name="input">The input string to be formatted.</param>
  /// <returns>The formatted string with the first character capitalized and the remaining characters in lowercase.</returns>
  /// <exception cref="T:System.ArgumentNullException">Thrown if the input string is null.</exception>
  public static string CapitalizeFirstAndLowerRest(this string input)
  {
    switch (input)
    {
      case null:
        throw new ArgumentNullException(nameof (input));
      case "":
        return input;
      default:
        if (input.Length == 1)
          return char.ToUpper(input[0]).ToString();
        string str1 = char.ToUpper(input[0]).ToString();
        string str2 = input;
        string lower = str2.Substring(1, str2.Length - 1).ToLower();
        return str1 + lower;
    }
  }

  /// <summary>
  /// Removes all leading occurrences of whitespace from the current string
  /// object. Does not throw an exception if string is null.
  /// </summary>
  /// <param name="str">The string object to remove leading whitespace from. Can safely be null.</param>
  /// <returns>The string that remains after all occurrences of leading whitespace are removed
  /// from <paramref name="str" />, or null if <paramref name="str" /> was null itself.</returns>
  public static string SafeTrimStart(this string str) => str == null ? str : str.TrimStart();
}
