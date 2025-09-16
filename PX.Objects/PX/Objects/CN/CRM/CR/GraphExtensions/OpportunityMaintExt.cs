// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.CRM.CR.GraphExtensions.OpportunityMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.CRM.CR.CacheExtensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.PM.CacheExtensions;
using System;

#nullable disable
namespace PX.Objects.CN.CRM.CR.GraphExtensions;

public class OpportunityMaintExt : PXGraphExtension<OpportunityMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectQuotes>();

  protected virtual void _(Events.RowSelected<CROpportunity> args)
  {
    CROpportunity row = args.Row;
    if (row == null)
      return;
    this.SetOpportunityAmountSource(row);
  }

  protected virtual void _(Events.RowInserting<CROpportunity> args)
  {
    CROpportunity row = args.Row;
    if (row == null)
      return;
    CrOpportunityExt extension = PXCacheEx.GetExtension<CrOpportunityExt>((IBqlTable) row);
    if (extension == null)
      return;
    extension.Cost = new Decimal?(0M);
  }

  protected virtual void _(Events.RowPersisted<CROpportunity> args)
  {
    ((PXSelectBase) this.Base.Quotes).Cache.Clear();
    ((PXSelectBase) this.Base.Quotes).View.Clear();
    ((PXSelectBase) this.Base.Quotes).View.RequestRefresh();
    CROpportunity row = args.Row;
    if (row == null)
      return;
    this.SetOpportunityAmountSource(row);
  }

  private void SetOpportunityAmountSource(CROpportunity opportunity)
  {
    CrOpportunityExt extension1 = PXCacheEx.GetExtension<CrOpportunityExt>((IBqlTable) opportunity);
    if (extension1 == null || opportunity.ManualTotalEntry.GetValueOrDefault())
      return;
    CRQuote crQuote = ((PXSelectBase<CRQuote>) new PXSelect<CRQuote, Where<CRQuote.opportunityID, Equal<Required<CROpportunity.opportunityID>>, And<CRQuote.quoteID, Equal<CRQuote.defQuoteID>>>>((PXGraph) this.Base)).SelectSingle(new object[1]
    {
      (object) opportunity.OpportunityID
    });
    if (crQuote == null)
      return;
    CRQuoteExt extension2 = PXCacheEx.GetExtension<CRQuoteExt>((IBqlTable) crQuote);
    extension1.GrossMarginAbsolute = extension2.CuryGrossMarginAmount;
    extension1.GrossMarginPercentage = extension2.GrossMarginPct;
    extension1.Cost = extension2.CostTotal;
  }
}
