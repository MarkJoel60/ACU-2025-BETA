// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.StringBuilderExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Objects.AP;

public static class StringBuilderExtensions
{
  public static StringBuilder Append(
    this StringBuilder str,
    string value,
    int startPosition,
    int fieldLength,
    PaddingEnum paddingStyle = PaddingEnum.None,
    char paddingChar = ' ',
    AlphaCharacterCaseEnum alphaCharacterCaseStyle = AlphaCharacterCaseEnum.Upper,
    string regexReplacePattern = "")
  {
    string input = value ?? string.Empty;
    if (str.Length + 1 != startPosition)
      throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("The system encountered at least one error while writing the {0} record (writing information at position {1}).", (object) str.ToString(0, 1), (object) startPosition));
    if (!string.IsNullOrEmpty(regexReplacePattern))
      input = Regex.Replace(input, regexReplacePattern, string.Empty);
    string str1 = input.Length > fieldLength ? input.Substring(0, fieldLength) : input;
    switch (alphaCharacterCaseStyle)
    {
      case AlphaCharacterCaseEnum.Lower:
        str1 = str1.ToLower();
        break;
      case AlphaCharacterCaseEnum.Upper:
        str1 = str1.ToUpper();
        break;
    }
    if (str1.Length < fieldLength)
      str1 = paddingStyle == PaddingEnum.Left ? str1.PadLeft(fieldLength, paddingChar) : str1.PadRight(fieldLength, paddingChar);
    return str.Append(str1);
  }

  public static StringBuilder Append(
    this StringBuilder str,
    Decimal value,
    int startPosition,
    int fieldLength,
    PaddingEnum paddingStyle = PaddingEnum.None,
    char paddingChar = ' ',
    AlphaCharacterCaseEnum alphaCharacterCaseStyle = AlphaCharacterCaseEnum.Upper,
    string regexReplacePattern = "")
  {
    string str1 = Convert.ToString(value).Replace(".", "");
    return str.Append(str1, startPosition, fieldLength, paddingStyle, paddingChar, alphaCharacterCaseStyle, regexReplacePattern);
  }
}
