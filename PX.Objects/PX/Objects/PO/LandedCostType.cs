// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.LandedCostType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PO;

public static class LandedCostType
{
  public const 
  #nullable disable
  string FreightOriginCharges = "FO";
  public const string CustomDuties = "CD";
  public const string VATTaxes = "VT";
  public const string MiscDestinationCharges = "DC";
  public const string Other = "OR";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[5]
      {
        PXStringListAttribute.Pair("FO", "Freight & Misc. Origin Charges"),
        PXStringListAttribute.Pair("CD", "Customs Duties"),
        PXStringListAttribute.Pair("VT", "VAT Taxes"),
        PXStringListAttribute.Pair("DC", "Misc. Destination Charges"),
        PXStringListAttribute.Pair("OR", "Other")
      })
    {
    }
  }

  public class freightOriginCharges : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    LandedCostType.freightOriginCharges>
  {
    public freightOriginCharges()
      : base("FO")
    {
    }
  }

  public class customDuties : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  LandedCostType.customDuties>
  {
    public customDuties()
      : base("CD")
    {
    }
  }

  public class vATTaxes : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  LandedCostType.vATTaxes>
  {
    public vATTaxes()
      : base("VT")
    {
    }
  }

  public class miscDestinationCharges : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    LandedCostType.miscDestinationCharges>
  {
    public miscDestinationCharges()
      : base("DC")
    {
    }
  }

  public class other : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  LandedCostType.other>
  {
    public other()
      : base("OR")
    {
    }
  }
}
