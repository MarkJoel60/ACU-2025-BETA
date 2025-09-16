// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.RMReportWildcard
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Reports.ARm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Objects.CS;

public static class RMReportWildcard
{
  public static void ConcatenateRangeWithDataSet(
    ARmDataSet target,
    ARmDataSet source,
    object startKey,
    object endKey,
    MergingMode mergingMode)
  {
    string source1 = RMReportWildcard.NormalizeDsValue(source[startKey]);
    string str = RMReportWildcard.NormalizeDsValue(source[endKey]);
    char ch;
    if (mergingMode != null)
    {
      if (mergingMode == 1)
        ch = ',';
      else
        throw new NotSupportedException(PXMessages.LocalizeFormatNoPrefixNLA("The {0} merging mode is not supported.", new object[1]
        {
          (object) mergingMode
        }));
    }
    else
      ch = '|';
    if (source1 == string.Empty && str == string.Empty)
      return;
    if (source1.Contains<char>(':') && str != string.Empty)
      throw new ArgumentException(PXMessages.LocalizeFormatNoPrefixNLA("A range is specified in {0}; the end value should be empty. {0}: {1}. {2}: {3}.", new object[4]
      {
        startKey,
        (object) source1,
        endKey,
        (object) str
      }));
    if (source1 == string.Empty && str != string.Empty)
      throw new ArgumentException(PXMessages.LocalizeFormatNoPrefixNLA("The data source is incomplete. If you set {0}, you should also define {1}. {0}: {2}.", new object[3]
      {
        endKey,
        startKey,
        (object) str
      }));
    if (!string.IsNullOrEmpty(target[startKey] as string))
    {
      ARmDataSet armDataSet = target;
      object obj = startKey;
      armDataSet[obj] = (object) (armDataSet[obj]?.ToString() + ch.ToString());
    }
    ARmDataSet armDataSet1 = target;
    object obj1 = startKey;
    armDataSet1[obj1] = (object) (armDataSet1[obj1]?.ToString() + source1);
    if (!(str != string.Empty))
      return;
    ARmDataSet armDataSet2 = target;
    object obj2 = startKey;
    armDataSet2[obj2] = (object) $"{armDataSet2[obj2]?.ToString()}:{str}";
  }

  public static string WildcardToFixed(string str, string wildcard)
  {
    if (str == null)
      return (string) null;
    return wildcard != null && wildcard.Length - str.Length > 0 ? str + new string(' ', wildcard.Length - str.Length) : str;
  }

  public static string EnsureWildcard(string str, string wildcard)
  {
    return RMReportWildcard.EnsureWildcard(str, wildcard, RMReportConstants.WildcardChars);
  }

  public static string EnsureWildcardForFixed(string str, string wildcard)
  {
    return RMReportWildcard.EnsureWildcard(RMReportWildcard.WildcardToFixed(str, wildcard), wildcard, ((IEnumerable<char>) RMReportConstants.WildcardChars).Where<char>((Func<char, bool>) (c => c != ' ')).ToArray<char>());
  }

  private static string EnsureWildcard(string str, string wildcard, char[] wildcardChars)
  {
    if (str == null || wildcard == null || wildcardChars == null)
      return str;
    StringBuilder stringBuilder = new StringBuilder(wildcard.Length);
    foreach (char ch in str)
    {
      bool flag = false;
      foreach (char wildcardChar in wildcardChars)
      {
        if ((int) ch == (int) wildcardChar)
        {
          stringBuilder.Append('_');
          flag = true;
          break;
        }
      }
      if (!flag)
        stringBuilder.Append(ch);
    }
    if (str.Length < wildcard.Length)
      stringBuilder.Append(wildcard, 0, wildcard.Length - str.Length);
    return stringBuilder.ToString();
  }

  public static IEnumerable<T> GetBetween<T>(
    string start,
    string end,
    string wildcard,
    IEnumerable items,
    Func<T, string> convertFunc)
  {
    return RMReportWildcard.GetBetween<T>(start, end, wildcard, items, convertFunc, RMReportConstants.WildcardChars);
  }

  public static IEnumerable<T> GetBetweenForFixed<T>(
    string start,
    string end,
    string wildcard,
    IEnumerable items,
    Func<T, string> convertFunc)
  {
    return RMReportWildcard.GetBetween<T>(start, end, wildcard, items, convertFunc, ((IEnumerable<char>) RMReportConstants.WildcardChars).Where<char>((Func<char, bool>) (c => c != ' ')).ToArray<char>());
  }

  private static IEnumerable<T> GetBetween<T>(
    string start,
    string end,
    string wildcard,
    IEnumerable items,
    Func<T, string> convertFunc,
    char[] wildcardChars)
  {
    string from = RMReportWildcard.EnsureWildcard(start, wildcard, wildcardChars).Replace('_', ' ');
    string to = (MainTools.Replace(end, wildcardChars, 'z') + new string('z', wildcard.Length)).Substring(0, wildcard.Length);
    return items.Cast<T>().Where<T>((Func<T, bool>) (item => RMReportWildcard.IsBetween(from, to, convertFunc(item))));
  }

  public static bool IsLike(string mask, string value)
  {
    for (int index = 0; index < mask.Length; ++index)
    {
      if (index >= value.Length || (int) mask[index] != (int) value[index] && mask[index] != '_')
        return false;
    }
    return true;
  }

  public static bool IsBetween(string from, string to, string value)
  {
    return string.Compare(value, from) >= 0 && string.Compare(value, to) <= 0;
  }

  public static bool IsBetweenByChar(string from, string to, string value)
  {
    bool flag1 = false;
    bool flag2 = false;
    for (int index = 0; index < value.Length; ++index)
    {
      if (!flag1 && index < from.Length && !((IEnumerable<char>) RMReportConstants.WildcardChars).Contains<char>(from[index]))
      {
        if ((int) value[index] < (int) from[index])
          return false;
        if ((int) value[index] > (int) from[index])
          flag1 = true;
      }
      if (!flag2 && index < to.Length && !((IEnumerable<char>) RMReportConstants.WildcardChars).Contains<char>(to[index]))
      {
        if ((int) value[index] > (int) to[index])
          return false;
        if ((int) value[index] < (int) to[index])
          flag2 = true;
      }
    }
    return true;
  }

  [PXInternalUseOnly]
  public static string NormalizeDsValue(object value)
  {
    string str = value as string;
    return !string.IsNullOrWhiteSpace(str) ? str : string.Empty;
  }
}
