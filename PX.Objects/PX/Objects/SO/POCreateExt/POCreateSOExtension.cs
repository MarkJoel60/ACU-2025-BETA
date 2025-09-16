// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.POCreateExt.POCreateSOExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.DAC;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PO;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.POCreateExt;

public class POCreateSOExtension : PXGraphExtension<POCreate>
{
  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase<POFixedDemand>) this.Base.FixedDemand).Join<LeftJoin<PX.Objects.SO.SOLineSplit, On<BqlOperand<PX.Objects.SO.SOLineSplit.planID, IBqlLong>.IsEqual<POFixedDemand.planID>>, LeftJoin<PX.Objects.SO.SOLine, On<PX.Objects.SO.SOLineSplit.FK.OrderLine>, LeftJoin<PX.Objects.SO.SOOrder, On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<CompositeKey<Field<PX.Objects.SO.SOLine.orderType>.IsRelatedTo<PX.Objects.SO.SOOrder.orderType>, Field<PX.Objects.SO.SOLine.orderNbr>.IsRelatedTo<PX.Objects.SO.SOOrder.orderNbr>>.WithTablesOf<PX.Objects.SO.SOOrder, PX.Objects.SO.SOLine>>, And<BqlOperand<PX.Objects.SO.SOOrder.noteID, IBqlGuid>.IsEqual<POFixedDemand.refNoteID>>>>.And<BqlOperand<PX.Objects.SO.SOOrder.status, IBqlString>.IsIn<SOOrderStatus.backOrder, SOOrderStatus.open, SOOrderStatus.shipping>>>, LeftJoin<DropShipLink, On<DropShipLink.FK.SOLine>>>>>>();
    ((PXSelectBase<POFixedDemand>) this.Base.FixedDemand).WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SOxPOCreateFilter.customerID>, IsNull>>>, Or<BqlOperand<PX.Objects.SO.SOOrder.customerID, IBqlInt>.IsEqual<BqlField<SOxPOCreateFilter.customerID, IBqlInt>.FromCurrent>>>>.Or<BqlOperand<PX.Objects.SO.SOOrder.orderNbr, IBqlString>.IsNull>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SOxPOCreateFilter.orderType>, IsNull>>>>.Or<BqlOperand<PX.Objects.SO.SOOrder.orderType, IBqlString>.IsEqual<BqlField<SOxPOCreateFilter.orderType, IBqlString>.FromCurrent>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SOxPOCreateFilter.orderNbr>, IsNull>>>>.Or<BqlOperand<PX.Objects.SO.SOOrder.orderNbr, IBqlString>.IsEqual<BqlField<SOxPOCreateFilter.orderNbr, IBqlString>.FromCurrent>>>>, And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.orderType, IsNull>>>, Or<BqlOperand<PX.Objects.SO.SOLineSplit.behavior, IBqlString>.IsNotEqual<SOBehavior.bL>>>>.Or<BqlOperand<PX.Objects.SO.SOLineSplit.pOCreateDate, IBqlDateTime>.IsLessEqual<BqlField<POCreate.POCreateFilter.purchDate, IBqlDateTime>.FromCurrent>>>>, And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POFixedDemand.supplyPlanID, IsNull>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.pONbr, IBqlString>.IsNull>>, And<BqlOperand<POFixedDemand.planType, IBqlString>.IsNotIn<INPlanConstants.plan6B, INPlanConstants.plan6E>>>, Or<BqlOperand<PX.Objects.SO.SOLineSplit.pONbr, IBqlString>.IsNotNull>>>.And<BqlOperand<POFixedDemand.planType, IBqlString>.IsIn<INPlanConstants.plan6B, INPlanConstants.plan6E>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POFixedDemand.planType, NotIn3<INPlanConstants.plan6D, INPlanConstants.plan6E>>>>>.Or<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.baseShippedQty, Equal<decimal0>>>>, And<BqlOperand<DropShipLink.sOLineNbr, IBqlInt>.IsNull>>>.And<BqlOperand<PX.Objects.SO.SOLine.isLegacyDropShip, IBqlBool>.IsEqual<False>>>>>>();
    PXUIFieldAttribute.SetDisplayName<PX.Objects.SO.SOLine.unitPrice>((PXCache) GraphHelper.Caches<PX.Objects.SO.SOLine>((PXGraph) this.Base), "Customer Price");
    PXUIFieldAttribute.SetDisplayName<PX.Objects.SO.SOLine.uOM>((PXCache) GraphHelper.Caches<PX.Objects.SO.SOLine>((PXGraph) this.Base), "Customer UOM");
    PXUIFieldAttribute.SetDisplayName<BAccountR.acctName>((PXCache) GraphHelper.Caches<BAccountR>((PXGraph) this.Base), "Customer Name");
    PXUIFieldAttribute.SetDisplayName<PX.Objects.SO.SOOrder.customerLocationID>((PXCache) GraphHelper.Caches<PX.Objects.SO.SOOrder>((PXGraph) this.Base), "Customer Location");
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<POFixedDemand, POFixedDemand.curyID, string> e)
  {
    if (!PXCacheEx.GetExtension<POFixedDemand, SOxPOFixedDemand>(((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<POFixedDemand, POFixedDemand.curyID, string>, POFixedDemand, string>) e).Row).IsSpecialOrder.GetValueOrDefault())
      return;
    PX.Objects.SO.SOOrder soOrder = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrder, PXSelect<PX.Objects.SO.SOOrder, Where<PX.Objects.SO.SOOrder.noteID, Equal<Required<PX.Objects.SO.SOOrder.noteID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<POFixedDemand, POFixedDemand.curyID, string>, POFixedDemand, string>) e).Row.RefNoteID
    }));
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<POFixedDemand, POFixedDemand.curyID, string>, POFixedDemand, string>) e).NewValue = soOrder.CuryID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<POFixedDemand, POFixedDemand.curyID, string>>) e).Cancel = true;
  }

  /// Overrides <see cref="M:PX.Objects.PO.POCreate.GetFixedDemandFieldScope" />
  [PXOverride]
  public IEnumerable<System.Type> GetFixedDemandFieldScope(
    Func<IEnumerable<System.Type>> base_GetFixedDemandFieldScope)
  {
    return base_GetFixedDemandFieldScope().Concat<System.Type>((IEnumerable<System.Type>) new System.Type[25]
    {
      typeof (PX.Objects.SO.SOOrder.orderType),
      typeof (PX.Objects.SO.SOOrder.orderNbr),
      typeof (PX.Objects.SO.SOOrder.customerID),
      typeof (PX.Objects.SO.SOOrder.customerLocationID),
      typeof (PX.Objects.SO.SOOrder.noteID),
      typeof (PX.Objects.SO.SOOrder.curyID),
      typeof (PX.Objects.SO.SOOrder.branchID),
      typeof (PX.Objects.SO.SOLine.orderType),
      typeof (PX.Objects.SO.SOLine.orderNbr),
      typeof (PX.Objects.SO.SOLine.lineNbr),
      typeof (PX.Objects.SO.SOLine.sortOrder),
      typeof (PX.Objects.SO.SOLine.unitPrice),
      typeof (PX.Objects.SO.SOLine.inventoryID),
      typeof (PX.Objects.SO.SOLine.uOM),
      typeof (PX.Objects.SO.SOLine.noteID),
      typeof (PX.Objects.SO.SOLine.projectID),
      typeof (PX.Objects.SO.SOLine.curyUnitCost),
      typeof (PX.Objects.SO.SOLine.isSpecialOrder),
      typeof (PX.Objects.SO.SOLine.requestDate),
      typeof (PX.Objects.SO.SOLine.pOSource),
      typeof (PX.Objects.SO.SOLine.lineSign),
      typeof (PX.Objects.SO.SOLine.openQty),
      typeof (PX.Objects.SO.SOLineSplit.uOM),
      typeof (PX.Objects.SO.SOLineSplit.qty),
      typeof (PX.Objects.SO.SOLineSplit.pOSiteID)
    });
  }

  [PXOverride]
  public void EnumerateAndPrepareFixedDemandRow(
    PXResult<POFixedDemand> record,
    Action<PXResult<POFixedDemand>> base_EnumerateAndPrepareFixedDemandRow)
  {
    POFixedDemand poFixedDemand1 = PXResult.Unwrap<POFixedDemand>((object) record);
    PX.Objects.SO.SOOrder soOrder = PXResult.Unwrap<PX.Objects.SO.SOOrder>((object) record);
    PX.Objects.SO.SOLine soLine = PXResult.Unwrap<PX.Objects.SO.SOLine>((object) record);
    PX.Objects.SO.SOLineSplit soLineSplit = PXResult.Unwrap<PX.Objects.SO.SOLineSplit>((object) record);
    SOxPOFixedDemand extension = PXCacheEx.GetExtension<POFixedDemand, SOxPOFixedDemand>(poFixedDemand1);
    extension.SalesBranchID = (int?) soOrder?.BranchID;
    extension.SalesCustomerID = (int?) soOrder?.CustomerID;
    extension.IsSpecialOrder = (bool?) soLine?.IsSpecialOrder;
    poFixedDemand1.DemandProjectID = (int?) soLine?.ProjectID;
    poFixedDemand1.NoteID = (Guid?) soLine?.NoteID;
    if (EnumerableExtensions.IsIn<string>(poFixedDemand1.PlanType, "6D", "6E"))
      poFixedDemand1.RequestedDate = (DateTime?) soLine?.RequestDate;
    if (soLine?.POSource == "D")
    {
      poFixedDemand1.DemandUOM = soLine.UOM;
      POFixedDemand poFixedDemand2 = poFixedDemand1;
      short? lineSign = soLine.LineSign;
      Decimal? nullable1 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
      Decimal? openQty = soLine.OpenQty;
      Decimal? nullable2 = nullable1.HasValue & openQty.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * openQty.GetValueOrDefault()) : new Decimal?();
      poFixedDemand2.PlanUnitQty = nullable2;
    }
    else if (soLine != null && soLine.IsSpecialOrder.GetValueOrDefault())
    {
      poFixedDemand1.DemandUOM = soLineSplit.UOM;
      poFixedDemand1.OrderQty = soLineSplit.Qty;
      poFixedDemand1.CuryID = soOrder.CuryID;
      poFixedDemand1.EffPrice = soLine.CuryUnitCost;
    }
    if (soLineSplit != null && soLineSplit.POSiteID.HasValue)
      poFixedDemand1.POSiteID = soLineSplit.POSiteID;
    base_EnumerateAndPrepareFixedDemandRow(record);
  }

  [PXOverride]
  public string GetSorterString(
    PXResult<POFixedDemand> record,
    Func<PXResult<POFixedDemand>, string> base_GetSorterString)
  {
    POFixedDemand poFixedDemand = PXResult.Unwrap<POFixedDemand>((object) record);
    if (poFixedDemand.PlanType == "90")
      return $"ZZ.{PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, poFixedDemand.InventoryID)?.InventoryCD ?? string.Empty}";
    PX.Objects.SO.SOLine soLine = PXResult.Unwrap<PX.Objects.SO.SOLine>((object) record);
    return soLine != null && soLine.OrderNbr != null ? $"{soLine.OrderType}.{soLine.OrderNbr}.{soLine.SortOrder.GetValueOrDefault():D7}" : base_GetSorterString(record);
  }

  /// Overrides <see cref="M:PX.Objects.PO.POCreate.TryRedirectToRelatedDocument(System.Nullable{System.Guid})" />
  [PXOverride]
  public void TryRedirectToRelatedDocument(
    Guid? refNoteID,
    Action<Guid?> base_TryRedirectToRelatedDocument)
  {
    PX.Objects.SO.SOOrder soOrder = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrder, PXViewOf<PX.Objects.SO.SOOrder>.BasedOn<SelectFromBase<PX.Objects.SO.SOOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.SO.SOOrder.noteID, IBqlGuid>.IsEqual<P.AsGuid>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) refNoteID
    }));
    if (soOrder != null)
    {
      SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
      ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current = soOrder;
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
    }
    base_TryRedirectToRelatedDocument(refNoteID);
  }

  /// Overrides <see cref="M:PX.Objects.PO.POCreate.RecalculateEffPrice(PX.Objects.PO.POFixedDemand,System.Func{System.Nullable{System.Int32},PX.Objects.AP.Vendor},System.Func{System.Nullable{System.Int32},PX.Objects.PO.POVendorInventory})" />
  [PXOverride]
  public void RecalculateEffPrice(
    POFixedDemand demand,
    Func<int?, PX.Objects.AP.Vendor> getVendor,
    Func<int?, POVendorInventory> getVendorInventory,
    Action<POFixedDemand, Func<int?, PX.Objects.AP.Vendor>, Func<int?, POVendorInventory>> base_RecalculateEffPrice)
  {
    if (PXCacheEx.GetExtension<POFixedDemand, SOxPOFixedDemand>(demand).IsSpecialOrder.GetValueOrDefault())
      base_RecalculateEffPrice(demand, (Func<int?, PX.Objects.AP.Vendor>) (viid => (PX.Objects.AP.Vendor) null), getVendorInventory);
    else
      base_RecalculateEffPrice(demand, getVendor, getVendorInventory);
  }

  public class SpecialOrderCurrencyError : PX.Objects.SO.GraphExtensions.SpecialOrderCurrencyError<POCreate>
  {
    protected virtual void _(PX.Data.Events.RowSelected<POFixedDemand> e)
    {
      POFixedDemand row = e.Row;
      if ((row != null ? (PXCacheEx.GetExtension<POFixedDemand, SOxPOFixedDemand>(row).IsSpecialOrder.GetValueOrDefault() ? 1 : 0) : 0) == 0 || PXUIFieldAttribute.GetErrorOnly<POFixedDemand.vendorID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POFixedDemand>>) e).Cache, (object) e.Row) != null)
        return;
      PXUIFieldAttribute.SetWarning<POFixedDemand.vendorID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POFixedDemand>>) e).Cache, (object) e.Row, this.GetSpecialOrderCurrencyError(e.Row.CuryID, e.Row.VendorID, ((PXSelectBase<POCreate.POCreateFilter>) this.Base.Filter).Current.BranchID, false));
    }
  }
}
