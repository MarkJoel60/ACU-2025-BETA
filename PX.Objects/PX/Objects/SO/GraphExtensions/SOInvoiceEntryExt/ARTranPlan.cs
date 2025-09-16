// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt.ARTranPlan
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt;

public class ARTranPlan : ItemPlan<SOInvoiceEntry, PX.Objects.AR.ARInvoice, PX.Objects.AR.ARTran>
{
  public override void _(PX.Data.Events.RowUpdated<PX.Objects.AR.ARInvoice> e)
  {
    base._(e);
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.AR.ARInvoice>>) e).Cache.ObjectsEqual<PX.Objects.AR.ARInvoice.docDate, PX.Objects.AR.ARInvoice.hold, PX.Objects.AR.ARInvoice.creditHold>((object) e.Row, (object) e.OldRow))
      return;
    foreach (PXResult<PX.Objects.AR.ARTran> pxResult in PXSelectBase<PX.Objects.AR.ARTran, PXSelect<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.tranType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>, And<PX.Objects.AR.ARTran.lineType, Equal<SOLineType.inventory>, And<PX.Objects.AR.ARTran.sOOrderNbr, IsNull, And<PX.Objects.AR.ARTran.sOShipmentNbr, IsNull>>>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()))
    {
      PX.Objects.AR.ARTran row = PXResult<PX.Objects.AR.ARTran>.op_Implicit(pxResult);
      this.RaiseRowUpdated(row);
      GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Transactions).Cache, (object) row);
    }
  }

  public override INItemPlan DefaultValues(INItemPlan planRow, PX.Objects.AR.ARTran tran)
  {
    if (!ARTranPlan.IsDirectLineNotLinkedToSO(tran) || tran.Released.GetValueOrDefault())
      return (INItemPlan) null;
    PX.Objects.AR.ARInvoice current = (PX.Objects.AR.ARInvoice) ((PXCache) GraphHelper.Caches<PX.Objects.AR.ARInvoice>((PXGraph) this.Base)).Current;
    bool? hold = current.Hold;
    bool? creditHold = current.CreditHold;
    bool? nullable1 = hold.GetValueOrDefault() || !creditHold.GetValueOrDefault() && !hold.HasValue ? hold : creditHold;
    planRow.BAccountID = tran.CustomerID;
    planRow.PlanType = nullable1.GetValueOrDefault() ? "69" : "62";
    planRow.InventoryID = tran.InventoryID;
    planRow.SubItemID = tran.SubItemID;
    planRow.SiteID = tran.SiteID;
    planRow.LocationID = tran.LocationID;
    planRow.CostCenterID = tran.CostCenterID;
    planRow.ProjectID = tran.ProjectID;
    planRow.TaskID = tran.TaskID;
    INItemPlan inItemPlan1 = planRow;
    short? invtMult = tran.InvtMult;
    int? nullable2 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num1 = 0;
    int num2 = nullable2.GetValueOrDefault() > num1 & nullable2.HasValue ? 1 : 0;
    Decimal? baseQty = tran.BaseQty;
    Decimal num3 = 0M;
    int num4 = baseQty.GetValueOrDefault() < num3 & baseQty.HasValue ? 1 : 0;
    bool? nullable3 = new bool?((num2 ^ num4) != 0);
    inItemPlan1.Reverse = nullable3;
    planRow.PlanDate = current.DocDate;
    planRow.UOM = tran.UOM;
    INItemPlan inItemPlan2 = planRow;
    baseQty = tran.BaseQty;
    Decimal? nullable4 = new Decimal?(Math.Abs(baseQty.GetValueOrDefault()));
    inItemPlan2.PlanQty = nullable4;
    planRow.RefNoteID = current.NoteID;
    planRow.Hold = nullable1;
    planRow.LotSerialNbr = tran.LotSerialNbr;
    PXCache<ARTranAsSplit> pxCache = GraphHelper.Caches<ARTranAsSplit>((PXGraph) this.Base);
    ARTranAsSplit arTranAsSplit = pxCache.Locate(ARTranAsSplit.FromARTran(tran));
    planRow.IsTempLotSerial = new bool?(arTranAsSplit != null && EnumerableExtensions.IsNotIn<PXEntryStatus>(pxCache.GetStatus(arTranAsSplit), (PXEntryStatus) 3, (PXEntryStatus) 4) && !string.IsNullOrEmpty(arTranAsSplit.AssignedNbr) && INLotSerialNbrAttribute.StringsEqual(arTranAsSplit.AssignedNbr, tran.LotSerialNbr));
    if (planRow.IsTempLotSerial.GetValueOrDefault())
      planRow.LotSerialNbr = (string) null;
    return planRow;
  }

  public static bool IsDirectLineNotLinkedToSO(PX.Objects.AR.ARTran tran)
  {
    if (tran.SOShipmentNbr != null || tran.SOOrderNbr != null || !(tran.LineType == "GI"))
      return false;
    short? invtMult = tran.InvtMult;
    int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num = 0;
    return !(nullable.GetValueOrDefault() == num & nullable.HasValue);
  }
}
