// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOFreightAcctSubDefault
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.SO;

public class SOFreightAcctSubDefault
{
  public const string MaskShipVia = "V";
  public const string MaskLocation = "L";
  public const string OrderType = "T";
  public const string MaskCompany = "C";
  public const string MaskEmployee = "E";

  public class AcctListAttribute : SOCustomListAttribute
  {
    private static Tuple<string, string>[] Pairs
    {
      get
      {
        return new Tuple<string, string>[3]
        {
          PXStringListAttribute.Pair("T", "Order Type"),
          PXStringListAttribute.Pair("L", SOCustomListAttribute.MaskLocationLabel),
          PXStringListAttribute.Pair("V", "Ship Via")
        };
      }
    }

    public AcctListAttribute()
      : base(SOFreightAcctSubDefault.AcctListAttribute.Pairs)
    {
    }

    protected override Tuple<string, string>[] GetPairs()
    {
      return SOFreightAcctSubDefault.AcctListAttribute.Pairs;
    }
  }

  public class SubListAttribute : SOCustomListAttribute
  {
    private static Tuple<string, string>[] Pairs
    {
      get
      {
        return new Tuple<string, string>[5]
        {
          PXStringListAttribute.Pair("T", "Order Type"),
          PXStringListAttribute.Pair("L", SOCustomListAttribute.MaskLocationLabel),
          PXStringListAttribute.Pair("V", "Ship Via"),
          PXStringListAttribute.Pair("C", "Branch"),
          PXStringListAttribute.Pair("E", "Employee")
        };
      }
    }

    public SubListAttribute()
      : base(SOFreightAcctSubDefault.SubListAttribute.Pairs)
    {
    }

    protected override Tuple<string, string>[] GetPairs()
    {
      return SOFreightAcctSubDefault.SubListAttribute.Pairs;
    }
  }
}
