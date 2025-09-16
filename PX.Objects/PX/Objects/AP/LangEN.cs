// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.LangEN
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Text;

#nullable disable
namespace PX.Objects.AP;

public static class LangEN
{
  public static string ToWords(Decimal amt, int Precision)
  {
    StringBuilder stringBuilder = new StringBuilder(LangEN.ToWords(new Decimal?(amt)));
    Decimal num = Math.Floor((amt - Math.Truncate(amt)) * (Decimal) Math.Pow(10.0, (double) Precision));
    if (amt != 0M)
    {
      if (num != 0M)
      {
        stringBuilder.Append(" and ");
        stringBuilder.Append((int) num);
        stringBuilder.Append("/");
        stringBuilder.Append((int) Math.Pow(10.0, (double) Precision));
      }
      else
        stringBuilder.Append(" Only");
    }
    else
      stringBuilder.Append("Zero");
    return stringBuilder.ToString();
  }

  public static string ToWords(Decimal? amt)
  {
    Decimal num1 = Math.Floor(amt.Value);
    string[] strArray1 = new string[9]
    {
      "One",
      "Two",
      "Three",
      "Four",
      "Five",
      "Six",
      "Seven",
      "Eight",
      "Nine"
    };
    string[] strArray2 = new string[9]
    {
      "Eleven",
      "Twelve",
      "Thirteen",
      "Fourteen",
      "Fifteen",
      "Sixteen",
      "Seventeen",
      "Eighteen",
      "Nineteen"
    };
    string[] strArray3 = new string[9]
    {
      "Ten",
      "Twenty",
      "Thirty",
      "Forty",
      "Fifty",
      "Sixty",
      "Seventy",
      "Eighty",
      "Ninety"
    };
    string[] strArray4 = new string[6]
    {
      "",
      "Thousand",
      "Million",
      "Billion",
      "Trillion",
      "Quadrillion"
    };
    string str1 = "Hundred";
    string str2 = " ";
    int num2 = (int) Math.Floor((double) (int) Math.Floor(Math.Log10((double) num1)) / 3.0) + 1;
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = num2; index > 0; --index)
    {
      int length = stringBuilder.Length;
      int num3 = (int) Math.Floor((double) num1 / Math.Pow(10.0, (double) ((index - 1) * 3)));
      if (num3 >= 100)
      {
        int num4 = (int) Math.Floor((double) num3 / 100.0);
        stringBuilder.Append(strArray1[num4 - 1]);
        stringBuilder.Append(str2);
        stringBuilder.Append(str1);
        stringBuilder.Append(str2);
        num3 -= 100 * num4;
      }
      if (num3 >= 20 || num3 == 10)
      {
        int num5 = (int) Math.Floor((double) num3 / 10.0);
        stringBuilder.Append(strArray3[num5 - 1]);
        stringBuilder.Append(str2);
        num3 -= 10 * num5;
      }
      if (num3 < 20 && num3 > 10)
      {
        stringBuilder.Append(strArray2[num3 - 10 - 1]);
        stringBuilder.Append(str2);
      }
      if (num3 > 0 && num3 < 10)
      {
        stringBuilder.Append(strArray1[num3 - 1]);
        stringBuilder.Append(str2);
      }
      if (stringBuilder.Length > length)
      {
        stringBuilder.Append(strArray4[index - 1]);
        stringBuilder.Append(str2);
      }
      long result;
      Math.DivRem((long) num1, (long) Math.Pow(10.0, (double) ((index - 1) * 3)), out result);
      num1 = (Decimal) result;
    }
    return stringBuilder.ToString().TrimEnd();
  }
}
