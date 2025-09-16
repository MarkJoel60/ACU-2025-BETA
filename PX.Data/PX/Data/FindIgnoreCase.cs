// Decompiled with JetBrains decompiler
// Type: PX.Data.FindIgnoreCase
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
internal class FindIgnoreCase
{
  private readonly string lower;
  private readonly string upper;
  public readonly int StrLen;

  public FindIgnoreCase(string str)
  {
    this.lower = str.ToLowerInvariant();
    this.upper = str.ToUpperInvariant();
    this.StrLen = str.Length;
  }

  public int indexIn(string text, int start, int len)
  {
    int num = 0;
    for (int index1 = 0; index1 <= len - this.StrLen; ++index1)
    {
      if (text[start + index1] == '\'')
        ++num;
      else if (num % 2 == 0)
      {
        bool flag = false;
        for (int index2 = 0; index2 < this.StrLen; ++index2)
        {
          char ch = text[start + index1 + index2];
          if ((int) ch != (int) this.upper[index2] && (int) ch != (int) this.lower[index2] && (this.upper[index2] != ' ' || (int) ch != (int) Environment.NewLine.First<char>() && (int) ch != (int) Environment.NewLine.Last<char>()))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          return start + index1;
      }
    }
    return -1;
  }
}
