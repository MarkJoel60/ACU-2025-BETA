// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INLotSerAssign
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class INLotSerAssign
{
  public const 
  #nullable disable
  string WhenReceived = "R";
  public const string WhenUsed = "U";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("R", "When Received"),
        PXStringListAttribute.Pair("U", "When Used")
      })
    {
    }
  }

  public class whenReceived : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INLotSerAssign.whenReceived>
  {
    public whenReceived()
      : base("R")
    {
    }
  }

  public class whenUsed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INLotSerAssign.whenUsed>
  {
    public whenUsed()
      : base("U")
    {
    }
  }
}
