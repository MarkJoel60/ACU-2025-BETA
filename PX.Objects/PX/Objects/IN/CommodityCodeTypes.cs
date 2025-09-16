// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.CommodityCodeTypes
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class CommodityCodeTypes
{
  public const 
  #nullable disable
  string HSCode = "HS";
  public const string HTSCode = "HTS";
  public const string HSNCode = "HSN";
  public const string NCMCode = "NCM";
  public const string UNSPSCCode = "UNSPSC";
  public const string ServiceCode = "Service";

  public class CustomListAttribute : PXStringListAttribute
  {
    public string[] AllowedValues => this._AllowedValues;

    public string[] AllowedLabels => this._AllowedLabels;

    public CustomListAttribute(string[] AllowedValues, string[] AllowedLabels)
      : base(AllowedValues, AllowedLabels)
    {
    }

    public CustomListAttribute(params Tuple<string, string>[] valuesToLabels)
      : base(valuesToLabels)
    {
    }
  }

  public class StockCommodityCodeListAttribute : CommodityCodeTypes.CustomListAttribute
  {
    public StockCommodityCodeListAttribute()
      : base(PXStringListAttribute.Pair("HS", "HS"), PXStringListAttribute.Pair("HTS", "HTS"), PXStringListAttribute.Pair("HSN", "HSN"), PXStringListAttribute.Pair("NCM", "NCM"), PXStringListAttribute.Pair("UNSPSC", "UNSPSC"))
    {
    }
  }

  public class NonStockCommodityCodeListAttribute : CommodityCodeTypes.CustomListAttribute
  {
    public NonStockCommodityCodeListAttribute()
      : base(PXStringListAttribute.Pair("HS", "HS"), PXStringListAttribute.Pair("HTS", "HTS"), PXStringListAttribute.Pair("HSN", "HSN"), PXStringListAttribute.Pair("NCM", "NCM"), PXStringListAttribute.Pair("UNSPSC", "UNSPSC"), PXStringListAttribute.Pair("Service", "Service"))
    {
    }
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[6]
      {
        PXStringListAttribute.Pair("HS", "HS"),
        PXStringListAttribute.Pair("HTS", "HTS"),
        PXStringListAttribute.Pair("HSN", "HSN"),
        PXStringListAttribute.Pair("NCM", "NCM"),
        PXStringListAttribute.Pair("UNSPSC", "UNSPSC"),
        PXStringListAttribute.Pair("Service", "Service")
      })
    {
    }
  }

  public class hSCode : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CommodityCodeTypes.hSCode>
  {
    public hSCode()
      : base("HS")
    {
    }
  }

  public class hTSCode : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CommodityCodeTypes.hTSCode>
  {
    public hTSCode()
      : base("HTS")
    {
    }
  }

  public class hSNCode : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CommodityCodeTypes.hSNCode>
  {
    public hSNCode()
      : base("HSN")
    {
    }
  }

  public class nCMCode : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CommodityCodeTypes.nCMCode>
  {
    public nCMCode()
      : base("NCM")
    {
    }
  }

  public class uNSPSCCode : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CommodityCodeTypes.uNSPSCCode>
  {
    public uNSPSCCode()
      : base("UNSPSC")
    {
    }
  }

  public class serviceCode : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CommodityCodeTypes.serviceCode>
  {
    public serviceCode()
      : base("Service")
    {
    }
  }
}
