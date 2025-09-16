// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOCreateShipment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.AR.MigrationMode;
using PX.Objects.CA;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO;

[TableAndChartDashboardType]
public class SOCreateShipment : PXGraph<SOCreateShipment>
{
  public PXCancel<SOOrderFilter> Cancel;
  public PXAction<SOOrderFilter> viewDocument;
  public PXFilter<SOOrderFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<SOOrder, SOOrderFilter> Orders;
  public PXSelect<PX.Objects.IN.INSite> INSites;
  public PXSelect<PX.Objects.CS.Carrier> Carriers;
  protected bool _ActionChanged;

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<SOOrder>) this.Orders).Current != null)
    {
      SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
      ((PXSelectBase<SOOrder>) instance.Document).Current = PXResultset<SOOrder>.op_Implicit(((PXSelectBase<SOOrder>) instance.Document).Search<SOOrder.orderNbr>((object) ((PXSelectBase<SOOrder>) this.Orders).Current.OrderNbr, new object[1]
      {
        (object) ((PXSelectBase<SOOrder>) this.Orders).Current.OrderType
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Order");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void INSite_Descr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void Carrier_Description_CacheAttached(PXCache sender)
  {
  }

  [PXInt]
  public virtual void SOOrder_EmployeeID_CacheAttached(PXCache sender)
  {
  }

  [PXDecimal]
  public virtual void SOOrder_CuryDocBal_CacheAttached(PXCache sender)
  {
  }

  public SOCreateShipment()
  {
    ARSetupNoMigrationMode.EnsureMigrationModeDisabled((PXGraph) this);
    ((PXProcessingBase<SOOrder>) this.Orders).SetSelected<SOOrder.selected>();
  }

  public virtual void SOOrderFilter_Action_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    SOOrderFilter row = (SOOrderFilter) e.Row;
    if (!EnumerableExtensions.IsIn<string>(row.Action, "SO301000$createChildOrders", "SO301000$processExpiredOrder"))
      return;
    sender.SetValueExt<SOOrderFilter.dateSel>((object) row, (object) "O");
    sender.SetValueExt<SOOrderFilter.carrierPluginID>((object) row, (object) null);
    sender.SetValueExt<SOOrderFilter.shipVia>((object) row, (object) null);
  }

  public virtual void SOOrderFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    SOOrderFilter row = (SOOrderFilter) e.Row;
    if (row == null)
      return;
    bool flag1 = row.Action == "SO301000$createShipmentIssue";
    bool flag2 = row.Action == "SO301000$OrchestrateOrder";
    PXUIFieldAttribute.SetVisible<SOOrderFilter.shipmentDate>(sender, (object) null, flag1 | flag2);
    PXUIFieldAttribute.SetVisible<SOOrderFilter.siteID>(sender, (object) null, flag1 | flag2);
    PXUIFieldAttribute.SetVisible<SOOrder.shipSeparately>(((PXSelectBase) this.Orders).Cache, (object) null, flag1);
    bool flag3 = row.Action == "SO301000$createChildOrders";
    bool flag4 = flag3 || row.Action == "SO301000$processExpiredOrder";
    PXUIFieldAttribute.SetVisible<SOOrderFilter.schedOrderDate>(sender, (object) null, flag3);
    PXUIFieldAttribute.SetEnabled<SOOrderFilter.schedOrderDate>(sender, (object) null, flag3);
    PXUIFieldAttribute.SetEnabled<SOOrderFilter.dateSel>(sender, (object) null, !flag4);
    PXUIFieldAttribute.SetVisible<SOOrderFilter.carrierPluginID>(sender, (object) null, !flag4);
    PXUIFieldAttribute.SetEnabled<SOOrderFilter.carrierPluginID>(sender, (object) null, !flag4);
    PXUIFieldAttribute.SetVisible<SOOrderFilter.shipVia>(sender, (object) null, !flag4);
    PXUIFieldAttribute.SetEnabled<SOOrderFilter.shipVia>(sender, (object) null, !flag4);
    PXUIFieldAttribute.SetVisible<SOOrder.minSchedOrderDate>(((PXSelectBase) this.Orders).Cache, (object) null, flag4);
    PXUIFieldAttribute.SetVisible<SOOrder.requestDate>(((PXSelectBase) this.Orders).Cache, (object) null, !flag4);
    PXUIFieldAttribute.SetVisible<SOOrder.shipDate>(((PXSelectBase) this.Orders).Cache, (object) null, !flag4);
    PXUIFieldAttribute.SetVisible<SOOrder.expireDate>(((PXSelectBase) this.Orders).Cache, (object) null, flag4);
    PXUIFieldAttribute.SetVisible<SOOrder.orchestrationStatus>(((PXSelectBase) this.Orders).Cache, (object) null, !flag4);
    if (string.IsNullOrEmpty(row.Action))
      return;
    ((PXProcessingBase<SOOrder>) this.Orders).SetProcessWorkflowAction(row.Action, new object[3]
    {
      (object) (flag1 ? row.ShipmentDate : (flag3 ? row.SchedOrderDate : row.EndDate)),
      (object) row.SiteID,
      (object) row.EndDate
    });
  }

  public virtual void SOOrderFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    this._ActionChanged = !sender.ObjectsEqual<SOOrderFilter.action>(e.Row, e.OldRow);
  }

  public virtual int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    if (viewName != "Orders")
      return ((PXGraph) this).ExecuteUpdate(viewName, keys, values, parameters);
    Dictionary<string, object> dictionary = new Dictionary<string, object>(keys.Count + 1);
    foreach (object key in (IEnumerable) values.Keys)
    {
      if (keys.Contains(key))
        dictionary.Add(key.ToString(), values[key]);
      else if (string.Equals(key.ToString(), ((PXSelectBase) this.Orders).Cache.GetField(typeof (SOOrder.selected)), StringComparison.InvariantCultureIgnoreCase))
        dictionary.Add(key.ToString(), values[key]);
    }
    return ((PXGraph) this).ExecuteUpdate(viewName, keys, (IDictionary) dictionary, parameters);
  }

  public virtual IEnumerable orders()
  {
    SOCreateShipment soCreateShipment = this;
    PXUIFieldAttribute.SetDisplayName<SOOrder.customerID>(((PXGraph) soCreateShipment).Caches[typeof (SOOrder)], "Customer ID");
    SOOrderFilter copy = PXCache<SOOrderFilter>.CreateCopy(((PXSelectBase<SOOrderFilter>) soCreateShipment.Filter).Current);
    if (!(copy.Action == "<SELECT>"))
    {
      if (soCreateShipment._ActionChanged)
        ((PXSelectBase) soCreateShipment.Orders).Cache.Clear();
      PXSelectBase<SOOrder> selectCommand = soCreateShipment.GetSelectCommand(copy);
      soCreateShipment.AddCommonFilters(copy, selectCommand);
      int startRow = PXView.StartRow;
      int num = 0;
      foreach (PXResult<SOOrder> pxResult in ((PXSelectBase) selectCommand).View.Select((object[]) null, (object[]) null, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num))
      {
        SOOrder soOrder1 = PXResult<SOOrder>.op_Implicit(pxResult);
        SOOrder soOrder2;
        if ((soOrder2 = (SOOrder) ((PXSelectBase) soCreateShipment.Orders).Cache.Locate((object) soOrder1)) != null)
          soOrder1.Selected = soOrder2.Selected;
        yield return (object) soOrder1;
      }
      PXView.StartRow = 0;
      ((PXSelectBase) soCreateShipment.Orders).Cache.IsDirty = false;
    }
  }

  protected virtual PXSelectBase<SOOrder> GetSelectCommand(SOOrderFilter filter)
  {
    string action = filter.Action;
    if (action != null)
    {
      switch (action.Length)
      {
        case 20:
          if (action == "SO301000$cancelOrder")
            return this.BuildCommandCancelOrder();
          break;
        case 22:
          if (action == "SO301000$completeOrder")
            return this.BuildCommandCompleteOrder();
          break;
        case 23:
          if (action == "SO301000$prepareInvoice")
            return this.BuildCommandPrepareInvoice();
          break;
        case 25:
          if (action == "SO301000$OrchestrateOrder")
            return this.BuildCommandOrchestrateOrder(filter);
          break;
        case 26:
          if (action == "SO301000$createChildOrders")
            return this.BuildCommandCreateChildOrders();
          break;
        case 28:
          switch (action[9])
          {
            case 'c':
              if (action == "SO301000$createShipmentIssue")
                return this.BuildCommandCreateShipment(filter);
              break;
            case 'p':
              if (action == "SO301000$processExpiredOrder")
                return this.BuildCommandProcessExpiredOrder();
              break;
          }
          break;
        case 32 /*0x20*/:
          if (action == "SO301000$createAndCapturePayment")
            return this.BuildCommandCreateCCAndEFTPayment();
          break;
        case 34:
          if (action == "SO301000$createAndAuthorizePayment")
            return this.BuildCommandCreateCCPayment();
          break;
      }
    }
    return this.BuildCommandDefault();
  }

  protected virtual PXSelectBase<SOOrder> BuildCommandCreateShipment(SOOrderFilter filter)
  {
    return (PXSelectBase<SOOrder>) new FbqlSelect<SelectFromBase<SOOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.AR.Customer>.On<SOOrder.FK.Customer>.SingleTableOnly>, FbqlJoins.Left<PX.Objects.CS.Carrier>.On<SOOrder.FK.Carrier>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrder.status, NotEqual<SOOrderStatus.shipping>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Customer.bAccountID, IsNull>>>>.Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>.And<Exists<SelectFromBase<INItemPlanToShip, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.INSite>.On<INItemPlanToShip.FK.Site>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemPlanToShip.refNoteID, Equal<SOOrder.noteID>>>>, And<BqlOperand<INItemPlanToShip.reverse, IBqlBool>.IsEqual<False>>>, And<BqlOperand<INItemPlanToShip.inclQtySOBackOrdered, IBqlShort>.IsEqual<short0>>>, And<Match<PX.Objects.IN.INSite, Current<AccessInfo.userName>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SOOrderFilter.siteID>, IsNull>>>>.Or<BqlOperand<INItemPlanToShip.siteID, IBqlInt>.IsEqual<BqlField<SOOrderFilter.siteID, IBqlInt>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SOOrderFilter.dateSel>, NotEqual<SOOrderFilter.dateSel.shipDate>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SOOrderFilter.startDate>, IsNull>>>>.Or<BqlOperand<INItemPlanToShip.planDate, IBqlDateTime>.IsGreaterEqual<BqlField<SOOrderFilter.startDate, IBqlDateTime>.FromCurrent>>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SOOrderFilter.endDate>, IsNull>>>>.Or<BqlOperand<INItemPlanToShip.planDate, IBqlDateTime>.IsLessEqual<BqlField<SOOrderFilter.endDate, IBqlDateTime>.FromCurrent>>>>>>>>>>, SOOrder>.View((PXGraph) this);
  }

  protected virtual PXSelectBase<SOOrder> BuildCommandPrepareInvoice()
  {
    PXSelectJoinGroupBy<SOOrder, InnerJoin<SOOrderType, On<SOOrderType.orderType, Equal<SOOrder.orderType>, And<SOOrderType.aRDocType, NotEqual<ARDocType.noUpdate>>>, LeftJoin<PX.Objects.CS.Carrier, On<SOOrder.shipVia, Equal<PX.Objects.CS.Carrier.carrierID>>, LeftJoin<SOOrderShipment, On<SOOrderShipment.orderType, Equal<SOOrder.orderType>, And<SOOrderShipment.orderNbr, Equal<SOOrder.orderNbr>>>, LeftJoinSingleTable<PX.Objects.AR.ARInvoice, On<PX.Objects.AR.ARInvoice.docType, Equal<SOOrderShipment.invoiceType>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<SOOrderShipment.invoiceNbr>>>, LeftJoinSingleTable<PX.Objects.AR.Customer, On<SOOrder.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>>>>>>, Where<SOOrder.hold, Equal<boolFalse>, And<SOOrder.cancelled, Equal<boolFalse>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>, Aggregate<GroupBy<SOOrder.orderType, GroupBy<SOOrder.orderNbr, GroupBy<SOOrder.approved>>>>> selectJoinGroupBy = new PXSelectJoinGroupBy<SOOrder, InnerJoin<SOOrderType, On<SOOrderType.orderType, Equal<SOOrder.orderType>, And<SOOrderType.aRDocType, NotEqual<ARDocType.noUpdate>>>, LeftJoin<PX.Objects.CS.Carrier, On<SOOrder.shipVia, Equal<PX.Objects.CS.Carrier.carrierID>>, LeftJoin<SOOrderShipment, On<SOOrderShipment.orderType, Equal<SOOrder.orderType>, And<SOOrderShipment.orderNbr, Equal<SOOrder.orderNbr>>>, LeftJoinSingleTable<PX.Objects.AR.ARInvoice, On<PX.Objects.AR.ARInvoice.docType, Equal<SOOrderShipment.invoiceType>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<SOOrderShipment.invoiceNbr>>>, LeftJoinSingleTable<PX.Objects.AR.Customer, On<SOOrder.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>>>>>>, Where<SOOrder.hold, Equal<boolFalse>, And<SOOrder.cancelled, Equal<boolFalse>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>, Aggregate<GroupBy<SOOrder.orderType, GroupBy<SOOrder.orderNbr, GroupBy<SOOrder.approved>>>>>((PXGraph) this);
    if (PXAccess.FeatureInstalled<FeaturesSet.inventory>())
      ((PXSelectBase<SOOrder>) selectJoinGroupBy).WhereAnd<Where<Sub<Sub<Sub<SOOrder.shipmentCntr, SOOrder.openShipmentCntr>, SOOrder.billedCntr>, SOOrder.releasedCntr>, Greater<short0>, Or2<Where2<Where<SOOrder.orderQty, Equal<decimal0>, Or<SOOrder.openLineCntr, Equal<int0>, And<SOOrder.isLegacyMiscBilling, Equal<False>>>>, And<Where<SOOrder.curyUnbilledMiscTot, Greater<decimal0>, Or<SOOrder.curyUnbilledMiscTot, Equal<decimal0>, And<SOOrder.unbilledOrderQty, Greater<decimal0>>>>>>, Or<Where<Not<SOBehavior.RequireShipment<SOOrder.behavior, SOOrderType.requireShipping>>>>>>>();
    else
      ((PXSelectBase<SOOrder>) selectJoinGroupBy).WhereAnd<Where<SOOrder.isLegacyMiscBilling, Equal<False>, And2<Where<SOOrder.curyUnbilledMiscTot, Greater<decimal0>, Or<SOOrder.curyUnbilledMiscTot, Equal<decimal0>, And<SOOrder.unbilledOrderQty, Greater<decimal0>>>>, Or<Where<SOOrder.curyUnbilledMiscTot, Greater<decimal0>, And<SOOrderShipment.shipmentNbr, IsNull, Or<Where<PX.Objects.AR.ARInvoice.refNbr, IsNull, And<Not<SOBehavior.RequireShipment<SOOrder.behavior, SOOrderType.requireShipping>>>>>>>>>>>();
    return (PXSelectBase<SOOrder>) selectJoinGroupBy;
  }

  protected virtual PXSelectBase<SOOrder> BuildCommandProcessExpiredOrder()
  {
    PXSelectBase<SOOrder> pxSelectBase = this.BuildCommandDefault();
    pxSelectBase.WhereAnd<Where<SOOrder.expireDate, Less<Current<AccessInfo.businessDate>>>>();
    return pxSelectBase;
  }

  protected virtual PXSelectBase<SOOrder> BuildCommandCancelOrder()
  {
    return (PXSelectBase<SOOrder>) new FbqlSelect<SelectFromBase<SOOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOOrderType>.On<SOOrder.FK.OrderType>>, FbqlJoins.Left<PX.Objects.CS.Carrier>.On<SOOrder.FK.Carrier>>, FbqlJoins.Left<PX.Objects.AR.Customer>.On<SOOrder.FK.Customer>.SingleTableOnly>, FbqlJoins.Left<SOOrderShipment>.On<SOOrderShipment.FK.Order>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrder.behavior, NotEqual<SOBehavior.bL>>>>, And<BqlOperand<SOOrderShipment.orderNbr, IBqlString>.IsNull>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Customer.bAccountID, IsNull>>>>.Or<Match<PX.Objects.AR.Customer, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>, SOOrder>.View((PXGraph) this);
  }

  protected virtual PXSelectBase<SOOrder> BuildCommandCompleteOrder()
  {
    return (PXSelectBase<SOOrder>) new FbqlSelect<SelectFromBase<SOOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOOrderType>.On<SOOrder.FK.OrderType>>, FbqlJoins.Left<PX.Objects.CS.Carrier>.On<SOOrder.FK.Carrier>>, FbqlJoins.Left<PX.Objects.AR.Customer>.On<SOOrder.FK.Customer>.SingleTableOnly>>.Where<BqlChainableConditionLite<Not<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrder.completed, Equal<True>>>>, Or<BqlOperand<SOOrder.shipmentCntr, IBqlInt>.IsEqual<Zero>>>>.Or<BqlOperand<SOOrder.openShipmentCntr, IBqlInt>.IsGreater<Zero>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Customer.bAccountID, IsNull>>>>.Or<Match<PX.Objects.AR.Customer, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>, SOOrder>.View((PXGraph) this);
  }

  protected virtual PXSelectBase<SOOrder> BuildCommandDefault()
  {
    return (PXSelectBase<SOOrder>) new PXSelectJoin<SOOrder, LeftJoin<PX.Objects.CS.Carrier, On<SOOrder.shipVia, Equal<PX.Objects.CS.Carrier.carrierID>>, LeftJoinSingleTable<PX.Objects.AR.Customer, On<SOOrder.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>>>, Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>((PXGraph) this);
  }

  protected virtual PXSelectBase<SOOrder> BuildCommandCreatePayment()
  {
    PXSelectBase<SOOrder> payment = this.BuildCommandDefault();
    payment.Join<InnerJoin<SOOrderType, On<SOOrder.FK.OrderType>>>();
    payment.Join<InnerJoin<PX.Objects.CA.PaymentMethod, On<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<SOOrder.paymentMethodID>>, InnerJoin<PX.Objects.AR.CustomerPaymentMethod, On<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<SOOrder.pMInstanceID>>, InnerJoin<CCProcessingCenter, On<CCProcessingCenter.processingCenterID, Equal<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID>>>>>>();
    payment.WhereAnd<Where<SOOrderType.aRDocType, Equal<ARDocType.invoice>, And<PX.Objects.CA.PaymentMethod.isActive, Equal<True>, And<PX.Objects.CA.PaymentMethod.useForAR, Equal<True>, And<SOOrder.pMInstanceID, IsNotNull, And<SOOrder.curyUnpaidBalance, Greater<decimal0>, And<CCProcessingCenter.isExternalAuthorizationOnly, NotEqual<True>>>>>>>>();
    return payment;
  }

  protected virtual PXSelectBase<SOOrder> BuildCommandCreateCCAndEFTPayment()
  {
    PXSelectBase<SOOrder> payment = this.BuildCommandCreatePayment();
    payment.WhereAnd<Where<PX.Objects.CA.PaymentMethod.paymentType, In3<PaymentMethodType.creditCard, PaymentMethodType.eft>>>();
    return payment;
  }

  protected virtual PXSelectBase<SOOrder> BuildCommandCreateCCPayment()
  {
    PXSelectBase<SOOrder> payment = this.BuildCommandCreatePayment();
    payment.WhereAnd<Where<PX.Objects.CA.PaymentMethod.paymentType, Equal<PaymentMethodType.creditCard>>>();
    return payment;
  }

  protected virtual PXSelectBase<SOOrder> BuildCommandCreateChildOrders()
  {
    PXSelectBase<SOOrder> childOrders = this.BuildCommandDefault();
    childOrders.WhereAnd<Where<SOOrder.minSchedOrderDate, LessEqual<Current<SOOrderFilter.schedOrderDate>>>>();
    return childOrders;
  }

  protected virtual PXSelectBase<SOOrder> BuildCommandOrchestrateOrder(SOOrderFilter filter)
  {
    return (PXSelectBase<SOOrder>) new FbqlSelect<SelectFromBase<SOOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.AR.Customer>.On<SOOrder.FK.Customer>.SingleTableOnly>, FbqlJoins.Left<PX.Objects.CS.Carrier>.On<SOOrder.FK.Carrier>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrder.orchestrationStrategy, NotEqual<OrchestrationStrategies.doNotOrchestrate>>>>, And<BqlOperand<SOOrder.orchestrationStatus, IBqlString>.IsIn<OrchestrationStatus.newOrchestration, OrchestrationStatus.unsuccessful>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Customer.bAccountID, IsNull>>>>.Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>.And<Exists<SelectFromBase<INItemPlanToShip, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.INSite>.On<INItemPlanToShip.FK.Site>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemPlanToShip.refNoteID, Equal<SOOrder.noteID>>>>, And<BqlOperand<INItemPlanToShip.reverse, IBqlBool>.IsEqual<False>>>, And<Match<PX.Objects.IN.INSite, Current<AccessInfo.userName>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SOOrderFilter.siteID>, IsNull>>>>.Or<BqlOperand<INItemPlanToShip.siteID, IBqlInt>.IsEqual<BqlField<SOOrderFilter.siteID, IBqlInt>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SOOrderFilter.dateSel>, NotEqual<SOOrderFilter.dateSel.shipDate>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SOOrderFilter.startDate>, IsNull>>>>.Or<BqlOperand<INItemPlanToShip.planDate, IBqlDateTime>.IsGreaterEqual<BqlField<SOOrderFilter.startDate, IBqlDateTime>.FromCurrent>>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SOOrderFilter.endDate>, IsNull>>>>.Or<BqlOperand<INItemPlanToShip.planDate, IBqlDateTime>.IsLessEqual<BqlField<SOOrderFilter.endDate, IBqlDateTime>.FromCurrent>>>>>>>>>>, SOOrder>.View((PXGraph) this);
  }

  protected virtual void AddCommonFilters(SOOrderFilter filter, PXSelectBase<SOOrder> cmd)
  {
    bool flag = filter.Action == "SO301000$createShipmentIssue";
    cmd.WhereAnd<Where<WorkflowAction.IsEnabled<SOOrder, SOOrderFilter.action>>>();
    if (filter.EndDate.HasValue)
    {
      switch (filter.DateSel)
      {
        case "S":
          if (!flag)
          {
            cmd.WhereAnd<Where<SOOrder.shipDate, LessEqual<Current<SOOrderFilter.endDate>>>>();
            break;
          }
          break;
        case "C":
          cmd.WhereAnd<Where<SOOrder.cancelDate, LessEqual<Current<SOOrderFilter.endDate>>>>();
          break;
        case "O":
          cmd.WhereAnd<Where<SOOrder.orderDate, LessEqual<Current<SOOrderFilter.endDate>>>>();
          break;
      }
    }
    if (filter.StartDate.HasValue)
    {
      switch (filter.DateSel)
      {
        case "S":
          if (!flag)
          {
            cmd.WhereAnd<Where<SOOrder.shipDate, GreaterEqual<Current<SOOrderFilter.startDate>>>>();
            break;
          }
          break;
        case "C":
          cmd.WhereAnd<Where<SOOrder.cancelDate, GreaterEqual<Current<SOOrderFilter.startDate>>>>();
          break;
        case "O":
          cmd.WhereAnd<Where<SOOrder.orderDate, GreaterEqual<Current<SOOrderFilter.startDate>>>>();
          break;
      }
    }
    if (!string.IsNullOrEmpty(filter.CarrierPluginID))
      cmd.WhereAnd<Where<PX.Objects.CS.Carrier.carrierPluginID, Equal<Current<SOOrderFilter.carrierPluginID>>>>();
    if (!string.IsNullOrEmpty(filter.ShipVia))
      cmd.WhereAnd<Where<SOOrder.shipVia, Equal<Current<SOOrderFilter.shipVia>>>>();
    if (!filter.CustomerID.HasValue)
      return;
    cmd.WhereAnd<Where<SOOrder.customerID, Equal<Current<SOOrderFilter.customerID>>>>();
  }

  public class WellKnownActions
  {
    public class SOOrderScreen
    {
      public const string ScreenID = "SO301000";
      public const string CreateChildOrders = "SO301000$createChildOrders";
      public const string CreateShipment = "SO301000$createShipmentIssue";
      public const string OpenOrder = "SO301000$openOrder";
      public const string ReleaseFromCreditHold = "SO301000$releaseFromCreditHold";
      public const string PrepareInvoice = "SO301000$prepareInvoice";
      public const string CancelOrder = "SO301000$cancelOrder";
      public const string ProcessExpiredOrder = "SO301000$processExpiredOrder";
      public const string CompleteOrder = "SO301000$completeOrder";
      public const string CreateAndAuthorizePayment = "SO301000$createAndAuthorizePayment";
      public const string CreateAndCapturePayment = "SO301000$createAndCapturePayment";
      public const string OrchestrateOrder = "SO301000$OrchestrateOrder";
    }
  }
}
