// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POReceiptEntryExt.SO2POSync
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.BQLConstants;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.IN;
using PX.Objects.SO;
using PX.Objects.SO.GraphExtensions.SO2PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POReceiptEntryExt;

public class SO2POSync : SO2POSyncFromPOReceiptExtension<POReceiptEntry>, IDisposable
{
  private PXCache<SOAddress> _soAddressCache;
  private PXCache<PX.Objects.SO.SOOrderShipment> _soOrderShipmentCache;

  public PXCache<SOAddress> SOAddressCache
  {
    get
    {
      return this._soAddressCache ?? (this._soAddressCache = GraphHelper.Caches<SOAddress>((PXGraph) this.Base));
    }
  }

  public PXCache<PX.Objects.SO.SOOrderShipment> SOOrderShipmentCache
  {
    get
    {
      return this._soOrderShipmentCache ?? (this._soOrderShipmentCache = GraphHelper.Caches<PX.Objects.SO.SOOrderShipment>((PXGraph) this.Base));
    }
  }

  public override void Initialize()
  {
    base.Initialize();
    GraphHelper.EnsureCachePersistence<SOAddress>((PXGraph) this.Base);
    GraphHelper.EnsureCachePersistence<PX.Objects.SO.SOOrderShipment>((PXGraph) this.Base, true);
    GraphHelper.EnsureCachePersistence<SOOrderSite>((PXGraph) this.Base);
  }

