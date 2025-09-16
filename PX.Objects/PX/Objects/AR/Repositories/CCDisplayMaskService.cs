// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Repositories.CCDisplayMaskService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Objects.AR.Repositories;

public class CCDisplayMaskService : ICCDisplayMaskService
{
  private static readonly Regex parseCardNum = new Regex("[\\d]+", RegexOptions.Compiled);
  private const string MaskedCardTmpl = "****-****-****-";
  private const string MaskedEFTTmpl = "*****";
  private const char CS_UNDERSCORE = '_';
  private const char CS_DASH = '-';
  private const char CS_DOT = '.';
  private const char CS_MASKER = '*';
  private const char CS_NUMBER_MASK_0 = '#';
  private const char CS_NUMBER_MASK_1 = '0';
  private const char CS_NUMBER_MASK_2 = '9';
  private const char CS_ANY_CHAR_0 = '&';
  private const char CS_ANY_CHAR_1 = 'C';
  private const char CS_ALPHANUMBER_MASK_0 = 'a';
  private const char CS_ALPHANUMBER_MASK_1 = 'A';
  private const char CS_ALPHA_MASK_0 = 'L';
  private const char CS_ALPHA_MASK_1 = '?';

  public virtual string UseDefaultMaskForCardNumber(
    string cardNbr,
    string cardType,
    MeansOfPayment? meansOfPayment)
  {
    string str1 = (string) null;
    Match match = CCDisplayMaskService.parseCardNum.Match(cardNbr);
    string str2 = meansOfPayment.GetValueOrDefault() == 1 ? "*****" : "****-****-****-";
    if (match.Success)
      str1 = cardType != null ? $"{cardType.Trim()}:{str2}{match.Value}" : str2 + match.Value;
    return str1;
  }

  public virtual string UseDisplayMaskForCardNumber(string aID, string aDisplayMask)
  {
    if (string.IsNullOrEmpty(aID) || string.IsNullOrEmpty(aDisplayMask) || string.IsNullOrEmpty(aDisplayMask.Trim()))
      return aID;
    int length1 = aDisplayMask.Length;
    int length2 = aID.Length;
    char[] charArray1 = aDisplayMask.ToCharArray();
    char[] charArray2 = aID.ToCharArray();
    int index1 = 0;
    StringBuilder stringBuilder = new StringBuilder(length1);
    for (int index2 = 0; index2 < length1 && index1 < length2; ++index2)
    {
      if (CCDisplayMaskService.IsSymbol(charArray1[index2]))
      {
        stringBuilder.Append(charArray2[index1]);
        ++index1;
      }
      else if (CCDisplayMaskService.IsSeparator(charArray1[index2]))
      {
        stringBuilder.Append(charArray1[index2]);
      }
      else
      {
        stringBuilder.Append('*');
        ++index1;
      }
    }
    return stringBuilder.ToString();
  }

  public virtual string UseAdjustedDisplayMaskForCardNumber(string aID, string aDisplayMask)
  {
    string aID1 = aID;
    int totalWidth = ((IEnumerable<char>) aDisplayMask.ToArray<char>()).Where<char>((Func<char, bool>) (symbol => !CCDisplayMaskService.IsSeparator(symbol))).Count<char>();
    if (aID.Length > totalWidth)
      aID1 = aID.Substring(aID.Length - totalWidth);
    else if (aID.Length < totalWidth)
      aID1 = aID.PadLeft(totalWidth, '0');
    return this.UseDisplayMaskForCardNumber(aID1, aDisplayMask);
  }

  private static bool IsSeparator(char aCh) => aCh == '_' || aCh == '-' || aCh == '.';

  private static bool IsSymbol(char aCh)
  {
    switch (aCh)
    {
      case '#':
      case '&':
      case '0':
      case '9':
      case '?':
      case 'A':
      case 'C':
      case 'L':
      case 'a':
        return true;
      default:
        return false;
    }
  }
}
