// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.CacheExtensions.CRQuoteExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CR.Standalone;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.PM.CacheExtensions;

[Serializable]
public sealed class CRQuoteExt : PXCacheExtension<
#nullable disable
PX.Objects.CR.CRQuote>
{
  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the extension is active.
  /// </summary>
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectQuotes>();

  /// <summary>
  /// A pointer to <see cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.CostTotal" />.
  /// </summary>
  [PXDBDecimal(4, BqlField = typeof (CROpportunityRevision.costTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? CostTotal { get; set; }

  /// <summary>
  /// A pointer to <see cref="P:PX.Objects.CR.Standalone.CROpportunityRevision.CuryCostTotal" />.
  /// </summary>
  [PXDBCurrency(typeof (PX.Objects.CR.CRQuote.curyInfoID), typeof (CRQuoteExt.costTotal), BqlField = typeof (CROpportunityRevision.curyCostTotal))]
  [PXUIField(DisplayName = "Cost", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? CuryCostTotal { get; set; }

  /// <summary>
  /// The gross margin amount, which is calculated from <see cref="P:PX.Objects.CR.CRQuote.LineTotal" /> and <see cref="P:PX.Objects.PM.CacheExtensions.CRQuoteExt.CostTotal" />.
  /// </summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Gross Margin")]
  public Decimal? GrossMarginAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (PX.Objects.CR.CRQuote.lineTotal), typeof (CRQuoteExt.costTotal)})] get
    {
      Decimal? lineTotal = this.Base.LineTotal;
      Decimal? costTotal = this.CostTotal;
      return !(lineTotal.HasValue & costTotal.HasValue) ? new Decimal?() : new Decimal?(lineTotal.GetValueOrDefault() - costTotal.GetValueOrDefault());
    }
  }

  /// <summary>
  /// The gross margin amount in the base currency. The amount is calculated from <see cref="P:PX.Objects.CR.CRQuote.CuryLineTotal" /> and <see cref="P:PX.Objects.PM.CacheExtensions.CRQuoteExt.CuryCostTotal" />.
  /// </summary>
  [PXCurrency(typeof (PX.Objects.CR.CRQuote.curyInfoID), typeof (CRQuoteExt.grossMarginAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Gross Margin Amount")]
  public Decimal? CuryGrossMarginAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (PX.Objects.CR.CRQuote.curyLineTotal), typeof (CRQuoteExt.curyCostTotal)})] get
    {
      Decimal? curyLineTotal = this.Base.CuryLineTotal;
      Decimal? curyCostTotal = this.CuryCostTotal;
      return !(curyLineTotal.HasValue & curyCostTotal.HasValue) ? new Decimal?() : new Decimal?(curyLineTotal.GetValueOrDefault() - curyCostTotal.GetValueOrDefault());
    }
  }

  /// <summary>The percentage of the gross margin.</summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Gross Margin (%)")]
  public Decimal? GrossMarginPct
  {
    [PXDependsOnFields(new System.Type[] {typeof (PX.Objects.CR.CRQuote.lineTotal), typeof (CRQuoteExt.costTotal)})] get
    {
      Decimal? lineTotal1 = this.Base.LineTotal;
      Decimal num1 = 0M;
      if (lineTotal1.GetValueOrDefault() == num1 & lineTotal1.HasValue)
        return new Decimal?(0M);
      Decimal num2 = (Decimal) 100;
      Decimal? lineTotal2 = this.Base.LineTotal;
      Decimal? costTotal = this.CostTotal;
      Decimal? nullable1 = lineTotal2.HasValue & costTotal.HasValue ? new Decimal?(lineTotal2.GetValueOrDefault() - costTotal.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable2 = nullable1.HasValue ? new Decimal?(num2 * nullable1.GetValueOrDefault()) : new Decimal?();
      Decimal? lineTotal3 = this.Base.LineTotal;
      return !(nullable2.HasValue & lineTotal3.HasValue) ? new Decimal?() : new Decimal?(nullable2.GetValueOrDefault() / lineTotal3.GetValueOrDefault());
    }
  }

  public abstract class costTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CRQuoteExt.costTotal>
  {
  }

  public abstract class curyCostTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CRQuoteExt.curyCostTotal>
  {
  }

  public abstract class grossMarginAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CRQuoteExt.grossMarginAmount>
  {
  }

  public abstract class curyGrossMarginAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CRQuoteExt.curyGrossMarginAmount>
  {
  }

  public abstract class grossMarginPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CRQuoteExt.grossMarginPct>
  {
  }
}
