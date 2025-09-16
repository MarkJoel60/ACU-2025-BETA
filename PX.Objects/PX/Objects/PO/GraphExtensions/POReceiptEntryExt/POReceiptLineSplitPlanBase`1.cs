// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POReceiptEntryExt.POReceiptLineSplitPlanBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POReceiptEntryExt;

public abstract class POReceiptLineSplitPlanBase<TItemPlanSource> : 
  ItemPlan<POReceiptEntry, PX.Objects.PO.POReceipt, TItemPlanSource>
  where TItemPlanSource : class, IItemPlanPOReceiptSource, IBqlTable, new()
{
  public override void _(PX.Data.Events.RowUpdated<PX.Objects.PO.POReceipt> e)
  {
    base._(e);
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.PO.POReceipt>>) e).Cache.ObjectsEqual<PX.Objects.PO.POReceipt.receiptDate, PX.Objects.PO.POReceipt.hold>((object) e.Row, (object) e.OldRow) || !NonGenericIEnumerableExtensions.Any_(((PXCache) this.PlanCache).Inserted))
      return;
    HashSet<long?> nullableSet = this.CollectReceiptPlans(e.Row);
    foreach (INItemPlan plan in ((PXCache) this.PlanCache).Inserted)
    {
      if (nullableSet.Contains(plan.PlanID))
        this.DefaultValuesFromReceipt(plan, e.Row);
    }
  }

  protected abstract HashSet<long?> CollectReceiptPlans(PX.Objects.PO.POReceipt receipt);

  protected virtual void DefaultValuesFromReceipt(INItemPlan plan, PX.Objects.PO.POReceipt receipt)
  {
    plan.Hold = receipt.Hold;
    plan.RefNoteID = receipt.NoteID;
    if ((!(receipt.ReceiptType == "RT") || receipt.OrigReceiptNbr == null ? 0 : (plan.Reverse.GetValueOrDefault() ? 1 : 0)) != 0)
      return;
    plan.PlanDate = receipt.ReceiptDate;
  }

  public override INItemPlan DefaultValues(INItemPlan planRow, TItemPlanSource splitRow)
  {
    PX.Objects.PO.POReceiptLine poReceiptLine = (PX.Objects.PO.POReceiptLine) null;
    if (!planRow.IsTemporary.GetValueOrDefault())
    {
      poReceiptLine = PXParentAttribute.SelectParent<PX.Objects.PO.POReceiptLine>((PXCache) this.ItemPlanSourceCache, (object) splitRow);
      if (poReceiptLine != null && poReceiptLine.Released.GetValueOrDefault())
        return (INItemPlan) null;
    }
    planRow.BAccountID = (int?) poReceiptLine?.VendorID;
    if (splitRow.PONbr != null)
    {
      planRow.OrigPlanLevel = new int?(0);
      if (poReceiptLine != null && !planRow.OrigPlanID.HasValue)
      {
        PX.Objects.PO.POLine poLine = PX.Objects.PO.POLine.PK.Find((PXGraph) this.Base, poReceiptLine.POType, poReceiptLine.PONbr, poReceiptLine.POLineNbr);
        planRow.OrigPlanID = poLine.PlanID;
      }
    }
    bool? nullable1;
    int? nullable2;
    switch (splitRow.LineType)
    {
      case "GI":
      case "GR":
      case "GM":
        if (poReceiptLine != null && poReceiptLine.IsCorrection.GetValueOrDefault())
        {
          nullable1 = poReceiptLine.IsAdjustedIN;
          bool flag = false;
          if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
          {
            planRow.PlanType = (string) null;
            break;
          }
        }
        if (poReceiptLine?.OrigTranType == "TRX")
        {
          if (!planRow.OrigNoteID.HasValue)
            planRow.OrigNoteID = poReceiptLine.OrigNoteID;
          INItemPlan inItemPlan1 = planRow;
          nullable2 = poReceiptLine.OrigToLocationID;
          int num1 = nullable2.HasValue ? 1 : 0;
          nullable1 = poReceiptLine.OrigIsLotSerial;
          int num2 = nullable1.GetValueOrDefault() ? 2 : 0;
          int? nullable3 = new int?(num1 | num2);
          inItemPlan1.OrigPlanLevel = nullable3;
          INItemPlan inItemPlan2 = planRow;
          nullable1 = poReceiptLine.OrigIsFixedInTransit;
          string str = nullable1.GetValueOrDefault() ? "45" : "43";
          inItemPlan2.PlanType = str;
          break;
        }
        planRow.PlanType = "71";
        INItemPlan inItemPlan3 = planRow;
        int num;
        if (!(splitRow.ReceiptType == "RN"))
        {
          nullable1 = splitRow.IsReverse;
          num = nullable1.GetValueOrDefault() ? 1 : 0;
        }
        else
          num = 1;
        bool? nullable4 = new bool?(num != 0);
        inItemPlan3.Reverse = nullable4;
        break;
      case "GS":
        if (!(splitRow.ReceiptType == "RT"))
          throw new PXException();
        if (poReceiptLine != null && poReceiptLine.IsCorrection.GetValueOrDefault())
        {
          nullable1 = poReceiptLine.IsAdjustedIN;
          bool flag = false;
          if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
          {
            planRow.PlanType = (string) null;
            break;
          }
        }
        if (poReceiptLine != null)
        {
          nullable1 = poReceiptLine.IsCorrection;
          if (nullable1.GetValueOrDefault())
          {
            nullable1 = splitRow.IsReverse;
            if (nullable1.GetValueOrDefault())
              planRow.Reverse = new bool?(true);
          }
        }
        planRow.PlanType = "77";
        break;
      case "GF":
        if (!(splitRow.ReceiptType == "RT"))
          throw new PXException();
        planRow.PlanType = "F9";
        break;
      case "GP":
        if (splitRow.ReceiptType == "RT")
        {
          planRow.PlanType = "75";
          break;
        }
        if (!(splitRow.ReceiptType == "RN"))
          throw new PXException();
        planRow.PlanType = (string) null;
        break;
      default:
        return (INItemPlan) null;
    }
    planRow.OrigPlanType = splitRow.OrigPlanType;
    planRow.InventoryID = splitRow.InventoryID;
    INItemPlan inItemPlan4 = planRow;
    nullable2 = splitRow.InventoryID;
    int? nullable5 = (int?) poReceiptLine?.InventoryID;
    int? nullable6;
    if (nullable2.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable2.HasValue == nullable5.HasValue)
    {
      nullable5 = new int?();
      nullable6 = nullable5;
    }
    else if (poReceiptLine == null)
    {
      nullable5 = new int?();
      nullable6 = nullable5;
    }
    else
      nullable6 = poReceiptLine.InventoryID;
    inItemPlan4.KitInventoryID = nullable6;
    planRow.SubItemID = splitRow.SubItemID;
    planRow.SiteID = splitRow.SiteID;
    planRow.LocationID = splitRow.LocationID;
    planRow.LotSerialNbr = splitRow.LotSerialNbr;
    planRow.IsTempLotSerial = new bool?(!string.IsNullOrEmpty(splitRow.AssignedNbr) && INLotSerialNbrAttribute.StringsEqual(splitRow.AssignedNbr, splitRow.LotSerialNbr));
    INItemPlan inItemPlan5 = planRow;
    int? nullable7;
    if (poReceiptLine == null)
    {
      nullable5 = new int?();
      nullable7 = nullable5;
    }
    else
      nullable7 = poReceiptLine.ProjectID;
    inItemPlan5.ProjectID = nullable7;
    INItemPlan inItemPlan6 = planRow;
    int? nullable8;
    if (poReceiptLine == null)
    {
      nullable5 = new int?();
      nullable8 = nullable5;
    }
    else
      nullable8 = poReceiptLine.TaskID;
    inItemPlan6.TaskID = nullable8;
    INItemPlan inItemPlan7 = planRow;
    nullable1 = planRow.IsTemporary;
    int? nullable9;
    if (nullable1.GetValueOrDefault())
      nullable9 = new int?(0);
    else if (poReceiptLine == null)
    {
      nullable5 = new int?();
      nullable9 = nullable5;
    }
    else
      nullable9 = poReceiptLine.CostCenterID;
    inItemPlan7.CostCenterID = nullable9;
    nullable1 = planRow.IsTempLotSerial;
    if (nullable1.GetValueOrDefault())
      planRow.LotSerialNbr = (string) null;
    planRow.UOM = poReceiptLine?.UOM;
    planRow.PlanQty = splitRow.BaseQty;
    planRow.PlanDate = splitRow.ReceiptDate;
    if (string.IsNullOrEmpty(planRow.PlanType))
      return (INItemPlan) null;
    this.DefaultValuesFromReceipt(planRow, ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current);
    return planRow;
  }
}
