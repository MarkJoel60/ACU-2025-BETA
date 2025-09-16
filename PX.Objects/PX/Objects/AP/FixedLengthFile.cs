// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.FixedLengthFile
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Objects.AP;

public class FixedLengthFile
{
  public void WriteToFile(List<object> records, StreamWriter writer)
  {
    if (records.Count <= 0)
      return;
    foreach (object record in records)
    {
      string str1 = string.Empty;
      foreach (PropertyInfo property in record.GetType().GetProperties())
      {
        try
        {
          FixedLengthAttribute fixedLengthAttribute = ((IEnumerable<FixedLengthAttribute>) property.GetCustomAttributes(typeof (FixedLengthAttribute), false).Cast<FixedLengthAttribute>().ToArray<FixedLengthAttribute>()).FirstOrDefault<FixedLengthAttribute>();
          if (fixedLengthAttribute != null)
          {
            string input = Convert.ToString(property.GetValue(record, (object[]) null));
            if (property.PropertyType == typeof (Decimal))
              input = input.Replace(".", "");
            if (!string.IsNullOrEmpty(fixedLengthAttribute.RegexReplacePattern))
              input = Regex.Replace(input, fixedLengthAttribute.RegexReplacePattern, string.Empty);
            string str2 = input.Length > fixedLengthAttribute.FieldLength ? input.Substring(0, fixedLengthAttribute.FieldLength) : input;
            switch (fixedLengthAttribute.AlphaCharacterCaseStyle)
            {
              case AlphaCharacterCaseEnum.Lower:
                str2 = str2.ToLower();
                break;
              case AlphaCharacterCaseEnum.Upper:
                str2 = str2.ToUpper();
                break;
            }
            if (str2.Length < fixedLengthAttribute.FieldLength)
              str2 = fixedLengthAttribute.PaddingStyle == PaddingEnum.Left ? str2.PadLeft(fixedLengthAttribute.FieldLength, fixedLengthAttribute.PaddingChar) : str2.PadRight(fixedLengthAttribute.FieldLength, fixedLengthAttribute.PaddingChar);
            if (str2.Length == fixedLengthAttribute.FieldLength)
            {
              if (str1.Length < fixedLengthAttribute.StartPosition)
              {
                str1 += str2;
              }
              else
              {
                string str3 = str1.Substring(0, fixedLengthAttribute.StartPosition - 1);
                string str4 = str1.Substring(fixedLengthAttribute.StartPosition, str1.Length);
                string str5 = str2;
                string str6 = str4;
                str1 = str3 + str5 + str6;
              }
            }
          }
        }
        catch (Exception ex)
        {
          throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("The system encountered at least one error while writing the {0} record (writing information about {1}). Errors: {2}. ", (object) record.GetType().Name, (object) property.Name, (object) ex.Message));
        }
      }
      writer.WriteLine(str1);
    }
  }
}
