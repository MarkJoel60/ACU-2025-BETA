// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_POReceiptEntry
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.BQLConstants;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;
using PX.Objects.PO;
using PX.Objects.PO.GraphExtensions.POReceiptEntryExt;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class SM_POReceiptEntry : 
  PXGraphExtension<PX.Objects.PO.GraphExtensions.POReceiptEntryExt.SO2POSync, UpdatePOOnRelease, POReceiptEntry.MultiCurrency, POReceiptEntry>
{
  public PXSelect<FSServiceOrder> serviceOrderView;
  public PXSelect<FSSODetSplit> soDetSplitView;
  public PXSelect<FSSODet> soDetView;
  public PXSelect<FSAppointment> appointmentView;
  public PXSelect<FSAppointmentDet> apptDetView;
  public PXSelect<FSApptLineSplit> apptSplitView;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXDBString(15, IsKey = true, IsUnicode = true)]
  [PXParent(typeof (Select<FSServiceOrder, Where<FSServiceOrder.srvOrdType, Equal<Current<FSSODetSplit.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Current<FSSODetSplit.refNbr>>>>>))]
  [PXDefault]
  protected virtual void FSSODetSplit_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBDate]
  [PXDefault]
  protected virtual void FSSODetSplit_OrderDate_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  protected virtual void FSSODetSplit_SiteID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  protected virtual void FSSODetSplit_LocationID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString]
  protected void _(PX.Data.Events.CacheAttached<FSSODetSplit.lotSerialNbr> e)
  {
  }

  [PXDBString]
  protected void _(
    PX.Data.Events.CacheAttached<FSAppointmentDet.lotSerialNbr> e)
  {
  }

  [PXDBString]
  protected void _(
    PX.Data.Events.CacheAttached<FSApptLineSplit.lotSerialNbr> e)
  {
  }

  [PXDBString(20, IsKey = true, IsUnicode = true, InputMask = "")]
  protected void _(PX.Data.Events.CacheAttached<FSApptLineSplit.apptNbr> e)
  {
  }

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  protected void _(
    PX.Data.Events.CacheAttached<FSApptLineSplit.origSrvOrdNbr> e)
  {
  }

  [PXDBInt]
  protected void _(
    PX.Data.Events.CacheAttached<FSApptLineSplit.origLineNbr> e)
  {
  }

  [PXDBDate]
  protected void _(PX.Data.Events.CacheAttached<FSApptLineSplit.apptDate> e)
  {
  }

  [PXOverride]
  public virtual void Copy(
    PX.Objects.PO.POReceiptLine aDest,
    PX.Objects.PO.POLine aSrc,
    Decimal aQtyAdj,
    Decimal aBaseQtyAdj,
    SM_POReceiptEntry.CopyOrig del)
  {
    if (del != null)
      del(aDest, aSrc, aQtyAdj, aBaseQtyAdj);
    INItemPlan inItemPlan = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.supplyPlanID, Equal<Required<INItemPlan.supplyPlanID>>, And<INItemPlan.planType, Equal<Required<INItemPlan.planType>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, new object[2]
    {
      (object) aSrc.PlanID,
      (object) "F6"
    }));
    if (inItemPlan == null)
      return;
    int? nullable1 = inItemPlan.LocationID;
    if (!nullable1.HasValue)
      return;
    nullable1 = aDest.SiteID;
    int? nullable2 = inItemPlan.SiteID;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
    {
      INItemSite inItemSite = PXResultset<INItemSite>.op_Implicit(PXSelectBase<INItemSite, PXViewOf<INItemSite>.BasedOn<SelectFromBase<INItemSite, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemSite.inventoryID, Equal<P.AsInt>>>>>.And<BqlOperand<INItemSite.siteID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, (object[]) null, new object[2]
      {
        (object) aDest.InventoryID,
        (object) aDest.SiteID
      }));
      PX.Objects.PO.POReceiptLine poReceiptLine = aDest;
      int? nullable3;
      if (inItemSite == null)
      {
        nullable2 = new int?();
        nullable3 = nullable2;
      }
      else
        nullable3 = inItemSite.DfltReceiptLocationID;
      poReceiptLine.LocationID = nullable3;
    }
    else
      aDest.LocationID = inItemPlan.LocationID;
  }

  [PXOverride]
  public void ReduceSOAllocationOnReleaseReturn(
    PXResult<PX.Objects.PO.POReceiptLine> row,
    POReceiptLineSplit posplit,
    PX.Objects.PO.POLine poline)
  {
    POReceiptLine2 poReceiptLine2 = ((PXResult) row).GetItem<POReceiptLine2>();
    PX.Objects.PO.POLine poline1 = GraphHelper.Caches<PX.Objects.PO.POLine>((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base).Locate(poline) ?? poline;
    if (!POOrderType.IsNormalType(poReceiptLine2?.POType) || !(poReceiptLine2.LineType == "GF") || poline1.Completed.GetValueOrDefault())
      return;
    IEnumerable<FSSODetSplit> fssoDetSplits = GraphHelper.RowCast<FSSODetSplit>((IEnumerable) PXSelectBase<FSSODetSplit, PXViewOf<FSSODetSplit>.BasedOn<SelectFromBase<FSSODetSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FSSODetFSSODetSplit>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSSODetSplit.srvOrdType, Equal<FSSODetFSSODetSplit.srvOrdType>>>>, And<BqlOperand<FSSODetSplit.refNbr, IBqlString>.IsEqual<FSSODetFSSODetSplit.refNbr>>>, And<BqlOperand<FSSODetSplit.lineNbr, IBqlInt>.IsEqual<FSSODetFSSODetSplit.lineNbr>>>>.And<BqlOperand<FSSODetSplit.parentSplitLineNbr, IBqlInt>.IsEqual<FSSODetFSSODetSplit.splitLineNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSSODetSplit.pOReceiptType, Equal<BqlField<PX.Objects.PO.POReceiptLine.receiptType, IBqlString>.FromCurrent>>>>, And<BqlOperand<FSSODetSplit.pOReceiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<FSSODetFSSODetSplit.pOType, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.pOType, IBqlString>.FromCurrent>>>, And<BqlOperand<FSSODetFSSODetSplit.pONbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.pONbr, IBqlString>.FromCurrent>>>, And<BqlOperand<FSSODetFSSODetSplit.pOLineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.pOLineNbr, IBqlInt>.FromCurrent>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current2<POReceiptLineSplit.lotSerialNbr>, IsNull>>>, Or<BqlOperand<Current2<POReceiptLineSplit.lotSerialNbr>, IBqlString>.IsEqual<EmptyString>>>, Or<BqlOperand<FSSODetSplit.lotSerialNbr, IBqlString>.IsNull>>, Or<BqlOperand<FSSODetSplit.lotSerialNbr, IBqlString>.IsEqual<EmptyString>>>>.Or<BqlOperand<FSSODetSplit.lotSerialNbr, IBqlString>.IsEqual<BqlField<POReceiptLineSplit.lotSerialNbr, IBqlString>.FromCurrent.NoDefault>>>>.Order<By<BqlField<FSSODetSplit.lotSerialNbr, IBqlString>.Desc>>>.ReadOnly.Config>.SelectMultiBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, new object[2]
    {
      (object) poReceiptLine2,
      (object) posplit
    }, Array.Empty<object>()));
    Decimal valueOrDefault = posplit.BaseQty.GetValueOrDefault();
    foreach (FSSODetSplit split in fssoDetSplits)
    {
      valueOrDefault -= this.UpdateFSAllocation(split, poline1, valueOrDefault);
      if (valueOrDefault <= 0M)
        break;
    }
  }

  [PXOverride]
  public void Process(
    (string Type, string Nbr) poReceiptKey,
    IEnumerable<PXResult<INItemPlan, INPlanType>> poDemands,
    Action<(string Type, string Nbr), IEnumerable<PXResult<INItemPlan, INPlanType>>> base_Process)
  {
    List<PXResult<INItemPlan, INPlanType>> pxResultList = this.ProcessPOReceipt((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, poDemands, poReceiptKey.Type, poReceiptKey.Nbr);
    base_Process(poReceiptKey, (IEnumerable<PXResult<INItemPlan, INPlanType>>) pxResultList);
  }

  protected virtual Decimal UpdateFSAllocation(
    FSSODetSplit split,
    PX.Objects.PO.POLine poline,
    Decimal returnQty)
  {
    Decimal num1 = 0M;
    FSSODetSplit activeSplit = this.FindActiveSplit(split);
    if ((activeSplit != null ? (!activeSplit.IsAllocated.GetValueOrDefault() ? 1 : 0) : 1) == 0)
    {
      Decimal? shippedQty = activeSplit.ShippedQty;
      Decimal num2 = 0M;
      if (shippedQty.GetValueOrDefault() == num2 & shippedQty.HasValue)
      {
        INItemPlan plan = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemPlan.planID, IBqlLong>.IsEqual<BqlField<FSSODetSplit.planID, IBqlLong>.FromCurrent>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, new object[1]
        {
          (object) activeSplit
        }, Array.Empty<object>()));
        if (plan?.PlanType != "F2")
          return num1;
        Decimal returnQty1 = this.SplitAllocation(activeSplit, plan, returnQty);
        this.UpdateParent(this.FindParent(split), poline, returnQty1);
        return returnQty1;
      }
    }
    return num1;
  }

  protected virtual FSSODetSplit FindActiveSplit(FSSODetSplit split)
  {
    FSSODetSplit activeSplit;
    POReceiptEntry poReceiptEntry;
    object[] objArray1;
    object[] objArray2;
    for (activeSplit = split; activeSplit != null && activeSplit.Completed.GetValueOrDefault(); activeSplit = PXResultset<FSSODetSplit>.op_Implicit(PXSelectBase<FSSODetSplit, PXViewOf<FSSODetSplit>.BasedOn<SelectFromBase<FSSODetSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSSODetSplit.srvOrdType, Equal<BqlField<FSSODetSplit.srvOrdType, IBqlString>.FromCurrent>>>>, And<BqlOperand<FSSODetSplit.refNbr, IBqlString>.IsEqual<BqlField<FSSODetSplit.refNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<FSSODetSplit.lineNbr, IBqlInt>.IsEqual<BqlField<FSSODetSplit.lineNbr, IBqlInt>.FromCurrent>>>, And<BqlOperand<FSSODetSplit.parentSplitLineNbr, IBqlInt>.IsEqual<BqlField<FSSODetSplit.splitLineNbr, IBqlInt>.FromCurrent>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSSODetSplit.lotSerialNbr, IsNull>>>>.Or<BqlOperand<FSSODetSplit.lotSerialNbr, IBqlString>.IsEqual<BqlField<FSSODetSplit.lotSerialNbr, IBqlString>.FromCurrent>>>>>.Config>.SelectSingleBound((PXGraph) poReceiptEntry, objArray1, objArray2)))
    {
      poReceiptEntry = ((PXGraphExtension<POReceiptEntry>) this).Base;
      objArray1 = new object[1]{ (object) activeSplit };
      objArray2 = Array.Empty<object>();
    }
    return activeSplit;
  }

  protected virtual Decimal SplitAllocation(FSSODetSplit split, INItemPlan plan, Decimal returnQty)
  {
    Decimal num1 = Math.Min(returnQty, split.BaseQty.GetValueOrDefault());
    FSSODetSplit fssoDetSplit1 = split;
    Decimal? nullable1 = fssoDetSplit1.BaseQty;
    Decimal num2 = num1;
    fssoDetSplit1.BaseQty = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num2) : new Decimal?();
    FSSODetSplit fssoDetSplit2 = split;
    PXCache cache = ((PXSelectBase) this.soDetSplitView).Cache;
    int? inventoryId = split.InventoryID;
    string uom = split.UOM;
    nullable1 = split.BaseQty;
    Decimal num3 = nullable1.Value;
    Decimal? nullable2 = new Decimal?(INUnitAttribute.ConvertFromBase(cache, inventoryId, uom, num3, INPrecision.QUANTITY));
    fssoDetSplit2.Qty = nullable2;
    nullable1 = split.BaseQty;
    Decimal num4 = 0M;
    if (nullable1.GetValueOrDefault() == num4 & nullable1.HasValue)
      split.Completed = new bool?(true);
    ((PXSelectBase<FSSODetSplit>) this.soDetSplitView).Update(split);
    nullable1 = plan.PlanQty;
    Decimal num5 = num1;
    if (!(nullable1.GetValueOrDefault() == num5 & nullable1.HasValue))
    {
      INItemPlan inItemPlan = plan;
      nullable1 = inItemPlan.PlanQty;
      Decimal num6 = num1;
      inItemPlan.PlanQty = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num6) : new Decimal?();
      GraphHelper.Caches<INItemPlan>((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base).Update(plan);
    }
    else
      GraphHelper.Caches<INItemPlan>((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base).Delete(plan);
    return num1;
  }

  protected virtual FSSODetSplit FindParent(FSSODetSplit split)
  {
    return PXResultset<FSSODetSplit>.op_Implicit(PXSelectBase<FSSODetSplit, PXViewOf<FSSODetSplit>.BasedOn<SelectFromBase<FSSODetSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSSODetSplit.srvOrdType, Equal<BqlField<FSSODetSplit.srvOrdType, IBqlString>.FromCurrent>>>>, And<BqlOperand<FSSODetSplit.refNbr, IBqlString>.IsEqual<BqlField<FSSODetSplit.refNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<FSSODetSplit.lineNbr, IBqlInt>.IsEqual<BqlField<FSSODetSplit.lineNbr, IBqlInt>.FromCurrent>>>>.And<BqlOperand<FSSODetSplit.splitLineNbr, IBqlInt>.IsEqual<BqlField<FSSODetSplit.parentSplitLineNbr, IBqlInt>.FromCurrent>>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, new object[1]
    {
      (object) split
    }, Array.Empty<object>()));
  }

  protected virtual void UpdateParent(FSSODetSplit parent, PX.Objects.PO.POLine poline, Decimal returnQty)
  {
    FSSODetSplit fssoDetSplit1 = parent;
    Decimal? nullable1 = fssoDetSplit1.BaseReceivedQty;
    Decimal num1 = returnQty;
    fssoDetSplit1.BaseReceivedQty = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num1) : new Decimal?();
    FSSODetSplit fssoDetSplit2 = parent;
    PXCache cache = ((PXSelectBase) this.soDetSplitView).Cache;
    int? inventoryId = parent.InventoryID;
    string uom = parent.UOM;
    nullable1 = parent.BaseReceivedQty;
    Decimal num2 = nullable1.Value;
    Decimal? nullable2 = new Decimal?(INUnitAttribute.ConvertFromBase(cache, inventoryId, uom, num2, INPrecision.QUANTITY));
    fssoDetSplit2.ReceivedQty = nullable2;
    if (!parent.Completed.GetValueOrDefault())
    {
      ((PXSelectBase<FSSODetSplit>) this.soDetSplitView).Update(parent);
      INItemPlan inItemPlan1 = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemPlan.planID, IBqlLong>.IsEqual<BqlField<FSSODetSplit.planID, IBqlLong>.FromCurrent>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, new object[1]
      {
        (object) parent
      }, Array.Empty<object>()));
      INItemPlan inItemPlan2 = inItemPlan1 != null ? inItemPlan1 : throw new RowNotFoundException((PXCache) GraphHelper.Caches<INItemPlan>((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base), new object[1]
      {
        (object) parent.PlanID
      });
      nullable1 = inItemPlan2.PlanQty;
      Decimal num3 = returnQty;
      inItemPlan2.PlanQty = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num3) : new Decimal?();
      GraphHelper.Caches<INItemPlan>((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base).Update(inItemPlan1);
    }
    else
    {
      parent.Completed = new bool?(false);
      parent.PlanID = this.InsertParentPlan(parent, poline, returnQty);
      ((PXSelectBase<FSSODetSplit>) this.soDetSplitView).Update(parent);
    }
  }

  protected virtual long? InsertParentPlan(FSSODetSplit parent, PX.Objects.PO.POLine poline, Decimal returnQty)
  {
    INItemPlan inItemPlan = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemPlan.planID, IBqlLong>.IsEqual<BqlField<PX.Objects.PO.POLine.planID, IBqlLong>.FromCurrent>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, new object[1]
    {
      (object) poline
    }, Array.Empty<object>()));
    if (inItemPlan == null)
      throw new RowNotFoundException((PXCache) GraphHelper.Caches<INItemPlan>((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base), new object[1]
      {
        (object) poline.PlanID
      });
    PX.Objects.PO.POOrder poOrder = PXParentAttribute.SelectParent<PX.Objects.PO.POOrder>(((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.poline).Cache, (object) poline);
    if (poOrder == null)
      throw new RowNotFoundException(((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.poOrderUPD).Cache, new object[2]
      {
        (object) poline.OrderType,
        (object) poline.OrderNbr
      });
    FSServiceOrder fsServiceOrder = PXParentAttribute.SelectParent<FSServiceOrder>(((PXSelectBase) this.soDetSplitView).Cache, (object) parent);
    if (fsServiceOrder == null)
      throw new RowNotFoundException(((PXSelectBase) this.serviceOrderView).Cache, new object[2]
      {
        (object) parent.SrvOrdType,
        (object) parent.RefNbr
      });
    FSSODet fssoDet = PXParentAttribute.SelectParent<FSSODet>(((PXSelectBase) this.soDetSplitView).Cache, (object) parent);
    if (fssoDet == null)
      throw new RowNotFoundException(((PXSelectBase) this.soDetView).Cache, new object[3]
      {
        (object) parent.SrvOrdType,
        (object) parent.RefNbr,
        (object) parent.LineNbr
      });
    INItemPlan copy = PXCache<INItemPlan>.CreateCopy(inItemPlan);
    copy.PlanID = new long?();
    copy.SupplyPlanID = inItemPlan.PlanID;
    copy.SiteID = fssoDet.SiteID;
    copy.SourceSiteID = fssoDet.SiteID;
    copy.CostCenterID = new int?(0);
    copy.PlanType = "F6";
    copy.VendorID = poOrder.VendorID;
    copy.VendorLocationID = poOrder.VendorLocationID;
    copy.UOM = parent.UOM;
    copy.BAccountID = fsServiceOrder.CustomerID;
    copy.Hold = fsServiceOrder.Hold;
    copy.RefNoteID = fsServiceOrder.NoteID;
    copy.RefEntityType = typeof (FSServiceOrder).FullName;
    copy.PlanQty = new Decimal?(returnQty);
    copy.PlanDate = fssoDet.ShipDate;
    return GraphHelper.Caches<INItemPlan>((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base).Insert(copy).PlanID;
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.PO.POOrder> e)
  {
    if (e.TranStatus != null || e.Operation != 1)
      return;
    PX.Objects.PO.POOrder row = e.Row;
    if (!((string) ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.PO.POOrder>>) e).Cache.GetValueOriginal<PX.Objects.PO.POOrder.status>((object) row) != row.Status))
      return;
    FSPOReceiptProcess.UpdateSrvOrdLinePOStatus(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.PO.POOrder>>) e).Cache.Graph, row);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.PO.POLine> e)
  {
    if (e.TranStatus != null || e.Operation != 1 || !(((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current?.ReceiptType == "RT"))
      return;
    PX.Objects.PO.POLine row = e.Row;
    FSSODet fsSODet = PXResultset<FSSODet>.op_Implicit(PXSelectBase<FSSODet, PXSelect<FSSODet, Where<FSSODet.poType, Equal<Required<FSSODet.poType>>, And<FSSODet.poNbr, Equal<Required<FSSODet.poNbr>>, And<FSSODet.poLineNbr, Equal<Required<FSSODet.poLineNbr>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, new object[3]
    {
      (object) row.OrderType,
      (object) row.OrderNbr,
      (object) row.LineNbr
    }));
    INItemPlan inItemPlan = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.supplyPlanID, Equal<Required<INItemPlan.supplyPlanID>>>>.Config>.Select((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, new object[1]
    {
      (object) row.PlanID
    }));
    ((PXSelectBase<PX.Objects.PO.POOrder>) ((PXGraphExtension<POReceiptEntry>) this).Base.poOrderUPD).Current = (PX.Objects.PO.POOrder) PXParentAttribute.SelectParent((PXCache) GraphHelper.Caches<PX.Objects.PO.POLine>((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base), (object) row, typeof (PX.Objects.PO.POOrder));
    FSPOReceiptProcess.UpdatePOReferenceInSrvOrdLine((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, (PXSelectBase<FSServiceOrder>) this.serviceOrderView, (PXSelectBase<FSSODet>) this.soDetView, (PXSelectBase<FSSODetSplit>) this.soDetSplitView, (PXSelectBase<FSAppointment>) this.appointmentView, (PXSelectBase<FSAppointmentDet>) this.apptDetView, fsSODet, ((PXSelectBase<PX.Objects.PO.POOrder>) ((PXGraphExtension<POReceiptEntry>) this).Base.poOrderUPD).Current, row.LineNbr, row.Completed, (PXCache) null, inItemPlan, false, true);
  }

  public virtual List<PXResult<INItemPlan, INPlanType>> ProcessPOReceipt(
    PXGraph graph,
    IEnumerable<PXResult<INItemPlan, INPlanType>> list,
    string POReceiptType,
    string POReceiptNbr)
  {
    return FSPOReceiptProcess.ProcessPOReceipt(graph, list, POReceiptType, POReceiptNbr, false);
  }

  public class FSSODetSplitPlanSyncOnly : ItemPlanSyncOnly<POReceiptEntry, FSSODetSplit>
  {
    public static bool IsActive()
    {
      return PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();
    }
  }

  public delegate void CopyOrig(
    PX.Objects.PO.POReceiptLine aDest,
    PX.Objects.PO.POLine aSrc,
    Decimal aQtyAdj,
    Decimal aBaseQtyAdj);
}
