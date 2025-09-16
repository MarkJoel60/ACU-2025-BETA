// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipComplete
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.SO;

public class SOShipComplete
{
  public const 
  #nullable disable
  string ShipComplete = "C";
  public const string BackOrderAllowed = "B";
  public const string CancelRemainder = "L";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("C", "Ship Complete"),
        PXStringListAttribute.Pair("B", "Back Order Allowed"),
        PXStringListAttribute.Pair("L", "Cancel Remainder")
      })
    {
    }
  }

  public class shipComplete : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOShipComplete.shipComplete>
  {
    public shipComplete()
      : base("C")
    {
    }
  }

  public class backOrderAllowed : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SOShipComplete.backOrderAllowed>
  {
    public backOrderAllowed()
      : base("B")
    {
    }
  }

  public class cancelRemainder : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOShipComplete.cancelRemainder>
  {
    public cancelRemainder()
      : base("L")
    {
    }
  }
}