  [PXRemoveBaseAttribute(typeof (PXDBDefaultAttribute))]
  [PXMergeAttributes]
  [PXDefault]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOLineSplit.orderNbr> e)
  {
  }

  [PXRemoveBaseAttribute(typeof (PXDBDefaultAttribute))]
  [PXMergeAttributes]
  [PXDefault]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOLineSplit.orderDate> e)
  {
  }

  [PXMergeAttributes]
  [PXDBInt]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOLineSplit.siteID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBInt]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOLineSplit.locationID> e)
  {
  }

  [PXRemoveBaseAttribute(typeof (PXDBDefaultAttribute))]
  [PXMergeAttributes]
  [PXDBDefault(typeof (SOAddress.addressID))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.SO.SOOrderShipment.shipAddressID> e)
  {
  }

  protected List<INItemPlan> POSupply { get; private set; }

  protected HashSet<PXResult<INItemPlan, INPlanType>> PODemand { get; private set; }

  public virtual SO2POSync InitReleasingContext()
  {
    this.POSupply = new List<INItemPlan>();
    this.PODemand = new HashSet<PXResult<INItemPlan, INPlanType>>();
    return this;
  }

  protected virtual void DisposeReleasingContext()
  {
    this.POSupply = (List<INItemPlan>) null;
    this.PODemand = (HashSet<PXResult<INItemPlan, INPlanType>>) null;
  }

  void IDisposable.Dispose() => this.DisposeReleasingContext();

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.siteID> e)
  {
    if (!(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.siteID>, PX.Objects.PO.POReceiptLine, object>) e).NewValue is int newValue) || e.Row.LineType == null)
      return;
    if (POLineType.IsDropShip(e.Row.LineType))
    {
      PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) this.Base, new int?(newValue));
      if (inSite != null && !inSite.DropShipLocationID.HasValue)
        throw new PXSetPropertyException((IBqlTable) e.Row, "The selected warehouse has no drop-ship location. Please select another warehouse or define the drop-ship location for the currently selected warehouse.")
        {
          ErrorValue = (object) inSite.SiteCD
        };
    }
    else
    {
      if (!EnumerableExtensions.IsIn<string>(e.Row.LineType, "GS", "NO") || string.IsNullOrEmpty(e.Row.PONbr))
        return;
      PX.Objects.SO.SOLineSplit soLineSplit = PXResultset<PX.Objects.SO.SOLineSplit>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLineSplit, PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.pOType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.pONbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.pOLineNbr, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.behavior, IBqlString>.IsEqual<SOBehavior.bL>>>>.And<BqlOperand<PX.Objects.SO.SOLineSplit.siteID, IBqlInt>.IsNotEqual<P.AsInt>>>>.ReadOnly.Config>.Select((PXGraph) this.Base, new object[4]
      {
        (object) e.Row.POType,
        (object) e.Row.PONbr,
        (object) e.Row.POLineNbr,
        (object) newValue
      }));
      if (soLineSplit != null)
      {
        PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) this.Base, new int?(newValue));
        throw new PXSetPropertyException((IBqlTable) e.Row, "Cannot change the warehouse because the line has a link to a line of the {0} blanket sales order.", new object[1]
        {
          (object) soLineSplit.OrderNbr
        })
        {
          ErrorValue = (inSite != null ? (object) inSite.SiteCD : (object) null) ?? ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.siteID>, PX.Objects.PO.POReceiptLine, object>) e).NewValue
        };
      }
    }
  }

  public virtual void ProcessDemandsOnRelease(
    PX.Objects.PO.POReceiptLine line,
    PX.Objects.PO.POLine poLine,
    POAddress poAddress)
  {
    this.CollectDemandOrdersMarkedForPO(line, poLine);
    this.UpdateDemandOrdersMarkedForDropship(line, poLine, poAddress);
    this.CollectDemandForTransferOrders(line);
  }

  protected virtual void CollectDemandOrdersMarkedForPO(PX.Objects.PO.POReceiptLine line, PX.Objects.PO.POLine poLine)
  {
    if (poLine == null)
      return;
    if (EnumerableExtensions.IsNotIn<string>(poLine.LineType, "GS", "NO", "GM", "NM", "GF", new string[1]
    {
      "NF"
    }))
      return;
    Decimal? nullable1 = line.BaseReceiptQty;
    foreach (PXResult<INItemPlan, INPlanType> pxResult in (IEnumerable<PXResult<INItemPlan>>) ((IEnumerable<PXResult<INItemPlan>>) PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INPlanType>.On<INItemPlan.FK.PlanType>>, FbqlJoins.Left<PX.Objects.SO.SOLineSplit>.On<BqlOperand<PX.Objects.SO.SOLineSplit.planID, IBqlLong>.IsEqual<INItemPlan.planID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemPlan.planQty, Greater<decimal0>>>>, And<BqlOperand<INPlanType.isDemand, IBqlBool>.IsEqual<True>>>, And<BqlOperand<INPlanType.isFixed, IBqlBool>.IsEqual<True>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemPlan.supplyPlanID, Equal<BqlField<PX.Objects.PO.POLine.planID, IBqlLong>.FromCurrent>>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.pONbr, IBqlString>.IsNull>>>.Or<KeysRelation<CompositeKey<Field<PX.Objects.SO.SOLineSplit.pOType>.IsRelatedTo<PX.Objects.PO.POLine.orderType>, Field<PX.Objects.SO.SOLineSplit.pONbr>.IsRelatedTo<PX.Objects.PO.POLine.orderNbr>, Field<PX.Objects.SO.SOLineSplit.pOLineNbr>.IsRelatedTo<PX.Objects.PO.POLine.lineNbr>>.WithTablesOf<PX.Objects.PO.POLine, PX.Objects.SO.SOLineSplit>, PX.Objects.PO.POLine, PX.Objects.SO.SOLineSplit>.SameAsCurrent.And<BqlOperand<PX.Objects.SO.SOLineSplit.pOCreate, IBqlBool>.IsEqual<True>>>>>>.Config>.SelectMultiBound((PXGraph) this.Base, (object[]) new PX.Objects.PO.POLine[1]
    {
      poLine
    }, Array.Empty<object>())).AsEnumerable<PXResult<INItemPlan>>().OrderBy<PXResult<INItemPlan>, bool>((Func<PXResult<INItemPlan>, bool>) (res => PXResult.Unwrap<PX.Objects.SO.SOLineSplit>((object) res).Behavior == "BL")))
    {
      PX.Objects.SO.SOLineSplit soLineSplit1 = ((PXResult) pxResult).GetItem<PX.Objects.SO.SOLineSplit>();
      PX.Objects.SO.SOLineSplit soLineSplit2 = this.SOSplitCache.Locate(soLineSplit1) ?? soLineSplit1;
      INItemPlan inItemPlan1 = PXResult<INItemPlan, INPlanType>.op_Implicit(pxResult);
      long? supplyPlanId = inItemPlan1.SupplyPlanID;
      long? planId = poLine.PlanID;
      bool? nullable2;
      if (!(supplyPlanId.GetValueOrDefault() == planId.GetValueOrDefault() & supplyPlanId.HasValue == planId.HasValue) || !string.IsNullOrEmpty(soLineSplit2.PONbr))
      {
        nullable2 = soLineSplit2.Completed;
        if (nullable2.GetValueOrDefault())
          continue;
      }
      bool flag = EnumerableExtensions.IsIn<string>(poLine.LineType, "GF", "NF");
      INItemPlan inItemPlan2 = new INItemPlan();
      inItemPlan2.InventoryID = line.InventoryID;
      inItemPlan2.SiteID = line.SiteID;
      inItemPlan2.SubItemID = line.SubItemID;
      inItemPlan2.DemandPlanID = inItemPlan1.PlanID;
      inItemPlan2.PlanDate = inItemPlan1.PlanDate;
      inItemPlan2.PlanType = flag ? "F5" : "64";
      inItemPlan2.RefNoteID = inItemPlan1.RefNoteID;
      inItemPlan2.RefEntityType = inItemPlan1.RefEntityType;
      inItemPlan2.Hold = new bool?(false);
      inItemPlan2.CostCenterID = line.CostCenterID;
      inItemPlan2.BAccountID = !flag ? inItemPlan1.BAccountID : new int?();
      inItemPlan2.LocationID = flag ? line.LocationID : new int?();
      int? siteId1 = line.SiteID;
      int? siteId2 = inItemPlan1.SiteID;
      inItemPlan2.FixedSource = !(siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue) ? "T" : "N";
      inItemPlan2.SourceSiteID = line.SiteID;
      inItemPlan2.ProjectID = line.ProjectID;
      inItemPlan2.TaskID = line.TaskID;
      INItemPlan inItemPlan3 = inItemPlan2;
      Decimal? nullable3 = nullable1;
      Decimal? nullable4 = inItemPlan1.PlanQty;
      if (nullable3.GetValueOrDefault() >= nullable4.GetValueOrDefault() & nullable3.HasValue & nullable4.HasValue)
      {
        inItemPlan3.PlanQty = inItemPlan1.PlanQty;
        this.POSupply.Add(inItemPlan3);
        nullable4 = nullable1;
        nullable3 = inItemPlan1.PlanQty;
        nullable1 = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
        this.PODemand.Add(pxResult);
      }
      else
      {
        nullable3 = nullable1;
        Decimal num = 0M;
        if (nullable3.GetValueOrDefault() > num & nullable3.HasValue)
        {
          inItemPlan3.PlanQty = nullable1;
          this.POSupply.Add(inItemPlan3);
          nullable1 = new Decimal?(0M);
        }
      }
      nullable2 = line.IsCorrection;
      if (!nullable2.GetValueOrDefault() || inItemPlan1.PlanType != "66")
      {
        nullable2 = poLine.Completed;
        if (nullable2.GetValueOrDefault())
          this.PODemand.Add(pxResult);
      }
    }
  }

  protected virtual void UpdateDemandOrdersMarkedForDropship(
    PX.Objects.PO.POReceiptLine line,
    PX.Objects.PO.POLine poLine,
    POAddress poAddress)
  {
    if (poLine == null || EnumerableExtensions.IsNotIn<string>(poLine.LineType, "GP", "NP"))
      return;
    PXResult<PX.Objects.SO.SOLineSplit, SOLine4>[] relatedDemand = this.GetRelatedDemand(line);
    if (!((IEnumerable<PXResult<PX.Objects.SO.SOLineSplit, SOLine4>>) relatedDemand).Any<PXResult<PX.Objects.SO.SOLineSplit, SOLine4>>())
      return;
    bool? nullable1 = line.IsCorrection;
    if (nullable1.GetValueOrDefault())
      this.RevertDemandOrdersOrigReceipt(line, poLine, relatedDemand);
    PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = this.Base.MultiCurrencyExt.GetDefaultCurrencyInfo();
    Decimal? nullable2 = new Decimal?();
    Decimal? nullable3 = new Decimal?();
    for (int index = 0; index < relatedDemand.Length; ++index)
    {
      SOLine4 soLine4_1 = PXResult<PX.Objects.SO.SOLineSplit, SOLine4>.op_Implicit(relatedDemand[index]);
      PX.Objects.SO.SOOrder order = PXParentAttribute.SelectParent<PX.Objects.SO.SOOrder>((PXCache) this.SOLineCache, (object) soLine4_1);
      if (order == null)
        throw new RowNotFoundException((PXCache) this.SOOrderCache, new object[2]
        {
          (object) soLine4_1.OrderType,
          (object) soLine4_1.OrderNbr
        });
      nullable1 = soLine4_1.Completed;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = line.IsCorrection;
        if (!nullable1.GetValueOrDefault())
        {
          PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, soLine4_1.InventoryID);
          EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelectReadonly<EPEmployee, Where<EPEmployee.defContactID, Equal<Required<PX.Objects.SO.SOOrder.ownerID>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
          {
            (object) order.OwnerID
          }));
          if (epEmployee == null)
            throw new PXException("In the {0} {1} sales order related to this purchase receipt, the line with the {2} item has already been completed.", new object[3]
            {
              (object) soLine4_1.OrderType,
              (object) soLine4_1.OrderNbr,
              (object) inventoryItem.InventoryCD
            });
          throw new PXException("In the {0} {1} sales order related to this purchase receipt, the line with the {2} item has already been completed. Contact {3} for details.", new object[4]
          {
            (object) soLine4_1.OrderType,
            (object) soLine4_1.OrderNbr,
            (object) inventoryItem.InventoryCD,
            (object) epEmployee.AcctName
          });
        }
      }
      this.PopulateDropshipFields(order);
      PX.Objects.SO.SOLineSplit sosplit = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(PXResult<PX.Objects.SO.SOLineSplit, SOLine4>.op_Implicit(relatedDemand[index]));
      SOLine4 copy = PXCache<SOLine4>.CreateCopy(soLine4_1);
      bool flag1 = line.UOM == copy.UOM;
      if (!nullable2.HasValue)
      {
        nullable2 = line.BaseReceiptQty;
        nullable3 = flag1 ? line.ReceiptQty : new Decimal?(INUnitAttribute.ConvertFromBase<SOLine4.inventoryID, SOLine4.uOM>((PXCache) this.SOLineCache, (object) copy, nullable2.GetValueOrDefault(), INPrecision.QUANTITY));
      }
      Decimal? baseSplitReceivedQty = new Decimal?();
      Decimal? splitReceivedQty = new Decimal?();
      Decimal? nullable4;
      Decimal? nullable5;
      Decimal? nullable6;
      if (index != relatedDemand.Length - 1)
      {
        nullable4 = nullable2;
        nullable5 = sosplit.BaseOpenQty;
        if (!(nullable4.GetValueOrDefault() < nullable5.GetValueOrDefault() & nullable4.HasValue & nullable5.HasValue))
        {
          nullable5 = nullable2;
          nullable4 = sosplit.BaseOpenQty;
          if (nullable5.GetValueOrDefault() >= nullable4.GetValueOrDefault() & nullable5.HasValue & nullable4.HasValue)
          {
            baseSplitReceivedQty = sosplit.BaseOpenQty;
            splitReceivedQty = sosplit.OpenQty;
            nullable4 = nullable2;
            nullable5 = baseSplitReceivedQty;
            Decimal? nullable7;
            if (!(nullable4.HasValue & nullable5.HasValue))
            {
              nullable6 = new Decimal?();
              nullable7 = nullable6;
            }
            else
              nullable7 = new Decimal?(nullable4.GetValueOrDefault() - nullable5.GetValueOrDefault());
            nullable2 = nullable7;
            nullable5 = nullable3;
            Decimal num = INUnitAttribute.ConvertFromBase<SOLine4.inventoryID, SOLine4.uOM>((PXCache) this.SOLineCache, (object) copy, baseSplitReceivedQty.GetValueOrDefault(), INPrecision.QUANTITY);
            Decimal? nullable8;
            if (!nullable5.HasValue)
            {
              nullable4 = new Decimal?();
              nullable8 = nullable4;
            }
            else
              nullable8 = new Decimal?(nullable5.GetValueOrDefault() - num);
            nullable3 = nullable8;
            goto label_27;
          }
          goto label_27;
        }
      }
      baseSplitReceivedQty = nullable2;
      splitReceivedQty = nullable3;
      nullable2 = new Decimal?(0M);
      nullable3 = new Decimal?(0M);
