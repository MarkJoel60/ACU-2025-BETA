// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INTransferType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class INTransferType
{
  public const 
  #nullable disable
  string OneStep = "1";
  public const string TwoStep = "2";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("1", "1-Step"),
        PXStringListAttribute.Pair("2", "2-Step")
      })
    {
    }
  }

  public class oneStep : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INTransferType.oneStep>
  {
    public oneStep()
      : base("1")
    {
    }
  }

  public class twoStep : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INTransferType.twoStep>
  {
    public twoStep()
      : base("2")
    {
    }
  }
}
