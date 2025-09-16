// Decompiled with JetBrains decompiler
// Type: PX.Data.StringTable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public class StringTable
{
  private Dictionary<string, string> Items = new Dictionary<string, string>();

  public virtual string Add(string s)
  {
    switch (s)
    {
      case null:
        return (string) null;
      case "":
        return "";
      default:
        string str;
        if (this.Items.TryGetValue(s, out str))
          return str;
        this.Items.Add(s, s);
        return s;
    }
  }
}
