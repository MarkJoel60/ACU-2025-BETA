// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Html.StyleBuilder
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser.Html;

internal static class StyleBuilder
{
  public static string MergeCssClasses(string propStr)
  {
    List<string> classes = new List<string>();
    propStr = StyleBuilder.GetAppliedClasses(propStr, classes);
    if (classes.Count == 0)
      return propStr;
    StringBuilder stringBuilder = new StringBuilder("class=\"");
    foreach (string str in classes)
    {
      stringBuilder.Append(" ");
      stringBuilder.Append(str);
    }
    stringBuilder.Append("\"");
    return $"{stringBuilder.ToString()} {propStr}";
  }

  private static string GetAppliedClasses(string propstr, List<string> classes)
  {
    int num1 = 0;
    int num2;
    while ((num2 = propstr.IndexOf("class", num1, StringComparison.OrdinalIgnoreCase)) != -1)
    {
      StringBuilder stringBuilder = new StringBuilder();
      int length = num2;
      num1 = num2 + 5;
      while (num1 < propstr.Length && char.IsWhiteSpace(propstr[num1]))
        ++num1;
      if (num1 != propstr.Length && propstr[num1] == '=')
      {
        ++num1;
        while (num1 < propstr.Length && char.IsWhiteSpace(propstr[num1]))
          ++num1;
        if (num1 != propstr.Length && propstr[num1] == '"')
        {
          int index;
          for (index = num1 + 1; index < propstr.Length && propstr[index] != '"'; ++index)
            stringBuilder.Append(propstr[index]);
          classes.Add(stringBuilder.ToString());
          int startIndex = index + 1;
          propstr = startIndex >= propstr.Length ? propstr.Substring(0, length) : propstr.Substring(0, length) + propstr.Substring(startIndex);
          num1 = 0;
        }
      }
    }
    return propstr;
  }
}
