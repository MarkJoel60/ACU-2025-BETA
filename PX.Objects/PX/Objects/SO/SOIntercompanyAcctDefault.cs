// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOIntercompanyAcctDefault
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.SO;

public class SOIntercompanyAcctDefault
{
  public const 
  #nullable disable
  string MaskItem = "I";
  public const string MaskLocation = "L";

  public class AcctSalesListAttribute : SOCustomListAttribute
  {
    private static Tuple<string, string>[] Pairs
    {
      get
      {
        return new Tuple<string, string>[2]
        {
          PXStringListAttribute.Pair("I", "Inventory Item"),
          PXStringListAttribute.Pair("L", SOCustomListAttribute.MaskLocationLabel)
        };
      }
    }

    public AcctSalesListAttribute()
      : base(SOIntercompanyAcctDefault.AcctSalesListAttribute.Pairs)
    {
    }

    protected override Tuple<string, string>[] GetPairs()
    {
      return SOIntercompanyAcctDefault.AcctSalesListAttribute.Pairs;
    }
  }

  public class AcctCOGSListAttribute : PXStringListAttribute
  {
    public AcctCOGSListAttribute()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("I", "Inventory Item"),
        PXStringListAttribute.Pair("L", "Customer")
      })
    {
    }
  }

  public class maskItem : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOIntercompanyAcctDefault.maskItem>
  {
    public maskItem()
      : base("I")
    {
    }
  }

  public class maskLocation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SOIntercompanyAcctDefault.maskLocation>
  {
    public maskLocation()
      : base("L")
    {
    }
  }
}
