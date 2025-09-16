// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INRegisterEntryBaseExt.INTranSplitPlanBase`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.INRegisterEntryBaseExt;

public abstract class INTranSplitPlanBase<TGraph, TRefEntity, TItemPlanSource> : 
  ItemPlan<TGraph, TRefEntity, TItemPlanSource>
  where TGraph : PXGraph
  where TRefEntity : class, IItemPlanRegister, IBqlTable, new()
  where TItemPlanSource : class, IItemPlanINSource, IBqlTable, new()
{
  public override void _(Events.RowUpdated<TRefEntity> e)
  {
    base._(e);
    bool? hold1 = e.Row.Hold;
    bool? hold2 = e.OldRow.Hold;
    if (hold1.GetValueOrDefault() == hold2.GetValueOrDefault() & hold1.HasValue == hold2.HasValue && !(e.Row.TransferType != e.OldRow.TransferType))
      return;
    bool flag = !object.Equals((object) e.Row.TransferType, (object) e.OldRow.TransferType);
    this.PrefetchDocumentPlansToCache();
    foreach (TItemPlanSource documentSplit in this.GetDocumentSplits())
    {
      foreach (INItemPlan inItemPlan in ((PXCache) this.PlanCache).Cached)
      {
        long? planId1 = inItemPlan.PlanID;
        long? planId2 = documentSplit.PlanID;
        if (planId1.GetValueOrDefault() == planId2.GetValueOrDefault() & planId1.HasValue == planId2.HasValue && EnumerableExtensions.IsNotIn<PXEntryStatus>(this.PlanCache.GetStatus(inItemPlan), (PXEntryStatus) 3, (PXEntryStatus) 4))
        {
          if (flag)
          {
            documentSplit.TransferType = e.Row.TransferType;
            GraphHelper.MarkUpdated((PXCache) this.ItemPlanSourceCache, (object) documentSplit, true);
            this.PlanCache.Update(this.DefaultValues(PXCache<INItemPlan>.CreateCopy(inItemPlan), documentSplit));
          }
          else
          {
            inItemPlan.Hold = e.Row.Hold;
            if (!this.GetAllocateDocumentsOnHold())
              this.PlanCache.Update(inItemPlan);
            else
              GraphHelper.MarkUpdated((PXCache) this.PlanCache, (object) inItemPlan, true);
          }
        }
      }
    }
  }

  protected abstract void PrefetchDocumentPlansToCache();

  protected abstract IEnumerable<TItemPlanSource> GetDocumentSplits();

  public override INItemPlan DefaultValues(INItemPlan planRow, TItemPlanSource origRow)
  {
    TRefEntity current = (TRefEntity) ((PXCache) GraphHelper.Caches<TRefEntity>((PXGraph) this.Base)).Current;
    planRow.OrigPlanType = origRow.OrigPlanType;
    planRow.InventoryID = origRow.InventoryID;
    planRow.SubItemID = origRow.SubItemID;
    planRow.SiteID = origRow.SiteID;
    planRow.LocationID = origRow.LocationID;
    planRow.LotSerialNbr = origRow.LotSerialNbr;
    planRow.IsTempLotSerial = new bool?(!string.IsNullOrEmpty(origRow.AssignedNbr) && INLotSerialNbrAttribute.StringsEqual(origRow.AssignedNbr, origRow.LotSerialNbr));
    if (planRow.IsTempLotSerial.GetValueOrDefault())
      planRow.LotSerialNbr = (string) null;
    planRow.PlanQty = origRow.BaseQty;
    planRow.PlanDate = !string.IsNullOrEmpty(origRow.SOLineType) ? origRow.TranDate : new DateTime?(new DateTime(1900, 1, 1));
    if (current.DocType == "T" && current.TransferType == "1")
    {
      int? siteId = current.SiteID;
      int? toSiteId = current.ToSiteID;
      if (siteId.GetValueOrDefault() == toSiteId.GetValueOrDefault() & siteId.HasValue == toSiteId.HasValue)
      {
        planRow.ExcludePlanLevel = new int?(!string.IsNullOrEmpty(planRow.LotSerialNbr) ? 327680 /*0x050000*/ : 65536 /*0x010000*/);
        goto label_6;
      }
    }
    planRow.ExcludePlanLevel = new int?();
label_6:
    planRow.RefNoteID = current.NoteID;
    planRow.Hold = current.Hold;
    string tranType = origRow.TranType;
    if (tranType != null && tranType.Length == 3)
    {
      switch (tranType[2])
      {
        case 'C':
          if (tranType == "ASC" || tranType == "NSC")
            goto label_38;
          goto label_38;
        case 'I':
          if (tranType == "III")
            goto label_20;
          goto label_38;
        case 'J':
          if (tranType == "ADJ")
            goto label_38;
          goto label_38;
        case 'M':
          switch (tranType)
          {
            case "CRM":
              break;
            case "DRM":
              goto label_20;
            default:
              goto label_38;
          }
          break;
        case 'P':
          if (tranType == "RCP")
            break;
          goto label_38;
        case 'T':
          if (tranType == "RET")
            break;
          goto label_38;
        case 'V':
          if (tranType == "INV")
            goto label_20;
          goto label_38;
        case 'X':
          if (tranType == "TRX")
          {
            short? invtMult = origRow.InvtMult;
            if ((invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?()).GetValueOrDefault() == -1)
            {
              if (origRow.TransferType == "1")
              {
                if (origRow.Released.GetValueOrDefault())
                  return (INItemPlan) null;
                planRow.PlanType = "40";
                goto label_39;
              }
              if (origRow.Released.GetValueOrDefault())
              {
                planRow.PlanType = origRow.IsFixedInTransit.GetValueOrDefault() ? "44" : "42";
                planRow.SiteID = origRow.ToSiteID;
                planRow.LocationID = origRow.ToLocationID;
                goto label_39;
              }
              planRow.PlanType = origRow.SOLineType == null ? "41" : "62";
              goto label_39;
            }
            if (origRow.Released.GetValueOrDefault())
              return (INItemPlan) null;
            planRow.PlanType = "43";
            if (string.IsNullOrEmpty(planRow.OrigPlanType))
            {
              planRow.OrigPlanType = "42";
              goto label_39;
            }
            goto label_39;
          }
          goto label_38;
        case 'Y':
          if (tranType == "ASY" || tranType == "DSY")
          {
            if (origRow.Released.GetValueOrDefault())
              return (INItemPlan) null;
            short? invtMult = origRow.InvtMult;
            int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
            int num = -1;
            planRow.PlanType = !(nullable.GetValueOrDefault() == num & nullable.HasValue) ? "51" : "50";
            goto label_39;
          }
          goto label_38;
        default:
          goto label_38;
      }
      if (origRow.Released.GetValueOrDefault())
        return (INItemPlan) null;
      planRow.PlanType = origRow.SOLineType != null ? "62" : (origRow.POLineType == "GS" ? "77" : (origRow.POLineType == "GF" ? "F9" : (origRow.POLineType == "GP" ? "75" : "10")));
      goto label_39;
label_20:
      if (origRow.Released.GetValueOrDefault())
        return (INItemPlan) null;
      planRow.PlanType = origRow.SOLineType == null ? "20" : "62";
label_39:
      return planRow;
    }
label_38:
    return (INItemPlan) null;
  }

  public virtual bool? IsTwoStepTransferPlanValid(TItemPlanSource split, INItemPlan plan)
  {
    if (!(split.DocType != "T") && !(split.TranType != "TRX") && !(split.TransferType != "2"))
    {
      short? invtMult = split.InvtMult;
      Decimal? nullable = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
      Decimal num = -1M;
      if (nullable.GetValueOrDefault() == num & nullable.HasValue && !split.Released.GetValueOrDefault())
        return split.SOLineType == null ? new bool?(plan?.PlanType == "41") : new bool?(plan?.PlanType == "62");
    }
    return new bool?();
  }

  public override TNode UpdateAllocatedQuantitiesBase<TNode>(
    INItemPlan plan,
    INPlanType plantype,
    bool InclQtyAvail)
  {
    TNode node = base.UpdateAllocatedQuantitiesBase<TNode>(plan, plantype, InclQtyAvail);
    if (typeof (TNode) == typeof (SiteStatusByCostCenter) && node is SiteStatusByCostCenter statusByCostCenter && ((PXCache) GraphHelper.Caches<INRegister>((PXGraph) this.Base)).Current is INRegister current && current.IsCorrection.GetValueOrDefault())
    {
      Guid? noteId = current.NoteID;
      Guid? refNoteId = plan.RefNoteID;
      if ((noteId.HasValue == refNoteId.HasValue ? (noteId.HasValue ? (noteId.GetValueOrDefault() == refNoteId.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        statusByCostCenter.NegAvailQty = new bool?(true);
    }
    return node;
  }
}
