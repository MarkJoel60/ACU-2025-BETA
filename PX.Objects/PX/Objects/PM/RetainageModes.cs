// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.RetainageModes
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>Retainage Modes options</summary>
public static class RetainageModes
{
  public const 
  #nullable disable
  string Normal = "N";
  public const string Contract = "C";
  public const string Line = "L";

  /// <summary>Retainage Modes List Attribute</summary>
  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("N", "Standard"),
        PXStringListAttribute.Pair("C", "Contract Cap"),
        PXStringListAttribute.Pair("L", "Contract Item Cap")
      })
    {
    }
  }

  /// <summary>Contract level retainage</summary>
  public class contract : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RetainageModes.contract>
  {
    public contract()
      : base("C")
    {
    }
  }

  /// <summary>Line level retainage</summary>
  public class line : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RetainageModes.line>
  {
    public line()
      : base("L")
    {
    }
  }
}
