// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.SOOrderExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Common;
using PX.Common.Collection;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.CM;
using PX.Objects.Common.Discount;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.IN;
using PX.Objects.SO.Exceptions;
using PX.Objects.SO.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;

public class SOOrderExtension : 
  PXGraphExtension<
  #nullable disable
  SOShipLineSplitPlanDefaultValuesExtension, SOShipmentEntry>
{
  [PXViewName("Sales Order Shipment")]
  public FbqlSelect<SelectFromBase<PX.Objects.SO.SOOrderShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.SO.SOOrder>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.SO.SOOrder.orderType, 
  #nullable disable
  Equal<PX.Objects.SO.SOOrderShipment.orderType>>>>>.And<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOOrder.orderNbr, IBqlString>.IsEqual<
  #nullable disable
  PX.Objects.SO.SOOrderShipment.orderNbr>>>>, FbqlJoins.Left<PX.Objects.CM.CurrencyInfo>.On<BqlOperand<
  #nullable enable
  PX.Objects.CM.CurrencyInfo.curyInfoID, IBqlLong>.IsEqual<
  #nullable disable
  PX.Objects.SO.SOOrder.curyInfoID>>>, FbqlJoins.Left<SOAddress>.On<BqlOperand<
  #nullable enable
  SOAddress.addressID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.SO.SOOrder.billAddressID>>>, FbqlJoins.Left<SOContact>.On<BqlOperand<
  #nullable enable
  SOContact.contactID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.SO.SOOrder.billContactID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.SO.SOOrderShipment.shipmentNbr, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PX.Objects.SO.SOShipment.shipmentNbr, IBqlString>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  PX.Objects.SO.SOOrderShipment.shipmentType, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.SO.SOShipment.shipmentType, IBqlString>.FromCurrent>>>, 
  #nullable disable
  PX.Objects.SO.SOOrderShipment>.View OrderList;
  public FbqlSelect<SelectFromBase<PX.Objects.SO.SOOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.SO.SOOrder.orderType, 
  #nullable disable
  Equal<P.AsString.ASCII>>>>>.And<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOOrder.orderNbr, IBqlString>.IsEqual<
  #nullable disable
  P.AsString>>>, PX.Objects.SO.SOOrder>.View soorder;
  public PXSetup<PX.Objects.SO.SOOrderType>.Where<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOOrderType.orderType, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.SO.SOOrder.orderType, IBqlString>.AsOptional>> soordertype;
  public 
  #nullable disable
  EPApprovalAutomation<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.approved, PX.Objects.SO.SOOrder.rejected, PX.Objects.SO.SOOrder.hold, SOSetupApproval> Approval;
  public FbqlSelect<SelectFromBase<SOShipmentDiscountDetail, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  SOShipmentDiscountDetail.shipmentNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.SO.SOShipment.shipmentNbr, IBqlString>.FromCurrent>>, 
  #nullable disable
  SOShipmentDiscountDetail>.View DiscountDetails;
  public FbqlSelect<SelectFromBase<PX.Objects.SO.SOOrderShipment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.SO.SOOrderShipment.shipmentNbr, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PX.Objects.SO.SOShipment.shipmentNbr, IBqlString>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  PX.Objects.SO.SOOrderShipment.shipmentType, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.SO.SOShipment.shipmentType, IBqlString>.FromCurrent>>>, 
  #nullable disable
  PX.Objects.SO.SOOrderShipment>.View OrderListSimple;
  public FbqlSelect<SelectFromBase<SOLine2, TypeArrayOf<IFbqlJoin>.Empty>, SOLine2>.View soline;
  public FbqlSelect<SelectFromBase<SOLineSplit2, TypeArrayOf<IFbqlJoin>.Empty>, SOLineSplit2>.View solinesplit;
  public FbqlSelect<SelectFromBase<PX.Objects.SO.SOLine, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.SO.SOLine>.View dummy_soline;
  public PXFilter<AddSOFilter> addsofilter;
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<SOOrderSite, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOOrderShipment>.On<PX.Objects.SO.SOOrderShipment.FK.OrderSite>>>.Where<KeysRelation<CompositeKey<Field<PX.Objects.SO.SOOrderShipment.shipmentType>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentType>, Field<PX.Objects.SO.SOOrderShipment.shipmentNbr>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentNbr>>.WithTablesOf<PX.Objects.SO.SOShipment, PX.Objects.SO.SOOrderShipment>, PX.Objects.SO.SOShipment, PX.Objects.SO.SOOrderShipment>.SameAsCurrent>, SOOrderSite>.View OrderSite;
  public FbqlSelect<SelectFromBase<PX.Objects.SO.SOShipmentPlan, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOLineSplit>.On<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOLineSplit.planID, IBqlLong>.IsEqual<
  #nullable disable
  PX.Objects.SO.SOShipmentPlan.planID>>>, FbqlJoins.Inner<PX.Objects.SO.SOLine>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.SO.SOLine.orderType, 
  #nullable disable
  Equal<PX.Objects.SO.SOLineSplit.orderType>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.SO.SOLine.orderNbr, 
  #nullable disable
  Equal<PX.Objects.SO.SOLineSplit.orderNbr>>>>>.And<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOLine.lineNbr, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.SO.SOLineSplit.lineNbr>>>>>>.Order<By<BqlField<
  #nullable enable
  PX.Objects.SO.SOLine.sortOrder, IBqlInt>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.SO.SOLine.lineNbr, IBqlInt>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.SO.SOLineSplit.lineNbr, IBqlInt>.Asc>>, 
  #nullable disable
  PX.Objects.SO.SOShipmentPlan>.View soshipmentplan;
  public FbqlSelect<SelectFromBase<PX.Objects.SO.SOShipmentPlan, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOLineSplit>.On<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOLineSplit.planID, IBqlLong>.IsEqual<
  #nullable disable
  PX.Objects.SO.SOShipmentPlan.planID>>>, FbqlJoins.Left<PX.Objects.SO.SOOrderShipment>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.SO.SOOrderShipment.orderType, 
  #nullable disable
  Equal<PX.Objects.SO.SOShipmentPlan.orderType>>>>, And<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOOrderShipment.orderNbr, IBqlString>.IsEqual<
  #nullable disable
  PX.Objects.SO.SOShipmentPlan.orderNbr>>>, And<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOOrderShipment.operation, IBqlString>.IsEqual<
  #nullable disable
  PX.Objects.SO.SOLineSplit.operation>>>, And<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOOrderShipment.siteID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.SO.SOShipmentPlan.siteID>>>, And<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOOrderShipment.confirmed, IBqlBool>.IsEqual<
  #nullable disable
  False>>>>.And<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOOrderShipment.shipmentNbr, IBqlString>.IsNotEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.SO.SOShipment.shipmentNbr, IBqlString>.FromCurrent>>>>, 
  #nullable disable
  FbqlJoins.Left<PX.Objects.SO.SOLine>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.SO.SOLineSplit.orderType, 
  #nullable disable
  Equal<PX.Objects.SO.SOLine.orderType>>>>, And<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOLineSplit.orderNbr, IBqlString>.IsEqual<
  #nullable disable
  PX.Objects.SO.SOLine.orderNbr>>>>.And<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOLineSplit.lineNbr, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.SO.SOLine.lineNbr>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.SO.SOShipmentPlan.orderType, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  AddSOFilter.orderType, IBqlString>.FromCurrent>>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOShipmentPlan.orderNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  AddSOFilter.orderNbr, IBqlString>.FromCurrent>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.SO.SOLine.lineNbr, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  AddSOFilter.orderLineNbr, IBqlInt>.FromCurrent>>>>>.Or<
  #nullable disable
  BqlOperand<Current<
  #nullable enable
  AddSOFilter.orderLineNbr>, IBqlInt>.IsNull>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOShipmentPlan.siteID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.SO.SOShipment.siteID, IBqlInt>.FromCurrent>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOOrderShipment.shipmentNbr, IBqlString>.IsNull>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOLineSplit.operation, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  AddSOFilter.operation, IBqlString>.FromCurrent>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  PX.Objects.SO.SOShipment.destinationSiteID>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOShipmentPlan.destinationSiteID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.SO.SOShipment.destinationSiteID, IBqlInt>.FromCurrent>>>>, 
  #nullable disable
  And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.SO.SOShipmentPlan.inclQtySOShipping, 
  #nullable disable
  Equal<short1>>>>, Or<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOShipmentPlan.inclQtySOShipped, IBqlShort>.IsEqual<
  #nullable disable
  short1>>>, Or<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOShipmentPlan.requireAllocation, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, Or<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOLineSplit.operation, IBqlString>.IsEqual<
  #nullable disable
  SOOperation.receipt>>>>.Or<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOLineSplit.lineType, IBqlString>.IsEqual<
  #nullable disable
  SOLineType.nonInventory>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  PX.Objects.SO.SOShipment.isManualPackage>, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOShipmentPlan.isManualPackage, IBqlBool>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.SO.SOShipment.isManualPackage, IBqlBool>.FromCurrent>>>>, 
  #nullable disable
  PX.Objects.SO.SOShipmentPlan>.View soshipmentplanSelect;
  public FbqlSelect<SelectFromBase<PX.Objects.SO.SOShipmentPlan, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOLineSplit>.On<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOLineSplit.planID, IBqlLong>.IsEqual<
  #nullable disable
  PX.Objects.SO.SOShipmentPlan.planID>>>, FbqlJoins.Inner<PX.Objects.SO.SOLine>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.SO.SOLine.orderType, 
  #nullable disable
  Equal<PX.Objects.SO.SOLineSplit.orderType>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.SO.SOLine.orderNbr, 
  #nullable disable
  Equal<PX.Objects.SO.SOLineSplit.orderNbr>>>>>.And<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOLine.lineNbr, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.SO.SOLineSplit.lineNbr>>>>>, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<BqlOperand<
  #nullable enable
  PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.SO.SOShipmentPlan.inventoryID>>>, FbqlJoins.Left<INLotSerClass>.On<PX.Objects.IN.InventoryItem.FK.LotSerialClass>>, FbqlJoins.Left<PX.Objects.IN.INSite>.On<PX.Objects.SO.SOLine.FK.Site>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.SO.SOShipmentPlan.siteID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  SOOrderFilter.siteID, IBqlInt>.AsOptional>>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOShipmentPlan.planDate, IBqlDateTime>.IsLessEqual<
  #nullable disable
  BqlField<
  #nullable enable
  SOOrderFilter.endDate, IBqlDateTime>.AsOptional>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOShipmentPlan.orderType, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  AddSOFilter.orderType, IBqlString>.AsOptional>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOShipmentPlan.orderNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  AddSOFilter.orderNbr, IBqlString>.AsOptional>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.SO.SOLine.lineNbr, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  AddSOFilter.orderLineNbr, IBqlInt>.AsOptional>>>>>.Or<
  #nullable disable
  BqlOperand<Optional<
  #nullable enable
  AddSOFilter.orderLineNbr>, IBqlInt>.IsNull>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  PX.Objects.SO.SOLine.operation, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  AddSOFilter.operation, IBqlString>.AsOptional>>>>.And<
  #nullable disable
  NotExists<SelectFromBase<SOShipLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  SOShipLine.origOrderType, 
  #nullable disable
  Equal<PX.Objects.SO.SOLineSplit.orderType>>>>, And<BqlOperand<
  #nullable enable
  SOShipLine.origOrderNbr, IBqlString>.IsEqual<
  #nullable disable
  PX.Objects.SO.SOLineSplit.orderNbr>>>, And<BqlOperand<
  #nullable enable
  SOShipLine.origLineNbr, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.SO.SOLineSplit.lineNbr>>>, And<BqlOperand<
  #nullable enable
  SOShipLine.origSplitLineNbr, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.SO.SOLineSplit.splitLineNbr>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  SOShipLine.shipmentNbr, 
  #nullable disable
  IsNull>>>>.Or<BqlOperand<
  #nullable enable
  SOShipLine.shipmentNbr, IBqlString>.IsNotEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.SO.SOShipment.shipmentNbr, IBqlString>.FromCurrent>>>>>>>.Order<
  #nullable disable
  By<BqlField<
  #nullable enable
  PX.Objects.SO.SOLineSplit.orderType, IBqlString>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.SO.SOLineSplit.orderNbr, IBqlString>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.SO.SOLineSplit.lineNbr, IBqlInt>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.SO.SOLineSplit.splitLineNbr, IBqlInt>.Asc>>, 
  #nullable disable
  PX.Objects.SO.SOShipmentPlan>.View ShipmentScheduleSelect;
  public PXAction<PX.Objects.SO.SOShipment> selectSO;
  public PXAction<PX.Objects.SO.SOShipment> addSO;
  public PXAction<PX.Objects.SO.SOShipment> addSOCancel;
  public PXWorkflowEventHandler<PX.Objects.SO.SOShipment, PX.Objects.SO.SOOrderShipment, SOInvoice> OnInvoiceLinked;
  public PXWorkflowEventHandler<PX.Objects.SO.SOShipment, PX.Objects.SO.SOOrderShipment, SOInvoice> OnInvoiceUnlinked;
  private bool isSkipAdjustFreeItemLines;

  public virtual IEnumerable SOShipmentPlan()
  {
    SOOrderExtension soOrderExtension = this;
    string freightAmountSource1 = ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) soOrderExtension).Base.Document).Current?.FreightAmountSource;
    AddSOFilter current = ((PXSelectBase<AddSOFilter>) soOrderExtension.addsofilter).Current;
    if (current != null)
    {
      string freightAmountSource2 = current.FreightAmountSource;
    }
    string freightAmountSource3 = ((PXSelectBase<AddSOFilter>) soOrderExtension.addsofilter).Current?.FreightAmountSource;
    if (EnumerableExtensions.IsIn<string>(freightAmountSource1, (string) null, freightAmountSource3))
    {
      // ISSUE: reference to a compiler-generated method
      Lazy<SOOrderExtension.OrigSOLineSplitSet> shipmentSOLineSplits = new Lazy<SOOrderExtension.OrigSOLineSplitSet>(new Func<SOOrderExtension.OrigSOLineSplitSet>(soOrderExtension.\u003CSOShipmentPlan\u003Eb__13_0));
      foreach (PXResult<PX.Objects.SO.SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOLine> pxResult in ((PXSelectBase<PX.Objects.SO.SOShipmentPlan>) soOrderExtension.soshipmentplanSelect).Select(Array.Empty<object>()))
      {
        PX.Objects.SO.SOLineSplit sls = PXResult<PX.Objects.SO.SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOLine>.op_Implicit(pxResult);
        if (!shipmentSOLineSplits.Value.Contains(sls))
          yield return (object) new PXResult<PX.Objects.SO.SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLine>(PXResult<PX.Objects.SO.SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOLine>.op_Implicit(pxResult), sls, PXResult<PX.Objects.SO.SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOLine>.op_Implicit(pxResult));
      }
    }
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable SelectSO(PXAdapter adapter)
  {
    if (((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Cache.AllowDelete && ((PXSelectBase<AddSOFilter>) this.addsofilter).AskExt() == 1)
      this.AddSO(adapter);
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddSO(PXAdapter adapter)
  {
    PX.Objects.SO.SOOrder soOrder = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrder, PXSelect<PX.Objects.SO.SOOrder, Where<PX.Objects.SO.SOOrder.orderType, Equal<Optional<AddSOFilter.orderType>>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<Optional<AddSOFilter.orderNbr>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, Array.Empty<object>()));
    int num;
    if (soOrder != null)
    {
      AddSOFilter current = ((PXSelectBase<AddSOFilter>) this.addsofilter).Current;
      num = (current != null ? (current.AddAllLines.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? 1 : (this.AnySelected<PX.Objects.SO.SOShipmentPlan.selected>(((PXSelectBase) this.soshipmentplan).Cache) ? 1 : 0);
    }
    else
      num = 0;
    if (num != 0)
    {
      try
      {
        using (((PXGraphExtension<SOShipmentEntry>) this).Base.LineSplittingExt.ForceUnattendedModeScope(true))
          ((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base).GetExtension<CreateShipmentExtension>().CreateShipment(new CreateShipmentArgs()
          {
            MassProcess = false,
            Order = soOrder,
            OrderLineNbr = ((PXSelectBase<AddSOFilter>) this.addsofilter).Current.OrderLineNbr,
            SiteID = ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current.SiteID,
            ShipDate = ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current.ShipDate,
            UseOptimalShipDate = new bool?(false),
            Operation = ((PXSelectBase<AddSOFilter>) this.addsofilter).Current.Operation,
            ShipmentList = ((PXSelectBase<AddSOFilter>) this.addsofilter).Current.AddAllLines.GetValueOrDefault() ? new DocumentList<PX.Objects.SO.SOShipment>((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base) : (DocumentList<PX.Objects.SO.SOShipment>) null
          });
      }
      finally
      {
        ((PXSelectBase<AddSOFilter>) this.addsofilter).Current.AddAllLines = new bool?(false);
      }
    }
    if (((PXSelectBase<AddSOFilter>) this.addsofilter).Current != null)
    {
      if (!((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base).IsImport)
      {
        try
        {
          ((PXSelectBase) this.addsofilter).Cache.SetDefaultExt<AddSOFilter.orderType>((object) ((PXSelectBase<AddSOFilter>) this.addsofilter).Current);
          ((PXSelectBase<AddSOFilter>) this.addsofilter).Current.OrderNbr = (string) null;
        }
        catch
        {
        }
      }
    }
    ((PXSelectBase) this.soshipmentplan).Cache.Clear();
    ((PXSelectBase) this.soshipmentplan).View.Clear();
    ((PXSelectBase) this.soshipmentplan).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase) this.soshipmentplanSelect).View.Clear();
    ((PXSelectBase) this.ShipmentScheduleSelect).View.Clear();
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddSOCancel(PXAdapter adapter)
  {
    ((PXSelectBase) this.addsofilter).Cache.SetDefaultExt<AddSOFilter.orderType>((object) ((PXSelectBase<AddSOFilter>) this.addsofilter).Current);
    ((PXSelectBase<AddSOFilter>) this.addsofilter).Current.OrderNbr = (string) null;
    ((PXSelectBase) this.soshipmentplan).Cache.Clear();
    ((PXSelectBase) this.soshipmentplan).View.Clear();
    return adapter.Get();
  }

  [PXMergeAttributes]
  [PXParent(typeof (SOShipLine.FK.OrderShipment), LeaveChildren = true)]
  [PXFormula(null, typeof (CountCalc<PX.Objects.SO.SOOrderShipment.lineCntr>))]
  protected virtual void _(PX.Data.Events.CacheAttached<SOShipLine.shipmentNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(null, typeof (SumCalc<PX.Objects.SO.SOOrderShipment.shipmentQty>))]
  protected virtual void _(PX.Data.Events.CacheAttached<SOShipLine.shippedQty> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(null, typeof (SumCalc<PX.Objects.SO.SOOrderShipment.lineTotal>))]
  protected virtual void _(PX.Data.Events.CacheAttached<SOShipLine.lineAmt> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(null, typeof (SumCalc<PX.Objects.SO.SOOrderShipment.shipmentWeight>))]
  protected virtual void _(PX.Data.Events.CacheAttached<SOShipLine.extWeight> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(null, typeof (SumCalc<PX.Objects.SO.SOOrderShipment.shipmentVolume>))]
  protected virtual void _(PX.Data.Events.CacheAttached<SOShipLine.extVolume> e)
  {
  }

  [PXMergeAttributes]
  [PXParent(typeof (SOShipLine.FK.Order))]
  protected virtual void _(PX.Data.Events.CacheAttached<SOShipLine.origOrderNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXParent(typeof (Select<SOLine2, Where<SOLine2.orderType, Equal<Current<SOShipLine.origOrderType>>, And<SOLine2.orderNbr, Equal<Current<SOShipLine.origOrderNbr>>, And<SOLine2.lineNbr, Equal<Current<SOShipLine.origLineNbr>>>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<SOShipLine.origLineNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXParent(typeof (Select<SOLineSplit2, Where<SOLineSplit2.orderType, Equal<Current<SOShipLine.origOrderType>>, And<SOLineSplit2.orderNbr, Equal<Current<SOShipLine.origOrderNbr>>, And<SOLineSplit2.lineNbr, Equal<Current<SOShipLine.origLineNbr>>, And<SOLineSplit2.splitLineNbr, Equal<Current<SOShipLine.origSplitLineNbr>>>>>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOShipLine.origSplitLineNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXUnboundFormula(typeof (BqlOperand<SOShipLine.baseShippedQty, IBqlDecimal>.Multiply<SOShipLine.sOLineSign>), typeof (SumCalc<SOLine2.baseShippedQty>), ValidateAggregateCalculation = true)]
  [PXFormula(null, typeof (SumCalc<SOLineSplit2.baseShippedQty>), ValidateAggregateCalculation = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<SOShipLine.baseShippedQty> e)
  {
  }

  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXMergeAttributes]
  [PXDBDefault(typeof (PX.Objects.SO.SOShipment.siteID))]
  protected void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOOrderShipment.siteID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBDefault(typeof (PX.Objects.SO.SOShipment.shipAddressID), DefaultForInsert = true, DefaultForUpdate = true)]
  protected void _(
    PX.Data.Events.CacheAttached<PX.Objects.SO.SOOrderShipment.shipAddressID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBDefault(typeof (PX.Objects.SO.SOShipment.shipContactID), DefaultForInsert = true, DefaultForUpdate = true)]
  protected void _(
    PX.Data.Events.CacheAttached<PX.Objects.SO.SOOrderShipment.shipContactID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBDefault(typeof (PX.Objects.SO.SOShipment.shipmentNbr), DefaultForInsert = true, DefaultForUpdate = true)]
  protected void _(
    PX.Data.Events.CacheAttached<PX.Objects.SO.SOOrderShipment.shipmentNbr> e)
  {
  }

  [PXDBBool]
  [PXFormula(typeof (Switch<Case<Where<PX.Objects.SO.SOLine.requireShipping, Equal<True>, And<PX.Objects.SO.SOLine.lineType, NotEqual<SOLineType.miscCharge>, And<PX.Objects.SO.SOLine.completed, NotEqual<True>>>>, True>, False>))]
  [DirtyFormula(typeof (Switch<Case<Where<PX.Objects.SO.SOLine.openLine, Equal<True>, And<Where<PX.Objects.SO.SOLine.isFree, NotEqual<True>, Or<PX.Objects.SO.SOLine.manualDisc, Equal<True>>>>>, int1>, int0>), typeof (SumCalc<PX.Objects.SO.SOOrder.openLineCntr>), true)]
  [PXUIField(DisplayName = "Open Line", Enabled = false)]
  public virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOLine.openLine> e)
  {
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.SO.SOLine.taxCategoryID))]
  public virtual void _(PX.Data.Events.CacheAttached<SOLine2.taxCategoryID> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<AddSOFilter> e)
  {
    PX.Objects.SO.SOShipment current = ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current;
    PXUIFieldAttribute.SetEnabled<AddSOFilter.operation>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<AddSOFilter>>) e).Cache, (object) e.Row, current?.Operation == null);
    AddSOFilter row = e.Row;
    if (row == null)
      return;
    PXSetPropertyException propertyException1;
    if (current != null && current.FreightAmountSource != null && row.FreightAmountSource != null && !(current.FreightAmountSource == row.FreightAmountSource))
      propertyException1 = new PXSetPropertyException((IBqlTable) row, "Cannot add the sales order because it uses shipping terms with the Invoice Freight Price Based On set to {0}.", (PXErrorLevel) 2, new object[1]
      {
        ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<AddSOFilter>>) e).Cache.GetValueExt<AddSOFilter.freightAmountSource>((object) row)
      });
    else
      propertyException1 = (PXSetPropertyException) null;
    PXSetPropertyException propertyException2 = propertyException1;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<AddSOFilter>>) e).Cache.RaiseExceptionHandling<AddSOFilter.orderNbr>((object) e.Row, (object) row.FreightAmountSource, (Exception) propertyException2);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.SO.SOShipment> e)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOShipment>>) e).Cache.ObjectsEqual<PX.Objects.SO.SOShipment.shipDate>((object) e.Row, (object) e.OldRow))
      return;
    this.SyncShipDateWithLinks(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOShipment> e)
  {
    if (e.Row == null)
      return;
    ((PXAction) this.selectSO).SetEnabled(e.Row.SiteID.HasValue && ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOShipment>>) e).Cache.AllowDelete);
    PXCacheEx.Adjust<PXSelectorAttribute>(((PXSelectBase) this.soline).Cache, (object) null).For<SOShipLine.operation>((Action<PXSelectorAttribute>) (a => a.ValidateValue = true));
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<SOShipLine, SOShipLine.orderUOM> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<SOShipLine, SOShipLine.orderUOM>, SOShipLine, object>) e).NewValue = (object) PXParentAttribute.SelectParent<SOLine2>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<SOShipLine, SOShipLine.orderUOM>>) e).Cache, (object) e.Row)?.UOM;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<SOShipLine, SOShipLine.unitCost> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<SOShipLine, SOShipLine.unitCost>, SOShipLine, object>) e).NewValue = (object) (Decimal?) PXParentAttribute.SelectParent<SOLine2>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<SOShipLine, SOShipLine.unitCost>>) e).Cache, (object) e.Row)?.UnitCost;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<SOShipLine, SOShipLine.unitPrice> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<SOShipLine, SOShipLine.unitPrice>, SOShipLine, object>) e).NewValue = (object) (Decimal?) PXParentAttribute.SelectParent<SOLine2>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<SOShipLine, SOShipLine.unitPrice>>) e).Cache, (object) e.Row)?.ActualUnitPrice;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<SOShipLine, SOShipLine.discPct> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<SOShipLine, SOShipLine.discPct>, SOShipLine, object>) e).NewValue = (object) (Decimal?) PXParentAttribute.SelectParent<SOLine2>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<SOShipLine, SOShipLine.discPct>>) e).Cache, (object) e.Row)?.DiscPct;
  }

  protected virtual void _(PX.Data.Events.RowSelected<SOShipLine> e)
  {
    if (e.Row == null)
      return;
    ((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.splits).Cache.AllowInsert = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOShipLine>>) e).Cache.AllowUpdate && this.SyncLineWithOrder(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowUpdating<SOShipLine> e)
  {
    if (this.SyncLineWithOrder(e.Row))
      return;
    e.Cancel = true;
    PXSetPropertyException<SOShipLine.shippedQty> propertyException = new PXSetPropertyException<SOShipLine.shippedQty>("The quantity in this line cannot be changed because the warehouse in the line differs from the warehouse in the related sales order line.", (PXErrorLevel) 2);
    if (((PX.Data.Events.Event<PXRowUpdatingEventArgs, PX.Data.Events.RowUpdating<SOShipLine>>) e).Cache.RaiseExceptionHandling<SOShipLine.shippedQty>((object) e.NewRow, (object) e.Row.ShippedQty, (Exception) propertyException))
      throw propertyException;
  }

  protected virtual void _(PX.Data.Events.RowUpdated<SOShipLine> e)
  {
    SOShipLine row = e.Row;
    if (row == null || row.IsFree.GetValueOrDefault() || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SOShipLine>>) e).Cache.ObjectsEqual<SOShipLine.shippedQty>((object) e.Row, (object) e.OldRow))
      return;
    foreach (PXResult<SOShipmentDiscountDetail> pxResult in ((PXSelectBase<SOShipmentDiscountDetail>) new PXSelect<SOShipmentDiscountDetail, Where<SOShipmentDiscountDetail.orderType, Equal<Required<SOShipmentDiscountDetail.orderType>>, And<SOShipmentDiscountDetail.orderNbr, Equal<Required<SOShipmentDiscountDetail.orderNbr>>, And<SOShipmentDiscountDetail.shipmentNbr, Equal<Required<SOShipmentDiscountDetail.shipmentNbr>>, And<SOShipmentDiscountDetail.type, Equal<DiscountType.LineDiscount>>>>>>((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base)).Select(new object[3]
    {
      (object) row.OrigOrderType,
      (object) row.OrigOrderNbr,
      (object) row.ShipmentNbr
    }))
    {
      SOShipmentDiscountDetail traceToDelete = PXResult<SOShipmentDiscountDetail>.op_Implicit(pxResult);
      this._discountEngine.DeleteDiscountDetail(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SOShipLine>>) e).Cache, (PXSelectBase<SOShipmentDiscountDetail>) this.DiscountDetails, traceToDelete);
    }
    PX.Objects.SO.SOOrder order = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) this.soorder).Select(new object[2]
    {
      (object) row.OrigOrderType,
      (object) row.OrigOrderNbr
    }));
    if (order == null || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SOShipLine>>) e).Cache.Graph.UnattendedMode)
      return;
    this.AllocateGroupFreeItems(order);
    this.AdjustFreeItemLines();
  }

  protected virtual void _(PX.Data.Events.RowDeleted<SOShipLine> e)
  {
    SOShipLine row = e.Row;
    if (row == null || ((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Cache.GetStatus((object) ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current) == 3)
      return;
    SOShipLine soShipLine = PXResultset<SOShipLine>.op_Implicit(PXSelectBase<SOShipLine, PXSelect<SOShipLine, Where<SOShipLine.shipmentType, Equal<Current<SOShipLine.shipmentType>>, And<SOShipLine.shipmentNbr, Equal<Current<SOShipLine.shipmentNbr>>, And<SOShipLine.origOrderType, Equal<Current<SOShipLine.origOrderType>>, And<SOShipLine.origOrderNbr, Equal<Current<SOShipLine.origOrderNbr>>>>>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    if (soShipLine == null)
      ((PXSelectBase<PX.Objects.SO.SOOrderShipment>) this.OrderList).Delete(PXResultset<PX.Objects.SO.SOOrderShipment>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrderShipment, PXSelect<PX.Objects.SO.SOOrderShipment, Where<PX.Objects.SO.SOOrderShipment.shipmentType, Equal<Current<SOShipLine.shipmentType>>, And<PX.Objects.SO.SOOrderShipment.shipmentNbr, Equal<Current<SOShipLine.shipmentNbr>>, And<PX.Objects.SO.SOOrderShipment.orderType, Equal<Current<SOShipLine.origOrderType>>, And<PX.Objects.SO.SOOrderShipment.orderNbr, Equal<Current<SOShipLine.origOrderNbr>>>>>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, new object[1]
      {
        (object) row
      }, Array.Empty<object>())));
    PX.Objects.SO.SOOrder order = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) this.soorder).Select(new object[2]
    {
      (object) row.OrigOrderType,
      (object) row.OrigOrderNbr
    }));
    if (order == null)
      return;
    this.AllocateGroupFreeItems(order);
    this.AdjustFreeItemLines();
    row.KeepManualFreight = new bool?(false);
    if (soShipLine != null)
      return;
    Guid[] fileNotes = PXNoteAttribute.GetFileNotes(((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base).Caches[typeof (PX.Objects.SO.SOOrder)], (object) order);
    foreach (NoteDoc noteDoc in ((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base).Caches[typeof (NoteDoc)].Cached)
    {
      Guid[] source = fileNotes;
      Guid? nullable = noteDoc.FileID;
      Guid guid = nullable ?? Guid.Empty;
      if (((IEnumerable<Guid>) source).Contains<Guid>(guid))
      {
        nullable = noteDoc.NoteID;
        Guid? noteId = ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current.NoteID;
        if ((nullable.HasValue == noteId.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() == noteId.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
          ((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base).Caches[typeof (NoteDoc)].Delete((object) noteDoc);
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.SO.SOOrderShipment> e)
  {
    this.UpdateShipmentCntr(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<PX.Objects.SO.SOOrderShipment>>) e).Cache, e.Row, new short?((short) 1));
    this.UpdateManualFreightCost(((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current, e.Row, new Decimal?(0M), e.Row.ShipmentQty, true);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrderShipment> e)
  {
    if (e.Row == e.OldRow)
      return;
    this.UpdateShipmentCntr(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrderShipment>>) e).Cache, e.OldRow, new short?((short) -1));
    this.UpdateShipmentCntr(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrderShipment>>) e).Cache, e.Row, new short?((short) 1));
    Decimal? shipmentQty1 = e.Row.ShipmentQty;
    Decimal? shipmentQty2 = e.OldRow.ShipmentQty;
    Decimal? nullable = shipmentQty1.HasValue & shipmentQty2.HasValue ? new Decimal?(shipmentQty1.GetValueOrDefault() - shipmentQty2.GetValueOrDefault()) : new Decimal?();
    Decimal num = 0M;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      return;
    this.UpdateManualFreightCost(((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current, e.Row, e.OldRow.ShipmentQty, e.Row.ShipmentQty);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.SO.SOOrderShipment> e)
  {
    this.UpdateShipmentCntr(((PX.Data.Events.Event<PXRowDeletedEventArgs, PX.Data.Events.RowDeleted<PX.Objects.SO.SOOrderShipment>>) e).Cache, e.Row, new short?((short) -1));
    ((SelectedEntityEvent<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOShipment>) PXEntityEventBase<PX.Objects.SO.SOOrderShipment>.Container<PX.Objects.SO.SOOrderShipment.Events>.Select<PX.Objects.SO.SOShipment>((Expression<Func<PX.Objects.SO.SOOrderShipment.Events, PXEntityEvent<PX.Objects.SO.SOOrderShipment.Events, PX.Objects.SO.SOShipment>>>) (ev => ev.ShipmentUnlinked))).FireOn((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, e.Row, ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current);
    this.UpdateManualFreightCost(((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current, e.Row, e.Row.ShipmentQty, new Decimal?(0M));
    this.RestoreCustomerOrderNbr();
    this.ResetManualPackageFlag();
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrderShipment.shipmentNbr> e)
  {
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrderShipment.shipmentNbr>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOOrderShipment> e)
  {
    PXUIFieldAttribute.SetEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrderShipment>>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOOrderShipment.selected>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrderShipment>>) e).Cache, (object) e.Row, true);
  }

  public virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOShipmentPlan> e)
  {
    if (e.Row == null)
      return;
    DateTime? shipDate = ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current.ShipDate;
    DateTime? planDate = e.Row.PlanDate;
    if ((shipDate.HasValue & planDate.HasValue ? (shipDate.GetValueOrDefault() < planDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    PXUIFieldAttribute.SetWarning<PX.Objects.SO.SOShipmentPlan.planDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOShipmentPlan>>) e).Cache, (object) e.Row, "Scheduled Shipment Date greater than Shipment Date");
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.SO.SOOrder> e)
  {
    PX.Objects.SO.SOOrder order = e.Row;
    if (e.Operation == 1)
    {
      int? shipmentCntr1 = order.ShipmentCntr;
      int num1 = 0;
      if (!(shipmentCntr1.GetValueOrDefault() < num1 & shipmentCntr1.HasValue))
      {
        int? openShipmentCntr1 = order.OpenShipmentCntr;
        int num2 = 0;
        if (!(openShipmentCntr1.GetValueOrDefault() < num2 & openShipmentCntr1.HasValue))
        {
          int? shipmentCntr2 = order.ShipmentCntr;
          int num3 = 0;
          if (!(shipmentCntr2.GetValueOrDefault() == num3 & shipmentCntr2.HasValue))
          {
            int? openShipmentCntr2 = order.OpenShipmentCntr;
            int num4 = 0;
            if (!(openShipmentCntr2.GetValueOrDefault() == num4 & openShipmentCntr2.HasValue))
              goto label_6;
          }
          if (((IEnumerable<PX.Objects.SO.SOOrderShipment>) ((PXSelectBase) this.OrderList).Cache.Inserted).Any<PX.Objects.SO.SOOrderShipment>((Func<PX.Objects.SO.SOOrderShipment, bool>) (a => a.OrderType == order.OrderType && a.OrderNbr == order.OrderNbr)))
            goto label_7;
label_6:
          int? shipmentCntr3 = order.ShipmentCntr;
          int num5 = 0;
          if (!(shipmentCntr3.GetValueOrDefault() == num5 & shipmentCntr3.HasValue) || !((IEnumerable<PX.Objects.SO.SOOrderShipment>) ((PXSelectBase) this.OrderList).Cache.Updated).Any<PX.Objects.SO.SOOrderShipment>((Func<PX.Objects.SO.SOOrderShipment, bool>) (a => a.OrderType == order.OrderType && a.OrderNbr == order.OrderNbr)))
            return;
        }
      }
label_7:
      throw new InvalidShipmentCountersException();
    }
  }

  /// Overrides <see cref="M:PX.Objects.SO.SOShipmentEntry.ValidateShipComplete(PX.Objects.SO.SOShipment)" />
  /// .
  [PXOverride]
  public void ValidateShipComplete(
    PX.Objects.SO.SOShipment shipment,
    Action<PX.Objects.SO.SOShipment> base_ValidateShipComplete)
  {
    base_ValidateShipComplete(shipment);
    string orderType = (string) null;
    string orderNbr = (string) null;
    this.ValidateLineShipComplete(shipment, ref orderType, ref orderNbr);
    this.ValidateOrderShipComplete(shipment, ref orderType, ref orderNbr);
    if (orderType != null)
      throw new PXException("Order {0} {1} cannot be shipped in full. Check Trace for more details.", new object[2]
      {
        (object) orderType,
        (object) orderNbr
      });
  }

  /// Overrides <see cref="M:PX.Objects.SO.SOShipmentEntry.CreateOrigDocumentGraph(System.String)" />
  /// .
  [PXOverride]
  public PXGraph CreateOrigDocumentGraph(
    string origDocumentType,
    Func<string, PXGraph> base_CreateOrigDocumentGraph)
  {
    return (PXGraph) PXGraph.CreateInstance<SOOrderEntry>();
  }

  /// Overrides <see cref="M:PX.Objects.SO.SOShipmentEntry.InitOrigDocumentGraph(PX.Data.PXGraph)" />
  /// .
  [PXOverride]
  public void InitOrigDocumentGraph(PXGraph graph, Action<PXGraph> base_InitOrigDocumentGraph)
  {
    base_InitOrigDocumentGraph(graph);
    PXCache cach1 = graph.Caches[typeof (SOShipLineSplit)];
    PXCache cach2 = graph.Caches[typeof (INTranSplit)];
  }

  /// Overrides <see cref="M:PX.Objects.SO.SOShipmentEntry.UpdateOrigDocumentOnCorrectShipment(PX.Objects.SO.Models.CorrectShipmentArgs)" />
  /// .
  [PXOverride]
  public void UpdateOrigDocumentOnCorrectShipment(
    CorrectShipmentArgs args,
    Action<CorrectShipmentArgs> base_UpdateOrigDocumentOnCorrectShipment)
  {
    base_UpdateOrigDocumentOnCorrectShipment(args);
    SOOrderEntry soOrderEntry = args.OrigDocumentGraph as SOOrderEntry;
    if (soOrderEntry == null)
      return;
    using (soOrderEntry.LineSplittingExt.SuppressedModeScope(true))
    {
      foreach (PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact> pxResult1 in ((PXSelectBase<PX.Objects.SO.SOOrderShipment>) this.OrderList).Select(Array.Empty<object>()))
      {
        PX.Objects.SO.SOOrderShipment order = PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact>.op_Implicit(pxResult1);
        PX.Objects.SO.SOOrder soOrder = PXResult<PX.Objects.SO.SOOrderShipment, PX.Objects.SO.SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact>.op_Implicit(pxResult1);
        PX.Objects.SO.SOOrderType soOrderType = PXResultset<PX.Objects.SO.SOOrderType>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrderType>) this.soordertype).Select(new object[1]
        {
          (object) order.OrderType
        }));
        if (!string.IsNullOrEmpty(order.InvoiceNbr) && soOrderType?.ARDocType != "UND" || !string.IsNullOrEmpty(order.InvtRefNbr))
          throw new PXException("Shipment already posted to inventory or invoiced for order {0} {1}, shipment cannot be reopened.", new object[2]
          {
            (object) order.OrderType,
            (object) order.OrderNbr
          });
        if (soOrder.Cancelled.GetValueOrDefault())
          throw new PXException("Order {0} {1} is cancelled, shipment cannot be reopened.", new object[2]
          {
            (object) order.OrderType,
            (object) order.OrderNbr
          });
        ((PXGraph) soOrderEntry).Clear();
        ((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) order.OrderNbr, new object[1]
        {
          (object) order.OrderType
        }));
        PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current;
        int? openShipmentCntr1 = current.OpenShipmentCntr;
        current.OpenShipmentCntr = openShipmentCntr1.HasValue ? new int?(openShipmentCntr1.GetValueOrDefault() + 1) : new int?();
        ((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current.Completed = new bool?(false);
        GraphHelper.MarkUpdated(((PXSelectBase) soOrderEntry.Document).Cache, (object) ((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current, true);
        SOOrderSite soOrderSite1 = ((PXSelectBase<SOOrderSite>) soOrderEntry.OrderSite).SelectSingle(new object[3]
        {
          (object) order.OrderType,
          (object) order.OrderNbr,
          (object) order.SiteID
        });
        SOOrderSite soOrderSite2 = soOrderSite1;
        int? openShipmentCntr2 = soOrderSite2.OpenShipmentCntr;
        soOrderSite2.OpenShipmentCntr = openShipmentCntr2.HasValue ? new int?(openShipmentCntr2.GetValueOrDefault() + 1) : new int?();
        ((PXSelectBase<SOOrderSite>) soOrderEntry.OrderSite).Update(soOrderSite1);
        ((PXSelectBase<PX.Objects.SO.SOOrderType>) soOrderEntry.soordertype).Current.RequireControlTotal = new bool?(false);
        soOrderEntry.RecalculateExternalTaxesSync = true;
        order.CreateINDoc = new bool?(false);
        order.Confirmed = new bool?(false);
        ((PXSelectBase) this.OrderList).Cache.Update((object) order);
        int? openShipmentCntr3 = ((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current.OpenShipmentCntr;
        int num1 = 1;
        if (openShipmentCntr3.GetValueOrDefault() > num1 & openShipmentCntr3.HasValue)
        {
          using (IEnumerator<PXResult<PX.Objects.SO.SOOrderShipment>> enumerator = PXSelectBase<PX.Objects.SO.SOOrderShipment, PXSelect<PX.Objects.SO.SOOrderShipment, Where<PX.Objects.SO.SOOrderShipment.orderType, Equal<Current<PX.Objects.SO.SOOrderShipment.orderType>>, And<PX.Objects.SO.SOOrderShipment.orderNbr, Equal<Current<PX.Objects.SO.SOOrderShipment.orderNbr>>, And<PX.Objects.SO.SOOrderShipment.siteID, Equal<Current<PX.Objects.SO.SOOrderShipment.siteID>>, And<PX.Objects.SO.SOOrderShipment.shipmentNbr, NotEqual<Current<PX.Objects.SO.SOOrderShipment.shipmentNbr>>, And<PX.Objects.SO.SOOrderShipment.shipmentType, NotEqual<INDocType.dropShip>>>>>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, new object[1]
          {
            (object) order
          }, Array.Empty<object>()).GetEnumerator())
          {
            if (enumerator.MoveNext())
            {
              PXResult<PX.Objects.SO.SOOrderShipment>.op_Implicit(enumerator.Current);
              throw new PXException("Another shipment already created for order {0} {1}, current shipment cannot be reopened.", new object[2]
              {
                (object) order.OrderType,
                (object) order.OrderNbr
              });
            }
          }
        }
        Dictionary<int?, List<INItemPlan>> dictionary = new Dictionary<int?, List<INItemPlan>>();
        foreach (PXResult<SOShipLineSplit, INItemPlan> pxResult2 in PXSelectBase<SOShipLineSplit, PXSelectReadonly2<SOShipLineSplit, InnerJoin<INItemPlan, On<INItemPlan.supplyPlanID, Equal<SOShipLineSplit.planID>>>, Where<SOShipLineSplit.shipmentNbr, Equal<Required<PX.Objects.SO.SOOrderShipment.shipmentNbr>>, And<SOShipLineSplit.origOrderType, Equal<Required<PX.Objects.SO.SOOrderShipment.orderType>>, And<SOShipLineSplit.origOrderNbr, Equal<Required<PX.Objects.SO.SOOrderShipment.orderNbr>>>>>>.Config>.Select((PXGraph) soOrderEntry, new object[3]
        {
          (object) order.ShipmentNbr,
          (object) order.OrderType,
          (object) order.OrderNbr
        }))
        {
          SOShipLineSplit soShipLineSplit = PXResult<SOShipLineSplit, INItemPlan>.op_Implicit(pxResult2);
          INItemPlan inItemPlan = PXResult<SOShipLineSplit, INItemPlan>.op_Implicit(pxResult2);
          EnumerableEx.Ensure<int?, List<INItemPlan>>((IDictionary<int?, List<INItemPlan>>) dictionary, soShipLineSplit.LineNbr, (Func<List<INItemPlan>>) (() => new List<INItemPlan>())).Add(inItemPlan);
        }
        HashSet<int?> nullableSet = new HashSet<int?>();
        Dictionary<int?, (PX.Objects.SO.SOLine, Decimal?, Decimal?)> lineOpenQuantities = new Dictionary<int?, (PX.Objects.SO.SOLine, Decimal?, Decimal?)>();
        PX.Objects.SO.SOLine soLine1 = (PX.Objects.SO.SOLine) null;
        foreach (PXResult<PX.Objects.SO.SOLine, SOShipLine> pxResult3 in PXSelectBase<PX.Objects.SO.SOLine, PXSelectJoin<PX.Objects.SO.SOLine, LeftJoin<SOShipLine, On<SOShipLine.origOrderType, Equal<PX.Objects.SO.SOLine.orderType>, And<SOShipLine.origOrderNbr, Equal<PX.Objects.SO.SOLine.orderNbr>, And<SOShipLine.origLineNbr, Equal<PX.Objects.SO.SOLine.lineNbr>, And<SOShipLine.shipmentType, Equal<Current<PX.Objects.SO.SOOrderShipment.shipmentType>>, And<SOShipLine.shipmentNbr, Equal<Current<PX.Objects.SO.SOOrderShipment.shipmentNbr>>>>>>>>, Where<PX.Objects.SO.SOLine.orderType, Equal<Current<PX.Objects.SO.SOOrderShipment.orderType>>, And<PX.Objects.SO.SOLine.orderNbr, Equal<Current<PX.Objects.SO.SOOrderShipment.orderNbr>>, And<PX.Objects.SO.SOLine.operation, Equal<Current<PX.Objects.SO.SOOrderShipment.operation>>, And<PX.Objects.SO.SOLine.lineType, NotEqual<SOLineType.miscCharge>, And<Where<PX.Objects.SO.SOLine.siteID, Equal<Current<PX.Objects.SO.SOOrderShipment.siteID>>, Or<SOShipLine.shipmentNbr, IsNotNull>>>>>>>>.Config>.SelectMultiBound((PXGraph) soOrderEntry, (object[]) new PX.Objects.SO.SOOrderShipment[1]
        {
          order
        }, Array.Empty<object>()))
        {
          PX.Objects.SO.SOLine soLine2;
          SOShipLine soShipLine1;
          pxResult3.Deconstruct(ref soLine2, ref soShipLine1);
          PX.Objects.SO.SOLine line = soLine2;
          SOShipLine shipLine = soShipLine1;
          int? nullable1 = shipLine.InventoryID;
          if (nullable1.HasValue)
          {
            PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, shipLine.InventoryID);
            if (inventoryItem != null)
            {
              bool? nullable2 = inventoryItem.IsConverted;
              if (nullable2.GetValueOrDefault())
              {
                nullable2 = shipLine.IsStockItem;
                if (nullable2.HasValue)
                {
                  nullable2 = shipLine.IsStockItem;
                  bool? stkItem = inventoryItem.StkItem;
                  if (!(nullable2.GetValueOrDefault() == stkItem.GetValueOrDefault() & nullable2.HasValue == stkItem.HasValue))
                    throw new PXException("The shipment cannot be corrected because the stock status of at least one item in it has changed.");
                }
              }
            }
          }
          nullable1 = shipLine.SiteID;
          int? nullable3;
          if (nullable1.HasValue)
          {
            nullable1 = line.SiteID;
            nullable3 = shipLine.SiteID;
            if (!(nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue))
            {
              Decimal? shippedQty = shipLine.ShippedQty;
              Decimal num2 = 0M;
              if (shippedQty.GetValueOrDefault() == num2 & shippedQty.HasValue)
              {
                PXCache cache = ((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache;
                SOShipLine soShipLine2 = (SOShipLine) cache.Locate((object) shipLine) ?? shipLine;
                soShipLine2.Confirmed = new bool?(false);
                SOShipLine soShipLine3 = soShipLine2;
                nullable3 = new int?();
                int? nullable4 = nullable3;
                soShipLine3.InvoiceGroupNbr = nullable4;
                GraphHelper.MarkUpdated(cache, (object) soShipLine2, true);
                cache.IsDirty = true;
                continue;
              }
              continue;
            }
          }
          int num3;
          if (line.ShipComplete == "L" && soOrder.ShipComplete == "L")
          {
            nullable3 = soOrder.SiteCntr;
            num3 = nullable3.GetValueOrDefault() == 1 ? 1 : 0;
          }
          else
            num3 = 0;
          bool flag1 = num3 != 0;
          if (shipLine.ShipmentNbr == null)
          {
            bool? completed = line.Completed;
            bool flag2 = false;
            if (!(completed.GetValueOrDefault() == flag2 & completed.HasValue))
            {
              short? lineSign = line.LineSign;
              Decimal? nullable5 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
              Decimal? shippedQty = line.ShippedQty;
              Decimal? nullable6 = nullable5.HasValue & shippedQty.HasValue ? new Decimal?(nullable5.GetValueOrDefault() * shippedQty.GetValueOrDefault()) : new Decimal?();
              Decimal num4 = 0M;
              if (!(nullable6.GetValueOrDefault() > num4 & nullable6.HasValue))
              {
                DateTime? shipDate1 = line.ShipDate;
                DateTime? shipDate2 = order.ShipDate;
                if ((shipDate1.HasValue & shipDate2.HasValue ? (shipDate1.GetValueOrDefault() > shipDate2.GetValueOrDefault() ? 1 : 0) : 0) == 0 || flag1)
                  goto label_43;
              }
            }
            nullableSet.Add(line.LineNbr);
            continue;
          }
label_43:
          int num5;
          if (soLine1 != null)
          {
            nullable3 = soLine1.LineNbr;
            nullable1 = line.LineNbr;
            num5 = !(nullable3.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable3.HasValue == nullable1.HasValue) ? 1 : 0;
          }
          else
            num5 = 1;
          bool lineSwitched = num5 != 0;
          soLine1 = soOrderEntry.CorrectSingleLine(line, shipLine, lineSwitched, lineOpenQuantities);
        }
        Decimal? nullable7 = new Decimal?(0M);
        PX.Objects.SO.SOLineSplit soLineSplit1 = (PX.Objects.SO.SOLineSplit) null;
        PXResultset<PX.Objects.SO.SOLineSplit> source = PXSelectBase<PX.Objects.SO.SOLineSplit, PXSelectJoin<PX.Objects.SO.SOLineSplit, LeftJoin<SOShipLine, On<SOShipLine.origOrderType, Equal<PX.Objects.SO.SOLineSplit.orderType>, And<SOShipLine.origOrderNbr, Equal<PX.Objects.SO.SOLineSplit.orderNbr>, And<SOShipLine.origLineNbr, Equal<PX.Objects.SO.SOLineSplit.lineNbr>, And<SOShipLine.origSplitLineNbr, Equal<PX.Objects.SO.SOLineSplit.splitLineNbr>, And<SOShipLine.shipmentType, Equal<Current<PX.Objects.SO.SOOrderShipment.shipmentType>>, And<SOShipLine.shipmentNbr, Equal<Current<PX.Objects.SO.SOOrderShipment.shipmentNbr>>>>>>>>>, Where<PX.Objects.SO.SOLineSplit.orderType, Equal<Current<PX.Objects.SO.SOOrderShipment.orderType>>, And<PX.Objects.SO.SOLineSplit.orderNbr, Equal<Current<PX.Objects.SO.SOOrderShipment.orderNbr>>, And<PX.Objects.SO.SOLineSplit.siteID, Equal<Current<PX.Objects.SO.SOOrderShipment.siteID>>, And<PX.Objects.SO.SOLineSplit.operation, Equal<Current<PX.Objects.SO.SOOrderShipment.operation>>, And2<Where<PX.Objects.SO.SOLineSplit.pOReceiptNbr, IsNull, Or<PX.Objects.SO.SOLineSplit.pOSource, NotEqual<INReplenishmentSource.dropShipToOrder>, Or<PX.Objects.SO.SOLineSplit.pOSource, IsNull>>>, And2<Where<PX.Objects.SO.SOLineSplit.fixedSource, Equal<INReplenishmentSource.none>, Or<Where<PX.Objects.SO.SOLineSplit.fixedSource, Equal<INReplenishmentSource.purchased>, And<PX.Objects.SO.SOLineSplit.completed, Equal<boolTrue>, And<PX.Objects.SO.SOLineSplit.pOCompleted, Equal<boolFalse>, And<PX.Objects.SO.SOLineSplit.pOCancelled, Equal<boolFalse>, And<PX.Objects.SO.SOLineSplit.pONbr, IsNull, And<PX.Objects.SO.SOLineSplit.pOReceiptNbr, IsNull, And<PX.Objects.SO.SOLineSplit.isAllocated, Equal<boolFalse>>>>>>>>>>, And<Where<PX.Objects.SO.SOLineSplit.shipmentNbr, IsNull, Or<PX.Objects.SO.SOLineSplit.shipmentNbr, Equal<Current<PX.Objects.SO.SOOrderShipment.shipmentNbr>>>>>>>>>>>, OrderBy<Asc<PX.Objects.SO.SOLineSplit.orderType, Asc<PX.Objects.SO.SOLineSplit.orderNbr, Asc<PX.Objects.SO.SOLineSplit.lineNbr, Desc<PX.Objects.SO.SOLineSplit.shipmentNbr, Asc<PX.Objects.SO.SOLineSplit.isAllocated, Desc<PX.Objects.SO.SOLineSplit.shipDate, Desc<PX.Objects.SO.SOLineSplit.pOCreate, Desc<PX.Objects.SO.SOLineSplit.splitLineNbr>>>>>>>>>>.Config>.SelectMultiBound((PXGraph) soOrderEntry, new object[1]
        {
          (object) order
        }, Array.Empty<object>());
        EnumerableExtensions.ForEach<PXResult<PX.Objects.SO.SOLineSplit>>((IEnumerable<PXResult<PX.Objects.SO.SOLineSplit>>) source, (Action<PXResult<PX.Objects.SO.SOLineSplit>>) (_ => _.CreateCopy()));
        foreach (PXResult<PX.Objects.SO.SOLineSplit, SOShipLine> pxResult4 in source)
        {
          PX.Objects.SO.SOLineSplit split = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(PXResult<PX.Objects.SO.SOLineSplit, SOShipLine>.op_Implicit(pxResult4));
          SOShipLine soShipLine = PXResult<PX.Objects.SO.SOLineSplit, SOShipLine>.op_Implicit(pxResult4);
          int? nullable8;
          int? nullable9;
          foreach (PX.Objects.SO.SOLineSplit selectSibling in PXParentAttribute.SelectSiblings(((PXSelectBase) soOrderEntry.splits).Cache, (object) split, typeof (PX.Objects.SO.SOLine)))
          {
            if (selectSibling.ShipmentNbr != null && split.ShipmentNbr != null)
            {
              nullable8 = selectSibling.ParentSplitLineNbr;
              nullable9 = split.SplitLineNbr;
              if (nullable8.GetValueOrDefault() == nullable9.GetValueOrDefault() & nullable8.HasValue == nullable9.HasValue)
                throw new PXException("The {0} shipment cannot be reopened. There are subsequent shipments for the {1} item in the {2} {3} sales order. Only the last shipment in an order can be reopened.", new object[4]
                {
                  (object) split.ShipmentNbr,
                  ((PXSelectBase) soOrderEntry.splits).Cache.GetValueExt<PX.Objects.SO.SOLineSplit.inventoryID>((object) split),
                  (object) order.OrderType,
                  (object) order.OrderNbr
                });
            }
          }
          if (!nullableSet.Contains(split.LineNbr))
          {
            if (soLineSplit1 != null)
            {
              nullable9 = soLineSplit1.LineNbr;
              nullable8 = split.LineNbr;
              if (nullable9.GetValueOrDefault() == nullable8.GetValueOrDefault() & nullable9.HasValue == nullable8.HasValue)
                goto label_64;
            }
            nullable7 = new Decimal?(0M);
            (PX.Objects.SO.SOLine, Decimal?, Decimal?) valueTuple;
            if (lineOpenQuantities.TryGetValue(split.LineNbr, out valueTuple))
              nullable7 = !(valueTuple.Item1.UOM == split.UOM) ? new Decimal?(INUnitAttribute.ConvertFromBase(((PXSelectBase) soOrderEntry.splits).Cache, split.InventoryID, split.UOM, valueTuple.Item2.Value, INPrecision.QUANTITY)) : valueTuple.Item3;
label_64:
            soLineSplit1 = split;
            Decimal? nullable10;
            Decimal? nullable11;
            if (split.ShipmentNbr == order.ShipmentNbr)
            {
              Decimal? nullable12 = new Decimal?(0M);
              if (split.IsAllocated.GetValueOrDefault())
              {
                Decimal? availFromSiteStatus = this.GetQtyHardAvailFromSiteStatus((PXGraph) soOrderEntry, split);
                Decimal? nullable13 = GraphHelper.RowCast<PX.Objects.SO.SOLineSplit>((IEnumerable) ((IEnumerable<PXResult<PX.Objects.SO.SOLineSplit>>) source).AsEnumerable<PXResult<PX.Objects.SO.SOLineSplit>>()).TakeWhile<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (_ => !((PXSelectBase) soOrderEntry.splits).Cache.ObjectsEqual((object) _, (object) split))).Where<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (_ =>
                {
                  int? inventoryId1 = _.InventoryID;
                  int? inventoryId2 = split.InventoryID;
                  if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
                  {
                    int? subItemId1 = _.SubItemID;
                    int? subItemId2 = split.SubItemID;
                    if (subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue)
                    {
                      int? siteId1 = _.SiteID;
                      int? siteId2 = split.SiteID;
                      if (siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue && _.ShipmentNbr == order.ShipmentNbr)
                        return _.IsAllocated.GetValueOrDefault();
                    }
                  }
                  return false;
                })).Sum<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, Decimal?>) (_ => _.BaseQty));
                Decimal? nullable14 = GraphHelper.RowCast<PX.Objects.SO.SOLineSplit>((IEnumerable) ((IEnumerable<PXResult<PX.Objects.SO.SOLineSplit>>) source).AsEnumerable<PXResult<PX.Objects.SO.SOLineSplit>>()).Where<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (_ =>
                {
                  int? parentSplitLineNbr = _.ParentSplitLineNbr;
                  int? splitLineNbr = split.SplitLineNbr;
                  return parentSplitLineNbr.GetValueOrDefault() == splitLineNbr.GetValueOrDefault() & parentSplitLineNbr.HasValue == splitLineNbr.HasValue && _.IsAllocated.GetValueOrDefault();
                })).Sum<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, Decimal?>) (_ => _.BaseQty));
                Decimal? nullable15 = availFromSiteStatus;
                Decimal? nullable16 = nullable13;
                Decimal? nullable17 = nullable15.HasValue & nullable16.HasValue ? new Decimal?(nullable15.GetValueOrDefault() + nullable16.GetValueOrDefault()) : new Decimal?();
                Decimal? nullable18 = nullable14;
                Decimal? nullable19 = nullable17.HasValue & nullable18.HasValue ? new Decimal?(nullable17.GetValueOrDefault() + nullable18.GetValueOrDefault()) : new Decimal?();
                Decimal num6 = 0M;
                Decimal? nullable20;
                if (!(nullable19.GetValueOrDefault() > num6 & nullable19.HasValue))
                {
                  nullable20 = new Decimal?(0M);
                }
                else
                {
                  Decimal? nullable21 = availFromSiteStatus;
                  Decimal? nullable22 = nullable13;
                  nullable19 = nullable21.HasValue & nullable22.HasValue ? new Decimal?(nullable21.GetValueOrDefault() + nullable22.GetValueOrDefault()) : new Decimal?();
                  Decimal? nullable23 = nullable14;
                  nullable20 = nullable19.HasValue & nullable23.HasValue ? new Decimal?(nullable19.GetValueOrDefault() + nullable23.GetValueOrDefault()) : new Decimal?();
                }
                Decimal? nullable24 = nullable20;
                nullable24 = new Decimal?(INUnitAttribute.ConvertFromBase(((PXSelectBase) soOrderEntry.splits).Cache, split.InventoryID, split.UOM, nullable24.Value, INPrecision.QUANTITY));
                nullable12 = new Decimal?(Math.Min(nullable7.Value, nullable24.Value));
              }
              else
                nullable12 = nullable7;
              nullable10 = nullable12;
              Decimal? qty1 = split.Qty;
              Decimal? shippedQty1 = split.ShippedQty;
              Decimal? nullable25 = qty1.HasValue & shippedQty1.HasValue ? new Decimal?(qty1.GetValueOrDefault() - shippedQty1.GetValueOrDefault()) : new Decimal?();
              if (nullable10.GetValueOrDefault() >= nullable25.GetValueOrDefault() & nullable10.HasValue & nullable25.HasValue)
              {
                nullable11 = nullable7;
                Decimal? qty2 = split.Qty;
                Decimal? shippedQty2 = split.ShippedQty;
                nullable10 = qty2.HasValue & shippedQty2.HasValue ? new Decimal?(qty2.GetValueOrDefault() - shippedQty2.GetValueOrDefault()) : new Decimal?();
                nullable7 = nullable11.HasValue & nullable10.HasValue ? new Decimal?(nullable11.GetValueOrDefault() - nullable10.GetValueOrDefault()) : new Decimal?();
              }
              else
              {
                nullable10 = nullable7;
                Decimal? nullable26 = nullable12;
                nullable7 = nullable10.HasValue & nullable26.HasValue ? new Decimal?(nullable10.GetValueOrDefault() - nullable26.GetValueOrDefault()) : new Decimal?();
                PX.Objects.SO.SOLineSplit soLineSplit2 = split;
                Decimal? shippedQty3 = split.ShippedQty;
                nullable10 = nullable12;
                Decimal? nullable27 = shippedQty3.HasValue & nullable10.HasValue ? new Decimal?(shippedQty3.GetValueOrDefault() + nullable10.GetValueOrDefault()) : new Decimal?();
                soLineSplit2.Qty = nullable27;
              }
            }
            else
            {
              nullable10 = split.Qty;
              Decimal? nullable28 = nullable7;
              if (nullable10.GetValueOrDefault() >= nullable28.GetValueOrDefault() & nullable10.HasValue & nullable28.HasValue)
              {
                split.Qty = nullable7;
                nullable7 = new Decimal?(0M);
              }
              else
              {
                Decimal? nullable29 = nullable7;
                nullable10 = split.Qty;
                nullable7 = nullable29.HasValue & nullable10.HasValue ? new Decimal?(nullable29.GetValueOrDefault() - nullable10.GetValueOrDefault()) : new Decimal?();
              }
            }
            bool flag = !string.IsNullOrEmpty(split.ShipmentNbr);
            split.Completed = new bool?(false);
            split.ShipmentNbr = (string) null;
            if (split.IsAllocated.GetValueOrDefault() && !string.IsNullOrEmpty(split.LotSerialNbr) && !string.IsNullOrEmpty(soShipLine.ShipmentNbr) && !string.Equals(split.LotSerialNbr, soShipLine.LotSerialNbr, StringComparison.InvariantCultureIgnoreCase))
            {
              INSiteLotSerial inSiteLotSerial = PXResultset<INSiteLotSerial>.op_Implicit(PXSelectBase<INSiteLotSerial, PXSelectReadonly<INSiteLotSerial, Where<INSiteLotSerial.inventoryID, Equal<Required<INSiteLotSerial.inventoryID>>, And<INSiteLotSerial.siteID, Equal<Required<INSiteLotSerial.siteID>>, And<INSiteLotSerial.lotSerialNbr, Equal<Required<INSiteLotSerial.lotSerialNbr>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, new object[3]
              {
                (object) split.InventoryID,
                (object) split.SiteID,
                (object) split.LotSerialNbr
              }));
              if (inSiteLotSerial != null)
              {
                nullable10 = inSiteLotSerial.QtyHardAvail;
                nullable11 = split.BaseQty;
                if (!(nullable10.GetValueOrDefault() < nullable11.GetValueOrDefault() & nullable10.HasValue & nullable11.HasValue))
                  goto label_81;
              }
              split.IsAllocated = new bool?(false);
              split.LotSerialNbr = (string) null;
              args.ShipLinesClearedSOAllocation.Add(soShipLine.LineNbr);
            }
label_81:
            PX.Objects.SO.SOLineSplit soLineSplit3 = (PX.Objects.SO.SOLineSplit) null;
            nullable11 = split.Qty;
            Decimal num7 = 0M;
            if (nullable11.GetValueOrDefault() <= num7 & nullable11.HasValue && !flag || ((PXSelectBase) soOrderEntry.splits).Cache.GetStatus((object) split) == 2)
            {
              ((PXSelectBase<PX.Objects.SO.SOLineSplit>) soOrderEntry.splits).Delete(split);
              soLineSplit3 = split;
              split = (PX.Objects.SO.SOLineSplit) null;
            }
            else
              split = ((PXSelectBase<PX.Objects.SO.SOLineSplit>) soOrderEntry.splits).Update(split);
            if (split != null && split.PlanID.HasValue)
            {
              nullable8 = soShipLine.LineNbr;
              List<INItemPlan> inItemPlanList;
              if (nullable8.HasValue && dictionary.TryGetValue(soShipLine.LineNbr, out inItemPlanList))
              {
                foreach (INItemPlan inItemPlan in inItemPlanList)
                {
                  inItemPlan.SupplyPlanID = split.PlanID;
                  GraphHelper.MarkUpdated(((PXGraph) soOrderEntry).Caches[typeof (INItemPlan)], (object) inItemPlan, true);
                }
                dictionary.Remove(soShipLine.LineNbr);
              }
            }
            if (soLineSplit3 != null)
            {
              long? planId = soLineSplit3.PlanID;
              long num8 = 0;
              if (planId.GetValueOrDefault() > num8 & planId.HasValue)
              {
                nullable8 = soLineSplit3.ParentSplitLineNbr;
                if (nullable8.HasValue)
                {
                  PX.Objects.SO.SOLineSplit soLineSplit4 = ((PXSelectBase<PX.Objects.SO.SOLineSplit>) soOrderEntry.splits).Locate(new PX.Objects.SO.SOLineSplit()
                  {
                    OrderType = soLineSplit3.OrderType,
                    OrderNbr = soLineSplit3.OrderNbr,
                    LineNbr = soLineSplit3.LineNbr,
                    SplitLineNbr = soLineSplit3.ParentSplitLineNbr
                  });
                  if (soLineSplit4 != null)
                  {
                    foreach (PXResult<INItemPlan> pxResult5 in PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemPlan.supplyPlanID, IBqlLong>.IsEqual<P.AsLong>>>.Config>.Select((PXGraph) soOrderEntry, new object[1]
                    {
                      (object) soLineSplit3.PlanID
                    }))
                    {
                      INItemPlan inItemPlan = PXResult<INItemPlan>.op_Implicit(pxResult5);
                      inItemPlan.SupplyPlanID = soLineSplit4.PlanID;
                      GraphHelper.MarkUpdated(((PXGraph) soOrderEntry).Caches[typeof (INItemPlan)], (object) inItemPlan);
                    }
                  }
                }
              }
            }
          }
        }
        PX.Objects.SO.SOOrder copy = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current);
        PXFormulaAttribute.CalcAggregate<PX.Objects.SO.SOLine.orderQty>(((PXSelectBase) soOrderEntry.Transactions).Cache, (object) copy);
        ((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Update(copy);
        ((SelectedEntityEvent<PX.Objects.SO.SOOrder>) PXEntityEventBase<PX.Objects.SO.SOOrder>.Container<PX.Objects.SO.SOOrder.Events>.Select((Expression<Func<PX.Objects.SO.SOOrder.Events, PXEntityEvent<PX.Objects.SO.SOOrder.Events>>>) (e => e.GotShipmentCorrected))).FireOn((PXGraph) soOrderEntry, ((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current);
        ((PXAction) soOrderEntry.Save).Press();
      }
    }
  }

  /// Overrides <see cref="M:PX.Objects.SO.SOShipmentEntry.GetShipLineSplitsToCorrectCommand(PX.Objects.SO.Models.CorrectShipmentArgs)" />
  /// .
  [PXOverride]
  public BqlCommand GetShipLineSplitsToCorrectCommand(
    CorrectShipmentArgs args,
    Func<CorrectShipmentArgs, BqlCommand> base_GetShipLineSplitsToCorrectCommand)
  {
    return BqlCommand.AppendJoin<LeftJoin<PX.Objects.SO.SOLineSplit, On<SOShipLineSplit.FK.OriginalOrderLineSplit>>>(base_GetShipLineSplitsToCorrectCommand(args)).WhereAnd<Where<SOShipLineSplit.origOrderType, IsNotNull>>();
  }

  [PXOverride]
  public INItemPlan GetUpdatedPlanByShipLineSplit(
    CorrectShipmentArgs args,
    PXResult<INItemPlan> pxResult,
    Func<CorrectShipmentArgs, PXResult<INItemPlan>, INItemPlan> base_GetUpdatedPlanByShipLineSplit)
  {
    if (PX.Objects.SO.SOOrderType.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, PXResult.Unwrap<SOShipLineSplit>((object) pxResult).OrigOrderType) == null)
      return PXResult<INItemPlan>.op_Implicit(pxResult);
    INItemPlan planByShipLineSplit = base_GetUpdatedPlanByShipLineSplit(args, pxResult);
    planByShipLineSplit.OrigPlanID = (long?) PXResult.Unwrap<PX.Objects.SO.SOLineSplit>((object) pxResult)?.PlanID;
    return planByShipLineSplit;
  }

  /// Overrides <see cref="M:PX.Objects.SO.SOShipmentEntry.UpdateShipLinesOnCorrectShipment(PX.Objects.SO.Models.CorrectShipmentArgs)" />
  /// .
  [PXOverride]
  public void UpdateShipLinesOnCorrectShipment(
    CorrectShipmentArgs args,
    Action<CorrectShipmentArgs> base_UpdateShipLinesOnCorrectShipment)
  {
    foreach (PXResult<SOLineSplit2, PX.Objects.SO.SOLine, SOShipLine, INItemPlan> pxResult in PXSelectBase<SOLineSplit2, PXSelectJoin<SOLineSplit2, InnerJoin<PX.Objects.SO.SOLine, On<PX.Objects.SO.SOLine.orderType, Equal<SOLineSplit2.orderType>, And<PX.Objects.SO.SOLine.orderNbr, Equal<SOLineSplit2.orderNbr>, And<PX.Objects.SO.SOLine.lineNbr, Equal<SOLineSplit2.lineNbr>>>>, InnerJoin<SOShipLine, On<SOShipLine.origOrderType, Equal<SOLineSplit2.orderType>, And<SOShipLine.origOrderNbr, Equal<SOLineSplit2.orderNbr>, And<SOShipLine.origLineNbr, Equal<SOLineSplit2.lineNbr>, And<SOShipLine.origSplitLineNbr, Equal<SOLineSplit2.splitLineNbr>>>>>, InnerJoin<INItemPlan, On<INItemPlan.planID, Equal<SOLineSplit2.planID>>>>>, Where<SOShipLine.shipmentNbr, Equal<Current<PX.Objects.SO.SOShipment.shipmentNbr>>, And<SOShipLine.shipmentType, Equal<Current<PX.Objects.SO.SOShipment.shipmentType>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, Array.Empty<object>()))
    {
      PXResult<SOLineSplit2, PX.Objects.SO.SOLine, SOShipLine, INItemPlan>.op_Implicit(pxResult);
      PX.Objects.SO.SOLine soline = PXResult<SOLineSplit2, PX.Objects.SO.SOLine, SOShipLine, INItemPlan>.op_Implicit(pxResult);
      SOShipLine soShipLine = PXResult<SOLineSplit2, PX.Objects.SO.SOLine, SOShipLine, INItemPlan>.op_Implicit(pxResult);
      INItemPlan inItemPlan = PXResult<SOLineSplit2, PX.Objects.SO.SOLine, SOShipLine, INItemPlan>.op_Implicit(pxResult);
      SOShipLine copy = PXCache<SOShipLine>.CreateCopy(soShipLine);
      ((PXGraphExtension<SOShipmentEntry>) this).Base.CorrectShipLine(args.ShipLinesClearedSOAllocation, copy);
      ((PXGraphExtension<SOShipmentEntry>) this).Base.UpdateOrigValues(copy, soline, inItemPlan.PlanQty);
      ((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base).Caches[typeof (SOShipLine)].Update((object) copy);
    }
  }

  /// Overrides <see cref="M:PX.Objects.SO.SOShipmentEntry.AfterCorrectShipment" />
  /// .
  [PXOverride]
  public void AfterCorrectShipment(Action base_AfterCorrectShipment)
  {
    base_AfterCorrectShipment();
    ((PXCache) GraphHelper.Caches<PX.Objects.SO.SOOrder>((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base)).Clear();
    ((PXCache) GraphHelper.Caches<PX.Objects.SO.SOOrder>((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base)).ClearQueryCache();
    ((PXCache) GraphHelper.Caches<SOOrderSite>((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base)).Clear();
    ((PXCache) GraphHelper.Caches<SOOrderSite>((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base)).ClearQueryCache();
    ((PXCache) GraphHelper.Caches<SOLine2>((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base)).Clear();
    ((PXCache) GraphHelper.Caches<SOLine2>((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base)).ClearQueryCache();
  }

  /// Overrides <see cref="M:PX.Objects.SO.SOShipmentEntry.GetBillToAddressContact" />
  /// .
  ///             <returns>Address and contact from single related SOOrderShipment, in any other cases (null, null).</returns>
  [PXOverride]
  public Tuple<SOAddress, SOContact> GetBillToAddressContact(
    Func<Tuple<SOAddress, SOContact>> base_GetBillToAddressContact)
  {
    PXResultset<PX.Objects.SO.SOOrderShipment> source = ((PXSelectBase<PX.Objects.SO.SOOrderShipment>) this.OrderList).Select(Array.Empty<object>());
    SOAddress soAddress = GraphHelper.RowCast<SOAddress>((IEnumerable) source).FirstOrDefault<SOAddress>();
    SOContact soContact = GraphHelper.RowCast<SOContact>((IEnumerable) source).FirstOrDefault<SOContact>();
    return ((IQueryable<PXResult<PX.Objects.SO.SOOrderShipment>>) source).Count<PXResult<PX.Objects.SO.SOOrderShipment>>() == 1 ? new Tuple<SOAddress, SOContact>(soAddress, soContact) : base_GetBillToAddressContact();
  }

  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.SOShipLineSplitPlanDefaultValuesExtension.GetOrigDocumentNoteID(PX.Objects.SO.SOShipLine)" />
  /// .
  [PXOverride]
  public Guid? GetOrigDocumentNoteID(
    SOShipLine shipLine,
    Func<SOShipLine, Guid?> base_GetOrigDocumentNoteID)
  {
    return PX.Objects.SO.SOOrder.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, shipLine.OrigOrderType, shipLine.OrigOrderNbr)?.NoteID;
  }

  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.SOShipLineSplitPlanDefaultValuesExtension.UpdateINItemPlanFromLineSourceSplit(PX.Objects.IN.INItemPlan,PX.Objects.SO.SOShipLine)" />
  /// .
  [PXOverride]
  public INItemPlan UpdateINItemPlanFromLineSourceSplit(
    INItemPlan planRow,
    SOShipLine shipLine,
    Func<INItemPlan, SOShipLine, INItemPlan> base_UpdateINItemPlanFromLineSourceSplit)
  {
    PX.Objects.SO.SOLineSplit soLineSplit = PX.Objects.SO.SOLineSplit.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, shipLine.OrigOrderType, shipLine.OrigOrderNbr, shipLine.OrigLineNbr, shipLine.OrigSplitLineNbr);
    planRow.OrigPlanLevel = new int?(!string.IsNullOrEmpty(soLineSplit.LotSerialNbr) ? 2 : 0);
    planRow.OrigPlanID = soLineSplit.PlanID;
    INItemPlan inItemPlan = planRow;
    bool? ignoreOrigPlan = inItemPlan.IgnoreOrigPlan;
    inItemPlan.IgnoreOrigPlan = (string.IsNullOrEmpty(soLineSplit.LotSerialNbr) ? 0 : (!string.Equals(planRow.LotSerialNbr, soLineSplit.LotSerialNbr, StringComparison.InvariantCultureIgnoreCase) ? 1 : 0)) != 0 ? new bool?(true) : ignoreOrigPlan;
    return planRow;
  }

  private void ValidateLineShipComplete(
    PX.Objects.SO.SOShipment shipment,
    ref string orderType,
    ref string orderNbr)
  {
    foreach (SOLine2 orderLine in ((PXSelectBase) this.soline).Cache.Updated)
    {
      if ((orderType == null ? 1 : (!(orderType == orderLine.OrderType) ? 0 : (orderNbr == orderLine.OrderNbr ? 1 : 0))) != 0 && !object.Equals(((PXSelectBase) this.soline).Cache.GetValueOriginal<SOLine2.baseShippedQty>((object) orderLine), (object) orderLine.BaseShippedQty))
      {
        PX.Objects.SO.SOOrderShipment soOrderShipment = new PX.Objects.SO.SOOrderShipment()
        {
          OrderType = orderLine.OrderType,
          OrderNbr = orderLine.OrderNbr,
          ShippingRefNoteID = shipment.NoteID
        };
        if (!EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) this.OrderList).Cache.GetStatus((object) (((PXSelectBase<PX.Objects.SO.SOOrderShipment>) this.OrderList).Locate(soOrderShipment) ?? soOrderShipment)), (PXEntryStatus) 3, (PXEntryStatus) 4) && !this.ValidateLineShipComplete(orderLine))
        {
          orderType = orderLine.OrderType;
          orderNbr = orderLine.OrderNbr;
        }
      }
    }
  }

  private bool ValidateLineShipComplete(SOLine2 orderLine)
  {
    if (orderLine.ShipComplete == "C" && orderLine.LineType != "MI")
    {
      short? lineSign = orderLine.LineSign;
      Decimal? nullable1 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
      Decimal? baseShippedQty1 = orderLine.BaseShippedQty;
      Decimal? nullable2 = nullable1.HasValue & baseShippedQty1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * baseShippedQty1.GetValueOrDefault()) : new Decimal?();
      lineSign = orderLine.LineSign;
      Decimal? nullable3 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
      Decimal? baseOrderQty = orderLine.BaseOrderQty;
      Decimal? nullable4 = nullable3.HasValue & baseOrderQty.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * baseOrderQty.GetValueOrDefault()) : new Decimal?();
      nullable1 = orderLine.CompleteQtyMin;
      Decimal? nullable5 = nullable4.HasValue & nullable1.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * nullable1.GetValueOrDefault() / 100M) : new Decimal?();
      if (nullable2.GetValueOrDefault() < nullable5.GetValueOrDefault() & nullable2.HasValue & nullable5.HasValue)
      {
        Decimal? baseShippedQty2 = orderLine.BaseShippedQty;
        Decimal num = 0M;
        if (baseShippedQty2.GetValueOrDefault() == num & baseShippedQty2.HasValue)
        {
          if (PXResultset<SOShipLine>.op_Implicit(PXSelectBase<SOShipLine, PXViewOf<SOShipLine>.BasedOn<SelectFromBase<SOShipLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipLine.origOrderType, Equal<BqlField<SOLine2.orderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<SOShipLine.origOrderNbr, IBqlString>.IsEqual<BqlField<SOLine2.orderNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<SOShipLine.origLineNbr, IBqlInt>.IsEqual<BqlField<SOLine2.lineNbr, IBqlInt>.FromCurrent>>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, new object[1]
          {
            (object) orderLine
          }, Array.Empty<object>())) == null)
            return true;
        }
        PXTrace.WriteInformation("The shipment cannot be saved because the quantity of the {0} item in the shipment is less than its quantity in the {1} {2} sales order, and the line has the Ship Complete rule selected.", new object[3]
        {
          (object) PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, orderLine.InventoryID)?.InventoryCD?.TrimEnd(),
          (object) orderLine.OrderType,
          (object) orderLine.OrderNbr
        });
        return false;
      }
    }
    return true;
  }

  private void ValidateOrderShipComplete(
    PX.Objects.SO.SOShipment shipment,
    ref string orderType,
    ref string orderNbr)
  {
    if (!NonGenericIEnumerableExtensions.Any_(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache.Deleted) && !NonGenericIEnumerableExtensions.Any_(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache.Inserted) && object.Equals(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Cache.GetValueOriginal<PX.Objects.SO.SOShipment.shipDate>((object) shipment), (object) shipment.ShipDate))
      return;
    Lazy<HashSet<(string, string, int?)>> lazy = new Lazy<HashSet<(string, string, int?)>>((Func<HashSet<(string, string, int?)>>) (() => GraphHelper.RowCast<SOShipLine>((IEnumerable) ((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).View.SelectMultiBound(new object[1]
    {
      (object) shipment
    }, Array.Empty<object>())).Select<SOShipLine, (string, string, int?)>((Func<SOShipLine, (string, string, int?)>) (l => (l.OrigOrderType, l.OrigOrderNbr, l.OrigLineNbr))).ToHashSet<(string, string, int?)>()));
    foreach (PX.Objects.SO.SOOrderShipment order in GraphHelper.RowCast<PX.Objects.SO.SOOrderShipment>((IEnumerable) ((PXSelectBase) this.OrderList).View.SelectMultiBound(new object[1]
    {
      (object) shipment
    }, Array.Empty<object>())))
    {
      if (((order.ShipComplete == "C" ? 1 : 0) & (orderType == null ? (true ? 1 : 0) : (!(orderType == order.OrderType) ? (false ? 1 : 0) : (orderNbr == order.OrderNbr ? 1 : 0)))) != 0 && !this.ValidateOrderShipComplete(lazy.Value, order))
      {
        orderType = order.OrderType;
        orderNbr = order.OrderNbr;
        break;
      }
    }
  }

  private bool ValidateOrderShipComplete(
    HashSet<(string orderType, string orderNbr, int? lineNbr)> shiplinesLines,
    PX.Objects.SO.SOOrderShipment order)
  {
    bool flag = true;
    foreach (PXResult<SOLine2> pxResult in PXSelectBase<SOLine2, PXViewOf<SOLine2>.BasedOn<SelectFromBase<SOLine2, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOLine2.orderType, Equal<BqlField<PX.Objects.SO.SOOrderShipment.orderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<SOLine2.orderNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOOrderShipment.orderNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<SOLine2.siteID, IBqlInt>.IsEqual<BqlField<PX.Objects.SO.SOOrderShipment.siteID, IBqlInt>.FromCurrent>>>, And<BqlOperand<SOLine2.operation, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOOrderShipment.operation, IBqlString>.FromCurrent>>>, And<BqlOperand<SOLine2.completed, IBqlBool>.IsNotEqual<True>>>, And<BqlOperand<SOLine2.lineType, IBqlString>.IsNotEqual<SOLineType.miscCharge>>>, And<BqlOperand<SOLine2.shipDate, IBqlDateTime>.IsLessEqual<BqlField<PX.Objects.SO.SOOrderShipment.shipDate, IBqlDateTime>.FromCurrent>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOLine2.pOSource, IsNull>>>>.Or<BqlOperand<SOLine2.pOSource, IBqlString>.IsNotEqual<INReplenishmentSource.dropShipToOrder>>>>>.ReadOnly.Config>.SelectMultiBound((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, new object[1]
    {
      (object) order
    }, Array.Empty<object>()))
    {
      SOLine2 soLine2 = PXResult<SOLine2>.op_Implicit(pxResult);
      if (!shiplinesLines.Contains((soLine2.OrderType, soLine2.OrderNbr, soLine2.LineNbr)))
      {
        flag = false;
        string str = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, soLine2.InventoryID)?.InventoryCD?.TrimEnd();
        PXTrace.WriteInformation("The shipment cannot be saved because it does not include the {2} item from the {0} {1} sales order that has the Ship Complete order-level shipping rule on the Shipping tab of the Sales Orders (SO301000) form.", new object[3]
        {
          (object) soLine2.OrderType,
          (object) soLine2.OrderNbr,
          (object) str
        });
      }
    }
    return flag;
  }

  private DiscountEngine<SOShipLine, SOShipmentDiscountDetail> _discountEngine
  {
    get => DiscountEngineProvider.GetEngineFor<SOShipLine, SOShipmentDiscountDetail>();
  }

  protected void AllocateGroupFreeItems(PX.Objects.SO.SOOrder order)
  {
    Dictionary<SOOrderExtension.DiscKey, Decimal> dictionary = new Dictionary<SOOrderExtension.DiscKey, Decimal>();
    List<SOShipLine> soShipLineList = new List<SOShipLine>();
    bool flag1 = false;
    foreach (PXResult<SOShipLine> pxResult in ((PXSelectBase<SOShipLine>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Select(Array.Empty<object>()))
    {
      SOShipLine soShipLine = PXResult<SOShipLine>.op_Implicit(pxResult);
      bool? isFree;
      if (soShipLine.OrigOrderType == order.OrderType && soShipLine.OrigOrderNbr == order.OrderNbr)
      {
        isFree = soShipLine.IsFree;
        bool flag2 = false;
        if (isFree.GetValueOrDefault() == flag2 & isFree.HasValue)
          soShipLineList.Add(soShipLine);
      }
      isFree = soShipLine.IsFree;
      if (isFree.GetValueOrDefault())
        flag1 = true;
    }
    bool flag3 = DiscountEngine.ApplyQuantityDiscountByBaseUOMForAR((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base);
    if (!flag1)
      return;
    ((PXSelectBase<PX.Objects.SO.SOOrder>) this.soorder).Current = order;
    PXCache cach = ((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base).Caches[typeof (PX.Objects.SO.SOLine)];
    PXSelectBase<PX.Objects.SO.SOLine> documentDetailsSelect = (PXSelectBase<PX.Objects.SO.SOLine>) new PXSelect<PX.Objects.SO.SOLine, Where<PX.Objects.SO.SOLine.orderType, Equal<Current<PX.Objects.SO.SOOrder.orderType>>, And<PX.Objects.SO.SOLine.orderNbr, Equal<Current<PX.Objects.SO.SOOrder.orderNbr>>>>>((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base);
    PXSelectBase<SOOrderDiscountDetail> discountDetailsSelect = (PXSelectBase<SOOrderDiscountDetail>) new PXSelect<SOOrderDiscountDetail, Where<SOOrderDiscountDetail.orderType, Equal<Current<PX.Objects.SO.SOOrder.orderType>>, And<SOOrderDiscountDetail.orderNbr, Equal<Current<PX.Objects.SO.SOOrder.orderNbr>>>>>((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base);
    TwoWayLookup<SOOrderDiscountDetail, PX.Objects.SO.SOLine> andDocumentLines = DiscountEngineProvider.GetEngineFor<PX.Objects.SO.SOLine, SOOrderDiscountDetail>().GetListOfLinksBetweenDiscountsAndDocumentLines(cach, documentDetailsSelect, discountDetailsSelect);
    if (((PXSelectBase<SOSetup>) ((PXGraphExtension<SOShipmentEntry>) this).Base.sosetup).Current.FreeItemShipping == "P")
    {
      foreach (SOOrderDiscountDetail orderDiscountDetail in andDocumentLines.LeftValues.Where<SOOrderDiscountDetail>((Func<SOOrderDiscountDetail, bool>) (x =>
      {
        Decimal? freeItemQty = x.FreeItemQty;
        Decimal num = 0M;
        return freeItemQty.GetValueOrDefault() > num & freeItemQty.HasValue && !x.SkipDiscount.GetValueOrDefault();
      })))
      {
        Decimal num = 0M;
        foreach (PX.Objects.SO.SOLine soLine in andDocumentLines.RightsFor(orderDiscountDetail))
        {
          foreach (SOShipLine soShipLine in soShipLineList)
          {
            int? lineNbr = soLine.LineNbr;
            int? origLineNbr = soShipLine.OrigLineNbr;
            if (lineNbr.GetValueOrDefault() == origLineNbr.GetValueOrDefault() & lineNbr.HasValue == origLineNbr.HasValue)
              num += (flag3 ? soShipLine.BaseShippedQty : soShipLine.ShippedQty).GetValueOrDefault();
          }
        }
        Decimal d = num * orderDiscountDetail.FreeItemQty.Value / orderDiscountDetail.DiscountableQty.Value;
        SOOrderExtension.DiscKey key = new SOOrderExtension.DiscKey(orderDiscountDetail.DiscountID, orderDiscountDetail.DiscountSequenceID, orderDiscountDetail.FreeItemID.Value);
        dictionary.Add(key, Math.Floor(d));
      }
    }
    else
    {
      foreach (SOOrderDiscountDetail orderDiscountDetail in andDocumentLines.LeftValues.Where<SOOrderDiscountDetail>((Func<SOOrderDiscountDetail, bool>) (x =>
      {
        Decimal? freeItemQty = x.FreeItemQty;
        Decimal num = 0M;
        return freeItemQty.GetValueOrDefault() > num & freeItemQty.HasValue && !x.SkipDiscount.GetValueOrDefault();
      })))
      {
        Decimal num1 = 0M;
        Decimal num2 = 0M;
        Decimal num3 = 0M;
        Decimal num4 = 0M;
        Decimal? nullable;
        foreach (PX.Objects.SO.SOLine soLine in andDocumentLines.RightsFor(orderDiscountDetail))
        {
          SOLine2 soLine2_1 = new SOLine2()
          {
            OrderType = soLine.OrderType,
            OrderNbr = soLine.OrderNbr,
            LineNbr = soLine.LineNbr
          };
          SOLine2 soLine2_2 = GraphHelper.Caches<SOLine2>((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base).Locate(soLine2_1);
          if (soLine2_2 != null)
          {
            num4 += soLine.Qty.GetValueOrDefault();
            if (soLine.ShipComplete == "B")
            {
              num2 += soLine.Qty.GetValueOrDefault();
              Decimal? shippedQty = soLine2_2.ShippedQty;
              nullable = soLine.OrderQty;
              if (shippedQty.GetValueOrDefault() >= nullable.GetValueOrDefault() & shippedQty.HasValue & nullable.HasValue)
              {
                int? lineNbr1 = soLine.LineNbr;
                int? lineNbr2 = soLine2_2.LineNbr;
                if (lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue)
                {
                  Decimal num5 = num1;
                  nullable = soLine2_2.ShippedQty;
                  Decimal valueOrDefault = nullable.GetValueOrDefault();
                  num1 = num5 + valueOrDefault;
                }
              }
            }
            else
            {
              Decimal num6 = num3;
              nullable = soLine2_2.ShippedQty;
              Decimal valueOrDefault = nullable.GetValueOrDefault();
              num3 = num6 + valueOrDefault;
            }
          }
        }
        Decimal d;
        if (num3 + num1 < num4)
        {
          Decimal num7 = num3 + num1;
          nullable = orderDiscountDetail.DiscountableQty;
          Decimal num8 = nullable.Value;
          Decimal num9 = num7 / num8;
          nullable = orderDiscountDetail.FreeItemQty;
          Decimal num10 = nullable.Value;
          d = num9 * num10;
        }
        else
        {
          nullable = orderDiscountDetail.FreeItemQty;
          d = nullable.Value;
        }
        SOOrderExtension.DiscKey key = new SOOrderExtension.DiscKey(orderDiscountDetail.DiscountID, orderDiscountDetail.DiscountSequenceID, orderDiscountDetail.FreeItemID.Value);
        dictionary.Add(key, num1 >= num2 ? Math.Floor(d) : 0M);
      }
    }
    foreach (KeyValuePair<SOOrderExtension.DiscKey, Decimal> keyValuePair in dictionary)
      this.UpdateInsertDiscountTrace(new SOShipmentDiscountDetail()
      {
        Type = "L",
        OrderType = order.OrderType,
        OrderNbr = order.OrderNbr,
        DiscountID = keyValuePair.Key.DiscID,
        DiscountSequenceID = keyValuePair.Key.DiscSeqID,
        FreeItemID = new int?(keyValuePair.Key.FreeItemID),
        FreeItemQty = new Decimal?(keyValuePair.Value)
      });
  }

  private void UpdateInsertDiscountTrace(SOShipmentDiscountDetail newTrace)
  {
    SOShipmentDiscountDetail trace = PXResultset<SOShipmentDiscountDetail>.op_Implicit(PXSelectBase<SOShipmentDiscountDetail, PXSelect<SOShipmentDiscountDetail, Where<SOShipmentDiscountDetail.shipmentNbr, Equal<Current<PX.Objects.SO.SOShipment.shipmentNbr>>, And<SOShipmentDiscountDetail.orderType, Equal<Required<SOShipmentDiscountDetail.orderType>>, And<SOShipmentDiscountDetail.orderNbr, Equal<Required<SOShipmentDiscountDetail.orderNbr>>, And<SOShipmentDiscountDetail.type, Equal<Required<SOShipmentDiscountDetail.type>>, And<SOShipmentDiscountDetail.discountID, Equal<Required<SOShipmentDiscountDetail.discountID>>, And<SOShipmentDiscountDetail.discountSequenceID, Equal<Required<SOShipmentDiscountDetail.discountSequenceID>>>>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, new object[5]
    {
      (object) newTrace.OrderType,
      (object) newTrace.OrderNbr,
      (object) newTrace.Type,
      (object) newTrace.DiscountID,
      (object) newTrace.DiscountSequenceID
    }));
    if (trace != null)
    {
      trace.DiscountableQty = newTrace.DiscountableQty;
      trace.DiscountPct = newTrace.DiscountPct;
      trace.FreeItemID = newTrace.FreeItemID;
      trace.FreeItemQty = newTrace.FreeItemQty;
      this._discountEngine.UpdateDiscountDetail(((PXSelectBase) this.DiscountDetails).Cache, (PXSelectBase<SOShipmentDiscountDetail>) this.DiscountDetails, trace);
    }
    else
      this._discountEngine.InsertDiscountDetail(((PXSelectBase) this.DiscountDetails).Cache, (PXSelectBase<SOShipmentDiscountDetail>) this.DiscountDetails, newTrace);
  }

  protected void AdjustFreeItemLines()
  {
    foreach (PXResult<SOShipLine> pxResult in ((PXSelectBase<SOShipLine>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Select(Array.Empty<object>()))
    {
      SOShipLine line = PXResult<SOShipLine>.op_Implicit(pxResult);
      bool? nullable = line.IsFree;
      if (nullable.GetValueOrDefault())
      {
        nullable = line.ManualDisc;
        if (!nullable.GetValueOrDefault())
          this.AdjustFreeItemLines(line);
      }
    }
    ((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).View.RequestRefresh();
  }

  private void AdjustFreeItemLines(SOShipLine line)
  {
    if (this.isSkipAdjustFreeItemLines)
      return;
    PXResultset<SOShipmentDiscountDetail> pxResultset = ((PXSelectBase<SOShipmentDiscountDetail>) new PXSelect<SOShipmentDiscountDetail, Where<SOShipmentDiscountDetail.shipmentNbr, Equal<Current<PX.Objects.SO.SOShipment.shipmentNbr>>, And<SOShipmentDiscountDetail.freeItemID, Equal<Required<SOShipmentDiscountDetail.freeItemID>>, And<SOShipmentDiscountDetail.orderType, Equal<Required<SOShipmentDiscountDetail.orderType>>, And<SOShipmentDiscountDetail.orderNbr, Equal<Required<SOShipmentDiscountDetail.orderNbr>>>>>>>((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base)).Select(new object[3]
    {
      (object) line.InventoryID,
      (object) line.OrigOrderType,
      (object) line.OrigOrderNbr
    });
    if (pxResultset.Count == 0)
      return;
    Decimal? nullable1 = new Decimal?(0M);
    foreach (PXResult<SOShipmentDiscountDetail> pxResult in pxResultset)
    {
      SOShipmentDiscountDetail shipmentDiscountDetail = PXResult<SOShipmentDiscountDetail>.op_Implicit(pxResult);
      if (shipmentDiscountDetail.FreeItemID.HasValue)
      {
        Decimal? nullable2 = shipmentDiscountDetail.FreeItemQty;
        if (nullable2.HasValue)
        {
          nullable2 = shipmentDiscountDetail.FreeItemQty;
          if (nullable2.Value > 0M)
          {
            nullable2 = nullable1;
            Decimal? nullable3 = shipmentDiscountDetail.FreeItemQty;
            Decimal num = nullable3.Value;
            Decimal? nullable4;
            if (!nullable2.HasValue)
            {
              nullable3 = new Decimal?();
              nullable4 = nullable3;
            }
            else
              nullable4 = new Decimal?(nullable2.GetValueOrDefault() + num);
            nullable1 = nullable4;
          }
        }
      }
    }
    SOShipLine copy = PXCache<SOShipLine>.CreateCopy(line);
    copy.ShippedQty = nullable1;
    ((PXSelectBase<SOShipLine>) ((PXGraphExtension<SOShipmentEntry>) this).Base.FreeItems).Update(copy);
  }

  protected virtual bool AnySelected<TSelectedField>(PXCache cache) where TSelectedField : IBqlField
  {
    return cache.Cached.Cast<IBqlTable>().Any<IBqlTable>((Func<IBqlTable, bool>) (p => ((bool?) cache.GetValue<TSelectedField>((object) p)).GetValueOrDefault() && EnumerableExtensions.IsNotIn<PXEntryStatus>(cache.GetStatus((object) p), (PXEntryStatus) 3, (PXEntryStatus) 4)));
  }

  protected virtual bool SyncLineWithOrder(SOShipLine row)
  {
    Decimal? shippedQty = row.ShippedQty;
    Decimal num = 0M;
    if (shippedQty.GetValueOrDefault() == num & shippedQty.HasValue)
    {
      SOLine2 soLine2 = PXParentAttribute.SelectParent<SOLine2>(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache, (object) row);
      if (soLine2 != null)
      {
        int? siteId1 = soLine2.SiteID;
        int? siteId2 = row.SiteID;
        return siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue;
      }
    }
    return true;
  }

  protected virtual void SyncShipDateWithLinks(PX.Objects.SO.SOShipment shipment)
  {
    PXCache cache = ((PXSelectBase) this.OrderList).Cache;
    foreach (PXResult<PX.Objects.SO.SOOrderShipment> pxResult in ((PXSelectBase<PX.Objects.SO.SOOrderShipment>) this.OrderList).Select(Array.Empty<object>()))
    {
      PX.Objects.SO.SOOrderShipment soOrderShipment = PXResult<PX.Objects.SO.SOOrderShipment>.op_Implicit(pxResult);
      if (soOrderShipment.ShipmentType != "H")
      {
        cache.SetValue<PX.Objects.SO.SOOrderShipment.shipDate>((object) soOrderShipment, (object) shipment.ShipDate);
        GraphHelper.MarkUpdated(cache, (object) soOrderShipment, true);
      }
    }
  }

  protected virtual void UpdateShipmentCntr(PXCache sender, PX.Objects.SO.SOOrderShipment row, short? Counter)
  {
    PX.Objects.SO.SOOrder soOrder1 = PXParentAttribute.SelectParent<PX.Objects.SO.SOOrder>(sender, (object) row);
    short? nullable1;
    int? nullable2;
    bool? confirmed;
    int? nullable3;
    if (soOrder1 != null)
    {
      PX.Objects.SO.SOOrder soOrder2 = soOrder1;
      nullable1 = Counter;
      bool? nullable4 = (nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?()).GetValueOrDefault() == -1 ? new bool?(true) : new bool?();
      soOrder2.ShipmentDeleted = nullable4;
      PX.Objects.SO.SOOrder soOrder3 = soOrder1;
      int? shipmentCntr = soOrder3.ShipmentCntr;
      nullable1 = Counter;
      nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      soOrder3.ShipmentCntr = shipmentCntr.HasValue & nullable2.HasValue ? new int?(shipmentCntr.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new int?();
      confirmed = row.Confirmed;
      bool flag = false;
      if (confirmed.GetValueOrDefault() == flag & confirmed.HasValue)
      {
        PX.Objects.SO.SOOrder soOrder4 = soOrder1;
        nullable2 = soOrder4.OpenShipmentCntr;
        nullable1 = Counter;
        nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
        soOrder4.OpenShipmentCntr = nullable2.HasValue & nullable3.HasValue ? new int?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new int?();
      }
      ((PXSelectBase) this.soorder).Cache.Update((object) soOrder1);
    }
    SOOrderSite soOrderSite1 = PXParentAttribute.SelectParent<SOOrderSite>(sender, (object) row);
    if (soOrderSite1 == null)
      return;
    nullable1 = Counter;
    int? nullable5;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable5 = nullable2;
    }
    else
      nullable5 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable5;
    int num = 0;
    if (nullable3.GetValueOrDefault() == num & nullable3.HasValue)
      return;
    SOOrderSite soOrderSite2 = soOrderSite1;
    nullable3 = soOrderSite2.ShipmentCntr;
    nullable1 = Counter;
    nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    soOrderSite2.ShipmentCntr = nullable3.HasValue & nullable2.HasValue ? new int?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new int?();
    confirmed = row.Confirmed;
    bool flag1 = false;
    if (confirmed.GetValueOrDefault() == flag1 & confirmed.HasValue)
    {
      SOOrderSite soOrderSite3 = soOrderSite1;
      nullable2 = soOrderSite3.OpenShipmentCntr;
      nullable1 = Counter;
      nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      soOrderSite3.OpenShipmentCntr = nullable2.HasValue & nullable3.HasValue ? new int?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new int?();
    }
    ((PXSelectBase<SOOrderSite>) this.OrderSite).Update(soOrderSite1);
  }

  private void UpdateManualFreightCost(
    PX.Objects.SO.SOShipment shipment,
    PX.Objects.SO.SOOrderShipment sOOrderShipment,
    Decimal? oldShipmentQty,
    Decimal? newShipmentQty,
    bool newOrderSelected = false)
  {
    PX.Objects.SO.SOOrder soOrder = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) this.soorder).Select(new object[2]
    {
      (object) sOOrderShipment.OrderType,
      (object) sOOrderShipment.OrderNbr
    }));
    if (shipment == null || soOrder == null)
      return;
    Decimal? nullable1 = soOrder.OrderQty;
    if (!nullable1.HasValue)
      return;
    nullable1 = soOrder.OrderQty;
    Decimal num1 = 0M;
    if (!(nullable1.GetValueOrDefault() > num1 & nullable1.HasValue))
      return;
    PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, soOrder.ShipVia);
    if (carrier == null || !(carrier.CalcMethod == "M"))
      return;
    if (((PXSelectBase<SOSetup>) ((PXGraphExtension<SOShipmentEntry>) this).Base.sosetup).Current?.FreightAllocation == "A")
    {
      int? shipmentCntr = soOrder.ShipmentCntr;
      int num2 = 1;
      if (shipmentCntr.GetValueOrDefault() > num2 & shipmentCntr.HasValue)
        return;
    }
    if (((PXSelectBase<SOSetup>) ((PXGraphExtension<SOShipmentEntry>) this).Base.sosetup).Current == null)
      return;
    PX.Objects.SO.SOShipment copy = PXCache<PX.Objects.SO.SOShipment>.CreateCopy(shipment);
    Decimal? freightCost1 = soOrder.FreightCost;
    Decimal? curyFreightCost = copy.CuryFreightCost;
    if (((PXSelectBase<SOSetup>) ((PXGraphExtension<SOShipmentEntry>) this).Base.sosetup).Current.FreightAllocation == "P")
    {
      SOShipmentEntry graph1 = ((PXGraphExtension<SOShipmentEntry>) this).Base;
      Decimal? nullable2 = oldShipmentQty;
      Decimal? nullable3 = soOrder.OrderQty;
      nullable1 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() / nullable3.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable4 = freightCost1;
      Decimal? nullable5;
      if (!(nullable1.HasValue & nullable4.HasValue))
      {
        nullable3 = new Decimal?();
        nullable5 = nullable3;
      }
      else
        nullable5 = new Decimal?(nullable1.GetValueOrDefault() * nullable4.GetValueOrDefault());
      nullable3 = nullable5;
      Decimal valueOrDefault1 = nullable3.GetValueOrDefault();
      Decimal num3 = PXCurrencyAttribute.BaseRound((PXGraph) graph1, valueOrDefault1);
      SOShipmentEntry graph2 = ((PXGraphExtension<SOShipmentEntry>) this).Base;
      nullable3 = newShipmentQty;
      nullable2 = soOrder.OrderQty;
      nullable1 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() / nullable2.GetValueOrDefault()) : new Decimal?();
      nullable4 = freightCost1;
      Decimal? nullable6;
      if (!(nullable1.HasValue & nullable4.HasValue))
      {
        nullable2 = new Decimal?();
        nullable6 = nullable2;
      }
      else
        nullable6 = new Decimal?(nullable1.GetValueOrDefault() * nullable4.GetValueOrDefault());
      nullable2 = nullable6;
      Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
      Decimal num4 = PXCurrencyAttribute.BaseRound((PXGraph) graph2, valueOrDefault2);
      nullable1 = curyFreightCost;
      Decimal num5 = -num3 + num4;
      Decimal? nullable7;
      if (!nullable1.HasValue)
      {
        nullable4 = new Decimal?();
        nullable7 = nullable4;
      }
      else
        nullable7 = new Decimal?(nullable1.GetValueOrDefault() + num5);
      Decimal? nullable8 = nullable7;
      copy.CuryFreightCost = new Decimal?(Math.Max(nullable8.Value, 0M));
      if (!EnumerableExtensions.IsNotIn<PXEntryStatus>(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Cache.GetStatus((object) shipment), (PXEntryStatus) 3, (PXEntryStatus) 4))
        return;
      ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Update(copy);
    }
    else
    {
      if (!newOrderSelected)
        return;
      nullable1 = curyFreightCost;
      Decimal? freightCost2 = soOrder.FreightCost;
      Decimal? nullable9 = nullable1.HasValue & freightCost2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + freightCost2.GetValueOrDefault()) : new Decimal?();
      copy.CuryFreightCost = new Decimal?(Math.Max(nullable9.Value, 0M));
      ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Update(copy);
    }
  }

  protected virtual void RestoreCustomerOrderNbr()
  {
    PX.Objects.SO.SOShipment current = ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current;
    if (current == null || current.OrderCntr.GetValueOrDefault() != 1 || current.CustomerOrderNbr != null)
      return;
    PX.Objects.SO.SOOrderShipment soOrderShipment = PXResultset<PX.Objects.SO.SOOrderShipment>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrderShipment>) this.OrderListSimple).Select(Array.Empty<object>()));
    if (soOrderShipment == null)
      return;
    PX.Objects.SO.SOOrder soOrder = PXParentAttribute.SelectParent<PX.Objects.SO.SOOrder>(((PXSelectBase) this.OrderListSimple).Cache, (object) soOrderShipment);
    if (string.IsNullOrEmpty(soOrder.CustomerOrderNbr))
      return;
    current.CustomerOrderNbr = soOrder.CustomerOrderNbr;
    ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Update(current);
  }

  protected virtual void ResetManualPackageFlag()
  {
    PX.Objects.SO.SOShipment current = ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current;
    if (current == null)
      return;
    int? orderCntr = current.OrderCntr;
    int num = 0;
    if (!(orderCntr.GetValueOrDefault() == num & orderCntr.HasValue))
      return;
    current.IsManualPackage = new bool?();
    ((PXSelectBase<PX.Objects.SO.SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Update(current);
  }

  protected virtual void ValidateLineType(PX.Objects.SO.SOLine line, PX.Objects.IN.InventoryItem item, string message)
  {
    if (item.KitItem.GetValueOrDefault() && !item.StkItem.GetValueOrDefault() && line.LineType == "GN")
      throw new PXException(message, new object[2]
      {
        (object) line.LineNbr,
        (object) line.OrderNbr
      });
  }

  protected virtual Decimal? GetQtyHardAvailFromSiteStatus(PXGraph docgraph, PX.Objects.SO.SOLineSplit split)
  {
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter1 = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter();
    statusByCostCenter1.InventoryID = split.InventoryID;
    statusByCostCenter1.SiteID = split.SiteID;
    statusByCostCenter1.SubItemID = split.SubItemID;
    statusByCostCenter1.CostCenterID = split.CostCenterID;
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter2 = statusByCostCenter1;
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter3 = GraphHelper.Caches<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>(docgraph).Insert(statusByCostCenter2);
    INSiteStatusByCostCenter statusByCostCenter4 = INSiteStatusByCostCenter.PK.Find(docgraph, split.InventoryID, split.SubItemID, split.SiteID, split.CostCenterID);
    Decimal? qtyHardAvail = statusByCostCenter3.QtyHardAvail;
    Decimal valueOrDefault = ((Decimal?) statusByCostCenter4?.QtyHardAvail).GetValueOrDefault();
    return !qtyHardAvail.HasValue ? new Decimal?() : new Decimal?(qtyHardAvail.GetValueOrDefault() + valueOrDefault);
  }

  protected virtual SOOrderExtension.OrigSOLineSplitSet CollectShipmentOrigSOLineSplits()
  {
    SOOrderExtension.OrigSOLineSplitSet origSoLineSplitSet = new SOOrderExtension.OrigSOLineSplitSet();
    PXSelectBase<SOShipLine> pxSelectBase = (PXSelectBase<SOShipLine>) new PXSelectReadonly<SOShipLine, Where<SOShipLine.shipmentNbr, Equal<Current<PX.Objects.SO.SOShipment.shipmentNbr>>>>((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base);
    using (new PXFieldScope(((PXSelectBase) pxSelectBase).View, new Type[6]
    {
      typeof (SOShipLine.shipmentNbr),
      typeof (SOShipLine.lineNbr),
      typeof (SOShipLine.origOrderType),
      typeof (SOShipLine.origOrderNbr),
      typeof (SOShipLine.origLineNbr),
      typeof (SOShipLine.origSplitLineNbr)
    }))
    {
      foreach (PXResult<SOShipLine> pxResult in pxSelectBase.Select(Array.Empty<object>()))
      {
        SOShipLine soShipLine = PXResult<SOShipLine>.op_Implicit(pxResult);
        origSoLineSplitSet.Add(soShipLine);
      }
    }
    foreach (SOShipLine soShipLine in ((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache.Deleted)
      origSoLineSplitSet.Remove(soShipLine);
    foreach (SOShipLine soShipLine in ((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache.Inserted)
      origSoLineSplitSet.Add(soShipLine);
    return origSoLineSplitSet;
  }

  public class SkipAdjustFreeItemLinesScope : IDisposable
  {
    private readonly SOOrderExtension parent;

    public SkipAdjustFreeItemLinesScope(SOOrderExtension shipmentEntry)
    {
      this.parent = shipmentEntry;
      this.parent.isSkipAdjustFreeItemLines = true;
    }

    void IDisposable.Dispose() => this.parent.isSkipAdjustFreeItemLines = false;
  }

  private struct DiscKey(string discID, string discSeqID, int freeItemID)
  {
    private readonly string discID = discID;
    private readonly string discSeqID = discSeqID;
    private readonly int freeItemID = freeItemID;

    public string DiscID => this.discID;

    public string DiscSeqID => this.discSeqID;

    public int FreeItemID => this.freeItemID;
  }

  public class OrigSOLineSplitSet : HashSet<SOShipLine>
  {
    private readonly SOShipLine _shipLine = new SOShipLine();

    public OrigSOLineSplitSet()
      : base((IEqualityComparer<SOShipLine>) new SOOrderExtension.OrigSOLineSplitSet.SplitComparer())
    {
    }

    public bool Contains(PX.Objects.SO.SOLineSplit sls)
    {
      this._shipLine.OrigOrderType = sls.OrderType;
      this._shipLine.OrigOrderNbr = sls.OrderNbr;
      this._shipLine.OrigLineNbr = sls.LineNbr;
      this._shipLine.OrigSplitLineNbr = sls.SplitLineNbr;
      return this.Contains(this._shipLine);
    }

    public class SplitComparer : IEqualityComparer<SOShipLine>
    {
      public bool Equals(SOShipLine a, SOShipLine b)
      {
        if (a.OrigOrderType == b.OrigOrderType && a.OrigOrderNbr == b.OrigOrderNbr)
        {
          int? nullable = a.OrigLineNbr;
          int? origLineNbr = b.OrigLineNbr;
          if (nullable.GetValueOrDefault() == origLineNbr.GetValueOrDefault() & nullable.HasValue == origLineNbr.HasValue)
          {
            int? origSplitLineNbr = a.OrigSplitLineNbr;
            nullable = b.OrigSplitLineNbr;
            return origSplitLineNbr.GetValueOrDefault() == nullable.GetValueOrDefault() & origSplitLineNbr.HasValue == nullable.HasValue;
          }
        }
        return false;
      }

      public int GetHashCode(SOShipLine a)
      {
        int num1 = 17 * 23;
        int? hashCode1 = a.OrigOrderType?.GetHashCode();
        int num2 = (hashCode1.HasValue ? new int?(num1 + hashCode1.GetValueOrDefault()) : new int?()).GetValueOrDefault() * 23;
        int? hashCode2 = a.OrigOrderNbr?.GetHashCode();
        int num3 = (hashCode2.HasValue ? new int?(num2 + hashCode2.GetValueOrDefault()) : new int?()).GetValueOrDefault() * 23;
        int? nullable = a.OrigLineNbr;
        int hashCode3 = nullable.GetHashCode();
        int num4 = (num3 + hashCode3) * 23;
        nullable = a.OrigSplitLineNbr;
        int hashCode4 = nullable.GetHashCode();
        return num4 + hashCode4;
      }
    }
  }
}