label_27:
      SOLine4 soLine4_2 = copy;
      nullable5 = soLine4_2.BaseShippedQty;
      short? lineSign = copy.LineSign;
      nullable6 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable9 = baseSplitReceivedQty;
      nullable4 = nullable6.HasValue & nullable9.HasValue ? new Decimal?(nullable6.GetValueOrDefault() * nullable9.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable10;
      if (!(nullable5.HasValue & nullable4.HasValue))
      {
        nullable9 = new Decimal?();
        nullable10 = nullable9;
      }
      else
        nullable10 = new Decimal?(nullable5.GetValueOrDefault() + nullable4.GetValueOrDefault());
      soLine4_2.BaseShippedQty = nullable10;
      if (flag1)
      {
        SOLine4 soLine4_3 = copy;
        nullable4 = soLine4_3.ShippedQty;
        lineSign = copy.LineSign;
        nullable9 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
        nullable6 = splitReceivedQty;
        nullable5 = nullable9.HasValue & nullable6.HasValue ? new Decimal?(nullable9.GetValueOrDefault() * nullable6.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable11;
        if (!(nullable4.HasValue & nullable5.HasValue))
        {
          nullable6 = new Decimal?();
          nullable11 = nullable6;
        }
        else
          nullable11 = new Decimal?(nullable4.GetValueOrDefault() + nullable5.GetValueOrDefault());
        soLine4_3.ShippedQty = nullable11;
      }
      else
        PXDBQuantityAttribute.CalcBaseQty<SOLine4.baseShippedQty>((PXCache) this.SOLineCache, (object) copy);
      nullable1 = poLine.Completed;
      if (nullable1.GetValueOrDefault())
      {
        int num1 = copy.UOM == poLine.UOM ? 1 : 0;
        Decimal? nullable12;
        ref Decimal? local1 = ref nullable12;
        nullable5 = sosplit.BaseQty;
        Decimal valueOrDefault1 = nullable5.GetValueOrDefault();
        nullable5 = sosplit.BaseShippedQty;
        Decimal valueOrDefault2 = nullable5.GetValueOrDefault();
        Decimal num2 = valueOrDefault1 - valueOrDefault2 - baseSplitReceivedQty.GetValueOrDefault();
        local1 = new Decimal?(num2);
        Decimal? nullable13;
        ref Decimal? local2 = ref nullable13;
        nullable5 = sosplit.Qty;
        Decimal valueOrDefault3 = nullable5.GetValueOrDefault();
        nullable5 = sosplit.ShippedQty;
        Decimal valueOrDefault4 = nullable5.GetValueOrDefault();
        Decimal num3 = valueOrDefault3 - valueOrDefault4 - splitReceivedQty.GetValueOrDefault();
        local2 = new Decimal?(num3);
        SOLine4 soLine4_4 = copy;
        nullable5 = soLine4_4.BaseUnbilledQty;
        lineSign = copy.LineSign;
        nullable6 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
        nullable9 = nullable12;
        nullable4 = nullable6.HasValue & nullable9.HasValue ? new Decimal?(nullable6.GetValueOrDefault() * nullable9.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable14;
        if (!(nullable5.HasValue & nullable4.HasValue))
        {
          nullable9 = new Decimal?();
          nullable14 = nullable9;
        }
        else
          nullable14 = new Decimal?(nullable5.GetValueOrDefault() - nullable4.GetValueOrDefault());
        soLine4_4.BaseUnbilledQty = nullable14;
        if (num1 != 0)
        {
          SOLine4 soLine4_5 = copy;
          nullable4 = soLine4_5.UnbilledQty;
          lineSign = copy.LineSign;
          nullable9 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
          nullable6 = nullable13;
          nullable5 = nullable9.HasValue & nullable6.HasValue ? new Decimal?(nullable9.GetValueOrDefault() * nullable6.GetValueOrDefault()) : new Decimal?();
          Decimal? nullable15;
          if (!(nullable4.HasValue & nullable5.HasValue))
          {
            nullable6 = new Decimal?();
            nullable15 = nullable6;
          }
          else
            nullable15 = new Decimal?(nullable4.GetValueOrDefault() - nullable5.GetValueOrDefault());
          soLine4_5.UnbilledQty = nullable15;
        }
        else
          PXDBQuantityAttribute.CalcTranQty<SOLine4.unbilledQty>((PXCache) this.SOLineCache, (object) copy);
        nullable1 = copy.OpenLine;
        bool flag2 = nullable1.GetValueOrDefault() && PXParentAttribute.SelectSiblings((PXCache) this.SOSplitCache, (object) sosplit, typeof (PX.Objects.SO.SOLine)).Cast<PX.Objects.SO.SOLineSplit>().Where<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (s =>
        {
          int? splitLineNbr1 = s.SplitLineNbr;
          int? splitLineNbr2 = sosplit.SplitLineNbr;
          return !(splitLineNbr1.GetValueOrDefault() == splitLineNbr2.GetValueOrDefault() & splitLineNbr1.HasValue == splitLineNbr2.HasValue);
        })).All<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (s => s.Completed.GetValueOrDefault()));
        if (flag2)
        {
          copy.OpenQty = new Decimal?(0M);
          copy.CuryOpenAmt = new Decimal?(0M);
          copy.Completed = new bool?(true);
          copy.OpenLine = new bool?(false);
        }
        this.SOLineCache.Update(copy);
        PX.Objects.SO.SOLineSplit soLineSplit1 = sosplit;
        nullable5 = soLineSplit1.BaseShippedQty;
        nullable4 = baseSplitReceivedQty;
        Decimal? nullable16;
        if (!(nullable5.HasValue & nullable4.HasValue))
        {
          nullable6 = new Decimal?();
          nullable16 = nullable6;
        }
        else
          nullable16 = new Decimal?(nullable5.GetValueOrDefault() + nullable4.GetValueOrDefault());
        soLineSplit1.BaseShippedQty = nullable16;
        if (flag1)
        {
          PX.Objects.SO.SOLineSplit soLineSplit2 = sosplit;
          nullable4 = soLineSplit2.ShippedQty;
          nullable5 = splitReceivedQty;
          Decimal? nullable17;
          if (!(nullable4.HasValue & nullable5.HasValue))
          {
            nullable6 = new Decimal?();
            nullable17 = nullable6;
          }
          else
            nullable17 = new Decimal?(nullable4.GetValueOrDefault() + nullable5.GetValueOrDefault());
          soLineSplit2.ShippedQty = nullable17;
        }
        else
          PXDBQuantityAttribute.CalcTranQty<PX.Objects.SO.SOLineSplit.shippedQty>((PXCache) this.SOSplitCache, (object) sosplit);
        long? planId = sosplit.PlanID;
        sosplit.Completed = new bool?(true);
        sosplit.POReceiptType = line.ReceiptType;
        sosplit.POReceiptNbr = line.ReceiptNbr;
        sosplit.POCompleted = new bool?(true);
        sosplit.PlanID = new long?();
        this.SOSplitCache.Update(sosplit);
        if (order != null)
        {
          if (flag2)
          {
            PX.Objects.SO.SOOrder soOrder = order;
            int? openLineCntr = soOrder.OpenLineCntr;
            soOrder.OpenLineCntr = openLineCntr.HasValue ? new int?(openLineCntr.GetValueOrDefault() - 1) : new int?();
          }
          nullable1 = order.Approved;
          if (!nullable1.GetValueOrDefault())
          {
            object valueExt = ((PXCache) this.SOOrderCache).GetValueExt<PX.Objects.SO.SOOrder.ownerID>((object) order);
            throw new PXException("The {0} {1} sales order related to this drop-ship receipt is not approved. Contact {2} for details.", new object[3]
            {
              (object) order.OrderType,
              (object) order.OrderNbr,
              valueExt
            });
          }
          this.CreateUpdateOrderShipment(order, copy, line, poAddress, defaultCurrencyInfo, baseSplitReceivedQty, splitReceivedQty);
          int? nullable18 = order.OpenShipmentCntr;
          int num4 = 0;
          if (nullable18.GetValueOrDefault() == num4 & nullable18.HasValue)
          {
            nullable18 = order.OpenLineCntr;
            int num5 = 0;
            if (nullable18.GetValueOrDefault() == num5 & nullable18.HasValue)
              order.MarkCompleted();
          }
        }
        INItemPlan inItemPlan = INItemPlan.PK.Find((PXGraph) this.Base, planId, (PKFindOptions) 1);
        if (inItemPlan != null)
          this.PlanCache.Delete(inItemPlan);
        foreach (PXResult<INItemPlan> pxResult in PXSelectBase<INItemPlan, PXSelectJoin<INItemPlan, InnerJoin<INPlanType, On<INItemPlan.FK.PlanType>>, Where<INItemPlan.supplyPlanID, Equal<Required<INItemPlan.supplyPlanID>>, And<INPlanType.isDemand, Equal<boolTrue>, And<INPlanType.isFixed, Equal<boolTrue>>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) poLine.PlanID
        }))
          this.PlanCache.Delete(PXResult<INItemPlan>.op_Implicit(pxResult));
      }
      else
      {
        this.SOLineCache.Update(copy);
        PX.Objects.SO.SOLineSplit soLineSplit3 = sosplit;
        nullable5 = soLineSplit3.BaseShippedQty;
        nullable4 = baseSplitReceivedQty;
        Decimal? nullable19;
        if (!(nullable5.HasValue & nullable4.HasValue))
        {
          nullable6 = new Decimal?();
          nullable19 = nullable6;
        }
        else
          nullable19 = new Decimal?(nullable5.GetValueOrDefault() + nullable4.GetValueOrDefault());
        soLineSplit3.BaseShippedQty = nullable19;
        if (flag1)
        {
          PX.Objects.SO.SOLineSplit soLineSplit4 = sosplit;
          nullable4 = soLineSplit4.ShippedQty;
          nullable5 = splitReceivedQty;
          Decimal? nullable20;
          if (!(nullable4.HasValue & nullable5.HasValue))
          {
            nullable6 = new Decimal?();
            nullable20 = nullable6;
          }
          else
            nullable20 = new Decimal?(nullable4.GetValueOrDefault() + nullable5.GetValueOrDefault());
          soLineSplit4.ShippedQty = nullable20;
        }
        else
          PXDBQuantityAttribute.CalcTranQty<PX.Objects.SO.SOLineSplit.shippedQty>((PXCache) this.SOSplitCache, (object) sosplit);
        sosplit.POReceiptType = line.ReceiptType;
        sosplit.POReceiptNbr = line.ReceiptNbr;
        this.SOSplitCache.Update(sosplit);
        if (order != null)
          this.CreateUpdateOrderShipment(order, copy, line, poAddress, defaultCurrencyInfo, baseSplitReceivedQty, splitReceivedQty);
      }
    }
  }

  protected virtual void RevertDemandOrdersOrigReceipt(
    PX.Objects.PO.POReceiptLine line,
    PX.Objects.PO.POLine poLine,
    PXResult<PX.Objects.SO.SOLineSplit, SOLine4>[] demand)
  {
  }

  protected virtual PX.Objects.SO.SOOrderShipment CreateUpdateOrderShipment(
    PX.Objects.SO.SOOrder order,
    SOLine4 soLine,
    PX.Objects.PO.POReceiptLine receiptLine,
    POAddress poAddress,
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo,
    Decimal? baseSplitReceivedQty,
    Decimal? splitReceivedQty)
  {
    Decimal? actualDiscUnitPrice = this.GetActualDiscUnitPrice(soLine);
    Decimal? amt;
    ref Decimal? local = ref amt;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = currencyInfo;
    Decimal? nullable1 = splitReceivedQty;
    Decimal? nullable2 = actualDiscUnitPrice;
    Decimal valueOrDefault = (nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
    Decimal num = currencyInfo1.RoundBase(valueOrDefault);
    local = new Decimal?(num);
    return this.CreateUpdateOrderShipment(order, receiptLine, poAddress, true, baseSplitReceivedQty, amt);
  }

  public virtual Decimal? GetActualDiscUnitPrice(SOLine4 soLine)
  {
    Decimal? orderQty1 = soLine.OrderQty;
    Decimal num1 = 0M;
    if (!(orderQty1.GetValueOrDefault() == num1 & orderQty1.HasValue))
    {
      Decimal? lineAmt = soLine.LineAmt;
      Decimal? orderQty2 = soLine.OrderQty;
      return !(lineAmt.HasValue & orderQty2.HasValue) ? new Decimal?() : new Decimal?(lineAmt.GetValueOrDefault() / orderQty2.GetValueOrDefault());
    }
    Decimal? unitPrice = soLine.UnitPrice;
    Decimal num2 = 1M;
    Decimal? discPct = soLine.DiscPct;
    Decimal num3 = 100M;
    Decimal? nullable1 = discPct.HasValue ? new Decimal?(discPct.GetValueOrDefault() / num3) : new Decimal?();
    Decimal? nullable2 = nullable1.HasValue ? new Decimal?(num2 - nullable1.GetValueOrDefault()) : new Decimal?();
    return !(unitPrice.HasValue & nullable2.HasValue) ? new Decimal?() : new Decimal?(unitPrice.GetValueOrDefault() * nullable2.GetValueOrDefault());
  }

  public virtual PX.Objects.SO.SOOrderShipment CreateUpdateOrderShipment(
    PX.Objects.SO.SOOrder order,
    PX.Objects.PO.POReceiptLine line,
    POAddress poAddress,
    bool confirmed,
    Decimal? qty,
    Decimal? amt)
  {
    PX.Objects.SO.SOOrderShipment soOrderShipment1 = PX.Objects.SO.SOOrderShipment.FromDropshipPOReceipt(((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current, order, line);
    SOAddress soAddress1 = SOAddress.PK.Find((PXGraph) this.Base, soOrderShipment1.ShipAddressID);
    bool? nullable1;
    if (poAddress != null)
    {
      nullable1 = poAddress.IsDefaultAddress;
      bool flag = false;
      if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue && !SOAddress.IsEquivalentAddress(soAddress1, poAddress))
      {
        SOAddress soAddress2 = this.SOAddressCache.Insert(SOAddress.CreateFromPOAddress(poAddress));
        soOrderShipment1.ShipAddressID = soAddress2.AddressID;
      }
    }
    soOrderShipment1.Confirmed = new bool?(confirmed);
    soOrderShipment1.ShipmentQty = qty;
    soOrderShipment1.LineTotal = amt;
    PX.Objects.SO.SOOrderShipment updateOrderShipment = PXResultset<PX.Objects.SO.SOOrderShipment>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrderShipment, PXSelect<PX.Objects.SO.SOOrderShipment, Where<PX.Objects.SO.SOOrderShipment.shipmentType, Equal<INDocType.dropShip>, And<PX.Objects.SO.SOOrderShipment.shipmentNbr, Equal<Required<PX.Objects.SO.SOOrderShipment.shipmentNbr>>, And<PX.Objects.SO.SOOrderShipment.orderType, Equal<Required<PX.Objects.SO.SOOrderShipment.orderType>>, And<PX.Objects.SO.SOOrderShipment.orderNbr, Equal<Required<PX.Objects.SO.SOOrderShipment.orderNbr>>>>>>>.Config>.Select((PXGraph) this.Base, new object[3]
    {
      (object) soOrderShipment1.ShipmentNbr,
      (object) soOrderShipment1.OrderType,
      (object) soOrderShipment1.OrderNbr
    }));
    if (updateOrderShipment == null)
    {
      PX.Objects.SO.SOOrder soOrder = order;
      int? shipmentCntr = soOrder.ShipmentCntr;
      soOrder.ShipmentCntr = shipmentCntr.HasValue ? new int?(shipmentCntr.GetValueOrDefault() + 1) : new int?();
      return this.SOOrderShipmentCache.Insert(soOrderShipment1);
    }
    if (confirmed)
    {
      nullable1 = updateOrderShipment.Confirmed;
      bool flag = false;
      if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
      {
        PX.Objects.SO.SOOrder soOrder = order;
        int? shipmentCntr = soOrder.ShipmentCntr;
        soOrder.ShipmentCntr = shipmentCntr.HasValue ? new int?(shipmentCntr.GetValueOrDefault() + 1) : new int?();
        updateOrderShipment.ShipmentQty = new Decimal?(0M);
        updateOrderShipment.LineTotal = new Decimal?(0M);
      }
    }
    updateOrderShipment.Confirmed = new bool?(confirmed);
    PX.Objects.SO.SOOrderShipment soOrderShipment2 = updateOrderShipment;
    Decimal? nullable2 = soOrderShipment2.ShipmentQty;
    Decimal? nullable3 = qty;
    soOrderShipment2.ShipmentQty = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
    PX.Objects.SO.SOOrderShipment soOrderShipment3 = updateOrderShipment;
    nullable3 = soOrderShipment3.LineTotal;
    nullable2 = amt;
    soOrderShipment3.LineTotal = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    GraphHelper.MarkUpdated((PXCache) this.SOOrderShipmentCache, (object) updateOrderShipment, true);
    return updateOrderShipment;
  }

  protected virtual void CollectDemandForTransferOrders(PX.Objects.PO.POReceiptLine line)
  {
    if (line.OrigTranType != "TRX" || string.IsNullOrEmpty(line.OrigRefNbr))
      return;
    INItemPlan inItemPlan1 = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXSelectJoin<INItemPlan, InnerJoin<INTransitLine, On<INTransitLine.noteID, Equal<INItemPlan.refNoteID>>>, Where<INTransitLine.transferNbr, Equal<Required<INTransitLine.transferNbr>>, And<INTransitLine.transferLineNbr, Equal<Required<INTransitLine.transferLineNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) line.OrigRefNbr,
      (object) line.OrigLineNbr
    }));
    Decimal? nullable1 = line.BaseReceiptQty;
    foreach (PXResult<INItemPlan> pxResult in PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.supplyPlanID, Equal<Required<INItemPlan.supplyPlanID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) inItemPlan1.PlanID
    }))
    {
      INItemPlan inItemPlan2 = PXResult<INItemPlan>.op_Implicit(pxResult);
      INPlanType inPlanType = INPlanType.PK.Find((PXGraph) this.Base, inItemPlan2.PlanType);
      INItemPlan inItemPlan3 = new INItemPlan()
      {
        InventoryID = line.InventoryID,
        SiteID = line.SiteID,
        SubItemID = line.SubItemID,
        DemandPlanID = inItemPlan2.PlanID,
        PlanDate = inItemPlan2.PlanDate,
        PlanType = "64",
        RefNoteID = inItemPlan2.RefNoteID,
        RefEntityType = inItemPlan2.RefEntityType,
        Hold = new bool?(false),
        CostCenterID = inItemPlan2.CostCenterID,
        BAccountID = inItemPlan2.BAccountID
      };
      Decimal? nullable2 = nullable1;
      Decimal? nullable3 = inItemPlan2.PlanQty;
      if (nullable2.GetValueOrDefault() >= nullable3.GetValueOrDefault() & nullable2.HasValue & nullable3.HasValue)
      {
        inItemPlan3.PlanQty = inItemPlan2.PlanQty;
        this.POSupply.Add(inItemPlan3);
        nullable3 = nullable1;
        nullable2 = inItemPlan2.PlanQty;
        nullable1 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
        this.PODemand.Add(new PXResult<INItemPlan, INPlanType>(inItemPlan2, inPlanType));
      }
      else
      {
        nullable2 = nullable1;
        Decimal num = 0M;
        if (nullable2.GetValueOrDefault() > num & nullable2.HasValue)
        {
          inItemPlan3.PlanQty = nullable1;
          this.POSupply.Add(inItemPlan3);
          nullable1 = new Decimal?(0M);
        }
      }
      if (line.AllowComplete.GetValueOrDefault())
        this.PODemand.Add(new PXResult<INItemPlan, INPlanType>(inItemPlan2, inPlanType));
    }
  }

  public virtual PXResult<PX.Objects.SO.SOLineSplit, SOLine4>[] GetRelatedDemand(
    PX.Objects.PO.POReceiptLine receiptLine)
  {
    return ((IEnumerable<PXResult<PX.Objects.SO.SOLineSplit>>) PXSelectBase<PX.Objects.SO.SOLineSplit, PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOLine4>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.orderType, Equal<SOLine4.orderType>>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.orderNbr, IBqlString>.IsEqual<SOLine4.orderNbr>>>>.And<BqlOperand<PX.Objects.SO.SOLineSplit.lineNbr, IBqlInt>.IsEqual<SOLine4.lineNbr>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.pOType, Equal<BqlField<PX.Objects.PO.POReceiptLine.pOType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.pONbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.pONbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.SO.SOLineSplit.pOLineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.pOLineNbr, IBqlInt>.FromCurrent>>>.Order<By<BqlField<PX.Objects.SO.SOLineSplit.splitLineNbr, IBqlInt>.Asc>>>.Config>.SelectMultiBound((PXGraph) this.Base, (object[]) new PX.Objects.PO.POReceiptLine[1]
    {
      receiptLine
    }, Array.Empty<object>())).AsEnumerable<PXResult<PX.Objects.SO.SOLineSplit>>().Cast<PXResult<PX.Objects.SO.SOLineSplit, SOLine4>>().ToArray<PXResult<PX.Objects.SO.SOLineSplit, SOLine4>>();
  }

  public virtual void ReattachDemandPlansToIN(
    INRegisterEntryBase inRegisterEntry,
    INTranSplit newSplit)
  {
    PXCache<INItemPlan> pxCache = GraphHelper.Caches<INItemPlan>((PXGraph) inRegisterEntry);
    foreach (PXResult<INItemPlan, INPlanType> pxResult in this.PODemand)
    {
      INItemPlan inItemPlan = PXResult<INItemPlan, INPlanType>.op_Implicit(pxResult);
      inItemPlan.SupplyPlanID = newSplit.PlanID;
      GraphHelper.MarkUpdated((PXCache) pxCache, (object) inItemPlan, true);
    }
    this.PODemand.Clear();
    Decimal? nullable1 = newSplit.BaseQty;
    foreach (INItemPlan inItemPlan1 in this.POSupply.ToArray())
    {
      Decimal? nullable2 = nullable1;
      Decimal num1 = 0M;
      if (nullable2.GetValueOrDefault() <= num1 & nullable2.HasValue)
        break;
      if (!string.IsNullOrEmpty(newSplit.LotSerialNbr))
      {
        INItemPlan copy = PXCache<INItemPlan>.CreateCopy(inItemPlan1);
        INItemPlan inItemPlan2 = copy;
        nullable2 = inItemPlan1.PlanQty;
        Decimal? nullable3 = new Decimal?(Math.Min(nullable2.Value, nullable1.Value));
        inItemPlan2.PlanQty = nullable3;
        copy.LotSerialNbr = newSplit.LotSerialNbr;
        copy.SupplyPlanID = newSplit.PlanID;
        pxCache.Insert(copy);
        INItemPlan inItemPlan3 = inItemPlan1;
        nullable2 = inItemPlan3.PlanQty;
        Decimal? nullable4 = copy.PlanQty;
        inItemPlan3.PlanQty = nullable2.HasValue & nullable4.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
        nullable4 = nullable1;
        nullable2 = copy.PlanQty;
        nullable1 = nullable4.HasValue & nullable2.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
        nullable2 = inItemPlan1.PlanQty;
        Decimal num2 = 0M;
        if (nullable2.GetValueOrDefault() <= num2 & nullable2.HasValue)
          this.POSupply.Remove(inItemPlan1);
      }
      else
      {
        inItemPlan1.SupplyPlanID = newSplit.PlanID;
        pxCache.Insert(inItemPlan1);
        this.POSupply.Remove(inItemPlan1);
      }
    }
  }

  public virtual bool TryFinalizeDemand(
    PX.Objects.PO.POReceipt receipt,
    PX.Objects.PO.POReceiptLine receiptLine,
    PX.Objects.PO.POLine poLine,
    INTran newline)
  {
    if (receiptLine.IsCorrection.GetValueOrDefault())
    {
      bool? isAdjustedIn = receiptLine.IsAdjustedIN;
      bool flag = false;
      if (isAdjustedIn.GetValueOrDefault() == flag & isAdjustedIn.HasValue)
        goto label_3;
    }
    Decimal? baseQty = receiptLine.BaseQty;
    Decimal num = 0M;
    if (!(baseQty.GetValueOrDefault() == num & baseQty.HasValue) && !this.TryProcessReceiptLinkedAllocation(receipt, poLine, newline))
      return false;
label_3:
    this.PODemand.Clear();
    this.POSupply.Clear();
    return true;
  }

  protected virtual bool TryProcessReceiptLinkedAllocation(
    PX.Objects.PO.POReceipt receipt,
    PX.Objects.PO.POLine poLine,
    INTran newLine)
  {
    if (newLine != null && (poLine == null || !EnumerableExtensions.IsIn<string>(poLine.LineType, "NO", "NF")))
      return false;
    INPlanType nonStockPlanType = new INPlanType()
    {
      ReplanOnEvent = "60"
    };
    List<PXResult<INItemPlan, INPlanType>> list = this.PODemand.Concat<PXResult<INItemPlan, INPlanType>>(this.POSupply.Select<INItemPlan, PXResult<INItemPlan, INPlanType>>(new Func<INItemPlan, PXResult<INItemPlan, INPlanType>>(ToPlanWithType))).ToList<PXResult<INItemPlan, INPlanType>>();
    this.Process((receipt.ReceiptType, receipt.ReceiptNbr), (IEnumerable<PXResult<INItemPlan, INPlanType>>) list);
    return true;

    PXResult<INItemPlan, INPlanType> ToPlanWithType(INItemPlan plan)
    {
      return new PXResult<INItemPlan, INPlanType>(this.PlanCache.Insert(plan), nonStockPlanType);
    }
  }

  public virtual void ReduceSOAllocationOnReleaseReturn(
    PXResult<PX.Objects.PO.POReceiptLine> row,
    POReceiptLineSplit poSplit,
    PX.Objects.PO.POLine poLine)
  {
    if (poLine == null)
      return;
    POReceiptLine2 poReceiptLine2 = ((PXResult) row).GetItem<POReceiptLine2>();
    PX.Objects.PO.POLine poLine1 = GraphHelper.Caches<PX.Objects.PO.POLine>((PXGraph) this.Base).Locate(poLine) ?? poLine;
    if (!POOrderType.IsNormalType(poReceiptLine2?.POType) || !(poReceiptLine2.LineType == "GS") || poLine1.Completed.GetValueOrDefault())
      return;
    IEnumerable<PX.Objects.SO.SOLineSplit> soLineSplits = GraphHelper.RowCast<PX.Objects.SO.SOLineSplit>((IEnumerable) PXSelectBase<PX.Objects.SO.SOLineSplit, PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<POOrderEntry.SOLineSplit3>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.orderType, Equal<POOrderEntry.SOLineSplit3.orderType>>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.orderNbr, IBqlString>.IsEqual<POOrderEntry.SOLineSplit3.orderNbr>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.lineNbr, IBqlInt>.IsEqual<POOrderEntry.SOLineSplit3.lineNbr>>>>.And<BqlOperand<PX.Objects.SO.SOLineSplit.parentSplitLineNbr, IBqlInt>.IsEqual<POOrderEntry.SOLineSplit3.splitLineNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.pOReceiptType, Equal<BqlField<PX.Objects.PO.POReceiptLine.receiptType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.pOReceiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<POOrderEntry.SOLineSplit3.pOType, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.pOType, IBqlString>.FromCurrent>>>, And<BqlOperand<POOrderEntry.SOLineSplit3.pONbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.pONbr, IBqlString>.FromCurrent>>>, And<BqlOperand<POOrderEntry.SOLineSplit3.pOLineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.pOLineNbr, IBqlInt>.FromCurrent>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current2<POReceiptLineSplit.lotSerialNbr>, IsNull>>>, Or<BqlOperand<Current2<POReceiptLineSplit.lotSerialNbr>, IBqlString>.IsEqual<EmptyString>>>, Or<BqlOperand<PX.Objects.SO.SOLineSplit.lotSerialNbr, IBqlString>.IsNull>>, Or<BqlOperand<PX.Objects.SO.SOLineSplit.lotSerialNbr, IBqlString>.IsEqual<EmptyString>>>>.Or<BqlOperand<PX.Objects.SO.SOLineSplit.lotSerialNbr, IBqlString>.IsEqual<BqlField<POReceiptLineSplit.lotSerialNbr, IBqlString>.FromCurrent.NoDefault>>>>.Order<By<BqlField<PX.Objects.SO.SOLineSplit.lotSerialNbr, IBqlString>.Desc>>>.ReadOnly.Config>.SelectMultiBound((PXGraph) this.Base, new object[2]
    {
      (object) poReceiptLine2,
      (object) poSplit
    }, Array.Empty<object>()));
    Decimal valueOrDefault = poSplit.BaseQty.GetValueOrDefault();
    foreach (PX.Objects.SO.SOLineSplit soSplit in soLineSplits)
    {
      valueOrDefault -= this.UpdateSOAllocation(soSplit, poLine1, valueOrDefault);
      if (valueOrDefault <= 0M)
        break;
    }
  }

  public virtual Decimal UpdateSOAllocation(
    PX.Objects.SO.SOLineSplit soSplit,
    PX.Objects.PO.POLine poLine,
    Decimal returnQty,
    bool isCorrection = false)
  {
    Decimal num1 = 0M;
    PX.Objects.SO.SOLineSplit activeSplit = this.FindActiveSplit(soSplit);
    if ((activeSplit != null ? (!activeSplit.IsAllocated.GetValueOrDefault() ? 1 : 0) : 1) == 0)
    {
      Decimal? shippedQty = activeSplit.ShippedQty;
      Decimal num2 = 0M;
      if (shippedQty.GetValueOrDefault() == num2 & shippedQty.HasValue)
        goto label_4;
    }
    if (!isCorrection)
      return num1;
label_4:
    INItemPlan plan = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemPlan.planID, IBqlLong>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.planID, IBqlLong>.FromCurrent>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
    {
      (object) activeSplit
    }, Array.Empty<object>()));
    if (plan?.PlanType != "61" && (!isCorrection || !(plan?.PlanType == "60")))
      return num1;
    Decimal returnQty1 = this.SplitAllocation(activeSplit, plan, returnQty);
    PX.Objects.SO.SOLineSplit parent = this.FindParent(soSplit);
    if (isCorrection && parent.PONbr == poLine.OrderNbr && parent.POType == poLine.OrderType)
    {
      int? poLineNbr = parent.POLineNbr;
      int? lineNbr = poLine.LineNbr;
      if (poLineNbr.GetValueOrDefault() == lineNbr.GetValueOrDefault() & poLineNbr.HasValue == lineNbr.HasValue)
        parent.POCompleted = poLine.Completed;
    }
    this.UpdateParent(parent, poLine, returnQty1);
    return returnQty1;
  }

  protected virtual PX.Objects.SO.SOLineSplit FindActiveSplit(PX.Objects.SO.SOLineSplit soSplit)
  {
    PX.Objects.SO.SOLineSplit activeSplit;
    POReceiptEntry poReceiptEntry;
    object[] objArray1;
    object[] objArray2;
    for (activeSplit = soSplit; activeSplit != null && activeSplit.Completed.GetValueOrDefault(); activeSplit = PXResultset<PX.Objects.SO.SOLineSplit>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLineSplit, PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.orderType, Equal<BqlField<PX.Objects.SO.SOLineSplit.orderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.orderNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.orderNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.lineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.lineNbr, IBqlInt>.FromCurrent>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.parentSplitLineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.splitLineNbr, IBqlInt>.FromCurrent>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.lotSerialNbr, IsNull>>>>.Or<BqlOperand<PX.Objects.SO.SOLineSplit.lotSerialNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.lotSerialNbr, IBqlString>.FromCurrent>>>>>.Config>.SelectSingleBound((PXGraph) poReceiptEntry, objArray1, objArray2)))
    {
      poReceiptEntry = this.Base;
      objArray1 = new object[1]{ (object) activeSplit };
      objArray2 = Array.Empty<object>();
    }
    return activeSplit;
  }

  public virtual Decimal SplitAllocation(PX.Objects.SO.SOLineSplit soSplit, INItemPlan plan, Decimal returnQty)
  {
    Decimal num1 = Math.Min(returnQty, soSplit.BaseQty.GetValueOrDefault());
    PX.Objects.SO.SOLineSplit soLineSplit1 = soSplit;
    Decimal? nullable1 = soLineSplit1.BaseQty;
    Decimal num2 = num1;
    soLineSplit1.BaseQty = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num2) : new Decimal?();
    PX.Objects.SO.SOLineSplit soLineSplit2 = soSplit;
    PXCache<PX.Objects.SO.SOLineSplit> soSplitCache = this.SOSplitCache;
    int? inventoryId = soSplit.InventoryID;
    string uom = soSplit.UOM;
    nullable1 = soSplit.BaseQty;
    Decimal num3 = nullable1.Value;
    Decimal? nullable2 = new Decimal?(INUnitAttribute.ConvertFromBase((PXCache) soSplitCache, inventoryId, uom, num3, INPrecision.QUANTITY));
    soLineSplit2.Qty = nullable2;
    nullable1 = soSplit.BaseQty;
    Decimal num4 = 0M;
    if (nullable1.GetValueOrDefault() == num4 & nullable1.HasValue)
      soSplit.Completed = new bool?(true);
    this.SOSplitCache.Update(soSplit);
    nullable1 = plan.PlanQty;
    Decimal num5 = num1;
    if (!(nullable1.GetValueOrDefault() == num5 & nullable1.HasValue))
    {
      INItemPlan inItemPlan = plan;
      nullable1 = inItemPlan.PlanQty;
      Decimal num6 = num1;
      inItemPlan.PlanQty = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num6) : new Decimal?();
      this.PlanCache.Update(plan);
    }
    else
      this.PlanCache.Delete(plan);
    return num1;
  }

  protected virtual PX.Objects.SO.SOLineSplit FindParent(PX.Objects.SO.SOLineSplit soSplit)
  {
    return PXResultset<PX.Objects.SO.SOLineSplit>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLineSplit, PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.orderType, Equal<BqlField<PX.Objects.SO.SOLineSplit.orderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.orderNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.orderNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.lineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.lineNbr, IBqlInt>.FromCurrent>>>>.And<BqlOperand<PX.Objects.SO.SOLineSplit.splitLineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.parentSplitLineNbr, IBqlInt>.FromCurrent>>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
    {
      (object) soSplit
    }, Array.Empty<object>()));
  }

  public virtual void UpdateParent(PX.Objects.SO.SOLineSplit parent, PX.Objects.PO.POLine poLine, Decimal returnQty)
  {
    PX.Objects.SO.SOLineSplit soLineSplit1 = parent;
    Decimal? nullable1 = soLineSplit1.BaseReceivedQty;
    Decimal num1 = returnQty;
    soLineSplit1.BaseReceivedQty = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num1) : new Decimal?();
    PX.Objects.SO.SOLineSplit soLineSplit2 = parent;
    PXCache<PX.Objects.SO.SOLineSplit> soSplitCache = this.SOSplitCache;
    int? inventoryId = parent.InventoryID;
    string uom = parent.UOM;
    nullable1 = parent.BaseReceivedQty;
    Decimal num2 = nullable1.Value;
    Decimal? nullable2 = new Decimal?(INUnitAttribute.ConvertFromBase((PXCache) soSplitCache, inventoryId, uom, num2, INPrecision.QUANTITY));
    soLineSplit2.ReceivedQty = nullable2;
    if (!parent.Completed.GetValueOrDefault())
    {
      this.SOSplitCache.Update(parent);
      INItemPlan inItemPlan1 = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemPlan.planID, IBqlLong>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.planID, IBqlLong>.FromCurrent>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
      {
        (object) parent
      }, Array.Empty<object>()));
      INItemPlan inItemPlan2 = inItemPlan1 != null ? inItemPlan1 : throw new RowNotFoundException((PXCache) this.PlanCache, new object[1]
      {
        (object) parent.PlanID
      });
      nullable1 = inItemPlan2.PlanQty;
      Decimal num3 = returnQty;
      inItemPlan2.PlanQty = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num3) : new Decimal?();
      this.PlanCache.Update(inItemPlan1);
    }
    else
    {
      parent.Completed = new bool?(false);
      if (parent.PlanID.HasValue)
      {
        INItemPlan inItemPlan3 = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemPlan.planID, IBqlLong>.IsEqual<BqlField<PX.Objects.SO.SOLineSplit.planID, IBqlLong>.FromCurrent>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
        {
          (object) parent
        }, Array.Empty<object>()));
        INItemPlan inItemPlan4 = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemPlan.planID, IBqlLong>.IsEqual<BqlField<PX.Objects.PO.POLine.planID, IBqlLong>.FromCurrent>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
        {
          (object) poLine
        }, Array.Empty<object>()));
        inItemPlan3.SupplyPlanID = inItemPlan4.PlanID;
        inItemPlan3.PlanQty = new Decimal?(returnQty);
        this.PlanCache.Update(inItemPlan3);
      }
      else
        parent.PlanID = this.InsertParentPlan(parent, poLine, returnQty);
      this.SOSplitCache.Update(parent);
    }
  }

  public virtual long? InsertParentPlan(PX.Objects.SO.SOLineSplit parent, PX.Objects.PO.POLine poLine, Decimal returnQty)
  {
    INItemPlan inItemPlan = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemPlan.planID, IBqlLong>.IsEqual<BqlField<PX.Objects.PO.POLine.planID, IBqlLong>.FromCurrent>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
    {
      (object) poLine
    }, Array.Empty<object>()));
    if (inItemPlan == null)
      throw new RowNotFoundException((PXCache) this.PlanCache, new object[1]
      {
        (object) poLine.PlanID
      });
    PX.Objects.PO.POOrder poOrder = PXParentAttribute.SelectParent<PX.Objects.PO.POOrder>((PXCache) GraphHelper.Caches<PX.Objects.PO.POLine>((PXGraph) this.Base), (object) poLine);
    if (poOrder == null)
      throw new RowNotFoundException((PXCache) GraphHelper.Caches<PX.Objects.PO.POOrder>((PXGraph) this.Base), new object[2]
      {
        (object) poLine.OrderType,
        (object) poLine.OrderNbr
      });
    PX.Objects.SO.SOOrder soOrder = PXParentAttribute.SelectParent<PX.Objects.SO.SOOrder>((PXCache) this.SOSplitCache, (object) parent);
    if (soOrder == null)
      throw new RowNotFoundException((PXCache) this.SOOrderCache, new object[2]
      {
        (object) parent.OrderType,
        (object) parent.OrderNbr
      });
    PX.Objects.SO.SOLine soLine = PXParentAttribute.SelectParent<PX.Objects.SO.SOLine>((PXCache) this.SOSplitCache, (object) parent);
    if (soLine == null)
      throw new RowNotFoundException((PXCache) this.SOLineCache, new object[3]
      {
        (object) parent.OrderType,
        (object) parent.OrderNbr,
        (object) parent.LineNbr
      });
    INItemPlan copy = PXCache<INItemPlan>.CreateCopy(inItemPlan);
    copy.PlanID = new long?();
    copy.SupplyPlanID = inItemPlan.PlanID;
    copy.SiteID = soLine.SiteID;
    copy.SourceSiteID = soLine.SiteID;
    copy.CostCenterID = soLine.CostCenterID;
    copy.PlanType = "66";
    copy.VendorID = poOrder.VendorID;
    copy.VendorLocationID = poOrder.VendorLocationID;
    copy.UOM = parent.UOM;
    copy.BAccountID = soOrder.CustomerID;
    copy.Hold = soOrder.Hold;
    copy.RefNoteID = soOrder.NoteID;
    copy.RefEntityType = typeof (PX.Objects.SO.SOOrder).FullName;
    copy.PlanQty = new Decimal?(returnQty);
    copy.PlanDate = soLine.ShipDate;
    return this.PlanCache.Insert(copy).PlanID;
  }

  public virtual void UpdateSOOrderLink(INTran newtran, PX.Objects.PO.POLine poLine, PX.Objects.PO.POReceiptLine line)
  {
  }

  public virtual void PopulateDropshipFields(PX.Objects.SO.SOOrder order)
  {
    PX.Objects.PO.POReceipt current = ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current;
    if (current.DropshipFieldsSet.GetValueOrDefault())
    {
      int? dropshipCustomerId = current.DropshipCustomerID;
      int? nullable1 = order.CustomerID;
      if (!(dropshipCustomerId.GetValueOrDefault() == nullable1.GetValueOrDefault() & dropshipCustomerId.HasValue == nullable1.HasValue))
      {
        PX.Objects.PO.POReceipt poReceipt = current;
        nullable1 = new int?();
        int? nullable2 = nullable1;
        poReceipt.DropshipCustomerID = nullable2;
      }
      nullable1 = current.DropshipCustomerLocationID;
      int? nullable3 = order.CustomerLocationID;
      if (!(nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue))
      {
        PX.Objects.PO.POReceipt poReceipt = current;
        nullable3 = new int?();
        int? nullable4 = nullable3;
        poReceipt.DropshipCustomerLocationID = nullable4;
      }
      if (current.DropshipCustomerOrderNbr != order.CustomerOrderNbr)
        current.DropshipCustomerOrderNbr = (string) null;
      if (current.DropshipShipVia != order.ShipVia)
        current.DropshipShipVia = (string) null;
    }
    else
    {
      current.DropshipCustomerID = order.CustomerID;
      current.DropshipCustomerLocationID = order.CustomerLocationID;
      current.DropshipCustomerOrderNbr = order.CustomerOrderNbr;
      current.DropshipShipVia = order.ShipVia;
      current.DropshipFieldsSet = new bool?(true);
    }
    ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).UpdateCurrent();
  }
}
