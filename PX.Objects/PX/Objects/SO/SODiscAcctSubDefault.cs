// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SODiscAcctSubDefault
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.SO;

public class SODiscAcctSubDefault
{
  public const string OrderType = "T";
  public const string MaskLocation = "L";
  public const string MaskCompany = "C";

  public class AcctListAttribute : SOCustomListAttribute
  {
    private static Tuple<string, string>[] Pairs
    {
      get
      {
        return new Tuple<string, string>[2]
        {
          PXStringListAttribute.Pair("T", "Order Type"),
          PXStringListAttribute.Pair("L", SOCustomListAttribute.MaskLocationLabel)
        };
      }
    }

    public AcctListAttribute()
      : base(SODiscAcctSubDefault.AcctListAttribute.Pairs)
    {
    }

    protected override Tuple<string, string>[] GetPairs()
    {
      return SODiscAcctSubDefault.AcctListAttribute.Pairs;
    }
  }

  public class SubListAttribute : SOCustomListAttribute
  {
    private static Tuple<string, string>[] Pairs
    {
      get
      {
        return new Tuple<string, string>[3]
        {
          PXStringListAttribute.Pair("T", "Order Type"),
          PXStringListAttribute.Pair("L", SOCustomListAttribute.MaskLocationLabel),
          PXStringListAttribute.Pair("C", "Branch")
        };
      }
    }

    public SubListAttribute()
      : base(SODiscAcctSubDefault.SubListAttribute.Pairs)
    {
    }

    protected override Tuple<string, string>[] GetPairs()
    {
      return SODiscAcctSubDefault.SubListAttribute.Pairs;
    }
  }
}
