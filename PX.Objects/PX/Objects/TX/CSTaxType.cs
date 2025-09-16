// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.CSTaxType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.TX;

public static class CSTaxType
{
  public const 
  #nullable disable
  string Sales = "S";
  public const string Use = "P";
  public const string VAT = "V";
  public const string Withholding = "W";
  /// <summary>
  /// A per unit tax type for Per Unit/Specific taxes which calculation is based on quantity of items.
  /// </summary>
  public const string PerUnit = "Q";

  public static IEnumerable<(string TaxType, string Label)> GetTaxTypesWithLabels(
    bool includeVAT,
    bool includePerUnit)
  {
    yield return ("S", "Sales");
    yield return ("P", "Use");
    if (includeVAT)
      yield return ("V", "VAT");
    yield return ("W", "Withholding");
    if (includePerUnit)
      yield return ("Q", "Per-Unit/Specific");
  }

  public static string GetTaxTypeLabel(string taxType)
  {
    foreach ((string TaxType, string Label) taxTypesWithLabel in CSTaxType.GetTaxTypesWithLabels(true, true))
    {
      if (taxTypesWithLabel.TaxType == taxType)
        return taxTypesWithLabel.Label;
    }
    throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("{0} tax type is not found.", new object[1]
    {
      (object) taxType
    }));
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[5]{ "S", "P", "V", "W", "Q" }, new string[5]
      {
        "Sales",
        "Use",
        "VAT",
        "Withholding",
        "Per-Unit/Specific"
      })
    {
    }
  }

  [Obsolete("The ListSimpleAttribute class is obsolete and will be removed in future versions of Acumatica")]
  public class ListSimpleAttribute : PXStringListAttribute
  {
    public ListSimpleAttribute()
      : base(new string[3]{ "S", "P", "W" }, new string[3]
      {
        "Sales",
        "Use",
        "Withholding"
      })
    {
    }
  }

  public class sales : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CSTaxType.sales>
  {
    public sales()
      : base("S")
    {
    }
  }

  public class use : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CSTaxType.use>
  {
    public use()
      : base("P")
    {
    }
  }

  public class vat : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CSTaxType.vat>
  {
    public vat()
      : base("V")
    {
    }
  }

  public class withholding : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CSTaxType.withholding>
  {
    public withholding()
      : base("W")
    {
    }
  }

  /// <summary>
  /// A per unit tax type for Per Unit/Specific taxes which calculation is based on quantity of items.
  /// </summary>
  public class perUnit : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CSTaxType.perUnit>
  {
    public perUnit()
      : base("Q")
    {
    }
  }
}
