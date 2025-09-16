// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.ConfirmShipmentExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.Attributes;
using PX.Objects.SO.Models;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;

public class ConfirmShipmentExtension : PXGraphExtension<SOShipmentEntry>
{
  public PXWorkflowEventHandler<PX.Objects.SO.SOShipment> OnShipmentConfirmed;
  public PXAction<PX.Objects.SO.SOShipment> confirmShipmentAction;

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ConfirmShipmentAction(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ConfirmShipmentExtension.\u003C\u003Ec__DisplayClass2_0 cDisplayClass20 = new ConfirmShipmentExtension.\u003C\u003Ec__DisplayClass2_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass20.list = adapter.Get<PX.Objects.SO.SOShipment>().ToList<PX.Objects.SO.SOShipment>();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass20.massProcess = adapter.MassProcess;
    ((PXAction) this.Base.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation<SOShipmentEntry>((PXGraphExtension<SOShipmentEntry>) this, new PXToggleAsyncDelegate((object) cDisplayClass20, __methodptr(\u003CConfirmShipmentAction\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass20.list;
  }

  public virtual void ConfirmShipment(ConfirmShipmentArgs args)
  {
    PX.Objects.SO.SOShipment shipment = args.Shipment;
    PXGraph origDocumentGraph = args.OrigDocumentGraph;
    if (!args.IsShipmentReadyForConfirmation)
      this.PrepareShipmentForConfirmation(shipment);
    if (((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current == null)
      return;
    this.MarkConfirmed(((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current);
    GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Document).Cache, (object) ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current, true);
    ((PXSelectBase) this.Base.Document).Cache.IsDirty = true;
    foreach (PXResult<INItemPlan, SOShipLineSplit> pxResult in PXSelectBase<INItemPlan, PXSelectJoin<INItemPlan, InnerJoin<SOShipLineSplit, On<SOShipLineSplit.planID, Equal<INItemPlan.planID>>>, Where<SOShipLineSplit.shipmentNbr, Equal<Current<PX.Objects.SO.SOShipment.shipmentNbr>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()))
    {
      SOShipLineSplit soShipLineSplit1 = PXResult<INItemPlan, SOShipLineSplit>.op_Implicit(pxResult);
      INItemPlan copy = PXCache<INItemPlan>.CreateCopy(PXResult<INItemPlan, SOShipLineSplit>.op_Implicit(pxResult));
      INPlanType inPlanType = INPlanType.PK.Find((PXGraph) this.Base, copy.PlanType);
      bool? deleteOnEvent = inPlanType.DeleteOnEvent;
      if (deleteOnEvent.GetValueOrDefault())
        ((PXGraph) this.Base).Caches[typeof (INItemPlan)].Delete((object) copy);
      else if (!string.IsNullOrEmpty(inPlanType.ReplanOnEvent))
      {
        copy.PlanType = inPlanType.ReplanOnEvent;
        copy.OrigPlanType = (string) null;
        ((PXGraph) this.Base).Caches[typeof (INItemPlan)].Update((object) copy);
      }
      SOShipLineSplit soShipLineSplit2 = (SOShipLineSplit) ((PXSelectBase) this.Base.splits).Cache.Locate((object) soShipLineSplit1) ?? soShipLineSplit1;
      GraphHelper.MarkUpdated(((PXSelectBase) this.Base.splits).Cache, (object) soShipLineSplit2, true);
      if (soShipLineSplit2 != null)
      {
        soShipLineSplit2.Confirmed = new bool?(true);
        deleteOnEvent = inPlanType.DeleteOnEvent;
        if (deleteOnEvent.GetValueOrDefault())
          soShipLineSplit2.PlanID = new long?();
      }
      ((PXSelectBase) this.Base.splits).Cache.IsDirty = true;
    }
    if ((PXAccess.FeatureInstalled<FeaturesSet.wMSAdvancedPicking>() || PXAccess.FeatureInstalled<FeaturesSet.wMSPaperlessPicking>()) && ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current.CurrentWorksheetNbr != null)
    {
      ((PXCache) GraphHelper.Caches<SOPickingWorksheet>((PXGraph) this.Base)).Clear();
      ((PXCache) GraphHelper.Caches<SOPickingWorksheet>((PXGraph) this.Base)).ClearQueryCacheObsolete();
      SOPickingWorksheet worksheet = PXResultset<SOPickingWorksheet>.op_Implicit(PXSelectBase<SOPickingWorksheet, PXViewOf<SOPickingWorksheet>.BasedOn<SelectFromBase<SOPickingWorksheet, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<SOPickingWorksheet.worksheetNbr, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current.CurrentWorksheetNbr
      }));
      if (EnumerableExtensions.IsIn<string>(worksheet.WorksheetType, "WV", "SS"))
        this.Base.CartSupportExt?.RemoveItemsFromCart();
      this.Base.TryCompleteWorksheet(worksheet);
    }
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      this.SetSuppressWorkflowOnConfirmShipment();
      this.UpdateOrigDocumentOnConfirmShipment(args);
      this.GroupShipLinesForInvoicing(((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current);
      this.Base.WorkLogExt?.CloseFor(((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current.ShipmentNbr);
      ((SelectedEntityEvent<PX.Objects.SO.SOShipment>) PXEntityEventBase<PX.Objects.SO.SOShipment>.Container<PX.Objects.SO.SOShipment.Events>.Select((Expression<Func<PX.Objects.SO.SOShipment.Events, PXEntityEvent<PX.Objects.SO.SOShipment.Events>>>) (e => e.ShipmentConfirmed))).FireOn((PXGraph) this.Base, ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current);
      ((PXAction) this.Base.Save).Press();
      transactionScope.Complete();
    }
    ((PXSelectBase) this.Base.Document).Cache.RestoreCopy((object) shipment, (object) ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current);
  }

  [PXInternalUseOnly]
  protected virtual void SetSuppressWorkflowOnConfirmShipment()
  {
    PXTransactionScope.SetSuppressWorkflow(true);
  }

  public virtual void PrepareShipmentForConfirmation(PX.Objects.SO.SOShipment shiporder)
  {
    ((PXGraph) this.Base).Clear();
    ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current = PXResultset<PX.Objects.SO.SOShipment>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Search<PX.Objects.SO.SOShipment.shipmentNbr>((object) shiporder.ShipmentNbr, Array.Empty<object>()));
    ParameterExpression parameterExpression;
    // ISSUE: method reference
    // ISSUE: field reference
    bool? nullable = WorkflowAction.HasWorkflowActionEnabled<SOShipmentEntry, PX.Objects.SO.SOShipment>(this.Base, Expression.Lambda<Func<SOShipmentEntry, PXAction<PX.Objects.SO.SOShipment>>>((Expression) Expression.Field((Expression) Expression.Call(g, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXGraph.GetExtension)), Array.Empty<Expression>()), FieldInfo.GetFieldFromHandle(__fieldref (ConfirmShipmentExtension.confirmShipmentAction))), parameterExpression), ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current);
    bool flag = false;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      throw new PXInvalidOperationException("The {0} action is not available in the {1} document at the moment. The document is being used by another process.", new object[2]
      {
        (object) ((PXAction) this.confirmShipmentAction).GetCaption(),
        (object) ((PXSelectBase) this.Base.Document).Cache.GetRowDescription((object) ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current)
      });
    this.ValidateShipment(shiporder);
  }

  public virtual void MarkConfirmed(PX.Objects.SO.SOShipment shipment)
  {
    shipment.Confirmed = new bool?(true);
    shipment.ConfirmedToVerify = new bool?(false);
    shipment.Status = "F";
  }

  public virtual bool ConfirmSingleLine(SOShipLine shipline, PX.Objects.IN.InventoryItem inventoryItem)
  {
    bool flag1 = false;
    object obj = ((PXGraph) this.Base).Caches[typeof (SOShipLine)].Locate((object) shipline);
    if (obj != null)
      shipline = (SOShipLine) obj;
    if (Math.Abs(shipline.BaseQty.Value) < 0.0000005M)
      this.Base.LineSplittingExt.RaiseRowDeleted(shipline);
    shipline.Confirmed = new bool?(true);
    if (shipline.LineType == "GI")
    {
      bool? stkItem = inventoryItem.StkItem;
      bool flag2 = false;
      if (stkItem.GetValueOrDefault() == flag2 & stkItem.HasValue && inventoryItem.KitItem.GetValueOrDefault())
      {
        if (PXResultset<SOShipLineSplit>.op_Implicit(PXSelectBase<SOShipLineSplit, PXSelectJoin<SOShipLineSplit, InnerJoin<PX.Objects.IN.InventoryItem, On2<SOShipLineSplit.FK.InventoryItem, And<PX.Objects.IN.InventoryItem.stkItem, Equal<True>>>>, Where<SOShipLineSplit.shipmentNbr, Equal<Current<SOShipLine.shipmentNbr>>, And<SOShipLineSplit.lineNbr, Equal<Current<SOShipLine.lineNbr>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
        {
          (object) shipline
        }, Array.Empty<object>())) == null)
        {
          shipline.RequireINUpdate = new bool?(false);
          goto label_10;
        }
      }
      shipline.RequireINUpdate = new bool?(true);
      flag1 = true;
    }
    else
      shipline.RequireINUpdate = new bool?(false);
label_10:
    GraphHelper.MarkUpdated(((PXGraph) this.Base).Caches[typeof (SOShipLine)], (object) shipline, true);
    ((PXGraph) this.Base).Caches[typeof (SOShipLine)].IsDirty = true;
    return flag1;
  }

  public virtual void UpdateOrigDocumentOnConfirmShipment(ConfirmShipmentArgs args)
  {
  }

  public virtual void GroupShipLinesForInvoicing(PX.Objects.SO.SOShipment ship)
  {
    foreach (IGrouping<\u003C\u003Ef__AnonymousType102<string, string, int?>, SOShipLine> source1 in GraphHelper.RowCast<SOShipLine>(((PXSelectBase) this.Base.Transactions).Cache.Updated).Where<SOShipLine>((Func<SOShipLine, bool>) (sl => sl.ShipmentNbr == ship.ShipmentNbr && sl.ShipmentType == ship.ShipmentType)).GroupBy(sl => new
    {
      OrigOrderType = sl.OrigOrderType,
      OrigOrderNbr = sl.OrigOrderNbr,
      OrigLineNbr = sl.OrigLineNbr
    }))
    {
      SOShipLine soShipLine = source1.First<SOShipLine>();
      IEnumerable<IGrouping<string, SOShipLine>> source2 = source1.GroupBy<SOShipLine, string>((Func<SOShipLine, string>) (sl => sl.UOM), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      if (source2.Count<IGrouping<string, SOShipLine>>() != 2)
      {
        EnumerableExtensions.ForEach<SOShipLine>((IEnumerable<SOShipLine>) source1, (Action<SOShipLine>) (sl => sl.InvoiceGroupNbr = new int?(1)));
      }
      else
      {
        PX.Objects.IN.InventoryItem item = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, soShipLine.InventoryID);
        IGrouping<string, SOShipLine> source3 = source2.FirstOrDefault<IGrouping<string, SOShipLine>>((Func<IGrouping<string, SOShipLine>, bool>) (g => string.Equals(g.Key, item?.BaseUnit, StringComparison.OrdinalIgnoreCase)));
        IGrouping<string, SOShipLine> grouping = source2.FirstOrDefault<IGrouping<string, SOShipLine>>((Func<IGrouping<string, SOShipLine>, bool>) (g => !string.Equals(g.Key, item?.BaseUnit, StringComparison.OrdinalIgnoreCase)));
        if (source3 == null || grouping == null)
        {
          EnumerableExtensions.ForEach<SOShipLine>((IEnumerable<SOShipLine>) source1, (Action<SOShipLine>) (sl => sl.InvoiceGroupNbr = new int?(1)));
        }
        else
        {
          EnumerableExtensions.ForEach<SOShipLine>((IEnumerable<SOShipLine>) grouping, (Action<SOShipLine>) (sl => sl.InvoiceGroupNbr = new int?(1)));
          Decimal? nullable = source3.Sum<SOShipLine>((Func<SOShipLine, Decimal?>) (sl => sl.ShippedQty));
          Decimal baseUomQtyConvertedToSalesUom = INUnitAttribute.ConvertFromBase(((PXSelectBase) this.Base.Transactions).Cache, soShipLine.InventoryID, grouping.Key, nullable.GetValueOrDefault(), INPrecision.NOROUND);
          EnumerableExtensions.ForEach<SOShipLine>((IEnumerable<SOShipLine>) source3, (Action<SOShipLine>) (sl => sl.InvoiceGroupNbr = new int?(baseUomQtyConvertedToSalesUom % 1M == 0M ? 1 : 2)));
        }
      }
    }
  }

  public virtual void ValidateShipment(PX.Objects.SO.SOShipment shiporder)
  {
    Decimal? nullable;
    if (((PXSelectBase<SOSetup>) this.Base.sosetup).Current.RequireShipmentTotal.GetValueOrDefault())
    {
      Decimal? shipmentQty = ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current.ShipmentQty;
      nullable = ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current.ControlQty;
      if (!(shipmentQty.GetValueOrDefault() == nullable.GetValueOrDefault() & shipmentQty.HasValue == nullable.HasValue))
        throw new PXException("Control Total is required for shipment confirmation.");
    }
    nullable = ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current.ShipmentQty;
    Decimal num = 0M;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      throw new PXException("Unable to confirm zero shipment {0}.", new object[1]
      {
        (object) ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current.ShipmentNbr
      });
    PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph) this.Base, ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current.ShipVia);
    if (carrier != null && carrier.IsExternal.GetValueOrDefault() && carrier.PackageRequired.GetValueOrDefault() && ((PXSelectBase<SOPackageDetailEx>) this.Base.Packages).SelectSingle(Array.Empty<object>()) == null)
      throw new PXException("At least one confirmed package is required to confirm this shipment.");
    foreach (PXResult<SOShipLine> pxResult in ((PXSelectBase<SOShipLine>) this.Base.Transactions).Select(Array.Empty<object>()))
      ConvertedInventoryItemAttribute.ValidateRow(((PXSelectBase) this.Base.Transactions).Cache, (object) PXResult<SOShipLine>.op_Implicit(pxResult));
  }
}
