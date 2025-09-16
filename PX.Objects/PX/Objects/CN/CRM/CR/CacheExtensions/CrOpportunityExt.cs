// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.CRM.CR.CacheExtensions.CrOpportunityExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.CN.CRM.CR.CacheExtensions;

public sealed class CrOpportunityExt : PXCacheExtension<
#nullable disable
CROpportunity>
{
  [PXDBBaseCury(null, null, BqlField = typeof (CrStandaloneOpportunityExt.cost))]
  [PXUIField(DisplayName = "Cost")]
  public Decimal? Cost { get; set; }

  [PXBaseCury]
  [PXFormula(typeof (Sub<CrOpportunityExt.curyAmount, CrOpportunityExt.cost>))]
  [PXUIField(DisplayName = "Gross Margin", Enabled = false)]
  public Decimal? GrossMarginAbsolute { get; set; }

  [PXDecimal]
  [PXFormula(typeof (Switch<Case<Where<CrOpportunityExt.curyAmount, NotEqual<decimal0>>, Mult<Div<Sub<CrOpportunityExt.curyAmount, CrOpportunityExt.cost>, CrOpportunityExt.curyAmount>, decimal100>>, decimal0>))]
  [PXUIField(DisplayName = "Gross Margin %", Enabled = false)]
  public Decimal? GrossMarginPercentage { get; set; }

  [PXDBBool(BqlField = typeof (CrStandaloneOpportunityExt.multipleAccounts))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Multiple Customers")]
  public bool? MultipleAccounts { get; set; }

  [Obsolete]
  [PXDBBaseCury(null, null, BqlField = typeof (CrStandaloneOpportunityExt.quotedAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount")]
  public Decimal? QuotedAmount { get; set; }

  [Obsolete]
  [PXDBBaseCury(null, null, BqlField = typeof (CrStandaloneOpportunityExt.totalAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Sub<CrOpportunityExt.quotedAmount, CROpportunity.curyDiscTot>))]
  [PXUIField(DisplayName = "Total", Enabled = false)]
  public Decimal? TotalAmount { get; set; }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectQuotes>();

  public abstract class cost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CrOpportunityExt.cost>
  {
  }

  public abstract class grossMarginAbsolute : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CrOpportunityExt.grossMarginAbsolute>
  {
  }

  public abstract class grossMarginPercentage : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CrOpportunityExt.grossMarginPercentage>
  {
  }

  public abstract class multipleAccounts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CrOpportunityExt.multipleAccounts>
  {
  }

  public abstract class quotedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CrOpportunityExt.quotedAmount>
  {
  }

  public abstract class totalAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CrOpportunityExt.totalAmount>
  {
  }

  public abstract class curyAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CrOpportunityExt.curyAmount>
  {
  }
}
