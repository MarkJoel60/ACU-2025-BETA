// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOSalesAcctSubDefault
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.SO;

public class SOSalesAcctSubDefault
{
  public const string MaskItem = "I";
  public const string MaskSite = "W";
  public const string MaskClass = "P";
  public const string MaskReasonCode = "R";
  public const string MaskLocation = "L";
  public const string MaskEmployee = "E";
  public const string MaskCompany = "C";
  public const string MaskSalesPerson = "S";

  public class AcctListAttribute : SOCustomListAttribute
  {
    private static Tuple<string, string>[] Pairs
    {
      get
      {
        return new Tuple<string, string>[5]
        {
          PXStringListAttribute.Pair("I", "Inventory Item"),
          PXStringListAttribute.Pair("W", "Warehouse"),
          PXStringListAttribute.Pair("P", "Posting Class"),
          PXStringListAttribute.Pair("L", SOCustomListAttribute.MaskLocationLabel),
          PXStringListAttribute.Pair("R", "Reason Code")
        };
      }
    }

    public AcctListAttribute()
      : base(SOSalesAcctSubDefault.AcctListAttribute.Pairs)
    {
    }

    protected override Tuple<string, string>[] GetPairs()
    {
      return SOSalesAcctSubDefault.AcctListAttribute.Pairs;
    }
  }

  public class SubListAttribute : SOCustomListAttribute
  {
    private static Tuple<string, string>[] Pairs
    {
      get
      {
        return new Tuple<string, string>[8]
        {
          PXStringListAttribute.Pair("I", "Inventory Item"),
          PXStringListAttribute.Pair("W", "Warehouse"),
          PXStringListAttribute.Pair("P", "Posting Class"),
          PXStringListAttribute.Pair("L", SOCustomListAttribute.MaskLocationLabel),
          PXStringListAttribute.Pair("E", "Employee"),
          PXStringListAttribute.Pair("C", "Branch"),
          PXStringListAttribute.Pair("S", "Salesperson"),
          PXStringListAttribute.Pair("R", "Reason Code")
        };
      }
    }

    public SubListAttribute()
      : base(SOSalesAcctSubDefault.SubListAttribute.Pairs)
    {
    }

    protected override Tuple<string, string>[] GetPairs()
    {
      return SOSalesAcctSubDefault.SubListAttribute.Pairs;
    }
  }
}
