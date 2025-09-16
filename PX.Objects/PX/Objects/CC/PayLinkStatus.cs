// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.PayLinkStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CC;

public static class PayLinkStatus
{
  public const 
  #nullable disable
  string None = "N";
  public const string Open = "O";
  public const string Closed = "C";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(PayLinkStatus.ListAttribute.ValueLabelPairs())
    {
    }

    public static Tuple<string, string>[] ValueLabelPairs()
    {
      return new Tuple<string, string>[3]
      {
        new Tuple<string, string>("N", "None"),
        new Tuple<string, string>("O", "Open"),
        new Tuple<string, string>("C", "Closed")
      };
    }
  }

  public class none : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PayLinkStatus.none>
  {
    public none()
      : base("N")
    {
    }
  }

  public class open : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PayLinkStatus.open>
  {
    public open()
      : base("O")
    {
    }
  }

  public class closed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PayLinkStatus.closed>
  {
    public closed()
      : base("C")
    {
    }
  }
}
