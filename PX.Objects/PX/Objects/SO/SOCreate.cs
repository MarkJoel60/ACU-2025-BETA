// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOCreate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.Maintenance;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.AR.MigrationMode;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.SO;

[TableAndChartDashboardType]
[Serializable]
public class SOCreate : PXGraph<
#nullable disable
SOCreate>
{
  public PXCancel<SOCreate.SOCreateFilter> Cancel;
  public PXAction<SOCreate.SOCreateFilter> viewDocument;
  public PXFilter<SOCreate.SOCreateFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessingJoin<SOCreate.SOFixedDemand, SOCreate.SOCreateFilter, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<SOCreate.SOFixedDemand.inventoryID>>, LeftJoin<SOOrder, On<SOOrder.noteID, Equal<SOCreate.SOFixedDemand.refNoteID>>, LeftJoin<SOLineSplit, On<SOLineSplit.planID, Equal<SOCreate.SOFixedDemand.planID>>, LeftJoin<INItemClass, On<PX.Objects.IN.InventoryItem.FK.ItemClass>>>>>, Where2<Where<SOCreate.SOFixedDemand.inventoryID, Equal<Current<SOCreate.SOCreateFilter.inventoryID>>, Or<Current<SOCreate.SOCreateFilter.inventoryID>, IsNull>>, And2<Where<SOCreate.SOFixedDemand.demandSiteID, Equal<Current<SOCreate.SOCreateFilter.siteID>>, Or<Current<SOCreate.SOCreateFilter.siteID>, IsNull>>, And2<Where<SOCreate.SOFixedDemand.sourceSiteID, Equal<Current<SOCreate.SOCreateFilter.sourceSiteID>>, Or<Current<SOCreate.SOCreateFilter.sourceSiteID>, IsNull>>, And2<Where<SOOrder.customerID, Equal<Current<SOCreate.SOCreateFilter.customerID>>, Or<Current<SOCreate.SOCreateFilter.customerID>, IsNull>>, And2<Where<SOOrder.orderType, Equal<Current<SOCreate.SOCreateFilter.orderType>>, Or<Current<SOCreate.SOCreateFilter.orderType>, IsNull>>, And2<Where<SOOrder.orderNbr, Equal<Current<SOCreate.SOCreateFilter.orderNbr>>, Or<Current<SOCreate.SOCreateFilter.orderNbr>, IsNull>>, And<Where<INItemClass.itemClassCD, Like<Current<SOCreate.SOCreateFilter.itemClassCDWildcard>>, Or<Current<SOCreate.SOCreateFilter.itemClassCDWildcard>, IsNull>>>>>>>>>, OrderBy<Asc<SOCreate.SOFixedDemand.inventoryID>>> FixedDemand;
  public PXAction<SOCreate.SOCreateFilter> inventorySummary;

  public SOCreate()
  {
    ARSetupNoMigrationMode.EnsureMigrationModeDisabled((PXGraph) this);
    PXUIFieldAttribute.SetEnabled<SOCreate.SOFixedDemand.sourceSiteID>(((PXSelectBase) this.FixedDemand).Cache, (object) null, true);
    PXUIFieldAttribute.SetDisplayName<PX.Objects.IN.InventoryItem.descr>(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)], "Item Description");
    PXUIFieldAttribute.SetDisplayName<PX.Objects.IN.INSite.descr>(((PXGraph) this).Caches[typeof (PX.Objects.IN.INSite)], "Warehouse Description");
    PXUIFieldAttribute.SetDisplayName<INPlanType.localizedDescr>(((PXGraph) this).Caches[typeof (INPlanType)], "Plan Type");
  }

  protected IEnumerable filter()
  {
    SOCreate.SOCreateFilter current = ((PXSelectBase<SOCreate.SOCreateFilter>) this.Filter).Current;
    current.OrderVolume = new Decimal?(0M);
    current.OrderWeight = new Decimal?(0M);
    foreach (SOCreate.SOFixedDemand soFixedDemand in ((PXSelectBase) this.FixedDemand).Cache.Updated)
    {
      if (soFixedDemand.Selected.GetValueOrDefault())
      {
        SOCreate.SOCreateFilter soCreateFilter1 = current;
        Decimal? nullable1 = soCreateFilter1.OrderVolume;
        Decimal? nullable2 = soFixedDemand.ExtVolume;
        Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
        Decimal? nullable3;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable3 = nullable2;
        }
        else
          nullable3 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault1);
        soCreateFilter1.OrderVolume = nullable3;
        SOCreate.SOCreateFilter soCreateFilter2 = current;
        nullable1 = soCreateFilter2.OrderWeight;
        nullable2 = soFixedDemand.ExtWeight;
        Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
        Decimal? nullable4;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable4 = nullable2;
        }
        else
          nullable4 = new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault2);
        soCreateFilter2.OrderWeight = nullable4;
      }
    }
    yield return (object) current;
  }

  protected virtual void SOCreateFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<SOCreate.SOFixedDemand>) this.FixedDemand).SetProcessDelegate(new PXProcessingBase<SOCreate.SOFixedDemand>.ProcessListDelegate((object) new SOCreate.\u003C\u003Ec__DisplayClass6_0()
    {
      filter = ((PXSelectBase<SOCreate.SOCreateFilter>) this.Filter).Current
    }, __methodptr(\u003CSOCreateFilter_RowSelected\u003Eb__0)));
    TimeSpan timeSpan;
    Exception exception;
    PXLongRunStatus status = PXLongOperation.GetStatus(((PXGraph) this).UID, ref timeSpan, ref exception);
    PXUIFieldAttribute.SetVisible<SOLine.orderNbr>(((PXGraph) this).Caches[typeof (SOLine)], (object) null, status == 2 || status == 3);
    if (!PXAccess.FeatureInstalled<FeaturesSet.warehouse>())
      return;
    PX.Objects.IN.INSite inSite = PXResultset<PX.Objects.IN.INSite>.op_Implicit(PXSelectBase<PX.Objects.IN.INSite, PXSelectReadonly<PX.Objects.IN.INSite, Where<PX.Objects.IN.INSite.siteID, Equal<Current<SOCreate.SOCreateFilter.siteID>>, And<PX.Objects.IN.INSite.active, Equal<True>, And<Where<PX.Objects.IN.INSite.addressID, IsNull, Or<PX.Objects.IN.INSite.contactID, IsNull>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      e.Row
    }, Array.Empty<object>()));
    if (inSite != null)
      throw new PXSetupNotEnteredException<PX.Objects.IN.INSite, PX.Objects.IN.INSite.siteCD>("The Multiple Warehouses feature and the Transfer order type are activated in the system, in this case an address and a contact must be configured for the '{1}' warehouse.", inSite.SiteCD, new object[1]
      {
        (object) inSite.SiteCD
      });
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  public virtual void SOCreateFilter_ItemClassCDWildCard_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<SOCreate.SOFixedDemand> e)
  {
    if (e.Row == null || !e.Row.IsSpecialOrder.GetValueOrDefault())
      return;
    PXUIFieldAttribute.SetEnabled<SOCreate.SOFixedDemand.sourceSiteID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOCreate.SOFixedDemand>>) e).Cache, (object) e.Row, false);
  }

  public static void SOCreateProc(List<SOCreate.SOFixedDemand> list, DateTime? PurchDate)
  {
    SOOrderEntry docgraph = PXGraph.CreateInstance<SOOrderEntry>();
    SOSetup current = ((PXSelectBase<SOSetup>) docgraph.sosetup).Current;
    DocumentList<SOOrder> documentList = new DocumentList<SOOrder>((PXGraph) docgraph);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) docgraph).ExceptionHandling.AddHandler<SOLineSplit.qty>(SOCreate.\u003C\u003Ec.\u003C\u003E9__9_0 ?? (SOCreate.\u003C\u003Ec.\u003C\u003E9__9_0 = new PXExceptionHandling((object) SOCreate.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CSOCreateProc\u003Eb__9_0))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) docgraph).ExceptionHandling.AddHandler<SOLineSplit.isAllocated>(SOCreate.\u003C\u003Ec.\u003C\u003E9__9_1 ?? (SOCreate.\u003C\u003Ec.\u003C\u003E9__9_1 = new PXExceptionHandling((object) SOCreate.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CSOCreateProc\u003Eb__9_1))));
    foreach (SOCreate.SOFixedDemand demand in list)
    {
      string str = current.TransferOrderType ?? "TR";
      string planType = demand.PlanType;
      try
      {
        int? sourceSiteId1 = demand.SourceSiteID;
        if (!sourceSiteId1.HasValue)
        {
          PXProcessing<SOCreate.SOFixedDemand>.SetWarning(list.IndexOf(demand), "Missing Source Site to create transfer.");
        }
        else
        {
          SOLineSplit2 soLineSplit2 = PXResultset<SOLineSplit2>.op_Implicit(PXSelectBase<SOLineSplit2, PXSelect<SOLineSplit2, Where<SOLineSplit2.planID, Equal<Required<SOLineSplit2.planID>>>>.Config>.Select((PXGraph) docgraph, new object[1]
          {
            (object) demand.PlanID
          }));
          SOOrder soOrder;
          if (soLineSplit2 != null)
          {
            soOrder = documentList.Find<SOOrder.orderType, SOOrder.destinationSiteID, SOOrder.defaultSiteID>((object) str, (object) soLineSplit2.ToSiteID, (object) soLineSplit2.SiteID);
          }
          else
          {
            sourceSiteId1 = demand.SourceSiteID;
            int? siteId = demand.SiteID;
            if (sourceSiteId1.GetValueOrDefault() == siteId.GetValueOrDefault() & sourceSiteId1.HasValue == siteId.HasValue)
            {
              PXProcessing<SOCreate.SOFixedDemand>.SetWarning(list.IndexOf(demand), "Source and destination sites should not be the same.");
              continue;
            }
            soOrder = documentList.Find<SOOrder.orderType, SOOrder.destinationSiteID, SOOrder.defaultSiteID>((object) str, (object) demand.SiteID, (object) demand.SourceSiteID);
          }
          if (soOrder == null)
            soOrder = new SOOrder();
          if (soOrder.OrderNbr == null)
          {
            ((PXGraph) docgraph).Clear();
            if (soLineSplit2 != null)
            {
              PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) docgraph, soLineSplit2.SiteID);
              soOrder.BranchID = inSite.BranchID;
              soOrder.OrderType = str;
              SOOrder copy = PXCache<SOOrder>.CreateCopy(((PXSelectBase<SOOrder>) docgraph.Document).Insert(soOrder));
              copy.DefaultSiteID = soLineSplit2.SiteID;
              copy.DestinationSiteID = soLineSplit2.ToSiteID;
              copy.OrderDate = PurchDate;
              ((PXSelectBase<SOOrder>) docgraph.Document).Update(copy);
            }
            else
            {
              PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) docgraph, demand.SourceSiteID);
              soOrder.BranchID = inSite.BranchID;
              soOrder.OrderType = str;
              SOOrder copy = PXCache<SOOrder>.CreateCopy(((PXSelectBase<SOOrder>) docgraph.Document).Insert(soOrder));
              copy.DefaultSiteID = demand.SourceSiteID;
              copy.DestinationSiteID = demand.SiteID;
              copy.OrderDate = PurchDate;
              ((PXSelectBase<SOOrder>) docgraph.Document).Update(copy);
            }
          }
          else if (!((PXSelectBase) docgraph.Document).Cache.ObjectsEqual((object) ((PXSelectBase<SOOrder>) docgraph.Document).Current, (object) soOrder))
            ((PXSelectBase<SOOrder>) docgraph.Document).Current = PXResultset<SOOrder>.op_Implicit(((PXSelectBase<SOOrder>) docgraph.Document).Search<SOOrder.orderNbr>((object) soOrder.OrderNbr, new object[1]
            {
              (object) soOrder.OrderType
            }));
          PXCache cach = ((PXGraph) docgraph).Caches[typeof (INItemPlan)];
          SOLine soLine1;
          INItemPlan copy1;
          SOLineSplit soLineSplit;
          if (soLineSplit2 != null)
          {
            soLine1 = (SOLine) null;
            if (!demand.IsSpecialOrder.GetValueOrDefault())
              ((PXSelectBase<SOLine>) docgraph.Transactions).Current = soLine1 = PXResultset<SOLine>.op_Implicit(((PXSelectBase<SOLine>) docgraph.Transactions).Search<SOLine.inventoryID, SOLine.subItemID, SOLine.uOM, SOLine.siteID, SOLine.pOCreate>((object) demand.InventoryID, (object) demand.SubItemID, (object) demand.DemandUOM, (object) demand.SiteID, (object) false, Array.Empty<object>()));
            bool? isSpecialOrder;
            if (soLine1 == null)
            {
              SOLine copy2 = PXCache<SOLine>.CreateCopy(((PXSelectBase<SOLine>) docgraph.Transactions).Insert());
              copy2.IsStockItem = new bool?(true);
              copy2.InventoryID = demand.InventoryID;
              copy2.SubItemID = demand.SubItemID;
              copy2.SiteID = demand.SiteID;
              copy2.UOM = demand.DemandUOM;
              copy2.OrderQty = new Decimal?(0M);
              copy2.IsSpecialOrder = demand.IsSpecialOrder;
              copy2.OrigOrderType = demand.OrderType;
              copy2.OrigOrderNbr = demand.OrderNbr;
              copy2.OrigLineNbr = demand.LineNbr;
              isSpecialOrder = demand.IsSpecialOrder;
              if (isSpecialOrder.GetValueOrDefault())
              {
                copy2.UnitCost = demand.UnitCost;
                copy2.CuryUnitCost = demand.CuryUnitCost;
              }
              soLine1 = ((PXSelectBase<SOLine>) docgraph.Transactions).Update(copy2);
            }
            SOLineSplit split = new SOLineSplit();
            split.InventoryID = demand.InventoryID;
            split.LotSerialNbr = soLineSplit2.LotSerialNbr;
            split.IsAllocated = new bool?(true);
            split.IsMergeable = new bool?(false);
            split.SiteID = demand.SiteID;
            setUomAndQty(split, demand);
            split.RefNoteID = demand.RefNoteID;
            copy1 = PXCache<INItemPlan>.CreateCopy((INItemPlan) demand);
            cach.RaiseRowDeleted((object) demand);
            soLineSplit = ((PXSelectBase<SOLineSplit>) docgraph.splits).Insert(split);
            soLineSplit2.SOOrderType = soLineSplit.OrderType;
            soLineSplit2.SOOrderNbr = soLineSplit.OrderNbr;
            soLineSplit2.SOLineNbr = soLineSplit.LineNbr;
            soLineSplit2.SOSplitLineNbr = soLineSplit.SplitLineNbr;
            ((PXSelectBase<SOLineSplit2>) docgraph.sodemand).Update(soLineSplit2);
            copy1.SiteID = soLineSplit2.ToSiteID;
            copy1.PlanType = "93";
            copy1.FixedSource = (string) null;
            copy1.SupplyPlanID = soLineSplit.PlanID;
            isSpecialOrder = demand.IsSpecialOrder;
            if (isSpecialOrder.GetValueOrDefault())
              copy1.CostCenterID = demand.LineCostCenterID;
            cach.RaiseRowInserted((object) copy1);
            GraphHelper.MarkUpdated(cach, (object) copy1, true);
          }
          else
          {
            PXOrderedSelect<SOOrder, SOLine, Where<SOLine.orderType, Equal<Current<SOOrder.orderType>>, And<SOLine.orderNbr, Equal<Current<SOOrder.orderNbr>>>>, OrderBy<Asc<SOLine.orderType, Asc<SOLine.orderNbr, Asc<SOLine.sortOrder, Asc<SOLine.lineNbr>>>>>> transactions1 = docgraph.Transactions;
            PXOrderedSelect<SOOrder, SOLine, Where<SOLine.orderType, Equal<Current<SOOrder.orderType>>, And<SOLine.orderNbr, Equal<Current<SOOrder.orderNbr>>>>, OrderBy<Asc<SOLine.orderType, Asc<SOLine.orderNbr, Asc<SOLine.sortOrder, Asc<SOLine.lineNbr>>>>>> transactions2 = docgraph.Transactions;
            // ISSUE: variable of a boxed type
            __Boxed<int?> inventoryId = (ValueType) demand.InventoryID;
            // ISSUE: variable of a boxed type
            __Boxed<int?> subItemId = (ValueType) demand.SubItemID;
            string demandUom = demand.DemandUOM;
            // ISSUE: variable of a boxed type
            __Boxed<int?> sourceSiteId2 = (ValueType) demand.SourceSiteID;
            // ISSUE: variable of a boxed type
            __Boxed<bool> hasValue = (ValueType) demand.VendorID.HasValue;
            object[] objArray = Array.Empty<object>();
            SOLine soLine2;
            soLine1 = soLine2 = PXResultset<SOLine>.op_Implicit(((PXSelectBase<SOLine>) transactions2).Search<SOLine.inventoryID, SOLine.subItemID, SOLine.uOM, SOLine.siteID, SOLine.pOCreate>((object) inventoryId, (object) subItemId, (object) demandUom, (object) sourceSiteId2, (object) hasValue, objArray));
            ((PXSelectBase<SOLine>) transactions1).Current = soLine2;
            bool flag = demand.FixedSource == "X";
            if (soLine1 == null)
            {
              SOLine copy3 = PXCache<SOLine>.CreateCopy(((PXSelectBase<SOLine>) docgraph.Transactions).Insert());
              copy3.IsStockItem = new bool?(true);
              copy3.InventoryID = demand.InventoryID;
              copy3.SubItemID = demand.SubItemID;
              copy3.SiteID = demand.SourceSiteID;
              copy3.UOM = demand.DemandUOM;
              copy3.OrderQty = new Decimal?(0M);
              if (flag)
              {
                copy3.POCreate = new bool?(true);
                copy3.POSource = "O";
              }
              copy3.VendorID = demand.VendorID;
              soLine1 = ((PXSelectBase<SOLine>) docgraph.Transactions).Update(copy3);
            }
            SOLineSplit split = new SOLineSplit();
            split.SiteID = soLine1.SiteID;
            split.InventoryID = demand.InventoryID;
            split.IsAllocated = soLine1.RequireAllocation;
            setUomAndQty(split, demand);
            if (flag)
            {
              split.POCreate = new bool?(true);
              split.POSource = "O";
            }
            split.VendorID = demand.VendorID;
            soLineSplit = ((PXSelectBase<SOLineSplit>) docgraph.splits).Insert(split) ?? ((PXSelectBase<SOLineSplit>) docgraph.splits).Current;
            copy1 = PXCache<INItemPlan>.CreateCopy((INItemPlan) demand);
            cach.RaiseRowDeleted((object) demand);
            copy1.SiteID = demand.SiteID;
            copy1.PlanType = "94";
            copy1.FixedSource = (string) null;
            copy1.SupplyPlanID = soLineSplit.PlanID;
            cach.RaiseRowInserted((object) copy1);
            GraphHelper.MarkUpdated(cach, (object) copy1, true);
          }
          if (!soLineSplit.PlanID.HasValue)
            throw new PXRowPersistedException(typeof (SOLine).Name, (object) soLine1, "Unable to create SO order, require location and lot/serial information should be off.");
          if (((PXSelectBase) docgraph.Transactions).Cache.IsInsertedUpdatedDeleted)
          {
            using (PXTransactionScope transactionScope = new PXTransactionScope())
            {
              ((PXAction) docgraph.Save).Press();
              if (planType == "90")
              {
                ((PXSelectBase<INReplenishmentOrder>) docgraph.Replenihment).Current = PXResultset<INReplenishmentOrder>.op_Implicit(((PXSelectBase<INReplenishmentOrder>) docgraph.Replenihment).Search<INReplenishmentOrder.noteID>((object) demand.RefNoteID, Array.Empty<object>()));
                if (((PXSelectBase<INReplenishmentOrder>) docgraph.Replenihment).Current != null)
                {
                  INReplenishmentLine copy4 = PXCache<INReplenishmentLine>.CreateCopy(((PXSelectBase<INReplenishmentLine>) docgraph.ReplenishmentLinesWithPlans).Insert(new INReplenishmentLine()));
                  copy4.InventoryID = soLineSplit.InventoryID;
                  copy4.SubItemID = soLineSplit.SubItemID;
                  copy4.UOM = soLineSplit.UOM;
                  copy4.Qty = soLineSplit.Qty;
                  copy4.SOType = soLineSplit.OrderType;
                  copy4.SONbr = ((PXSelectBase<SOOrder>) docgraph.Document).Current.OrderNbr;
                  copy4.SOLineNbr = soLineSplit.LineNbr;
                  copy4.SOSplitLineNbr = soLineSplit.SplitLineNbr;
                  copy4.SiteID = demand.SourceSiteID;
                  copy4.DestinationSiteID = demand.SiteID;
                  copy4.PlanID = demand.PlanID;
                  ((PXSelectBase<INReplenishmentLine>) docgraph.ReplenishmentLinesWithPlans).Update(copy4);
                  INItemPlan inItemPlan = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.planID, Equal<Required<INItemPlan.planID>>>>.Config>.SelectWindowed((PXGraph) docgraph, 0, 1, new object[1]
                  {
                    (object) demand.SupplyPlanID
                  }));
                  if (inItemPlan != null)
                  {
                    copy1.SupplyPlanID = inItemPlan.PlanID;
                    GraphHelper.MarkUpdated(cach, (object) copy1, true);
                  }
                  ((PXAction) docgraph.Save).Press();
                }
              }
              transactionScope.Complete();
            }
            PXProcessing<SOCreate.SOFixedDemand>.SetInfo(list.IndexOf(demand), PXMessages.LocalizeFormatNoPrefixNLA("Transfer Order '{0}' created.", new object[1]
            {
              (object) ((PXSelectBase<SOOrder>) docgraph.Document).Current.OrderNbr
            }));
            if (documentList.Find((object) ((PXSelectBase<SOOrder>) docgraph.Document).Current) == null)
              documentList.Add(((PXSelectBase<SOOrder>) docgraph.Document).Current);
          }
        }
      }
      catch (Exception ex)
      {
        ((PXGraph) docgraph).Clear();
        PXProcessing<SOCreate.SOFixedDemand>.SetError(list.IndexOf(demand), ex);
      }
    }
    if (documentList.Count != 1)
      return;
    using (new PXTimeStampScope((byte[]) null))
    {
      ((PXGraph) docgraph).Clear();
      ((PXSelectBase<SOOrder>) docgraph.Document).Current = PXResultset<SOOrder>.op_Implicit(((PXSelectBase<SOOrder>) docgraph.Document).Search<PX.Objects.PO.POOrder.orderNbr>((object) documentList[0].OrderNbr, new object[1]
      {
        (object) documentList[0].OrderType
      }));
      throw new PXRedirectRequiredException((PXGraph) docgraph, "Sales Order");
    }

    void setUomAndQty(SOLineSplit split, SOCreate.SOFixedDemand demand)
    {
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) docgraph, split.InventoryID);
      object obj;
      ((PXSelectBase) docgraph.splits).Cache.RaiseFieldDefaulting<SOLineSplit.uOM>((object) split, ref obj);
      split.UOM = (string) obj ?? inventoryItem.BaseUnit;
      split.Qty = !(split.UOM == inventoryItem.BaseUnit) ? (!(split.UOM == demand.DemandUOM) ? new Decimal?(INUnitAttribute.ConvertFromBase(((PXSelectBase) docgraph.splits).Cache, split.InventoryID, split.UOM, demand.PlanQty.GetValueOrDefault(), INPrecision.QUANTITY)) : demand.OrderQty) : demand.PlanQty;
      split.BaseQty = demand.PlanQty;
    }
  }

  [PXUIField]
  [PXButton(VisibleOnProcessingResults = true, IsLockedOnToolbar = true)]
  public virtual IEnumerable InventorySummary(PXAdapter adapter)
  {
    PXCache cache = ((PXSelectBase) this.FixedDemand).Cache;
    SOCreate.SOFixedDemand current = ((PXSelectBase<SOCreate.SOFixedDemand>) this.FixedDemand).Current;
    if (current == null)
      return adapter.Get();
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, current.InventoryID);
    if (inventoryItem != null && inventoryItem.StkItem.GetValueOrDefault())
    {
      INSubItem inSubItem = (INSubItem) PXSelectorAttribute.Select<SOCreate.SOFixedDemand.subItemID>(cache, (object) current);
      InventorySummaryEnq.Redirect(inventoryItem.InventoryID, inSubItem?.SubItemCD, current.SiteID, current.LocationID);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    PXCache cache = ((PXSelectBase) this.FixedDemand).Cache;
    SOCreate.SOFixedDemand current = ((PXSelectBase<SOCreate.SOFixedDemand>) this.FixedDemand).Current;
    if (current == null)
      return adapter.Get();
    SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
    ((PXSelectBase<SOOrder>) instance.Document).Current = PXResultset<SOOrder>.op_Implicit(((PXSelectBase<SOOrder>) instance.Document).Search<SOOrder.orderNbr>((object) current.OrderNbr, new object[1]
    {
      (object) current.OrderType
    }));
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Document");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [Serializable]
  public class SOCreateFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected bool? _MyOwner;
    protected int? _OwnerID;
    protected int? _WorkGroupID;
    protected bool? _MyWorkGroup;
    protected int? _VendorID;
    protected int? _SiteID;
    protected DateTime? _EndDate;
    protected DateTime? _PurchDate;
    protected int? _CustomerID;
    protected int? _InventoryID;
    protected string _ItemClassCD;
    protected int? _SourceSiteID;
    protected string _OrderType;
    protected string _OrderNbr;
    protected Decimal? _OrderWeight;
    protected Decimal? _OrderVolume;

    [PXDBInt]
    [CRCurrentOwnerID]
    public virtual int? CurrentOwnerID { get; set; }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Me")]
    public virtual bool? MyOwner
    {
      get => this._MyOwner;
      set => this._MyOwner = value;
    }

    [SubordinateOwner(DisplayName = "Product Manager")]
    public virtual int? OwnerID
    {
      get => !this._MyOwner.GetValueOrDefault() ? this._OwnerID : this.CurrentOwnerID;
      set => this._OwnerID = value;
    }

    [PXDBInt]
    [PXUIField(DisplayName = "Product  Workgroup")]
    [PXSelector(typeof (Search<EPCompanyTree.workGroupID, Where<EPCompanyTree.workGroupID, IsWorkgroupOrSubgroupOfContact<Current<AccessInfo.contactID>>>>), SubstituteKey = typeof (EPCompanyTree.description))]
    public virtual int? WorkGroupID
    {
      get => !this._MyWorkGroup.GetValueOrDefault() ? this._WorkGroupID : new int?();
      set => this._WorkGroupID = value;
    }

    [PXDefault(false)]
    [PXDBBool]
    [PXUIField]
    public virtual bool? MyWorkGroup
    {
      get => this._MyWorkGroup;
      set => this._MyWorkGroup = value;
    }

    [PXDefault(false)]
    [PXDBBool]
    public virtual bool? FilterSet
    {
      get
      {
        return new bool?(this.OwnerID.HasValue || this.WorkGroupID.HasValue || this.MyWorkGroup.GetValueOrDefault());
      }
    }

    [VendorNonEmployeeActive]
    public virtual int? VendorID
    {
      get => this._VendorID;
      set => this._VendorID = value;
    }

    [Site(DisplayName = " To Warehouse")]
    public virtual int? SiteID
    {
      get => this._SiteID;
      set => this._SiteID = value;
    }

    [PXDBDate]
    [PXUIField(DisplayName = "Date Promised")]
    [PXDefault(typeof (AccessInfo.businessDate))]
    public virtual DateTime? EndDate
    {
      get => this._EndDate;
      set => this._EndDate = value;
    }

    [PXDBDate]
    [PXUIField(DisplayName = "Creation Date")]
    [PXDefault(typeof (AccessInfo.businessDate))]
    public virtual DateTime? PurchDate
    {
      get => this._PurchDate;
      set => this._PurchDate = value;
    }

    [Customer]
    public virtual int? CustomerID
    {
      get => this._CustomerID;
      set => this._CustomerID = value;
    }

    [StockItem]
    public virtual int? InventoryID
    {
      get => this._InventoryID;
      set => this._InventoryID = value;
    }

    [PXDBString(30, IsUnicode = true)]
    [PXUIField]
    [PXDimensionSelector("INITEMCLASS", typeof (Search<INItemClass.itemClassCD, Where<INItemClass.stkItem, Equal<boolTrue>>>), DescriptionField = typeof (INItemClass.descr), ValidComboRequired = true)]
    public virtual string ItemClassCD
    {
      get => this._ItemClassCD;
      set => this._ItemClassCD = value;
    }

    [PXString(IsUnicode = true)]
    [PXUIField]
    [PXDimension("INITEMCLASS", ParentSelect = typeof (Select<INItemClass>), ParentValueField = typeof (INItemClass.itemClassCD), AutoNumbering = false)]
    public virtual string ItemClassCDWildcard
    {
      get => DimensionTree<INItemClass.dimension>.MakeWildcard(this.ItemClassCD);
      set
      {
      }
    }

    [Site(DisplayName = "From Warehouse", DescriptionField = typeof (PX.Objects.IN.INSite.descr))]
    public virtual int? SourceSiteID
    {
      get => this._SourceSiteID;
      set => this._SourceSiteID = value;
    }

    [PXDBString(2, IsFixed = true, InputMask = ">aa")]
    [PXSelector(typeof (Search<SOOrderType.orderType, Where<SOOrderType.active, Equal<boolTrue>>>))]
    [PXUIField]
    public virtual string OrderType
    {
      get => this._OrderType;
      set => this._OrderType = value;
    }

    [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
    [PXUIField]
    [PX.Objects.SO.SO.RefNbr(typeof (Search2<SOOrder.orderNbr, LeftJoinSingleTable<PX.Objects.AR.Customer, On<SOOrder.customerID, Equal<PX.Objects.AR.Customer.bAccountID>, And<Where<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>, Where<SOOrder.orderType, Equal<Optional<SOCreate.SOCreateFilter.orderType>>, And<Where<SOOrder.orderType, Equal<SOOrderTypeConstants.transferOrder>, Or<PX.Objects.AR.Customer.bAccountID, IsNotNull>>>>, OrderBy<Desc<SOOrder.orderNbr>>>))]
    [PXFormula(typeof (Default<SOCreate.SOCreateFilter.orderType>))]
    public virtual string OrderNbr
    {
      get => this._OrderNbr;
      set => this._OrderNbr = value;
    }

    [PXDBDecimal(6)]
    [PXUIField(DisplayName = "Weight", Enabled = false)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXFormula(null, typeof (SumCalc<SOCreate.SOFixedDemand.extWeight>))]
    public virtual Decimal? OrderWeight
    {
      get => this._OrderWeight;
      set => this._OrderWeight = value;
    }

    [PXDBDecimal(6)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Volume", Enabled = false)]
    [PXFormula(null, typeof (SumCalc<SOCreate.SOFixedDemand.extVolume>))]
    public virtual Decimal? OrderVolume
    {
      get => this._OrderVolume;
      set => this._OrderVolume = value;
    }

    public abstract class currentOwnerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOCreate.SOCreateFilter.currentOwnerID>
    {
    }

    public abstract class myOwner : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOCreate.SOCreateFilter.myOwner>
    {
    }

    public abstract class ownerID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOCreate.SOCreateFilter.ownerID>
    {
    }

    public abstract class workGroupID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOCreate.SOCreateFilter.workGroupID>
    {
    }

    public abstract class myWorkGroup : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      SOCreate.SOCreateFilter.myWorkGroup>
    {
    }

    public abstract class filterSet : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      SOCreate.SOCreateFilter.filterSet>
    {
    }

    public abstract class vendorID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOCreate.SOCreateFilter.vendorID>
    {
    }

    public abstract class siteID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOCreate.SOCreateFilter.siteID>
    {
    }

    public abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      SOCreate.SOCreateFilter.endDate>
    {
    }

    public abstract class purchDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      SOCreate.SOCreateFilter.purchDate>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOCreate.SOCreateFilter.customerID>
    {
    }

    public abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOCreate.SOCreateFilter.inventoryID>
    {
    }

    public abstract class itemClassCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOCreate.SOCreateFilter.itemClassCD>
    {
    }

    public abstract class itemClassCDWildcard : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOCreate.SOCreateFilter.itemClassCDWildcard>
    {
    }

    public abstract class sourceSiteID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOCreate.SOCreateFilter.sourceSiteID>
    {
    }

    public abstract class orderType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOCreate.SOCreateFilter.orderType>
    {
    }

    public abstract class orderNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOCreate.SOCreateFilter.orderNbr>
    {
    }

    public abstract class orderWeight : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      SOCreate.SOCreateFilter.orderWeight>
    {
    }

    public abstract class orderVolume : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      SOCreate.SOCreateFilter.orderVolume>
    {
    }
  }

  /// <summary>
  /// Returns records that are displayed in SOCreate screen.
  /// Please refer to SOCreate screen documentation for details.
  /// </summary>
  public class SOCreateProjectionAttribute : OwnedFilter.ProjectionAttribute
  {
    public SOCreateProjectionAttribute()
      : base(typeof (SOCreate.SOCreateFilter), BqlCommand.Compose(new System.Type[7]
      {
        typeof (Select2<,,>),
        typeof (INItemPlan),
        typeof (InnerJoin<INPlanType, On<INItemPlan.FK.PlanType>, InnerJoin<PX.Objects.IN.InventoryItem, On<INItemPlan.FK.InventoryItem>, InnerJoin<INUnit, On<INUnit.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<INUnit.toUnit, Equal<PX.Objects.IN.InventoryItem.baseUnit>>>, LeftJoin<SOLineSplit, On<SOLineSplit.planID, Equal<INItemPlan.planID>>, LeftJoin<SOLine, On<SOLineSplit.FK.OrderLine>, LeftJoin<PX.Objects.IN.S.INItemSite, On<PX.Objects.IN.S.INItemSite.inventoryID, Equal<INItemPlan.inventoryID>, And<PX.Objects.IN.S.INItemSite.siteID, Equal<INItemPlan.siteID>>>>>>>>>),
        typeof (Where2<,>),
        typeof (Where<INItemPlan.hold, Equal<boolFalse>, And2<Where<INItemPlan.fixedSource, Equal<INReplenishmentSource.transfer>, Or<INItemPlan.fixedSource, Equal<INReplenishmentSource.transferToPurchase>>>, And<INItemPlan.supplyPlanID, IsNull, And<INUnit.fromUnit, Equal<IsNull<SOLineSplit.uOM, PX.Objects.IN.InventoryItem.purchaseUnit>>>>>>),
        typeof (And<>),
        OwnedFilter.ProjectionAttribute.ComposeWhere(typeof (SOCreate.SOCreateFilter), typeof (PX.Objects.IN.INItemSite.productWorkgroupID), typeof (PX.Objects.IN.INItemSite.productManagerID))
      }))
    {
    }
  }

  [SOCreate.SOCreateProjection]
  public class SOFixedDemand : INItemPlan
  {
    protected int? _DemandSiteID;
    protected string _UnitMultDiv;
    protected Decimal? _UnitRate;
    protected Decimal? _OrderQty;
    protected string _OrderType;
    protected string _OrderNbr;
    protected Decimal? _ExtWeight;
    protected Decimal? _ExtVolume;

    [PXDBDate]
    [PXDefault]
    [PXUIField(DisplayName = "Requested On")]
    public override DateTime? PlanDate
    {
      get => this._PlanDate;
      set => this._PlanDate = value;
    }

    [PXDBString(1, IsFixed = true)]
    [PXUIField(DisplayName = "Fixed Source", Enabled = false)]
    [PXDefault("P")]
    [INReplenishmentSource.INPlanList]
    public override string FixedSource
    {
      get => this._FixedSource;
      set => this._FixedSource = value;
    }

    [PXDBString(2, IsFixed = true)]
    [PXDefault]
    [PXSelector(typeof (Search<INPlanType.planType>), CacheGlobal = true, DescriptionField = typeof (INPlanType.localizedDescr))]
    public override string PlanType
    {
      get => this._PlanType;
      set => this._PlanType = value;
    }

    [PXDBString(60, IsUnicode = true, BqlField = typeof (INPlanType.descr))]
    public virtual string PlanDescr { get; set; }

    [PXString(60, IsUnicode = true)]
    [PXUIField(DisplayName = "Plan Type")]
    [INPlanType.LocalizedField(typeof (SOCreate.SOFixedDemand.planDescr))]
    public virtual string LocalizedPlanDescr { get; set; }

    [PXInt]
    [PXDBCalced(typeof (IsNull<SOLineSplit.toSiteID, INItemPlan.siteID>), typeof (int))]
    [PXUIField]
    [PXDimensionSelector("INSITE", typeof (Search<PX.Objects.IN.INSite.siteID>), typeof (PX.Objects.IN.INSite.siteCD), DescriptionField = typeof (PX.Objects.IN.INSite.descr), CacheGlobal = true)]
    public virtual int? DemandSiteID
    {
      get => this._DemandSiteID;
      set => this._DemandSiteID = value;
    }

    [SiteAvail(typeof (SOCreate.SOFixedDemand.inventoryID), typeof (SOCreate.SOFixedDemand.subItemID), typeof (CostCenter.freeStock), new System.Type[] {typeof (PX.Objects.IN.INSite.siteCD), typeof (INSiteStatusByCostCenter.qtyOnHand), typeof (PX.Objects.IN.INSite.descr), typeof (PX.Objects.IN.INSite.replenishmentClassID)}, DisplayName = "From Warehouse", DescriptionField = typeof (PX.Objects.IN.INSite.descr), BqlField = typeof (INItemPlan.sourceSiteID))]
    public override int? SourceSiteID
    {
      get => this._SourceSiteID;
      set => this._SourceSiteID = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Requested Qty.")]
    public override Decimal? PlanQty
    {
      get => this._PlanQty;
      set => this._PlanQty = value;
    }

    /// <exclude />
    [PXDBString(BqlField = typeof (INUnit.fromUnit))]
    [PXUIField(DisplayName = "UOM")]
    public virtual string DemandUOM { get; set; }

    [PXDBString(1, IsFixed = true, BqlField = typeof (INUnit.unitMultDiv))]
    public virtual string UnitMultDiv
    {
      get => this._UnitMultDiv;
      set => this._UnitMultDiv = value;
    }

    [PXDBDecimal(6, BqlField = typeof (INUnit.unitRate))]
    public virtual Decimal? UnitRate
    {
      get => this._UnitRate;
      set => this._UnitRate = value;
    }

    [PXDBCalced(typeof (Switch<Case<Where<INUnit.unitMultDiv, Equal<MultDiv.divide>>, Mult<INItemPlan.planQty, INUnit.unitRate>>, Div<INItemPlan.planQty, INUnit.unitRate>>), typeof (Decimal))]
    [PXQuantity]
    [PXUIField(DisplayName = "Quantity")]
    public virtual Decimal? OrderQty
    {
      get => this._OrderQty;
      set => this._OrderQty = value;
    }

    [PXRefNote]
    [PXUIField(DisplayName = "Reference Nbr.")]
    public override Guid? RefNoteID
    {
      get => this._RefNoteID;
      set => this._RefNoteID = value;
    }

    [PXNote(BqlTable = typeof (SOLine))]
    public virtual Guid? NoteID { get; set; }

    [PXDBString(2, IsFixed = true, BqlField = typeof (SOLineSplit.orderType))]
    [PXUIField(DisplayName = "Order Type", Enabled = false)]
    public virtual string OrderType
    {
      get => this._OrderType;
      set => this._OrderType = value;
    }

    [PXDBString(15, BqlField = typeof (SOLineSplit.orderNbr), IsUnicode = true)]
    [PXUIField(DisplayName = "Order Nbr.", Enabled = false)]
    public virtual string OrderNbr
    {
      get => this._OrderNbr;
      set => this._OrderNbr = value;
    }

    [PXDBInt(BqlField = typeof (SOLineSplit.lineNbr))]
    public virtual int? LineNbr { get; set; }

    [PXDecimal(6)]
    [PXUIField(DisplayName = "Weight")]
    [PXFormula(typeof (Mult<SOCreate.SOFixedDemand.orderQty, Selector<SOCreate.SOFixedDemand.inventoryID, PX.Objects.IN.InventoryItem.baseWeight>>))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? ExtWeight
    {
      get => this._ExtWeight;
      set => this._ExtWeight = value;
    }

    [PXDecimal(6)]
    [PXUIField(DisplayName = "Volume")]
    [PXFormula(typeof (Mult<SOCreate.SOFixedDemand.orderQty, Selector<SOCreate.SOFixedDemand.inventoryID, PX.Objects.IN.InventoryItem.baseVolume>>))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? ExtVolume
    {
      get => this._ExtVolume;
      set => this._ExtVolume = value;
    }

    [PXDBBool(BqlField = typeof (SOLine.isSpecialOrder))]
    public virtual bool? IsSpecialOrder { get; set; }

    [PXDBInt(BqlField = typeof (SOLine.costCenterID))]
    public virtual int? LineCostCenterID { get; set; }

    [PXDBDecimal(6, BqlField = typeof (SOLine.curyUnitCost))]
    public virtual Decimal? CuryUnitCost { get; set; }

    /// <exclude />
    [PXDBPriceCost(BqlField = typeof (SOLine.unitCost))]
    public virtual Decimal? UnitCost { get; set; }

    public new abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.selected>
    {
    }

    public new abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.inventoryID>
    {
    }

    public new abstract class siteID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOCreate.SOFixedDemand.siteID>
    {
    }

    public new abstract class planDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.planDate>
    {
    }

    public new abstract class fixedSource : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.fixedSource>
    {
    }

    public new abstract class planID : BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    SOCreate.SOFixedDemand.planID>
    {
    }

    public new abstract class planType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.planType>
    {
    }

    public abstract class planDescr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.planDescr>
    {
    }

    public abstract class localizedPlanDescr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.localizedPlanDescr>
    {
    }

    public abstract class demandSiteID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.demandSiteID>
    {
    }

    public new abstract class sourceSiteID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.sourceSiteID>
    {
    }

    public new abstract class subItemID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.subItemID>
    {
    }

    public new abstract class locationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.locationID>
    {
    }

    public new abstract class lotSerialNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.lotSerialNbr>
    {
    }

    public new abstract class supplyPlanID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.supplyPlanID>
    {
    }

    public new abstract class planQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.planQty>
    {
    }

    public abstract class demandUOM : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.demandUOM>
    {
    }

    public abstract class unitMultDiv : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.unitMultDiv>
    {
    }

    public abstract class unitRate : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.unitRate>
    {
    }

    public abstract class orderQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.orderQty>
    {
    }

    public new abstract class refNoteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.refNoteID>
    {
    }

    public abstract class noteID : BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOCreate.SOFixedDemand.noteID>
    {
    }

    public new abstract class hold : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOCreate.SOFixedDemand.hold>
    {
    }

    public abstract class vendorID_Vendor_acctName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.vendorID_Vendor_acctName>
    {
    }

    public abstract class inventoryID_InventoryItem_descr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.inventoryID_InventoryItem_descr>
    {
    }

    public abstract class siteID_INSite_descr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.siteID_INSite_descr>
    {
    }

    public abstract class orderType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.orderType>
    {
    }

    public abstract class orderNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.orderNbr>
    {
    }

    public abstract class lineNbr : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOCreate.SOFixedDemand.lineNbr>
    {
    }

    public abstract class extWeight : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.extWeight>
    {
    }

    public abstract class extVolume : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.extVolume>
    {
    }

    public abstract class isSpecialOrder : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.isSpecialOrder>
    {
    }

    public abstract class lineCostCenterID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.lineCostCenterID>
    {
    }

    public abstract class curyUnitCost : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.curyUnitCost>
    {
    }

    /// <exclude />
    public abstract class unitCost : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      SOCreate.SOFixedDemand.unitCost>
    {
    }
  }
}
