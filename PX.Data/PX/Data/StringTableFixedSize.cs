// Decompiled with JetBrains decompiler
// Type: PX.Data.StringTableFixedSize
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

public class StringTableFixedSize : StringTable
{
  private const uint _sz = 1999;
  private readonly string[] Items = new string[1999];

  public override string Add(string s)
  {
    switch (s)
    {
      case null:
        return (string) null;
      case "":
        return "";
      default:
        uint index = (uint) s.GetHashCode() % 1999U;
        string str = this.Items[(int) index];
        if (str == s)
          return str;
        this.Items[(int) index] = s;
        return s;
    }
  }
}
