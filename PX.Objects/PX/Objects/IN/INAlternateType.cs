// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INAlternateType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class INAlternateType
{
  public const 
  #nullable disable
  string CPN = "0CPN";
  public const string VPN = "0VPN";
  public const string MFPN = "MFPN";
  public const string Global = "GLBL";
  public const string Substitute = "SBST";
  public const string Obsolete = "OBSL";
  public const string SKU = "SKU";
  public const string UPC = "UPC";
  public const string EAN = "EAN";
  public const string ISBN = "ISBN";
  public const string GTIN = "GTIN";
  public const string Barcode = "BAR";
  public const string GIN = "GIN";
  public const string ExternalSKu = "ESKU";

  public static string ConvertFromPrimary(INPrimaryAlternateType aPrimaryType)
  {
    return aPrimaryType != INPrimaryAlternateType.VPN ? "0CPN" : "0VPN";
  }

  public static INPrimaryAlternateType? ConvertToPrimary(string aAlternateType)
  {
    INPrimaryAlternateType? primary = new INPrimaryAlternateType?();
    switch (aAlternateType)
    {
      case "0VPN":
        primary = new INPrimaryAlternateType?(INPrimaryAlternateType.VPN);
        break;
      case "0CPN":
        primary = new INPrimaryAlternateType?(INPrimaryAlternateType.CPN);
        break;
    }
    return primary;
  }

  public class List : PXStringListAttribute
  {
    public List()
      : base(new Tuple<string, string>[6]
      {
        PXStringListAttribute.Pair("0CPN", "Customer Part Number"),
        PXStringListAttribute.Pair("0VPN", "Vendor Part Number"),
        PXStringListAttribute.Pair("GLBL", "Global"),
        PXStringListAttribute.Pair("BAR", "Barcode"),
        PXStringListAttribute.Pair("GIN", "GTIN/EAN/UPC/ISBN"),
        PXStringListAttribute.Pair("ESKU", "External SKU")
      })
    {
    }
  }

  public class cPN : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INAlternateType.cPN>
  {
    public cPN()
      : base("0CPN")
    {
    }
  }

  public class vPN : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INAlternateType.vPN>
  {
    public vPN()
      : base("0VPN")
    {
    }
  }

  public class global : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INAlternateType.global>
  {
    public global()
      : base("GLBL")
    {
    }
  }

  public class obsolete : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INAlternateType.obsolete>
  {
    public obsolete()
      : base("OBSL")
    {
    }
  }

  public class barcode : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INAlternateType.barcode>
  {
    public barcode()
      : base("BAR")
    {
    }
  }

  public class gIN : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INAlternateType.gIN>
  {
    public gIN()
      : base("GIN")
    {
    }
  }

  public class externalSku : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INAlternateType.externalSku>
  {
    public externalSku()
      : base("ESKU")
    {
    }
  }
}
