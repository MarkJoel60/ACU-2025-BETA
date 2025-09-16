// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOperation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.SO;

public class SOOperation
{
  public const 
  #nullable disable
  string Issue = "I";
  public const string Receipt = "R";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("I", "Issue"),
        PXStringListAttribute.Pair("R", "Receipt")
      })
    {
    }
  }

  public class issue : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOOperation.issue>
  {
    public issue()
      : base("I")
    {
    }
  }

  public class receipt : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOOperation.receipt>
  {
    public receipt()
      : base("R")
    {
    }
  }
}
