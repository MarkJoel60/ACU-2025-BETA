// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.NumberHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.PM;

internal static class NumberHelper
{
  internal static string IncreaseNumber(string number, int increaseValue)
  {
    int lastNumericDigitsWithZeroCount;
    string str1 = (NumberHelper.GetNumericValue(number, out lastNumericDigitsWithZeroCount) + (long) increaseValue).ToString();
    string str2 = string.Empty;
    if (number.Length - lastNumericDigitsWithZeroCount > 0)
      str2 = number.Substring(0, number.Length - lastNumericDigitsWithZeroCount);
    string str3 = string.Empty;
    if (str1.Length < lastNumericDigitsWithZeroCount)
      str3 = new string('0', lastNumericDigitsWithZeroCount - str1.Length);
    return str2 + str3 + str1;
  }

  internal static string DecreaseNumber(string number, int decreaseValue)
  {
    int lastNumericDigitsWithZeroCount;
    long num = NumberHelper.GetNumericValue(number, out lastNumericDigitsWithZeroCount) - (long) decreaseValue;
    if (num < 0L)
      num = 0L;
    string str1 = num.ToString();
    string str2 = string.Empty;
    if (number.Length - lastNumericDigitsWithZeroCount > 0)
      str2 = number.Substring(0, number.Length - lastNumericDigitsWithZeroCount);
    string str3 = string.Empty;
    if (str1.Length < lastNumericDigitsWithZeroCount)
      str3 = new string('0', lastNumericDigitsWithZeroCount - str1.Length);
    return str2 + str3 + str1;
  }

  internal static string GetTextPrefix(string number)
  {
    int lastNumericDigitsWithZeroCount;
    NumberHelper.GetNumericValue(number, out lastNumericDigitsWithZeroCount);
    string textPrefix = string.Empty;
    if (number.Length - lastNumericDigitsWithZeroCount > 0)
      textPrefix = number.Substring(0, number.Length - lastNumericDigitsWithZeroCount);
    return textPrefix;
  }

  internal static long GetNumericValue(string number)
  {
    return NumberHelper.GetNumericValue(number, out int _);
  }

  private static long GetNumericValue(string number, out int lastNumericDigitsWithZeroCount)
  {
    lastNumericDigitsWithZeroCount = 0;
    int length = 0;
    for (int index = number.Length - 1; index >= 0; --index)
    {
      switch (number[index])
      {
        case '0':
          ++lastNumericDigitsWithZeroCount;
          break;
        case '1':
        case '2':
        case '3':
        case '4':
        case '5':
        case '6':
        case '7':
        case '8':
        case '9':
          ++lastNumericDigitsWithZeroCount;
          length = lastNumericDigitsWithZeroCount;
          break;
        default:
          goto label_6;
      }
    }
label_6:
    long numericValue = 0;
    if (length > 0)
      numericValue = long.Parse(number.Substring(number.Length - length, length));
    return numericValue;
  }
}
