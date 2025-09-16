// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.PerUnitTax.PerUnitTaxPostOptions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.Extensions.PerUnitTax;

/// <summary>
/// A set of post options for per unit tax amount during the release of the document.
/// </summary>
public class PerUnitTaxPostOptions
{
  /// <summary>
  /// Post per-unit tax amount to the document line account.
  /// </summary>
  public const 
  #nullable disable
  string LineAccount = "L";
  /// <summary>
  /// Post per-unit tax amount to the account specified in tax settings.
  /// </summary>
  public const string TaxAccount = "T";

  /// <summary>
  /// Post per-unit tax amount to the document line account.
  /// </summary>
  public class lineAccount : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PerUnitTaxPostOptions.lineAccount>
  {
    public lineAccount()
      : base("L")
    {
    }
  }

  /// <summary>
  /// Post per-unit tax amount to the account specified in tax settings.
  /// </summary>
  public class taxAccount : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PerUnitTaxPostOptions.taxAccount>
  {
    public taxAccount()
      : base("T")
    {
    }
  }

  /// <summary>
  /// String list attribute with a list of per unit tax post options.
  /// </summary>
  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "L", "T" }, new string[2]
      {
        "Line Account",
        "Provisional Account"
      })
    {
    }
  }
}
