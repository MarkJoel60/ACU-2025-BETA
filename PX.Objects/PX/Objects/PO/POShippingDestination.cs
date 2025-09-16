// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POShippingDestination
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PO;

public class POShippingDestination
{
  public const 
  #nullable disable
  string CompanyLocation = "L";
  public const string Customer = "C";
  public const string Vendor = "V";
  public const string Site = "S";
  public const string ProjectSite = "P";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("L", "Branch"),
        PXStringListAttribute.Pair("C", "Customer"),
        PXStringListAttribute.Pair("V", "Vendor"),
        PXStringListAttribute.Pair("S", "Warehouse")
      })
    {
    }
  }

  public class company : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POShippingDestination.company>
  {
    public company()
      : base("L")
    {
    }
  }

  public class customer : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POShippingDestination.customer>
  {
    public customer()
      : base("C")
    {
    }
  }

  public class vendor : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POShippingDestination.vendor>
  {
    public vendor()
      : base("V")
    {
    }
  }

  public class site : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POShippingDestination.site>
  {
    public site()
      : base("S")
    {
    }
  }

  public class projectSite : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POShippingDestination.projectSite>
  {
    public projectSite()
      : base("P")
    {
    }
  }
}
