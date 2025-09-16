// Decompiled with JetBrains decompiler
// Type: PX.Data.PXNumberSeparatorListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXNumberSeparatorListAttribute : PXStringListAttribute
{
  public static readonly string Space = " ";
  public static readonly string SpaceTitple = nameof (Space);
  public static readonly string SpaceCode = "0";

  public PXNumberSeparatorListAttribute()
    : base(new string[1]
    {
      PXNumberSeparatorListAttribute.SpaceCode
    }, new string[1]
    {
      PXNumberSeparatorListAttribute.SpaceTitple
    })
  {
    this.ExclusiveValues = false;
  }

  public static string Encode(string str)
  {
    return str == null || str.Length < 1 || char.ConvertToUtf32(str, 0) != 32 /*0x20*/ && char.ConvertToUtf32(str, 0) != 160 /*0xA0*/ ? str : PXNumberSeparatorListAttribute.SpaceCode;
  }

  public static string Decode(string str)
  {
    return string.Equals(str, PXNumberSeparatorListAttribute.SpaceCode) ? PXNumberSeparatorListAttribute.Space : str;
  }
}
