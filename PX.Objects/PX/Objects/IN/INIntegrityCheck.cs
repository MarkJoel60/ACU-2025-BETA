// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INIntegrityCheck
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.IN.InventoryRelease;
using PX.Objects.IN.InventoryRelease.Accumulators.ItemHistory;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.Abstraction;
using PX.Objects.IN.InventoryRelease.Accumulators.Statistics.ItemCustomer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.IN;

[TableAndChartDashboardType]
public class INIntegrityCheck : 
  PXGraph<
  #nullable disable
  INIntegrityCheck>,
  PXImportAttribute.IPXPrepareItems,
  PXImportAttribute.IPXProcess
{
  public PXCancel<INRecalculateInventoryFilter> Cancel;
  public PXFilter<INRecalculateInventoryFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessingJoin<InventoryItemCommon, INRecalculateInventoryFilter, LeftJoin<INSiteStatusSummary, On<INSiteStatusSummary.inventoryID, Equal<InventoryItemCommon.inventoryID>, And<INSiteStatusSummary.siteID, Equal<Current<INRecalculateInventoryFilter.siteID>>>>>, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  InventoryItemCommon.itemStatus, 
  #nullable disable
  NotIn3<InventoryItemStatus.unknown, InventoryItemStatus.inactive>>>>>.And<BqlOperand<
  #nullable enable
  InventoryItemCommon.isTemplate, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, OrderBy<Asc<InventoryItemCommon.inventoryCD>>> INItemList;
  public PXSetup<INSetup> insetup;
  public PXSelect<INItemSite> itemsite;
  public PXSelect<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter> sitestatusbycostcenter;
  public PXSelect<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter> locationstatusbycostcenter;
  public PXSelect<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter> lotserialstatusbycostcenter;
  public PXSelect<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial> itemlotserial;
  public PXSelect<SiteLotSerial> sitelotserial;
  public PXSelect<INItemPlan> initemplan;
  public PXSelect<ItemSiteHist> itemsitehist;
  public PXSelect<ItemSiteHistByCostCenterD> itemsitehistbycostcenterd;
  public PXSelect<ItemSiteHistDay> itemsitehistday;
  public PXSelect<ItemCostHist> itemcosthist;
  public PXSelect<ItemSalesHistD> itemsalehistd;
  public PXSelect<ItemCustSalesStats> itemcustsalesstats;
  public PXSelect<ItemCustDropShipStats> itemcustdropshipstats;

  public INIntegrityCheck.ItemPlanHelper ItemPlanHelperExt
  {
    get => ((PXGraph) this).FindImplementation<INIntegrityCheck.ItemPlanHelper>();
  }

  public INIntegrityCheck()
  {
    INSetup current = ((PXSelectBase<INSetup>) this.insetup).Current;
    ((PXProcessingBase<InventoryItemCommon>) this.INItemList).SuppressUpdate = true;
    ((PXProcessing<InventoryItemCommon>) this.INItemList).SetProcessCaption("Process");
    ((PXProcessing<InventoryItemCommon>) this.INItemList).SetProcessAllCaption("Process All");
    PXDBDefaultAttribute.SetDefaultForUpdate<INTranSplit.refNbr>(((PXGraph) this).Caches[typeof (INTranSplit)], (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<INTranSplit.tranDate>(((PXGraph) this).Caches[typeof (INTranSplit)], (object) null, false);
  }

  public virtual void InitCacheMapping(Dictionary<Type, Type> map)
  {
    ((PXGraph) this).InitCacheMapping(map);
    ((PXGraph) this).Caches.AddCacheMapping(typeof (INSiteStatusByCostCenter), typeof (INSiteStatusByCostCenter));
    ((PXGraph) this).Caches.AddCacheMapping(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter), typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter));
  }

  protected virtual PXView CreateItemsLoadView(
    INRecalculateInventoryFilter filter,
    List<object> parameters)
  {
    return (filter != null ? (!filter.SiteID.HasValue ? 1 : 0) : 1) != 0 ? (PXView) null : new PXView((PXGraph) this, ((PXSelectBase) this.INItemList).View.IsReadOnly, this.AppendFilter(((PXSelectBase) this.INItemList).View.BqlSelect, (IList<object>) parameters, filter));
  }

  protected IEnumerable initemlist()
  {
    List<object> parameters = new List<object>();
    PXView itemsLoadView = this.CreateItemsLoadView(((PXSelectBase<INRecalculateInventoryFilter>) this.Filter).Current, parameters);
    if (itemsLoadView == null)
      return (IEnumerable) Array<object>.Empty;
    int startRow = PXView.StartRow;
    int num = 0;
    List<object> objectList = itemsLoadView.Select(PXView.Currents, parameters.ToArray(), PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num);
    PXView.StartRow = 0;
    return (IEnumerable) objectList;
  }

  public virtual BqlCommand AppendFilter(
    BqlCommand cmd,
    IList<object> parameters,
    INRecalculateInventoryFilter filter)
  {
    if (filter.ItemClassID.HasValue)
    {
      cmd = cmd.WhereAnd<Where<BqlOperand<InventoryItemCommon.itemClassID, IBqlInt>.IsEqual<P.AsInt>>>();
      parameters.Add((object) filter.ItemClassID);
    }
    if (filter.ShowOnlyAllocatedItems.GetValueOrDefault())
    {
      cmd = cmd.WhereAnd<Where<Exists<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemPlan.siteID, Equal<P.AsInt>>>>, And<BqlOperand<INItemPlan.inventoryID, IBqlInt>.IsEqual<InventoryItemCommon.inventoryID>>>>.And<BqlOperand<INItemPlan.planQty, IBqlDecimal>.IsNotEqual<decimal0>>>>>>();
      parameters.Add((object) filter.SiteID);
    }
    return cmd;
  }

  [PXMergeAttributes]
  [PXUnboundDefault(true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter.negQty> e)
  {
  }

  [PXMergeAttributes]
  [PXUnboundDefault(true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter.negActualQty> e)
  {
  }

  [PXMergeAttributes]
  [PXUnboundDefault(true)]
  protected virtual void _(PX.Data.Events.CacheAttached<SiteLotSerial.negQty> e)
  {
  }

  [PXMergeAttributes]
  [PXUnboundDefault(true)]
  protected virtual void _(PX.Data.Events.CacheAttached<SiteLotSerial.negActualQty> e)
  {
  }

  [PXMergeAttributes]
  [PXUnboundDefault(true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter.negQty> e)
  {
  }

  [PXMergeAttributes]
  [PXUnboundDefault(true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter.negAvailQty> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<INRecalculateInventoryFilter> e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    INIntegrityCheck.\u003C\u003Ec__DisplayClass32_0 cDisplayClass320 = new INIntegrityCheck.\u003C\u003Ec__DisplayClass32_0();
    if (e.Row == null)
      return;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass320.filter = e.Row;
    // ISSUE: method pointer
    ((PXProcessingBase<InventoryItemCommon>) this.INItemList).SetProcessDelegate<INIntegrityCheck>(new PXProcessingBase<InventoryItemCommon>.ProcessItemDelegate<INIntegrityCheck>((object) cDisplayClass320, __methodptr(\u003C_\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    PXUIFieldAttribute.SetEnabled<INRecalculateInventoryFilter.finPeriodID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRecalculateInventoryFilter>>) e).Cache, (object) null, cDisplayClass320.filter.RebuildHistory.GetValueOrDefault());
    PXUIFieldAttribute.SetRequired<INRecalculateInventoryFilter.siteID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INRecalculateInventoryFilter>>) e).Cache, true);
  }

  public TNode UpdateAllocatedQuantities<TNode>(
    INItemPlan plan,
    INPlanType plantype,
    bool InclQtyAvail)
    where TNode : class, IQtyAllocatedBase
  {
    INPlanType targetPlanType = this.ItemPlanHelperExt.GetTargetPlanType<TNode>(plan, plantype);
    return this.ItemPlanHelperExt.UpdateAllocatedQuantitiesBase<TNode>(plan, targetPlanType, InclQtyAvail);
  }

  public virtual void IntegrityCheckProc(
    INItemSiteSummary itemsite,
    string minPeriod,
    bool replanBackorders)
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      this.DeleteOrphanedItemPlans(itemsite);
      this.CreateItemPlansForTransit(itemsite);
      this.DeleteLotSerialStatusForNotTrackedItems(itemsite);
      this.ClearSiteStatusAllocatedQuantities(itemsite);
      this.ClearLocationStatusAllocatedQuantities(itemsite);
      this.ClearLotSerialStatusAllocatedQuantities(itemsite);
      this.PopulateSiteAvailQtyByLocationStatus(itemsite);
      this.UpdateAllocatedQuantitiesWithExistingPlans(itemsite);
      this.ReplanBackOrders(replanBackorders);
      this.PersistCaches();
      this.RebuildItemHistory(minPeriod, itemsite);
      this.DeleteZeroStatusRecords(itemsite);
      transactionScope.Complete();
    }
    this.OnCachePersisted();
  }

  protected virtual void PersistCaches()
  {
    ((PXGraph) this).Caches[typeof (INTranSplit)].Persist((PXDBOperation) 1);
    ((PXSelectBase) this.sitestatusbycostcenter).Cache.Persist((PXDBOperation) 2);
    ((PXSelectBase) this.sitestatusbycostcenter).Cache.Persist((PXDBOperation) 1);
    ((PXSelectBase) this.locationstatusbycostcenter).Cache.Persist((PXDBOperation) 2);
    ((PXSelectBase) this.locationstatusbycostcenter).Cache.Persist((PXDBOperation) 1);
    ((PXSelectBase) this.lotserialstatusbycostcenter).Cache.Persist((PXDBOperation) 2);
    ((PXSelectBase) this.lotserialstatusbycostcenter).Cache.Persist((PXDBOperation) 1);
    ((PXSelectBase) this.itemlotserial).Cache.Persist((PXDBOperation) 2);
    ((PXSelectBase) this.itemlotserial).Cache.Persist((PXDBOperation) 1);
    ((PXSelectBase) this.sitelotserial).Cache.Persist((PXDBOperation) 2);
    ((PXSelectBase) this.sitelotserial).Cache.Persist((PXDBOperation) 1);
  }

  protected virtual void OnCachePersisted()
  {
    ((PXSelectBase) this.initemplan).Cache.Persisted(false);
    ((PXGraph) this).Caches[typeof (INTranSplit)].Persisted(false);
    ((PXSelectBase) this.sitestatusbycostcenter).Cache.Persisted(false);
    ((PXSelectBase) this.locationstatusbycostcenter).Cache.Persisted(false);
    ((PXSelectBase) this.lotserialstatusbycostcenter).Cache.Persisted(false);
    ((PXSelectBase) this.itemlotserial).Cache.Persisted(false);
    ((PXSelectBase) this.sitelotserial).Cache.Persisted(false);
    ((PXSelectBase) this.itemcosthist).Cache.Persisted(false);
    ((PXSelectBase) this.itemsitehist).Cache.Persisted(false);
    ((PXSelectBase) this.itemsitehistbycostcenterd).Cache.Persisted(false);
    ((PXSelectBase) this.itemsitehistday).Cache.Persisted(false);
    ((PXSelectBase) this.itemsalehistd).Cache.Persisted(false);
    ((PXSelectBase) this.itemcustsalesstats).Cache.Persisted(false);
    ((PXSelectBase) this.itemcustdropshipstats).Cache.Persisted(false);
  }

  protected virtual void DeleteOrphanedItemPlans(INItemSiteSummary itemsite)
  {
    this.DeleteItemPlansWithoutParentDocument(itemsite);
    foreach (PXResult<INItemPlan> pxResult in PXSelectBase<INItemPlan, PXSelectReadonly2<INItemPlan, InnerJoin<INRegister, On<INRegister.noteID, Equal<INItemPlan.refNoteID>>>, Where<INRegister.released, Equal<boolTrue>, And<INItemPlan.refEntityType, Equal<PX.Objects.Common.Constants.DACName<INRegister>>, And<INItemPlan.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>, And<INItemPlan.siteID, Equal<Current<INItemSiteSummary.siteID>>>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) itemsite
    }, Array.Empty<object>()))
      PXDatabase.Delete<INItemPlan>(new PXDataFieldRestrict[1]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<INItemPlan.planID>((PXDbType) 0, new int?(8), (object) PXResult<INItemPlan>.op_Implicit(pxResult).PlanID, (PXComp) 0)
      });
    foreach (PXResult<INItemPlan> pxResult in PXSelectBase<INItemPlan, PXSelectReadonly2<INItemPlan, InnerJoin<PX.Objects.PO.POReceipt, On<PX.Objects.PO.POReceipt.noteID, Equal<INItemPlan.refNoteID>>, LeftJoin<PX.Objects.PO.Table.POReceiptLineSplit, On<PX.Objects.PO.Table.POReceiptLineSplit.receiptNbr, Equal<PX.Objects.PO.POReceipt.receiptNbr>, And<PX.Objects.PO.Table.POReceiptLineSplit.receiptType, Equal<PX.Objects.PO.POReceipt.receiptType>, And<PX.Objects.PO.Table.POReceiptLineSplit.planID, Equal<INItemPlan.planID>>>>>>, Where<PX.Objects.PO.Table.POReceiptLineSplit.receiptNbr, IsNull, And<INItemPlan.refEntityType, Equal<PX.Objects.Common.Constants.DACName<PX.Objects.PO.POReceipt>>, And<INItemPlan.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>, And<INItemPlan.siteID, Equal<Current<INItemSiteSummary.siteID>>>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) itemsite
    }, Array.Empty<object>()))
      PXDatabase.Delete<INItemPlan>(new PXDataFieldRestrict[1]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<INItemPlan.planID>((PXDbType) 0, new int?(8), (object) PXResult<INItemPlan>.op_Implicit(pxResult).PlanID, (PXComp) 0)
      });
    foreach (PXResult<INItemPlan> pxResult in PXSelectBase<INItemPlan, PXSelectReadonly2<INItemPlan, InnerJoin<PX.Objects.SO.SOOrder, On<PX.Objects.SO.SOOrder.noteID, Equal<INItemPlan.refNoteID>>, LeftJoin<PX.Objects.SO.SOLineSplit, On<PX.Objects.SO.SOLineSplit.orderType, Equal<PX.Objects.SO.SOOrder.orderType>, And<PX.Objects.SO.SOLineSplit.orderNbr, Equal<PX.Objects.SO.SOOrder.orderNbr>, And<PX.Objects.SO.SOLineSplit.planID, Equal<INItemPlan.planID>>>>>>, Where<PX.Objects.SO.SOLineSplit.orderNbr, IsNull, And<INItemPlan.planType, NotEqual<INPlanConstants.plan64>, And<INItemPlan.refEntityType, Equal<PX.Objects.Common.Constants.DACName<PX.Objects.SO.SOOrder>>, And<INItemPlan.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>, And<INItemPlan.siteID, Equal<Current<INItemSiteSummary.siteID>>>>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) itemsite
    }, Array.Empty<object>()))
      PXDatabase.Delete<INItemPlan>(new PXDataFieldRestrict[1]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<INItemPlan.planID>((PXDbType) 0, new int?(8), (object) PXResult<INItemPlan>.op_Implicit(pxResult).PlanID, (PXComp) 0)
      });
    foreach (PXResult<INItemPlan> pxResult in PXSelectBase<INItemPlan, PXSelectReadonly2<INItemPlan, InnerJoin<PX.Objects.SO.SOShipment, On<PX.Objects.SO.SOShipment.noteID, Equal<INItemPlan.refNoteID>>, LeftJoin<PX.Objects.SO.Table.SOShipLineSplit, On<PX.Objects.SO.Table.SOShipLineSplit.shipmentNbr, Equal<PX.Objects.SO.SOShipment.shipmentNbr>, And<PX.Objects.SO.Table.SOShipLineSplit.planID, Equal<INItemPlan.planID>>>>>, Where<PX.Objects.SO.Table.SOShipLineSplit.shipmentNbr, IsNull, And<INItemPlan.refEntityType, Equal<PX.Objects.Common.Constants.DACName<PX.Objects.SO.SOShipment>>, And<INItemPlan.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>, And<INItemPlan.siteID, Equal<Current<INItemSiteSummary.siteID>>>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) itemsite
    }, Array.Empty<object>()))
      PXDatabase.Delete<INItemPlan>(new PXDataFieldRestrict[1]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<INItemPlan.planID>((PXDbType) 0, new int?(8), (object) PXResult<INItemPlan>.op_Implicit(pxResult).PlanID, (PXComp) 0)
      });
    foreach (PXResult<INItemPlan> pxResult in PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POOrder>.On<BqlOperand<PX.Objects.PO.POOrder.noteID, IBqlGuid>.IsEqual<INItemPlan.refNoteID>>>, FbqlJoins.Left<PX.Objects.PO.POLine>.On<KeysRelation<CompositeKey<Field<PX.Objects.PO.POLine.orderType>.IsRelatedTo<PX.Objects.PO.POOrder.orderType>, Field<PX.Objects.PO.POLine.orderNbr>.IsRelatedTo<PX.Objects.PO.POOrder.orderNbr>>.WithTablesOf<PX.Objects.PO.POOrder, PX.Objects.PO.POLine>, PX.Objects.PO.POOrder, PX.Objects.PO.POLine>.And<BqlOperand<PX.Objects.PO.POLine.planID, IBqlLong>.IsEqual<INItemPlan.planID>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POLine.orderNbr, IsNull>>>, And<BqlOperand<INItemPlan.refEntityType, IBqlString>.IsEqual<PX.Objects.Common.Constants.DACName<PX.Objects.PO.POOrder>>>>, And<BqlOperand<INItemPlan.inventoryID, IBqlInt>.IsEqual<BqlField<INItemSiteSummary.inventoryID, IBqlInt>.FromCurrent>>>>.And<BqlOperand<INItemPlan.siteID, IBqlInt>.IsEqual<BqlField<INItemSiteSummary.siteID, IBqlInt>.FromCurrent>>>>.ReadOnly.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) itemsite
    }, Array.Empty<object>()))
      PXDatabase.Delete<INItemPlan>(new PXDataFieldRestrict[1]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<INItemPlan.planID>((PXDbType) 0, new int?(8), (object) PXResult<INItemPlan>.op_Implicit(pxResult).PlanID, (PXComp) 0)
      });
    foreach (PXResult<INItemPlan> pxResult in PXSelectBase<INItemPlan, PXSelectReadonly2<INItemPlan, LeftJoin<INItemPlanSupply, On<INItemPlanSupply.planID, Equal<INItemPlan.supplyPlanID>>>, Where<INItemPlanSupply.planID, IsNull, And<INItemPlan.supplyPlanID, IsNotNull, And<INItemPlan.planType, Equal<INPlanConstants.plan94>, And<INItemPlan.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>, And<INItemPlan.siteID, Equal<Current<INItemSiteSummary.siteID>>>>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) itemsite
    }, Array.Empty<object>()))
      PXDatabase.Delete<INItemPlan>(new PXDataFieldRestrict[1]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<INItemPlan.planID>((PXDbType) 0, new int?(8), (object) PXResult<INItemPlan>.op_Implicit(pxResult).PlanID, (PXComp) 0)
      });
  }

  protected virtual void DeleteItemPlansWithoutParentDocument(INItemSiteSummary itemsite)
  {
    Type[] documentsNoteFields = this.GetParentDocumentsNoteFields();
    List<string> stringList = new List<string>();
    foreach (Type type in documentsNoteFields)
    {
      Type itemType = BqlCommand.GetItemType(type);
      PXView pxView = new PXView((PXGraph) this, true, BqlTemplate.OfCommand<Select2<INItemPlan, LeftJoin<BqlPlaceholder.E, On<BqlPlaceholder.N, Equal<INItemPlan.refNoteID>>>, Where<INItemPlan.refEntityType, Equal<Required<INItemPlan.refEntityType>>, And<INItemPlan.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>, And<INItemPlan.siteID, Equal<Current<INItemSiteSummary.siteID>>, And<BqlPlaceholder.N, IsNull>>>>>>.Replace<BqlPlaceholder.E>(itemType).Replace<BqlPlaceholder.N>(type).ToCommand());
      string fullName = itemType.FullName;
      object[] objArray1 = new object[1]
      {
        (object) itemsite
      };
      object[] objArray2 = new object[1]
      {
        (object) fullName
      };
      foreach (PXResult<INItemPlan> pxResult in pxView.SelectMultiBound(objArray1, objArray2))
        PXDatabase.Delete<INItemPlan>(new PXDataFieldRestrict[1]
        {
          (PXDataFieldRestrict) new PXDataFieldRestrict<INItemPlan.planID>((PXDbType) 0, new int?(8), (object) PXResult<INItemPlan>.op_Implicit(pxResult).PlanID, (PXComp) 0)
        });
      stringList.Add(fullName);
    }
    foreach (PXResult<INItemPlan> pxResult in PXSelectBase<INItemPlan, PXSelectReadonly2<INItemPlan, LeftJoin<Note, On<Note.noteID, Equal<INItemPlan.refNoteID>>>, Where<INItemPlan.refEntityType, NotIn<Required<INItemPlan.refEntityType>>, And<INItemPlan.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>, And<INItemPlan.siteID, Equal<Current<INItemSiteSummary.siteID>>, And<Note.noteID, IsNull>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) itemsite
    }, new object[1]{ (object) stringList.ToArray() }))
      PXDatabase.Delete<INItemPlan>(new PXDataFieldRestrict[1]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<INItemPlan.planID>((PXDbType) 0, new int?(8), (object) PXResult<INItemPlan>.op_Implicit(pxResult).PlanID, (PXComp) 0)
      });
  }

  public virtual Type[] GetParentDocumentsNoteFields()
  {
    List<Type> typeList = new List<Type>()
    {
      typeof (PX.Objects.SO.SOOrder.noteID),
      typeof (PX.Objects.SO.SOShipment.noteID),
      typeof (PX.Objects.PO.POOrder.noteID),
      typeof (PX.Objects.PO.POReceipt.noteID),
      typeof (INRegister.noteID),
      typeof (INTransitLine.noteID)
    };
    if (PXAccess.FeatureInstalled<FeaturesSet.replenishment>())
      typeList.Add(typeof (INReplenishmentOrder.noteID));
    if (PXAccess.FeatureInstalled<FeaturesSet.kitAssemblies>())
      typeList.Add(typeof (INKitRegister.noteID));
    return typeList.ToArray();
  }

  protected virtual void CreateItemPlansForTransit(INItemSiteSummary itemsite)
  {
    INTransferEntry instance1 = PXGraph.CreateInstance<INTransferEntry>();
    foreach (PXResult<INLocationStatusInTransit, INTransitLine> pxResult1 in PXSelectBase<INLocationStatusInTransit, PXSelectJoin<INLocationStatusInTransit, InnerJoin<INTransitLine, On<INTransitLine.costSiteID, Equal<INLocationStatusInTransit.locationID>>, LeftJoin<INItemPlan, On<INItemPlan.refNoteID, Equal<INTransitLine.noteID>>>>, Where<INLocationStatusInTransit.qtyOnHand, Greater<decimal0>, And<INLocationStatusInTransit.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>, And<INTransitLine.toSiteID, Equal<Current<INItemSiteSummary.siteID>>, And<INItemPlan.planID, IsNull>>>>>.Config>.SelectMultiBound((PXGraph) instance1, new object[1]
    {
      (object) itemsite
    }, Array.Empty<object>()))
    {
      INLocationStatusInTransit locationStatusInTransit1 = PXResult<INLocationStatusInTransit, INTransitLine>.op_Implicit(pxResult1);
      INTransitLine inTransitLine = PXResult<INLocationStatusInTransit, INTransitLine>.op_Implicit(pxResult1);
      INItemPlan inItemPlan;
      foreach (PXResult<TransitLotSerialStatusByCostCenter> pxResult2 in PXSelectBase<TransitLotSerialStatusByCostCenter, PXSelect<TransitLotSerialStatusByCostCenter, Where<TransitLotSerialStatusByCostCenter.locationID, Equal<Current<INLocationStatusInTransit.locationID>>, And<TransitLotSerialStatusByCostCenter.inventoryID, Equal<Current<INLocationStatusInTransit.inventoryID>>, And<TransitLotSerialStatusByCostCenter.subItemID, Equal<Current<INLocationStatusInTransit.subItemID>>, And<TransitLotSerialStatusByCostCenter.costCenterID, Equal<Current<INLocationStatusInTransit.costCenterID>>, And<TransitLotSerialStatusByCostCenter.qtyOnHand, Greater<decimal0>>>>>>>.Config>.SelectMultiBound((PXGraph) instance1, new object[1]
      {
        (object) locationStatusInTransit1
      }, Array.Empty<object>()))
      {
        TransitLotSerialStatusByCostCenter statusByCostCenter = PXResult<TransitLotSerialStatusByCostCenter>.op_Implicit(pxResult2);
        INItemPlan instance2 = (INItemPlan) ((PXGraph) instance1).Caches[typeof (INItemPlan)].CreateInstance();
        instance2.PlanType = inTransitLine.SOShipmentNbr == null ? "42" : "44";
        instance2.InventoryID = statusByCostCenter.InventoryID;
        instance2.SubItemID = statusByCostCenter.SubItemID ?? locationStatusInTransit1.SubItemID;
        instance2.LotSerialNbr = statusByCostCenter.LotSerialNbr;
        instance2.CostCenterID = statusByCostCenter.CostCenterID;
        instance2.SiteID = inTransitLine.ToSiteID;
        instance2.LocationID = inTransitLine.ToLocationID;
        instance2.FixedSource = "P";
        instance2.PlanDate = inTransitLine.CreatedDateTime;
        instance2.Reverse = new bool?(false);
        instance2.Hold = new bool?(false);
        instance2.PlanQty = statusByCostCenter.QtyOnHand;
        INLocationStatusInTransit locationStatusInTransit2 = locationStatusInTransit1;
        Decimal? qtyOnHand1 = locationStatusInTransit2.QtyOnHand;
        Decimal? qtyOnHand2 = statusByCostCenter.QtyOnHand;
        locationStatusInTransit2.QtyOnHand = qtyOnHand1.HasValue & qtyOnHand2.HasValue ? new Decimal?(qtyOnHand1.GetValueOrDefault() - qtyOnHand2.GetValueOrDefault()) : new Decimal?();
        instance2.RefNoteID = inTransitLine.NoteID;
        instance2.RefEntityType = typeof (INTransitLine).FullName;
        inItemPlan = (INItemPlan) ((PXGraph) instance1).Caches[typeof (INItemPlan)].Insert((object) instance2);
      }
      Decimal? qtyOnHand = locationStatusInTransit1.QtyOnHand;
      Decimal num = 0M;
      if (!(qtyOnHand.GetValueOrDefault() <= num & qtyOnHand.HasValue))
      {
        INItemPlan instance3 = (INItemPlan) ((PXGraph) instance1).Caches[typeof (INItemPlan)].CreateInstance();
        instance3.PlanType = inTransitLine.SOShipmentNbr == null ? "42" : "44";
        instance3.InventoryID = locationStatusInTransit1.InventoryID;
        instance3.SubItemID = locationStatusInTransit1.SubItemID;
        instance3.SiteID = inTransitLine.ToSiteID;
        instance3.LocationID = inTransitLine.ToLocationID;
        instance3.CostCenterID = locationStatusInTransit1.CostCenterID;
        instance3.FixedSource = "P";
        instance3.PlanDate = inTransitLine.CreatedDateTime;
        instance3.Reverse = new bool?(false);
        instance3.Hold = new bool?(false);
        instance3.PlanQty = locationStatusInTransit1.QtyOnHand;
        instance3.RefNoteID = inTransitLine.NoteID;
        instance3.RefEntityType = typeof (INTransitLine).FullName;
        inItemPlan = (INItemPlan) ((PXGraph) instance1).Caches[typeof (INItemPlan)].Insert((object) instance3);
      }
    }
    ((PXAction) instance1.Save).Press();
  }

  protected virtual void DeleteLotSerialStatusForNotTrackedItems(INItemSiteSummary itemsite)
  {
    if (PXResultset<InventoryItem>.op_Implicit(PXSelectBase<InventoryItem, PXSelectReadonly2<InventoryItem, InnerJoin<INLotSerClass, On2<InventoryItem.FK.LotSerialClass, And<INLotSerClass.lotSerTrack, Equal<INLotSerTrack.notNumbered>>>, InnerJoin<INLotSerialStatusByCostCenter, On<INLotSerialStatusByCostCenter.FK.InventoryItem>>>, Where<InventoryItem.inventoryID, Equal<Required<InventoryItem.inventoryID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) itemsite.InventoryID
    })) == null || InventoryItemMaint.IsQtyStillPresent((PXGraph) this, itemsite.InventoryID))
      return;
    this.DeleteLotSerialStatusForNotTrackedItemsByItem(itemsite.InventoryID);
  }

  protected virtual void DeleteLotSerialStatusForNotTrackedItemsByItem(int? inventoryID)
  {
    PXDatabase.Delete<INLotSerialStatusByCostCenter>(new PXDataFieldRestrict[2]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<INLotSerialStatusByCostCenter.inventoryID>((PXDbType) 8, new int?(4), (object) inventoryID, (PXComp) 0),
      (PXDataFieldRestrict) new PXDataFieldRestrict<INLotSerialStatusByCostCenter.qtyOnHand>((PXDbType) 5, new int?(4), (object) 0M, (PXComp) 0)
    });
    PXDatabase.Delete<INItemLotSerial>(new PXDataFieldRestrict[2]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<INItemLotSerial.inventoryID>((PXDbType) 8, new int?(4), (object) inventoryID, (PXComp) 0),
      (PXDataFieldRestrict) new PXDataFieldRestrict<INItemLotSerial.qtyOnHand>((PXDbType) 5, new int?(4), (object) 0M, (PXComp) 0)
    });
    PXDatabase.Delete<INSiteLotSerial>(new PXDataFieldRestrict[2]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<INSiteLotSerial.inventoryID>((PXDbType) 8, new int?(4), (object) inventoryID, (PXComp) 0),
      (PXDataFieldRestrict) new PXDataFieldRestrict<INSiteLotSerial.qtyOnHand>((PXDbType) 5, new int?(4), (object) 0M, (PXComp) 0)
    });
  }

  protected virtual void ClearSiteStatusAllocatedQuantities(INItemSiteSummary itemsite)
  {
    PXDatabase.Update<INSiteStatusByCostCenter>(this.AssignAllDBDecimalFieldsToZeroCommand(((PXSelectBase) this.sitestatusbycostcenter).Cache, "qtyOnHand").Append<PXDataFieldParam>((PXDataFieldParam) new PXDataFieldRestrict<INSiteStatusByCostCenter.inventoryID>((PXDbType) 8, new int?(4), (object) itemsite.InventoryID, (PXComp) 0)).Append<PXDataFieldParam>((PXDataFieldParam) new PXDataFieldRestrict<INSiteStatusByCostCenter.siteID>((PXDbType) 8, new int?(4), (object) itemsite.SiteID, (PXComp) 0)).ToArray<PXDataFieldParam>());
  }

  protected virtual void ClearLocationStatusAllocatedQuantities(INItemSiteSummary itemsite)
  {
    PXDatabase.Update<INLocationStatusByCostCenter>(this.AssignAllDBDecimalFieldsToZeroCommand(((PXSelectBase) this.locationstatusbycostcenter).Cache, "qtyOnHand", "qtyAvail", "qtyHardAvail", "qtyActual").Append<PXDataFieldParam>((PXDataFieldParam) new PXDataFieldAssign<INLocationStatusByCostCenter.qtyAvail>((PXDbType) 200, (object) "QtyOnHand")).Append<PXDataFieldParam>((PXDataFieldParam) new PXDataFieldAssign<INLocationStatusByCostCenter.qtyHardAvail>((PXDbType) 200, (object) "QtyOnHand")).Append<PXDataFieldParam>((PXDataFieldParam) new PXDataFieldAssign<INLocationStatusByCostCenter.qtyActual>((PXDbType) 200, (object) "QtyOnHand")).Append<PXDataFieldParam>((PXDataFieldParam) new PXDataFieldRestrict<INLocationStatusByCostCenter.inventoryID>((PXDbType) 8, new int?(4), (object) itemsite.InventoryID, (PXComp) 0)).Append<PXDataFieldParam>((PXDataFieldParam) new PXDataFieldRestrict<INLocationStatusByCostCenter.siteID>((PXDbType) 8, new int?(4), (object) itemsite.SiteID, (PXComp) 0)).ToArray<PXDataFieldParam>());
  }

  protected virtual void ClearLotSerialStatusAllocatedQuantities(INItemSiteSummary itemsite)
  {
    PXDatabase.Update<INLotSerialStatusByCostCenter>(this.AssignAllDBDecimalFieldsToZeroCommand(((PXSelectBase) this.lotserialstatusbycostcenter).Cache, "qtyOnHand", "qtyAvail", "qtyHardAvail", "qtyActual").Append<PXDataFieldParam>((PXDataFieldParam) new PXDataFieldAssign<INLotSerialStatusByCostCenter.qtyAvail>((PXDbType) 200, (object) "QtyOnHand")).Append<PXDataFieldParam>((PXDataFieldParam) new PXDataFieldAssign<INLotSerialStatusByCostCenter.qtyHardAvail>((PXDbType) 200, (object) "QtyOnHand")).Append<PXDataFieldParam>((PXDataFieldParam) new PXDataFieldAssign<INLotSerialStatusByCostCenter.qtyActual>((PXDbType) 200, (object) "QtyOnHand")).Append<PXDataFieldParam>((PXDataFieldParam) new PXDataFieldRestrict<INLotSerialStatusByCostCenter.inventoryID>((PXDbType) 8, new int?(4), (object) itemsite.InventoryID, (PXComp) 0)).Append<PXDataFieldParam>((PXDataFieldParam) new PXDataFieldRestrict<INLotSerialStatusByCostCenter.siteID>((PXDbType) 8, new int?(4), (object) itemsite.SiteID, (PXComp) 0)).ToArray<PXDataFieldParam>());
    PXDatabase.Update<INItemLotSerial>(this.AssignAllDBDecimalFieldsToZeroCommand(((PXSelectBase) this.itemlotserial).Cache, "qtyOnHand", "qtyAvail", "qtyHardAvail", "qtyActual", "qtyOrig").Append<PXDataFieldParam>((PXDataFieldParam) new PXDataFieldAssign<INItemLotSerial.qtyAvail>((PXDbType) 200, (object) "QtyOnHand")).Append<PXDataFieldParam>((PXDataFieldParam) new PXDataFieldAssign<INItemLotSerial.qtyHardAvail>((PXDbType) 200, (object) "QtyOnHand")).Append<PXDataFieldParam>((PXDataFieldParam) new PXDataFieldAssign<INItemLotSerial.qtyActual>((PXDbType) 200, (object) "QtyOnHand")).Append<PXDataFieldParam>((PXDataFieldParam) new PXDataFieldRestrict<INItemLotSerial.inventoryID>((PXDbType) 8, new int?(4), (object) itemsite.InventoryID, (PXComp) 0)).ToArray<PXDataFieldParam>());
    PXDatabase.Update<INSiteLotSerial>(this.AssignAllDBDecimalFieldsToZeroCommand(((PXSelectBase) this.sitelotserial).Cache, "qtyOnHand").Append<PXDataFieldParam>((PXDataFieldParam) new PXDataFieldRestrict<INSiteLotSerial.inventoryID>((PXDbType) 8, new int?(4), (object) itemsite.InventoryID, (PXComp) 0)).Append<PXDataFieldParam>((PXDataFieldParam) new PXDataFieldRestrict<INSiteLotSerial.siteID>((PXDbType) 8, new int?(4), (object) itemsite.SiteID, (PXComp) 0)).ToArray<PXDataFieldParam>());
  }

  protected virtual void PopulateSiteAvailQtyByLocationStatus(INItemSiteSummary itemsite)
  {
    foreach (PXResult<ReadOnlyLocationStatusByCostCenter, INLocation> pxResult in PXSelectBase<ReadOnlyLocationStatusByCostCenter, PXSelectJoinGroupBy<ReadOnlyLocationStatusByCostCenter, InnerJoin<INLocation, On<INLocation.locationID, Equal<ReadOnlyLocationStatusByCostCenter.locationID>>>, Where<ReadOnlyLocationStatusByCostCenter.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>, And<ReadOnlyLocationStatusByCostCenter.siteID, Equal<Current<INItemSiteSummary.siteID>>>>, Aggregate<GroupBy<ReadOnlyLocationStatusByCostCenter.inventoryID, GroupBy<ReadOnlyLocationStatusByCostCenter.siteID, GroupBy<ReadOnlyLocationStatusByCostCenter.subItemID, GroupBy<ReadOnlyLocationStatusByCostCenter.costCenterID, GroupBy<INLocation.inclQtyAvail, Sum<ReadOnlyLocationStatusByCostCenter.qtyOnHand>>>>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) itemsite
    }, Array.Empty<object>()))
    {
      ReadOnlyLocationStatusByCostCenter statusByCostCenter1 = PXResult<ReadOnlyLocationStatusByCostCenter, INLocation>.op_Implicit(pxResult);
      PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter2 = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter();
      statusByCostCenter2.InventoryID = statusByCostCenter1.InventoryID;
      statusByCostCenter2.SubItemID = statusByCostCenter1.SubItemID;
      statusByCostCenter2.SiteID = statusByCostCenter1.SiteID;
      statusByCostCenter2.CostCenterID = statusByCostCenter1.CostCenterID;
      PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter3 = ((PXSelectBase<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>) this.sitestatusbycostcenter).Insert(statusByCostCenter2);
      Decimal? nullable1;
      Decimal? nullable2;
      if (PXResult<ReadOnlyLocationStatusByCostCenter, INLocation>.op_Implicit(pxResult).InclQtyAvail.GetValueOrDefault())
      {
        PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter4 = statusByCostCenter3;
        nullable1 = statusByCostCenter4.QtyAvail;
        nullable2 = statusByCostCenter1.QtyOnHand;
        statusByCostCenter4.QtyAvail = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
        PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter5 = statusByCostCenter3;
        nullable2 = statusByCostCenter5.QtyHardAvail;
        nullable1 = statusByCostCenter1.QtyOnHand;
        statusByCostCenter5.QtyHardAvail = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
        PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter6 = statusByCostCenter3;
        nullable1 = statusByCostCenter6.QtyActual;
        nullable2 = statusByCostCenter1.QtyOnHand;
        statusByCostCenter6.QtyActual = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      }
      else
      {
        PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter7 = statusByCostCenter3;
        nullable2 = statusByCostCenter7.QtyNotAvail;
        nullable1 = statusByCostCenter1.QtyOnHand;
        statusByCostCenter7.QtyNotAvail = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
      }
    }
    foreach (PXResult<INLotSerialStatusByCostCenter, INLocation> pxResult in PXSelectBase<INLotSerialStatusByCostCenter, PXSelectJoinGroupBy<INLotSerialStatusByCostCenter, InnerJoin<INLocation, On<INLocation.locationID, Equal<INLotSerialStatusByCostCenter.locationID>>>, Where<INLotSerialStatusByCostCenter.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>, And<INLotSerialStatusByCostCenter.siteID, Equal<Current<INItemSiteSummary.siteID>>>>, Aggregate<GroupBy<INLotSerialStatusByCostCenter.inventoryID, GroupBy<INLotSerialStatusByCostCenter.siteID, GroupBy<INLotSerialStatusByCostCenter.lotSerialNbr, GroupBy<INLocation.inclQtyAvail, Sum<INLotSerialStatusByCostCenter.qtyOnHand>>>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) itemsite
    }, Array.Empty<object>()))
    {
      INLotSerialStatusByCostCenter statusByCostCenter = PXResult<INLotSerialStatusByCostCenter, INLocation>.op_Implicit(pxResult);
      SiteLotSerial siteLotSerial1 = new SiteLotSerial();
      siteLotSerial1.InventoryID = statusByCostCenter.InventoryID;
      siteLotSerial1.SiteID = statusByCostCenter.SiteID;
      siteLotSerial1.LotSerialNbr = statusByCostCenter.LotSerialNbr;
      SiteLotSerial siteLotSerial2 = ((PXSelectBase<SiteLotSerial>) this.sitelotserial).Insert(siteLotSerial1);
      Decimal? nullable3;
      Decimal? nullable4;
      if (PXResult<INLotSerialStatusByCostCenter, INLocation>.op_Implicit(pxResult).InclQtyAvail.GetValueOrDefault())
      {
        SiteLotSerial siteLotSerial3 = siteLotSerial2;
        nullable3 = siteLotSerial3.QtyAvail;
        nullable4 = statusByCostCenter.QtyOnHand;
        siteLotSerial3.QtyAvail = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
        SiteLotSerial siteLotSerial4 = siteLotSerial2;
        nullable4 = siteLotSerial4.QtyHardAvail;
        nullable3 = statusByCostCenter.QtyOnHand;
        siteLotSerial4.QtyHardAvail = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
        SiteLotSerial siteLotSerial5 = siteLotSerial2;
        nullable3 = siteLotSerial5.QtyActual;
        nullable4 = statusByCostCenter.QtyOnHand;
        siteLotSerial5.QtyActual = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
      }
      else
      {
        SiteLotSerial siteLotSerial6 = siteLotSerial2;
        nullable4 = siteLotSerial6.QtyNotAvail;
        nullable3 = statusByCostCenter.QtyOnHand;
        siteLotSerial6.QtyNotAvail = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
      }
    }
  }

  protected virtual void UpdateAllocatedQuantitiesWithExistingPlans(INItemSiteSummary itemsite)
  {
    foreach (PXResult<INItemPlan, InventoryItem> pxResult in PXSelectBase<INItemPlan, PXSelectJoin<INItemPlan, InnerJoin<InventoryItem, On<INItemPlan.FK.InventoryItem>>, Where<INItemPlan.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>, And<INItemPlan.siteID, Equal<Current<INItemSiteSummary.siteID>>, And<InventoryItem.stkItem, Equal<boolTrue>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) itemsite
    }, Array.Empty<object>()))
    {
      INItemPlan plan = PXResult<INItemPlan, InventoryItem>.op_Implicit(pxResult);
      INPlanType plantype = INPlanType.PK.Find((PXGraph) this, plan.PlanType);
      this.UpdateAllocatedQuantitiesWithPlans(itemsite, plan, plantype);
    }
    foreach (PXResult<INItemPlan> pxResult in PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>, And<INItemPlan.lotSerialNbr, NotEqual<StringEmpty>, And<INItemPlan.lotSerialNbr, IsNotNull>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) itemsite
    }, Array.Empty<object>()))
    {
      INItemPlan plan = PXResult<INItemPlan>.op_Implicit(pxResult);
      INPlanType plantype = INPlanType.PK.Find((PXGraph) this, plan.PlanType);
      int? nullable = plan.InventoryID;
      if (nullable.HasValue)
      {
        nullable = plan.SubItemID;
        if (nullable.HasValue)
        {
          nullable = plan.SiteID;
          if (nullable.HasValue)
          {
            nullable = plan.LocationID;
            if (nullable.HasValue)
              this.UpdateAllocatedQuantities<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial>(plan, plantype, true);
            else
              this.UpdateAllocatedQuantities<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial>(plan, plantype, true);
          }
        }
      }
    }
  }

  protected virtual void UpdateAllocatedQuantitiesWithPlans(
    INItemSiteSummary itemsite,
    INItemPlan plan,
    INPlanType plantype)
  {
    if (!plan.InventoryID.HasValue || !plan.SubItemID.HasValue || !plan.SiteID.HasValue)
      return;
    if (plan.LocationID.HasValue)
    {
      PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter statusByCostCenter = this.UpdateAllocatedQuantities<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter>(plan, plantype, true);
      this.UpdateAllocatedQuantities<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>(plan, plantype, statusByCostCenter.InclQtyAvail.Value);
      if (string.IsNullOrEmpty(plan.LotSerialNbr))
        return;
      this.UpdateAllocatedQuantities<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter>(plan, plantype, true);
      this.UpdateAllocatedQuantities<SiteLotSerial>(plan, plantype, statusByCostCenter.InclQtyAvail.Value);
    }
    else
    {
      this.UpdateAllocatedQuantities<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>(plan, plantype, true);
      if (string.IsNullOrEmpty(plan.LotSerialNbr))
        return;
      this.UpdateAllocatedQuantities<SiteLotSerial>(plan, plantype, true);
    }
  }

  protected virtual void ReplanBackOrders(bool replanBackorders)
  {
    if (!replanBackorders)
      return;
    INReleaseProcess.ReplanBackOrders((PXGraph) this);
    ((PXSelectBase) this.initemplan).Cache.Persist((PXDBOperation) 2);
    ((PXSelectBase) this.initemplan).Cache.Persist((PXDBOperation) 1);
  }

  protected virtual void RebuildItemHistory(string minPeriod, INItemSiteSummary itemsite)
  {
    if (minPeriod == null)
      return;
    MasterFinPeriod masterFinPeriod = PXResultset<MasterFinPeriod>.op_Implicit(PXSelectBase<MasterFinPeriod, PXSelect<MasterFinPeriod, Where<MasterFinPeriod.finPeriodID, Equal<Required<MasterFinPeriod.finPeriodID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) minPeriod
    }));
    if (masterFinPeriod == null)
      return;
    DateTime dateTime = masterFinPeriod.StartDate.Value;
    PXDatabase.Delete<INItemCostHist>(new PXDataFieldRestrict[3]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<INItemCostHist.inventoryID>((PXDbType) 8, new int?(4), (object) itemsite.InventoryID, (PXComp) 0),
      (PXDataFieldRestrict) new PXDataFieldRestrict<INItemCostHist.costSiteID>((PXDbType) 8, new int?(4), (object) itemsite.SiteID, (PXComp) 0),
      (PXDataFieldRestrict) new PXDataFieldRestrict<INItemCostHist.finPeriodID>((PXDbType) 3, new int?(6), (object) minPeriod, (PXComp) 3)
    });
    PXDatabase.Update<INItemSalesHistD>(this.AssignAllDBDecimalFieldsToZeroCommand(((PXSelectBase) this.itemsalehistd).Cache, "qtyPlanSales", "qtyLostSales").Append<PXDataFieldParam>((PXDataFieldParam) new PXDataFieldRestrict<INItemSalesHistD.inventoryID>((PXDbType) 8, new int?(4), (object) itemsite.InventoryID, (PXComp) 0)).Append<PXDataFieldParam>((PXDataFieldParam) new PXDataFieldRestrict<INItemSalesHistD.siteID>((PXDbType) 8, new int?(4), (object) itemsite.SiteID, (PXComp) 0)).Append<PXDataFieldParam>((PXDataFieldParam) new PXDataFieldRestrict<INItemSalesHistD.sDate>((PXDbType) 4, new int?(8), (object) dateTime, (PXComp) 3)).ToArray<PXDataFieldParam>());
    PXDatabase.Update<INItemCustSalesStats>(new PXDataFieldParam[6]
    {
      (PXDataFieldParam) new PXDataFieldAssign<INItemCustSalesStats.lastQty>((PXDbType) 5, (object) null),
      (PXDataFieldParam) new PXDataFieldAssign<INItemCustSalesStats.lastDate>((PXDbType) 4, (object) null),
      (PXDataFieldParam) new PXDataFieldAssign<INItemCustSalesStats.lastUnitPrice>((PXDbType) 5, (object) null),
      (PXDataFieldParam) new PXDataFieldRestrict<INItemCustSalesStats.inventoryID>((PXDbType) 8, new int?(4), (object) itemsite.InventoryID, (PXComp) 0),
      (PXDataFieldParam) new PXDataFieldRestrict<INItemCustSalesStats.siteID>((PXDbType) 8, new int?(4), (object) itemsite.SiteID, (PXComp) 0),
      (PXDataFieldParam) new PXDataFieldRestrict<INItemCustSalesStats.lastDate>((PXDbType) 4, new int?(8), (object) dateTime, (PXComp) 3)
    });
    PXDatabase.Update<INItemCustSalesStats>(new PXDataFieldParam[6]
    {
      (PXDataFieldParam) new PXDataFieldAssign<INItemCustSalesStats.dropShipLastQty>((PXDbType) 5, (object) null),
      (PXDataFieldParam) new PXDataFieldAssign<INItemCustSalesStats.dropShipLastDate>((PXDbType) 4, (object) null),
      (PXDataFieldParam) new PXDataFieldAssign<INItemCustSalesStats.dropShipLastUnitPrice>((PXDbType) 5, (object) null),
      (PXDataFieldParam) new PXDataFieldRestrict<INItemCustSalesStats.inventoryID>((PXDbType) 8, new int?(4), (object) itemsite.InventoryID, (PXComp) 0),
      (PXDataFieldParam) new PXDataFieldRestrict<INItemCustSalesStats.siteID>((PXDbType) 8, new int?(4), (object) itemsite.SiteID, (PXComp) 0),
      (PXDataFieldParam) new PXDataFieldRestrict<INItemCustSalesStats.dropShipLastDate>((PXDbType) 4, new int?(8), (object) dateTime, (PXComp) 3)
    });
    PXDatabase.Delete<INItemCustSalesStats>(new PXDataFieldRestrict[4]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<INItemCustSalesStats.inventoryID>((PXDbType) 8, new int?(4), (object) itemsite.InventoryID, (PXComp) 0),
      (PXDataFieldRestrict) new PXDataFieldRestrict<INItemCustSalesStats.siteID>((PXDbType) 8, new int?(4), (object) itemsite.SiteID, (PXComp) 0),
      (PXDataFieldRestrict) new PXDataFieldRestrict<INItemCustSalesStats.lastDate>((PXDbType) 4, new int?(8), (object) dateTime, (PXComp) 6),
      (PXDataFieldRestrict) new PXDataFieldRestrict<INItemCustSalesStats.dropShipLastDate>((PXDbType) 4, new int?(8), (object) dateTime, (PXComp) 6)
    });
    foreach (PXResult<INLocation> pxResult in PXSelectBase<INLocation, PXSelectJoinGroupBy<INLocation, InnerJoin<INItemCostHist, On<INItemCostHist.costSiteID, Equal<INLocation.locationID>>>, Where<INLocation.siteID, Equal<Current<INItemSiteSummary.siteID>>, And<INItemCostHist.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>>>, Aggregate<GroupBy<INLocation.locationID>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) itemsite
    }, Array.Empty<object>()))
    {
      INLocation inLocation = PXResult<INLocation>.op_Implicit(pxResult);
      PXDatabase.Delete<INItemCostHist>(new PXDataFieldRestrict[3]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<INItemCostHist.inventoryID>((PXDbType) 8, new int?(4), (object) itemsite.InventoryID, (PXComp) 0),
        (PXDataFieldRestrict) new PXDataFieldRestrict<INItemCostHist.costSiteID>((PXDbType) 8, new int?(4), (object) inLocation.LocationID, (PXComp) 0),
        (PXDataFieldRestrict) new PXDataFieldRestrict<INItemCostHist.finPeriodID>((PXDbType) 3, new int?(6), (object) minPeriod, (PXComp) 3)
      });
    }
    PXDatabase.Delete<INItemSiteHist>(new PXDataFieldRestrict[3]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<INItemSiteHist.inventoryID>((PXDbType) 8, new int?(4), (object) itemsite.InventoryID, (PXComp) 0),
      (PXDataFieldRestrict) new PXDataFieldRestrict<INItemSiteHist.siteID>((PXDbType) 8, new int?(4), (object) itemsite.SiteID, (PXComp) 0),
      (PXDataFieldRestrict) new PXDataFieldRestrict<INItemSiteHist.finPeriodID>((PXDbType) 3, new int?(6), (object) minPeriod, (PXComp) 3)
    });
    PXDatabase.Delete<INItemSiteHistByCostCenterD>(new PXDataFieldRestrict[3]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<INItemSiteHistByCostCenterD.inventoryID>((PXDbType) 8, new int?(4), (object) itemsite.InventoryID, (PXComp) 0),
      (PXDataFieldRestrict) new PXDataFieldRestrict<INItemSiteHistByCostCenterD.siteID>((PXDbType) 8, new int?(4), (object) itemsite.SiteID, (PXComp) 0),
      (PXDataFieldRestrict) new PXDataFieldRestrict<INItemSiteHistByCostCenterD.sDate>((PXDbType) 4, new int?(8), (object) dateTime, (PXComp) 3)
    });
    PXDatabase.Delete<INItemSiteHistDay>(new PXDataFieldRestrict[3]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<INItemSiteHistDay.inventoryID>((PXDbType) 8, new int?(4), (object) itemsite.InventoryID, (PXComp) 0),
      (PXDataFieldRestrict) new PXDataFieldRestrict<INItemSiteHistDay.siteID>((PXDbType) 8, new int?(4), (object) itemsite.SiteID, (PXComp) 0),
      (PXDataFieldRestrict) new PXDataFieldRestrict<INItemSiteHistDay.sDate>((PXDbType) 4, new int?(8), (object) dateTime, (PXComp) 3)
    });
    INTran inTran = (INTran) null;
    PXSelectReadonly2<INTran, LeftJoin<INTranSplit, On<INTranSplit.FK.Tran>>, Where<INTran.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>, And<INTran.siteID, Equal<Current<INItemSiteSummary.siteID>>, And<INTran.finPeriodID, GreaterEqual<Required<INTran.finPeriodID>>, And<INTran.released, Equal<boolTrue>>>>>, OrderBy<Asc<INTran.tranType, Asc<INTran.refNbr, Asc<INTran.lineNbr>>>>> pxSelectReadonly2_1 = new PXSelectReadonly2<INTran, LeftJoin<INTranSplit, On<INTranSplit.FK.Tran>>, Where<INTran.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>, And<INTran.siteID, Equal<Current<INItemSiteSummary.siteID>>, And<INTran.finPeriodID, GreaterEqual<Required<INTran.finPeriodID>>, And<INTran.released, Equal<boolTrue>>>>>, OrderBy<Asc<INTran.tranType, Asc<INTran.refNbr, Asc<INTran.lineNbr>>>>>((PXGraph) this);
    using (new PXFieldScope(((PXSelectBase) pxSelectReadonly2_1).View, new Type[30]
    {
      typeof (INTran.docType),
      typeof (INTran.refNbr),
      typeof (INTran.lineNbr),
      typeof (INTran.tranDate),
      typeof (INTran.baseQty),
      typeof (INTran.subItemID),
      typeof (INTran.tranType),
      typeof (INTran.invtMult),
      typeof (INTran.origModule),
      typeof (INTran.inventoryID),
      typeof (INTran.siteID),
      typeof (INTran.bAccountID),
      typeof (INTran.aRRefNbr),
      typeof (INTran.tranAmt),
      typeof (INTran.unitPrice),
      typeof (INTran.finPeriodID),
      typeof (INTran.tranPeriodID),
      typeof (INTran.locationID),
      typeof (INTran.costCenterID),
      typeof (INTranSplit.baseQty),
      typeof (INTranSplit.tranType),
      typeof (INTranSplit.invtMult),
      typeof (INTranSplit.origModule),
      typeof (INTranSplit.inventoryID),
      typeof (INTranSplit.siteID),
      typeof (INTranSplit.subItemID),
      typeof (INTranSplit.locationID),
      typeof (INTranSplit.tranDate),
      typeof (INTranSplit.toSiteID),
      typeof (INTranSplit.skipCostUpdate)
    }))
    {
      PXView view = ((PXSelectBase) pxSelectReadonly2_1).View;
      object[] objArray1 = new object[1]
      {
        (object) itemsite
      };
      object[] objArray2 = new object[1]
      {
        (object) minPeriod
      };
      foreach (PXResult<INTran, INTranSplit> pxResult in view.SelectMultiBound(objArray1, objArray2))
      {
        INTran intran = PXResult<INTran, INTranSplit>.op_Implicit(pxResult);
        INTranSplit split = PXResult<INTran, INTranSplit>.op_Implicit(pxResult);
        if (!((PXGraph) this).Caches[typeof (INTran)].ObjectsEqual((object) inTran, (object) intran))
        {
          INReleaseProcess.UpdateSalesHistD((PXGraph) this, intran);
          INReleaseProcess.UpdateCustSalesStats((PXGraph) this, intran);
          inTran = intran;
        }
        if (split.BaseQty.GetValueOrDefault() != 0M)
        {
          INReleaseProcess.UpdateSiteHist((PXGraph) this, PXResult<INTran, INTranSplit>.op_Implicit(pxResult), split);
          INReleaseProcess.UpdateSiteHistByCostCenterD((PXGraph) this, PXResult<INTran, INTranSplit>.op_Implicit(pxResult), split);
          INReleaseProcess.UpdateSiteHistDay((PXGraph) this, PXResult<INTran, INTranSplit>.op_Implicit(pxResult), split);
        }
      }
    }
    PXSelectReadonly2<INTran, InnerJoin<INTranCost, On<INTranCost.FK.Tran>>, Where<INTranCost.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>, And<INTranCost.siteID, Equal<Current<INItemSiteSummary.siteID>>, And<INTranCost.finPeriodID, GreaterEqual<Required<INTran.finPeriodID>>, And<INTranCost.costSiteID, NotEqual<SiteAnyAttribute.transitSiteID>>>>>> pxSelectReadonly2_2 = new PXSelectReadonly2<INTran, InnerJoin<INTranCost, On<INTranCost.FK.Tran>>, Where<INTranCost.inventoryID, Equal<Current<INItemSiteSummary.inventoryID>>, And<INTranCost.siteID, Equal<Current<INItemSiteSummary.siteID>>, And<INTranCost.finPeriodID, GreaterEqual<Required<INTran.finPeriodID>>, And<INTranCost.costSiteID, NotEqual<SiteAnyAttribute.transitSiteID>>>>>>((PXGraph) this);
    using (new PXFieldScope(((PXSelectBase) pxSelectReadonly2_2).View, new Type[21]
    {
      typeof (INTranCost.tranType),
      typeof (INTranCost.invtMult),
      typeof (INTranCost.qty),
      typeof (INTranCost.costDocType),
      typeof (INTranCost.costRefNbr),
      typeof (INTranCost.costSubItemID),
      typeof (INTranCost.inventoryID),
      typeof (INTranCost.invtAcctID),
      typeof (INTranCost.invtSubID),
      typeof (INTranCost.costSiteID),
      typeof (INTranCost.tranAmt),
      typeof (INTranCost.tranPeriodID),
      typeof (INTranCost.finPeriodID),
      typeof (INTranCost.tranCost),
      typeof (INTran.docType),
      typeof (INTran.refNbr),
      typeof (INTran.origModule),
      typeof (INTran.siteID),
      typeof (INTran.tranDate),
      typeof (INTran.inventoryID),
      typeof (INTran.costCenterID)
    }))
    {
      PXView view = ((PXSelectBase) pxSelectReadonly2_2).View;
      object[] objArray3 = new object[1]
      {
        (object) itemsite
      };
      object[] objArray4 = new object[1]
      {
        (object) minPeriod
      };
      foreach (PXResult<INTran, INTranCost> pxResult in view.SelectMultiBound(objArray3, objArray4))
      {
        INReleaseProcess.UpdateCostHist((PXGraph) this, PXResult<INTran, INTranCost>.op_Implicit(pxResult), PXResult<INTran, INTranCost>.op_Implicit(pxResult));
        INReleaseProcess.UpdateSiteHistByCostCenterDCost((PXGraph) this, PXResult<INTran, INTranCost>.op_Implicit(pxResult), PXResult<INTran, INTranCost>.op_Implicit(pxResult));
      }
    }
    ((PXSelectBase) this.itemcosthist).Cache.Persist((PXDBOperation) 2);
    ((PXSelectBase) this.itemcosthist).Cache.Persist((PXDBOperation) 1);
    ((PXSelectBase) this.itemsitehist).Cache.Persist((PXDBOperation) 2);
    ((PXSelectBase) this.itemsitehist).Cache.Persist((PXDBOperation) 1);
    ((PXSelectBase) this.itemsitehistbycostcenterd).Cache.Persist((PXDBOperation) 2);
    ((PXSelectBase) this.itemsitehistbycostcenterd).Cache.Persist((PXDBOperation) 1);
    ((PXSelectBase) this.itemsitehistday).Cache.Persist((PXDBOperation) 2);
    ((PXSelectBase) this.itemsitehistday).Cache.Persist((PXDBOperation) 1);
    ((PXSelectBase) this.itemsalehistd).Cache.Persist((PXDBOperation) 2);
    ((PXSelectBase) this.itemsalehistd).Cache.Persist((PXDBOperation) 1);
    ((PXSelectBase) this.itemcustsalesstats).Cache.Persist((PXDBOperation) 2);
    ((PXSelectBase) this.itemcustsalesstats).Cache.Persist((PXDBOperation) 1);
    ((PXSelectBase) this.itemcustdropshipstats).Cache.Persist((PXDBOperation) 2);
    ((PXSelectBase) this.itemcustdropshipstats).Cache.Persist((PXDBOperation) 1);
  }

  public virtual IEnumerable<PXDataFieldParam> AssignAllDBDecimalFieldsToZeroCommand(
    PXCache cache,
    params string[] excludeFields)
  {
    return (IEnumerable<PXDataFieldParam>) cache.GetAllDBDecimalFields(excludeFields).Select<string, PXDataFieldAssign>((Func<string, PXDataFieldAssign>) (f => new PXDataFieldAssign(f, (PXDbType) 5, (object) 0M)));
  }

  public virtual void DeleteZeroStatusRecords(INItemSiteSummary itemsite)
  {
    List<PXDataFieldRestrict> dataFieldRestrictList = new List<PXDataFieldRestrict>();
    dataFieldRestrictList.Add(new PXDataFieldRestrict("InventoryID", (PXDbType) 8, new int?(4), (object) itemsite.InventoryID, (PXComp) 0));
    dataFieldRestrictList.Add(new PXDataFieldRestrict("SiteID", (PXDbType) 8, new int?(4), (object) itemsite.SiteID, (PXComp) 0));
    dataFieldRestrictList.Add(new PXDataFieldRestrict("QtyOnHand", (PXDbType) 5, (object) 0M));
    dataFieldRestrictList.Add(new PXDataFieldRestrict("QtyAvail", (PXDbType) 5, (object) 0M));
    dataFieldRestrictList.Add(new PXDataFieldRestrict("QtyHardAvail", (PXDbType) 5, (object) 0M));
    dataFieldRestrictList.Add(new PXDataFieldRestrict("QtyActual", (PXDbType) 5, (object) 0M));
    dataFieldRestrictList.AddRange(((PXSelectBase) this.locationstatusbycostcenter).Cache.GetAllDBDecimalFields().Select<string, PXDataFieldRestrict>((Func<string, PXDataFieldRestrict>) (f => new PXDataFieldRestrict(f, (PXDbType) 5, (object) 0M))));
    PXDatabase.Delete<INLocationStatusByCostCenter>(dataFieldRestrictList.ToArray());
  }

  public virtual bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    return true;
  }

  public virtual bool RowImporting(string viewName, object row) => row == null;

  public virtual bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public virtual void PrepareItems(string viewName, IEnumerable items)
  {
  }

  public virtual void ImportDone(PXImportAttribute.ImportMode.Value mode)
  {
  }

  public class ItemPlanHelper : PX.Objects.IN.GraphExtensions.ItemPlanHelper<INIntegrityCheck>
  {
  }
}
