// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMQuoteMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Discount;
using PX.Objects.CR;
using PX.Objects.CR.Standalone;
using PX.Objects.CS;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

public class PMQuoteMaintExt : PXGraphExtension<PMQuoteMaint.PMDiscount, PMQuoteMaint>
{
  public virtual void Initialize()
  {
    PXCache cache = ((PXSelectBase) ((PXGraphExtension<PMQuoteMaint>) this).Base.Quote).Cache;
    if (!(cache.Current is PMQuote current))
      return;
    this.VisibilityHandler(cache, current);
  }

  protected virtual void PMQuote_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e,
    PXRowSelected sel)
  {
    sel?.Invoke(sender, e);
    if (!(e.Row is PMQuote row))
      return;
    this.VisibilityHandler(sender, row);
  }

  private void VisibilityHandler(PXCache sender, PMQuote row)
  {
    CROpportunityRevision opportunityRevision = PXResult<CROpportunityRevision>.op_Implicit(((IQueryable<PXResult<CROpportunityRevision>>) PXSelectBase<CROpportunityRevision, PXSelectReadonly<CROpportunityRevision, Where<CROpportunityRevision.noteID, Equal<Required<CROpportunityRevision.noteID>>>>.Config>.Select((PXGraph) ((PXGraphExtension<PMQuoteMaint>) this).Base, new object[1]
    {
      (object) row.QuoteID
    })).FirstOrDefault<PXResult<CROpportunityRevision>>());
    PXResult<PX.Objects.CR.Standalone.CROpportunity> pxResult;
    if (opportunityRevision != null)
      pxResult = ((IQueryable<PXResult<PX.Objects.CR.Standalone.CROpportunity>>) PXSelectBase<PX.Objects.CR.Standalone.CROpportunity, PXSelectReadonly<PX.Objects.CR.Standalone.CROpportunity, Where<PX.Objects.CR.Standalone.CROpportunity.opportunityID, Equal<Required<PX.Objects.CR.Standalone.CROpportunity.opportunityID>>>>.Config>.Select((PXGraph) ((PXGraphExtension<PMQuoteMaint>) this).Base, new object[1]
      {
        (object) opportunityRevision.OpportunityID
      })).FirstOrDefault<PXResult<PX.Objects.CR.Standalone.CROpportunity>>();
    else
      pxResult = (PXResult<PX.Objects.CR.Standalone.CROpportunity>) null;
    PX.Objects.CR.Standalone.CROpportunity crOpportunity1 = PXResult<PX.Objects.CR.Standalone.CROpportunity>.op_Implicit(pxResult);
    PX.Objects.CR.Standalone.CROpportunity crOpportunity2 = PXResult<PX.Objects.CR.Standalone.CROpportunity>.op_Implicit(((IQueryable<PXResult<PX.Objects.CR.Standalone.CROpportunity>>) PXSelectBase<PX.Objects.CR.Standalone.CROpportunity, PXSelect<PX.Objects.CR.Standalone.CROpportunity, Where<PX.Objects.CR.Standalone.CROpportunity.opportunityID, Equal<Required<PX.Objects.CR.Standalone.CROpportunity.opportunityID>>>>.Config>.Select((PXGraph) ((PXGraphExtension<PMQuoteMaint>) this).Base, new object[1]
    {
      (object) row.OpportunityID
    })).FirstOrDefault<PXResult<PX.Objects.CR.Standalone.CROpportunity>>());
    int num;
    if (crOpportunity2 == null)
    {
      num = 0;
    }
    else
    {
      bool? isActive = crOpportunity2.IsActive;
      bool flag = false;
      num = isActive.GetValueOrDefault() == flag & isActive.HasValue ? 1 : 0;
    }
    bool flag1 = num != 0;
    bool flag2 = !row.IsDisabled.GetValueOrDefault() && !flag1 && row.Status != "C";
    if (crOpportunity1?.OpportunityID == crOpportunity2?.OpportunityID)
    {
      if (!DimensionMaint.IsAutonumbered(sender.Graph, "PROJECT"))
      {
        PXCache cach = ((PXGraph) ((PXGraphExtension<PMQuoteMaint>) this).Base).Caches[typeof (PMQuote)];
        foreach (string field in (List<string>) cach.Fields)
        {
          if (!cach.Keys.Contains(field) && field != cach.GetField(typeof (PMQuote.isPrimary)) && field != cach.GetField(typeof (PMQuote.curyID)) && field != cach.GetField(typeof (PMQuote.locationID)))
            PXUIFieldAttribute.SetEnabled(sender, (object) row, field, flag2);
        }
      }
      else
      {
        PXUIFieldAttribute.SetEnabled(sender, (object) row, flag2);
        PXUIFieldAttribute.SetEnabled<PMQuote.quoteNbr>(sender, (object) row, true);
      }
    }
    else
    {
      PXCache cach = ((PXGraph) ((PXGraphExtension<PMQuoteMaint>) this).Base).Caches[typeof (PMQuote)];
      foreach (string field in (List<string>) cach.Fields)
      {
        if (!cach.Keys.Contains(field) && field != cach.GetField(typeof (PMQuote.opportunityID)) && field != cach.GetField(typeof (PMQuote.isPrimary)) && field != cach.GetField(typeof (PMQuote.quoteNbr)))
          PXUIFieldAttribute.SetEnabled(sender, (object) row, field, flag2);
      }
    }
    ((PXGraph) ((PXGraphExtension<PMQuoteMaint>) this).Base).Caches[typeof (PMQuote)].AllowDelete = !flag1;
    System.Type[] typeArray = new System.Type[7]
    {
      typeof (CROpportunityDiscountDetail),
      typeof (CROpportunityProducts),
      typeof (CRTaxTran),
      typeof (CRAddress),
      typeof (CRContact),
      typeof (PMQuoteTask),
      typeof (CRShippingAddress)
    };
    foreach (System.Type type in typeArray)
      ((PXGraph) ((PXGraphExtension<PMQuoteMaint>) this).Base).Caches[type].AllowInsert = ((PXGraph) ((PXGraphExtension<PMQuoteMaint>) this).Base).Caches[type].AllowUpdate = ((PXGraph) ((PXGraphExtension<PMQuoteMaint>) this).Base).Caches[type].AllowDelete = flag2;
    if (!DimensionMaint.IsAutonumbered(sender.Graph, "PROJECT"))
    {
      if (row.Status == "A")
        PXUIFieldAttribute.SetEnabled<PMQuote.quoteProjectCD>(sender, (object) row);
      else if (row.Status == "D")
      {
        PXUIFieldAttribute.SetEnabled<PMQuote.opportunityID>(sender, (object) row, row.OpportunityID == null || !((PXGraphExtension<PMQuoteMaint>) this).Base.IsReadonlyPrimaryQuote(row.QuoteID));
        if (row.OpportunityID == null)
          PXUIFieldAttribute.SetEnabled<PMQuote.bAccountID>(sender, (object) row, row.OpportunityID == null);
      }
    }
    ((PXGraph) ((PXGraphExtension<PMQuoteMaint>) this).Base).Caches[typeof (PMQuoteMaint.CopyQuoteFilter)].AllowUpdate = true;
    ((PXGraph) ((PXGraphExtension<PMQuoteMaint>) this).Base).Caches[typeof (RecalcDiscountsParamFilter)].AllowUpdate = true;
    if (crOpportunity1?.OpportunityID != crOpportunity2?.OpportunityID)
    {
      PXUIFieldAttribute.SetEnabled<PMQuote.subject>(sender, (object) row, true);
      PXUIFieldAttribute.SetEnabled<PMQuote.status>(sender, (object) row, false);
    }
    PXUIFieldAttribute.SetEnabled<PMQuote.quoteProjectCD>(sender, (object) row, !DimensionMaint.IsAutonumbered(sender.Graph, "PROJECT"));
  }
}
